using UnityEngine;

public class SmoothMovement : MonoBehaviour
{
    public float moveDistance = 5f; // La distancia que el objeto se moverá de ida y vuelta.
    public float moveSpeed = 2f; // La velocidad de movimiento.

    private Vector3 initialPosition;
    private Vector3 targetPosition;

    private void Start()
    {
        initialPosition = transform.position;
        targetPosition = initialPosition + new Vector3(moveDistance, 0f, 0f);
    }

    private void Update()
    {
        // Calcula el nuevo objetivo basado en el movimiento de ida y vuelta.
        Vector3 newPosition = Vector3.Lerp(initialPosition, targetPosition, Mathf.PingPong(Time.time * moveSpeed, 1f));
        transform.position = newPosition;

        // Si el objeto alcanza el objetivo, cambia el objetivo para invertir la dirección.
        if (transform.position == targetPosition)
        {
            Vector3 temp = initialPosition;
            initialPosition = targetPosition;
            targetPosition = temp;
        }
    }
}
