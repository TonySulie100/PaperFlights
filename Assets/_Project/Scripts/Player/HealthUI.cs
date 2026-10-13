using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the visual health bar UI, updating heart icons based on current player health.
/// </summary>
public class HealthUI : MonoBehaviour
{
    [Header("UI Heart References")]
    [SerializeField] private Image[] heartImages;

    private readonly Color fullColor = Color.white;
    private readonly Color emptyColor = new Color(0.2f, 0.2f, 0.2f, 0.5f);

    public void UpdateHearts(int currentHealth)
    {
        if (heartImages == null) return;

        for (int i = 0; i < heartImages.Length; i++)
        {
            if (heartImages[i] != null)
            {
                heartImages[i].color = i < currentHealth ? fullColor : emptyColor;
            }
        }
    }
}