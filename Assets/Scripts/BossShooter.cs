using UnityEngine;

public class BossShooter : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 1.5f; // Fires every 1.5 seconds

    [Header("Spread Settings")]
    [SerializeField] private float spreadAngle = 25f; // Angle difference between upper, middle, and lower bullets

    [Header("Audio Settings")]
    [SerializeField] private AudioClip shootSFX;

    private float fireTimer = 0f;

    private void Update()
    {
        // Don't shoot until the boss has slid onto the screen
        if (transform.position.x > 7.5f) return;

        fireTimer += Time.deltaTime;

        if (fireTimer >= fireRate)
        {
            fireTimer = 0f;
            ShootSpread();
        }
    }

    private void ShootSpread()
    {
        if (bulletPrefab == null || firePoint == null) return;

        // Play Boss Shoot SFX
        if (shootSFX != null)
        {
            AudioSource.PlayClipAtPoint(shootSFX, transform.position);
        }

        float baseAngle = 180f;
        float[] angleOffsets = { -spreadAngle, 0f, spreadAngle };

        foreach (float offset in angleOffsets)
        {
            Quaternion bulletRotation = Quaternion.Euler(0f, 0f, baseAngle + offset);
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, bulletRotation);

            EnemyBullet bulletScript = bullet.GetComponent<EnemyBullet>();
            if (bulletScript != null)
            {
                Vector2 dir = bulletRotation * Vector3.right;
                bulletScript.SetDirection(dir);
            }
        }
    }
}