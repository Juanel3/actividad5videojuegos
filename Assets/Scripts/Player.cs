using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // Necesario para el sistema de Input

public class Player : MonoBehaviour
{
    [Header("Movement")] 
    [SerializeField] private float speed; 
    [SerializeField] private float jumpHeight; 
    [SerializeField] private float rotationSensitivity; 

    private Transform head; 
    private readonly float gravity = -9.8f; 
    private CharacterController character; 
    private Control input; 
    private float velocityY = 0; 

    [Header("Shooting")] // Atributos de disparo
    [SerializeField] private bool useRaycast = true; 
    [SerializeField] private Camera playerCamera; // Cámara del jugador para el disparo
    [SerializeField] private float range = 100f; // Rango del raycast
    [SerializeField] private GameObject projectilePrefab; 
    [SerializeField] private Transform shootPoint; // Punto desde donde se dispara el proyectil
    [SerializeField] private float projectileForce = 700f; // Fuerza del proyectil

    [Header("Health")] 
    [SerializeField] private int maxHealth = 10; 
    private int currentHealth; // Salud actual del jugador

    private void Awake()
    {
        input = new Control();
        currentHealth = maxHealth;

        // Configurar salto
        input.FPS.Jump.performed += ctx =>
        {
            if (character.isGrounded)
            {
                velocityY += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
            }
        };

        
        input.FPS.Shoot.performed += ctx => Shoot();

        Cursor.lockState = CursorLockMode.Locked; 
    }

    private void OnEnable()
    {
        input.FPS.Enable();
    }

    private void OnDisable()
    {
        input.FPS.Disable();
    }

    private void Start()
    {
        character = GetComponent<CharacterController>();
        head = transform.GetChild(0); 
    }

    private void FixedUpdate()
    {
        Vector2 mov = input.FPS.Move.ReadValue<Vector2>(); 
        Vector2 rot = input.FPS.Look.ReadValue<Vector2>(); 

        if (character.isGrounded && velocityY < 0)
            velocityY = 0;

        transform.Rotate(Vector3.up * rot.x * rotationSensitivity);
        head.Rotate(Vector3.right * rot.y * rotationSensitivity);

        Vector3 velocityXZ = transform.rotation * new Vector3(mov.x, 0, mov.y);
        character.Move(velocityXZ * speed);

        velocityY += gravity * Time.deltaTime;
        character.Move(velocityY * Vector3.up * Time.deltaTime);
    }

    private void Shoot()
    {
        if (useRaycast)
        {
            ShootRaycast();
        }
        else
        {
            ShootProjectile();
        }
    }

    private void ShootRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
        {
            Debug.Log($"Hit: {hit.collider.name}");

            ZombieHealth zombieHealth = hit.collider.GetComponent<ZombieHealth>();
                if (zombieHealth != null)
                {
                    zombieHealth.TakeDamage(1); 
                }
        }
    }

    private void ShootProjectile()
    {
        if (projectilePrefab != null && shootPoint != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(playerCamera.transform.forward * projectileForce);
            }
        }
        else
        {
            Debug.LogWarning("El Prefab del proyectil o el punto de disparo no están asignados.");
        }
    }

    // Método para aplicar daño al jugador
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"Salud actual del jugador: {currentHealth}");

        // Reiniciar la escena si la salud llega a 0
        if (currentHealth <= 0)
        {
            Debug.Log("El jugador ha muerto.");
            // Aquí puedes reiniciar la escena
            // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
