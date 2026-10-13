using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages boss health, updating the dynamic UI health bar and triggering victory states.
/// </summary>
public class BossHealth : MonoBehaviour
{
    [Header("Boss Health Settings")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private Slider healthSlider;

    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        InitializeHealthBar();
    }

    private void InitializeHealthBar()
    {
        // Fallback to find it dynamically if not assigned in Inspector (essential for prefabs)
        if (healthSlider == null)
        {
            GameObject canvasObj = GameObject.Find("Canvas");
            if (canvasObj != null)
            {
                Transform healthBarTransform = canvasObj.transform.Find("BossHealthBar");
                if (healthBarTransform != null)
                {
                    healthSlider = healthBarTransform.GetComponent<Slider>();
                }
            }
        }

        if (healthSlider != null)
        {
            healthSlider.gameObject.SetActive(true);
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
        else
        {
            Debug.LogWarning("BossHealthBar slider could not be found or assigned.");
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        WinManager winManager = FindFirstObjectByType<WinManager>();
        if (winManager != null)
        {
            winManager.ShowWinPanel();
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet") || collision.CompareTag("PlayerBullet"))
        {
            TakeDamage(1);
            Destroy(collision.gameObject);
        }
    }
}