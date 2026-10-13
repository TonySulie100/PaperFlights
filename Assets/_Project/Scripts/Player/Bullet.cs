using UnityEngine;

/// <summary>
/// Controls standard player projectile movement, lifetime expiration, and enemy damage dealing.
/// </summary>
public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 15f;
    [SerializeField] private float lifetime = 3f;
    [SerializeField] private int damage = 1;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Modern Unity C# pattern using TryGetComponent for zero unnecessary allocations
            if (other.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}