using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages boss health, updating the dynamic UI health bar and triggering victory states.
/// </summary>
public class BossHealth : MonoBehaviour
{
    [Header("Boss Health Settings")]
    [SerializeField] private int maxHealth = 100;

    private int currentHealth;
    private Slider healthSlider;

    private void Start()
    {
        currentHealth = maxHealth;
        InitializeHealthBar();
    }

    private void InitializeHealthBar()
    {
        GameObject canvasObj = GameObject.Find("Canvas");
        if (canvasObj == null)
        {
            Debug.LogWarning("Canvas not found in scene.");
            return;
        }

        Transform healthBarTransform = canvasObj.transform.Find("BossHealthBar");
        if (healthBarTransform == null)
        {
            Debug.LogWarning("BossHealthBar not found as a child of Canvas.");
            return;
        }

        GameObject healthBarObj = healthBarTransform.gameObject;
        healthBarObj.SetActive(true);

        healthSlider = healthBarObj.GetComponent<Slider>();
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
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