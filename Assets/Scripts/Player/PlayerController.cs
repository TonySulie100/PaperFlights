using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Handles player movement within screen bounds and firing logic supporting standard or triple shot.
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private Vector2 screenBounds = new Vector2(8f, 4f);

    [Header("Shooting")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 0.15f;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip shootSFX;

    private float nextFireTime;
    private PlayerPowerUps powerUps;

    private void Awake()
    {
        powerUps = GetComponent<PlayerPowerUps>();
    }

    private void Update()
    {
        if (PauseManager.IsPaused || Mathf.Approximately(Time.timeScale, 0f)) return;

        HandleMovement();
        HandleShooting();
    }

    private void HandleMovement()
    {
        float moveX = 0f;
        float moveY = 0f;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.leftArrowKey.isPressed) moveX = -1f;
            if (Keyboard.current.rightArrowKey.isPressed) moveX = 1f;
            if (Keyboard.current.downArrowKey.isPressed) moveY = -1f;
            if (Keyboard.current.upArrowKey.isPressed) moveY = 1f;
        }

        Vector3 direction = new Vector3(moveX, moveY, 0f).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        float clampedX = Mathf.Clamp(transform.position.x, -screenBounds.x, screenBounds.x);
        float clampedY = Mathf.Clamp(transform.position.y, -screenBounds.y, screenBounds.y);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    private void HandleShooting()
    {
        bool isShooting = Keyboard.current != null && Keyboard.current.zKey.isPressed;

        if (isShooting && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        if (bulletPrefab == null || firePoint == null) return;

        if (powerUps != null && powerUps.IsTripleShotActive)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation * Quaternion.Euler(0f, 0f, 15f));
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation * Quaternion.Euler(0f, 0f, -15f));
        }
        else
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }

        if (shootSFX != null && Camera.main != null)
        {
            AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, 1.0f);
        }
    }
}