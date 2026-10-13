using UnityEngine;

/// <summary>
/// Controls homing rocket projectiles that track the closest enemy or boss, dealing high damage.
/// </summary>
public class HomingRocket : MonoBehaviour
{
    [SerializeField] private float speed = 12f;
    [SerializeField] private int rocketDamage = 10;
    [SerializeField] private float spriteRotationOffset = -90f;
    [SerializeField] private float offscreenRightX = 15f;

    private Transform target;

    private void Start()
    {
        FindClosestEnemy();
    }

    private void Update()
    {
        if (target == null)
        {
            FlyStraight();
            return;
        }

        TrackAndMoveToTarget();
    }

    private void FlyStraight()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, spriteRotationOffset);
        transform.position += Vector3.right * speed * Time.deltaTime;

        if (transform.position.x > offscreenRightX)
        {
            Destroy(gameObject);
        }
    }

    private void TrackAndMoveToTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) + spriteRotationOffset;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        transform.position += direction * speed * Time.deltaTime;
    }

    private void FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            if (enemy == null) continue;

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
            // Replaced slow SendMessage with direct, type-safe component calls
            bool isBoss = false;

            if (other.TryGetComponent(out BossHealth bossHealth))
            {
                bossHealth.TakeDamage(rocketDamage);
                isBoss = true;
            }
            else if (other.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(rocketDamage);
            }

            if (!isBoss)
            {
                Destroy(other.gameObject);
            }

            Destroy(gameObject);
        }
    }
}