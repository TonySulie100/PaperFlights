using UnityEngine;

public class HomingRocket : MonoBehaviour
{
    [SerializeField] private float speed = 12f;
    [SerializeField] private int rocketDamage = 10;
    [SerializeField] private float spriteRotationOffset = -90f; // Align UP sprite to target

    private Transform target;

    private void Start()
    {
        FindClosestEnemy();
    }

    private void Update()
    {
        if (target == null)
        {
            // If no target exists, fly straight right
            transform.rotation = Quaternion.Euler(0, 0, spriteRotationOffset);
            transform.position += Vector3.right * speed * Time.deltaTime;

            // Despawn off-screen
            if (transform.position.x > 15f)
            {
                Destroy(gameObject);
            }
            return;
        }

        // Calculate direction and angle to target
        Vector3 direction = (target.position - transform.position).normalized;
        float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) + spriteRotationOffset;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // Move towards target
        transform.position += direction * speed * Time.deltaTime;
    }

    private void FindClosestEnemy()
    {
        // Finds standard enemies or boss
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                target = enemy.transform;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Deal 10 rocket damage
            other.SendMessage("TakeDamage", rocketDamage, SendMessageOptions.DontRequireReceiver);

            // Check if the object has a boss script (or contains "boss" in its name)
            bool isBoss = other.GetComponent<BossHealth>() != null || other.name.ToLower().Contains("boss");

            // Only force-destroy regular minion enemies
            if (!isBoss)
            {
                Destroy(other.gameObject);
            }

            // Always destroy the rocket on hit
            Destroy(gameObject);
        }
    }
}