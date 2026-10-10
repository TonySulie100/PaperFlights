using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [Header("UI Heart References")]
    [SerializeField] private Image[] heartImages;

    // Active red color vs depleted empty color
    private Color fullColor = Color.white;
    private Color emptyColor = new Color(0.2f, 0.2f, 0.2f, 0.5f); // Dark gray / semi-transparent

    public void UpdateHearts(int currentHealth)
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (i < currentHealth)
            {
                // Active health: keep natural red image colors
                heartImages[i].color = fullColor;
            }
            else
            {
                // Lost health: drain the red color
                heartImages[i].color = emptyColor;
            }
        }
    }
}