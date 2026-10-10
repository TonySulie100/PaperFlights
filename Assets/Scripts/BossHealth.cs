using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    [Header("Boss Health Settings")]
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    private Slider healthSlider;

    private void Start()
    {
        currentHealth = maxHealth;

        // 1. Find the active Canvas in the scene
        GameObject canvasObj = GameObject.Find("Canvas");
        if (canvasObj != null)
        {
            // 2. Search for the child BossHealthBar (works even if inactive)
            Transform healthBarTransform = canvasObj.transform.Find("BossHealthBar");
            if (healthBarTransform != null)
            {
                GameObject healthBarObj = healthBarTransform.gameObject;

                // 3. Activate the UI
                healthBarObj.SetActive(true);
                healthSlider = healthBarObj.GetComponent<Slider>();

                if (healthSlider != null)
                {
                    healthSlider.maxValue = maxHealth;
                    healthSlider.value = currentHealth;
                }
            }
            else
            {
                Debug.LogWarning("BossHealthBar not found as a child of Canvas!");
            }
        }
        else
        {
            Debug.LogWarning("Canvas not found in scene!");
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

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
        // Trigger the Win Panel
        WinManager winManager = FindFirstObjectByType<WinManager>();
        if (winManager != null)
        {
            winManager.ShowWinPanel();
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if hit by a player bullet
        if (collision.CompareTag("Bullet") || collision.CompareTag("PlayerBullet"))
        {
            TakeDamage(1); // Each bullet does 1 damage
            Destroy(collision.gameObject); // Destroy player bullet
        }
    }
}