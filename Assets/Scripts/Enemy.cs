using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum ShootingType { None, Straight, AimedAtPlayer }

    [Header("Enemy Attributes")]
    [SerializeField] private int maxHealth = 1;
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float offscreenX = -10f;

    [Header("Shooting Settings")]
    [SerializeField] private ShootingType shootingType = ShootingType.None;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 2f;

    [Header("Drop Settings & Score")]
    [SerializeField] private GameObject coinPrefab;
    [SerializeField][Range(0f, 1f)] private float coinDropChance = 1f; // 1.0 = 100% drop rate
    [SerializeField] private int scoreValue = 1;
    [Header("Audio Settings")]
    [SerializeField] private AudioClip shootSFX;

    private int currentHealth;
    private float nextFireTime;
    private Transform playerTransform;

    private void Start()
    {
        currentHealth = maxHealth;
        nextFireTime = Time.time + Random.Range(0.5f, fireRate);

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
        }
    }

    private void Update()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

        if (shootingType != ShootingType.None && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }

        if (transform.position.x < offscreenX)
        {
            Destroy(gameObject);
        }
    }

    private void Shoot()
    {
        if (bulletPrefab == null) return;

        Vector3 spawnPos = (firePoint != null) ? firePoint.position : transform.position;
        GameObject bulletObj = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
        EnemyBullet bullet = bulletObj.GetComponent<EnemyBullet>();

        if (bullet != null)
        {
            if (shootingType == ShootingType.Straight)
            {
                bullet.SetDirection(Vector2.left);
            }
            else if (shootingType == ShootingType.AimedAtPlayer && playerTransform != null)
            {
                Vector2 directionToPlayer = (playerTransform.position - spawnPos).normalized;
                bullet.SetDirection(directionToPlayer);
            }
            else
            {
                bullet.SetDirection(Vector2.left);
            }
        }
        if (shootSFX != null && Camera.main != null)
        {
            AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, 1f);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Add score
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(scoreValue);
        }

        // Spawn Coin Drop
        if (coinPrefab != null && Random.value <= coinDropChance)
        {
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        // Notify the spawner that an enemy was removed
        if (EnemySpawner.Instance != null)
        {
            EnemySpawner.Instance.OnEnemyDestroyed();
        }
    }
}