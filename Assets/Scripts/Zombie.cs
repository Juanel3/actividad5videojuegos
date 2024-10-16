using UnityEngine;

public class Zombie : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public float speed = 2.0f; // Velocidad del zombie
    public float stoppingDistance = 1.0f; // Distancia para detenerse
    public float damageInterval = 1.0f; // Intervalo de daño
    private float nextDamageTime = 0f; // Tiempo para el próximo daño

    private void Update()
    {
        if (player != null)
        {
            // Calcular la dirección hacia el jugador
            Vector3 direction = player.position - transform.position;
            float distance = direction.magnitude;

            // Si está más lejos que la distancia de detención, mover hacia el jugador
            if (distance > stoppingDistance)
            {
                // Normalizar la dirección y mover al zombie
                Vector3 moveDirection = direction.normalized;
                transform.position += moveDirection * speed * Time.deltaTime;
            }
            else
            {
                // Infligir daño al jugador si está dentro de la distancia de parada
                if (Time.time >= nextDamageTime)
                {
                    nextDamageTime = Time.time + damageInterval; // Actualizar el tiempo para el próximo daño
                    PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                    if (playerHealth != null)
                    {
                        playerHealth.TakeDamage(1); // Infligir 1 punto de daño
                    }
                }
            }
        }
        else
        {
            Debug.LogWarning("El jugador no está asignado en el ZombieAI.");
        }
    }

    private void OnDrawGizmos()
    {
        // Dibuja una línea desde el zombie hasta el jugador en el editor para visualizar el camino
        if (player != null)
        {
            Gizmos.color = Color.red; // Color de la línea
            Gizmos.DrawLine(transform.position, player.position);
        }
    }
}

