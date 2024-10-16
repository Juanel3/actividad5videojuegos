using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    private int score; // Puntuación del jugador

    // Método para obtener el puntaje actual
    public int GetScore()
    {
        return score;
    }

    // Método para incrementar el puntaje
    public void IncreaseScore()
    {
        score++;
        Debug.Log($"Puntuación actual: {score}"); // Imprimir el puntaje en la consola
    }
}

