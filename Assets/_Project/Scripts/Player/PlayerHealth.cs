using System.Collections;
using UnityEngine;

/// <summary>
/// Manages player health, damage handling, flashing invulnerability routines, and death triggers.
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private HealthUI healthUI;

    [Header("Invulnerability Settings")]
    [SerializeField] private float invincibilityDuration = 3f;
    [SerializeField] private float flashSpeed = 0.15f;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip hurtSFX;

    private int currentHealth;
    private bool isInvulnerable;
    private SpriteRenderer spriteRenderer;
    private PlayerPowerUps powerUps;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        powerUps = GetComponent<PlayerPowerUps>();
    }

    private void Start()
    {
        if (healthUI == null)
        {
            healthUI = FindFirstObjectByType<HealthUI>();
        }

        currentHealth = maxHealth;
        if (healthUI != null)
        {
            healthUI.UpdateHearts(currentHealth);
        }
    }

    public void TakeDamage(int damage)
    {
        if ((powerUps != null && powerUps.IsShieldActive) || isInvulnerable) return;

        if (hurtSFX != null && Camera.main != null)
        {
            AudioSource.PlayClipAtPoint(hurtSFX, Camera.main.transform.position, 1.0f);
        }

        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);

        if (healthUI != null)
        {
            healthUI.UpdateHearts(currentHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvulnerabilityRoutine());
        }
    }

    private IEnumerator InvulnerabilityRoutine()
    {
        isInvulnerable = true;
        float elapsed = 0f;

        while (elapsed < invincibilityDuration)
        {
            if (spriteRenderer != null)
            {
                Color color = spriteRenderer.color;
                color.a = Mathf.Approximately(color.a, 1f) ? 0.3f : 1f;
                spriteRenderer.color = color;
            }

            yield return new WaitForSeconds(flashSpeed);
            elapsed += flashSpeed;
        }

        if (spriteRenderer != null)
        {
            Color resetColor = spriteRenderer.color;
            resetColor.a = 1f;
            spriteRenderer.color = resetColor;
        }

        isInvulnerable = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeDamage(1);
            Destroy(other.gameObject);
        }
    }

    private void Die()
    {
        GameOverManager gameOverManager = FindFirstObjectByType<GameOverManager>();
        if (gameOverManager != null)
        {
            gameOverManager.ShowGameOverPanel();
        }

        Destroy(gameObject);
    }
}