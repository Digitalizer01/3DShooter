using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyAnimation : MonoBehaviour
{
    public float speed = 1.0f; // Velocidad de oscilación
    public float amplitude = 1.0f; // Amplitud del movimiento

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position; // Guarda la posición inicial
    }

    void Update()
    {
        // Calcula la nueva posición Y usando la función seno
        float newY = initialPosition.y + Mathf.Sin(Time.time * speed) * amplitude;

        // Aplica la nueva posición al transform
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
