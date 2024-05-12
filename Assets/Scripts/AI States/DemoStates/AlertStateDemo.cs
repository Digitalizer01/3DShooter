using UnityEngine;
using System.Collections;

public class AlertStateDemo : IEnemyStateDemo
{
    enemyAIDemo myEnemy;

    // Cuando llamamos al constructor, guardamos 
    // una referencia a la IA de nuestro enemigo
    public AlertStateDemo(enemyAIDemo enemy)
    {
        myEnemy = enemy;
    }

    // Aquí va toda la funcionalidad que queramos 
    // que haga el enemigo cuando esté en este
    // estado.
    public void UpdateState()
    {
        myEnemy.myMaterial.color = Color.red;
    }

    // Como ya estamos en el estado Alert, no
    // llamaremos nunca a esta función desde 
    // este estado
    public void GoToAlertState() { }

    public void GoToIdleState()
    {
        myEnemy.currentState = myEnemy.idleState;
    }


    // En este estado el player ya está dentro, por lo que no haremos caso.
    public void OnTriggerEnter(Collider col) { }

    // Orientaremos el enemigo mirando siempre al 
    // player mientras le ataquemos
    public void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player")
        {
            // Estaremos mirando siempre al player.
            Vector3 lookDirection = col.transform.position -
                                myEnemy.transform.position;

            // Rotando solamente en el eje Y
            myEnemy.transform.rotation =
                Quaternion.FromToRotation(Vector3.forward,
                                            new Vector3(lookDirection.x, 0, lookDirection.z));
        }
    }

    // Si el player sale de su radio, pasa a modo Idle.
    public void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
            GoToIdleState();
    }
}

