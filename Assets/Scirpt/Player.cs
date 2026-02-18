using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{

    private Rigidbody rb;
    private float movementX;
    private float movementY;
    public float speed = 10f;

     public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 20f;
    private float nextFireTime = 0f;
    public float fireRate = 0.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void FixedUpdate()
    {
        
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    void Shoot()
    {
        
        GameObject bulletInstance = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        
        Rigidbody rb = bulletInstance.GetComponent<Rigidbody>();
        
        rb.AddForce(firePoint.forward * bulletSpeed, ForceMode.Impulse);

        
        Destroy(bulletInstance, 5f); 
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
