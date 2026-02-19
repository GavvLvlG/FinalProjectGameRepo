using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;

    [Header("Bullet")]
    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    public Transform firePoint; 

    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction shootAction;
    private Rigidbody2D rb; 

    private void Awake()
    {
        
        playerInput = GetComponent<PlayerInput>();

       
        rb = GetComponent<Rigidbody2D>();

        
        if (playerInput == null)
        {
            Debug.LogError("PlayerInput component not found! Please add one to this GameObject.");
            enabled = false; 
            return;
        }

       
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found! Please add one to this GameObject.");
            enabled = false; 
            return;
        }
    }

    void Start()
    {
        
        moveAction = playerInput.actions.FindAction("Move");
        shootAction = playerInput.actions.FindAction("Shoot");

        if (bulletPrefab == null)
        {
            Debug.LogError("Bullet Prefab is not assigned!");
        }

        if (firePoint == null)
        {
            Debug.LogWarning("Fire Point is not assigned! Bullets will be instantiated at the player's position.");
        }
    }


    void Update()
    {
        if (shootAction.WasPressedThisFrame())
        {
            Shoot();
        }
    }

    
    void FixedUpdate()
    {
      
        Vector2 direction = moveAction.ReadValue<Vector2>();
        Move(direction);
    }

    private void Move(Vector2 direction)
    {
        
        Vector2 normalizedDirection = direction.normalized;

        
        Vector2 velocity = normalizedDirection * speed;

        
        rb.linearVelocity = velocity;
    }

    public void Shoot()
    {
        if (bulletPrefab != null)
        {
            
            Vector3 spawnPosition = transform.position;
            if (firePoint != null)
            {
                spawnPosition = firePoint.position;
            }

            
            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

            if (bulletRb != null)
            {
                bulletRb.linearVelocity = Vector2.right * bulletForce; 
            }
            else
            {
                Debug.LogError("Bullet prefab is missing a Rigidbody2D component!");
                Destroy(bullet); 
            }
        }
        else
        {
            Debug.LogError("Bullet Prefab is not assigned!");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) 
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Exit");
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Stay");
    }
}
