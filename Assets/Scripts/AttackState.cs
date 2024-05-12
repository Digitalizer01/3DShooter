using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IEnemyState
{
    enemyAI myEnemy;
    float actualTimeBetweenShoots = 0;
    float timeSinceLastTrigger = 0f;
    float timeToPatrol = 5f; // Tiempo en segundos antes de volver a patrullar

    // Cuando llamamos al constructor, guardamos
    // una referencia a la IA de nuestro enemigo
    public AttackState(enemyAI enemy)
    {
        myEnemy = enemy;
    }

    // Aquí va toda la funcionalidad que queramos
    // que haga el enemigo cuando esté en este estado.
    public void UpdateState()
    {
        myEnemy.myLight.color = Color.red;
        actualTimeBetweenShoots += Time.deltaTime;

        // Actualizar el tiempo desde el último trigger
        timeSinceLastTrigger += Time.deltaTime;

        // Si ha pasado el tiempo sin que se active ningún OnTrigger, pasar a modo de patrulla
        if (timeSinceLastTrigger >= timeToPatrol)
        {
            GoToPatrolState();
        }
    }

    // Si el player nos ha disparado no haremos nada.
    public void Impact() {
        
    }

    // Como ya estamos en el estado Attack, no llamaremos nunca a estas funciones desde este estado
    public void GoToAttackState() {}
    public void GoToPatrolState()
    {
        // Volvemos a ponerlo en marcha
        myEnemy.navMeshAgent.Resume();
        myEnemy.currentState = myEnemy.patrolState;
        // Reiniciar el tiempo desde el último trigger
        timeSinceLastTrigger = 0f;
    }

    public void GoToAlertState()
    {
        myEnemy.currentState = myEnemy.alertState;
    }

    // El player ya está en nuestro trigger
    public void OnTriggerEnter(Collider col) {}

    // Orientaremos el enemigo mirando siempre al
    // player mientras le ataquemos
    public void OnTriggerStay(Collider col)
    {
        // Estaremos mirando siempre al player.
        Vector3 lookDirection =
            col.transform.position - myEnemy.transform.position;

        // Rotando solamente en el eje y
        myEnemy.transform.rotation =
            Quaternion.FromToRotation(Vector3.forward,
                new Vector3(lookDirection.x, 0, lookDirection.z));
        
        // Le toca volver a disparar
        if(actualTimeBetweenShoots > myEnemy.timeBetweenShoots)
        {
            actualTimeBetweenShoots = 0;
            col.gameObject.GetComponent<PlayerController>().Hit(myEnemy.damageForce);
        }
    }

    // Si el player sale de su radio, pasa a modo Alert.
    public void OnTriggerExit(Collider col)
    {
        GoToAlertState();
    }
}
