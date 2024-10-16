using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    [Header("Zombie Health")]
    [SerializeField] private int maxHealth = 3; // Salud máxima del zombie
    private int currentHealth; // Salud actual del zombie

    private void Start()
    {
        currentHealth = maxHealth; // Inicializa la salud actual
    }

    // Método para recibir daño
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"Salud del zombie actual: {currentHealth}");

        // Si la salud llega a 0 o menos, destruir al zombie
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("El zombie ha muerto.");
        Destroy(gameObject); // Destruir el objeto zombie
    }
}

