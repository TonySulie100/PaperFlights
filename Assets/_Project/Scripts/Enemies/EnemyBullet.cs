using UnityEngine;

/// <summary>
/// Controls enemy projectile movement, rotation alignment, and player damage interaction.
/// </summary>
public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private float lifetime = 5f;
    [SerializeField] private int damage = 1;

    private Vector2 moveDirection = Vector2.left;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Modern Unity C# pattern using TryGetComponent to prevent GC allocations
            if (other.TryGetComponent(out PlayerHealth playerHealth))
            {
                playerHealth.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}