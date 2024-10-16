using System.Collections;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [Header("Zombie Settings")]
    public GameObject zombiePrefab; // Prefab del zombie a instanciar
    public float spawnInterval = 3.0f; // Intervalo de tiempo entre spawns
    public Transform spawnPoint; // Punto donde se generarán los zombies

    private void Start()
    {
        StartCoroutine(SpawnZombies()); // Iniciar la coroutine para spawnear zombies
    }

    private IEnumerator SpawnZombies()
    {
        while (true) // Bucle infinito
        {
            SpawnZombie(); // Llama al método para spawnear un zombie
            yield return new WaitForSeconds(spawnInterval); // Espera el intervalo de tiempo
        }
    }

    private void SpawnZombie()
    {
        if (zombiePrefab != null && spawnPoint != null)
        {
            // Instanciar un nuevo zombie en la posición del spawnPoint con su rotación original
            Instantiate(zombiePrefab, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Debug.LogWarning("Prefab de zombie o punto de spawn no asignados.");
        }
    }
}


