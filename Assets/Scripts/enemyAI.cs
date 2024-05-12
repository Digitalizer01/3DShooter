using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAI : MonoBehaviour
{
    [HideInInspector] public PatrolState patrolState;
    [HideInInspector] public AlertState alertState;
    [HideInInspector] public AttackState attackState;
    [HideInInspector] public IEnemyState currentState;

    [HideInInspector] public UnityEngine.AI.NavMeshAgent navMeshAgent;

    public Light myLight;
    public float life = 100;
    public float timeBetweenShoots = 0.1f;
    public float damageForce = 10f;
    public float rotationTime = 1.5f;
    public float shootHeight = 0.5f;
    public Transform[] wayPoints;

    public Collider smallCollider; // Agrega este campo para almacenar el collider más pequeño
    public Collider bigCollider; // Agrega este campo para almacenar el collider más grande
    public AudioClip FlyingSound;
    public AudioClip ExplosionSound;
    public AudioClip HitSound;
    public GameObject player;

    [HideInInspector] public AudioSource _audioSource;

    void Start()
    {
        // Creamos los estados de nuestra IA.
        patrolState = new PatrolState(this);
        alertState = new AlertState(this);
        attackState = new AttackState(this);

        // Le decimos que inicialmente empezará patrullando.
        currentState = patrolState;

        // Guardamos la referencia es nuestro NavMesh Agent.
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Como nuestros estados no heredan de
        // MonoBehaviour, no se llama a su update
        // automáticamente, y nos encargaremos
        // nosotros de llamarlo a cada frame.
        currentState.UpdateState();

        // Morir
        if(life <= 0){
            // Reproducir la animación de muerte
            Animator animator = GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetBool("death", true); // Setting initial animation state
            }

            // Eliminar todos los componentes excepto Transform
            Component[] components = GetComponents<Component>();
            foreach (Component component in components)
            {
                if (!(component is Transform) && !(component is Animator) && !(component is AudioSource))
                {
                    Destroy(component);
                }
            }

            // Agregar un Rigidbody
            Rigidbody rb = gameObject.AddComponent<Rigidbody>();
            // Configurar el Rigidbody según tus necesidades
            rb.mass = 1f;
            rb.drag = 0f;
            rb.angularDrag = 0.05f;
            rb.useGravity = true;
            rb.isKinematic = false;

            _audioSource.clip = ExplosionSound;
            _audioSource.Play();

            // Destruir el objeto después de 5 segundos
            Destroy(gameObject, 5f);
        }
    }

    // Cuando el player nos dispara, nos quitamos vida
    // y avisamos al estado en el que estemos de que
    // nos han disparado.
    public void Hit(float damage)
    {
        life -= damage;
        Debug.Log("Enemy hitted" + life);

        _audioSource.clip = HitSound;
        _audioSource.Play();

        currentState.Impact();
    }

    // Ya que nuestros states no heredan de
    // MonoBehaviour, tendremos que avisarles
    // cuando algo entra, está o sale de nuestro trigger.
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