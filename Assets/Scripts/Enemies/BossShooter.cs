using UnityEngine;

/// <summary>
/// Controls multi-directional spread shooting patterns for boss encounters.
/// </summary>
public class BossShooter : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 1.5f;

    [Header("Spread Settings")]
    [SerializeField] private float spreadAngle = 25f;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip shootSFX;

    private float fireTimer;

    private void Update()
    {
        // Don't shoot until fully on screen
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

        if (shootSFX != null && Camera.main != null)
        {
            AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, 1.0f);
        }

        float baseAngle = 180f;
        float[] angleOffsets = { -spreadAngle, 0f, spreadAngle };

        foreach (float offset in angleOffsets)
        {
            Quaternion bulletRotation = Quaternion.Euler(0f, 0f, baseAngle + offset);
            GameObject bulletObj = Instantiate(bulletPrefab, firePoint.position, bulletRotation);

            EnemyBullet bulletScript = bulletObj.GetComponent<EnemyBullet>();
            if (bulletScript != null)
            {
                Vector2 dir = bulletRotation * Vector3.right;
                bulletScript.SetDirection(dir);
            }
        }
    }
}