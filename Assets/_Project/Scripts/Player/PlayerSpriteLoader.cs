using UnityEngine;

/// <summary>
/// Loads and applies the player's selected plane sprite based on persistent player preferences.
/// </summary>
public class PlayerSpriteLoader : MonoBehaviour
{
    [SerializeField] private Sprite[] planeSprites;

    private void Start()
    {
        int selectedIndex = PlayerPrefs.GetInt("SelectedPlane", 0);

        if (selectedIndex >= 0 && selectedIndex < planeSprites.Length)
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (sr != null && planeSprites[selectedIndex] != null)
            {
                sr.sprite = planeSprites[selectedIndex];
            }
        }
    }
}