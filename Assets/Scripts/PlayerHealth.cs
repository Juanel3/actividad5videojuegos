using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para reiniciar la escena

public class PlayerHealth : MonoBehaviour
{
    [Header("Player Health")]
    [SerializeField] private int maxHealth = 10; // Salud máxima del jugador
    private int currentHealth; // Salud actual del jugador

    private void Start()
    {
        currentHealth = maxHealth; // Inicializa la salud actual
    }

    // Método para recibir daño
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"Salud del jugador actual: {currentHealth}");

        // Si la salud llega a 0 o menos, reiniciar la escena
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("El jugador ha muerto. Reiniciando escena...");
        // Reiniciar la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
