using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 3f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.AddScore(1);
            }
            else
            {
                Debug.LogWarning("Bullet: GameManager.instance is null. Score not updated.");
            }

            Destroy(gameObject);
        }

        if (collision.CompareTag("Border"))
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.AddScore(-1);
            }
            else
            {
                Debug.LogWarning("Bullet: GameManager.instance is null. Score not updated.");
            }

            Destroy(gameObject);
        }
    }
}
