using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyState
{
    enemyAI myEnemy;
    private int nextWayPoint = 0;

    // Cuando llamamos al constructor, guardamos
    // una referencia a la IA de nuestro enemigo
    public PatrolState(enemyAI enemy)
    {
        myEnemy = enemy;
    }

    // Aquí va toda la funcionalidad que queramos
    // que haga el enemigo cuando esté en este estado.
    public void UpdateState()
    {
        myEnemy.myLight.color = Color.green;

        // Le decimos al NavMeshAgent cuál es el punto
        // al que ha de dirigirse.
        myEnemy.navMeshAgent.destination = myEnemy.wayPoints[nextWayPoint].position;

        // Si hemos llegado al destino, cambiamos la
        // referencia al siguiente Waypoint
        if(myEnemy.navMeshAgent.remainingDistance <= myEnemy.navMeshAgent.stoppingDistance)
        {
            nextWayPoint = (nextWayPoint + 1) % myEnemy.wayPoints.Length;
        }
    }

    // Si el player nos ha disparado
    public void Impact()
    {
        Debug.Log("PatrolState - Impact");
        GoToAttackState();
    }

    public void GoToAlertState()
    {
        // Paramos su movimiento
        myEnemy.navMeshAgent.Stop();
        myEnemy.currentState = myEnemy.alertState;
    }

    public void GoToAttackState()
    {
        // Paramos su movimiento
        myEnemy.navMeshAgent.Stop();
        myEnemy.currentState = myEnemy.attackState;
    }

    // Como ya estamos en el estado Patrol, no
    // llamaremos nunca a esta función desde este estado
    public void GoToPatrolState() {}

    public void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            GoToAlertState();
        }
    }

    public void OnTriggerStay(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            GoToAlertState();
        }
    }

    public void OnTriggerExit(Collider col) {}
}
