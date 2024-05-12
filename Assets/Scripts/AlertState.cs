using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : IEnemyState
{
    enemyAI myEnemy;
    float currentRotationTime = 0;

    // Cuando llamamos al constructor, guardamos
    // una referencia a la IA de nuestro enemigo
    public AlertState(enemyAI enemy)
    {
        myEnemy = enemy;
    }

    // Aquí va toda la funcionalidad que queramos
    // que haga el enemigo cuando esté en este estado.
    public void UpdateState()
    {
        myEnemy.myLight.color = Color.yellow;

        // Rotamos al enemigo una vuelta completa en el tiempo
        // indicado por rotationTime
        myEnemy.transform.rotation *= Quaternion.Euler(0f,
            Time.deltaTime * 360 * 1.0f / myEnemy.rotationTime, 0f);

        // Si hemos dado la vuelta
        if(currentRotationTime > myEnemy.rotationTime)
        {
            currentRotationTime = 0;
            GoToPatrolState();
        }
        else
        {
            // si aun etamos dando vueltas, lanzamos
            // un rayo desde una altura de 0.5 m desde
            // la posición del enemigo hacia donde mira
            RaycastHit hit;
            if(Physics.Raycast(
                new Ray(
                    new Vector3(myEnemy.transform.position.x,
                                0.5f,
                                myEnemy.transform.position.z),
                    myEnemy.transform.forward * 100f),
                    out hit)
                )
            {
                if(hit.collider.gameObject.tag == "Player")
                {
                    Debug.Log(hit.collider.name);
                    GoToAttackState();
                }
            }

            currentRotationTime += Time.deltaTime;
        }
    }

    // Si el player nos ha disparado
    public void Impact()
    {
        GoToAttackState();
    }

    // Como ya estamos en el estado Alert, no
    // llamaremos nunca a esta función desde este estado.
    public void GoToAlertState() {}

    public void GoToAttackState()
    {
        myEnemy.currentState = myEnemy.attackState;
    }

    public void GoToPatrolState()
    {
        // Volvemos a ponerlo en marcha
        myEnemy.navMeshAgent.Resume();
        myEnemy.currentState = myEnemy.patrolState;
    }

    // Al estar buscando no haremos caso del trigger.
    public void OnTriggerEnter(Collider col) {}
    public void OnTriggerStay(Collider col) {}
    public void OnTriggerExit(Collider col) {}
}
