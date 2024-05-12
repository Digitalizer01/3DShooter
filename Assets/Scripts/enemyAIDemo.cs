using UnityEngine;
using System.Collections;

public class enemyAIDemo : MonoBehaviour
{
    [HideInInspector] public IdleStateDemo    idleState;
    [HideInInspector] public AlertStateDemo   alertState;
    [HideInInspector] public IEnemyStateDemo  currentState;

    public Material myMaterial;

    void Start()
    {
        // Creamos los estados de nuestra IA.
        idleState = new IdleStateDemo(this);
        alertState  = new AlertStateDemo(this);

        // Le decimos que inicialmente empezará en Idle
        currentState = idleState;
    }
    
    void Update()
    {
        // Como nuestros estados no heredan de
        // MonoBehaviour, no se llama a su update 
        // automáticamente, y nos encargaremos 
        // nosotros de llamarlo a cada frame.
        currentState.UpdateState();
    }

    // Ya que nuestros states no heredan de 
    // MonoBehaviour, tendremos que avisarles
    // cuando algo entra, está o sale de nuestro
    // trigger.
    void OnTriggerEnter(Collider col)
    {
        currentState.OnTriggerEnter(col);
    }

    void OnTriggerStay(Collider col)
    {
        currentState.OnTriggerStay(col);
    }

    void OnTriggerExit(Collider col)
    {
        currentState.OnTriggerExit(col);
    }
}
