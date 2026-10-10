using UnityEngine;

public class PlayerSpriteLoader : MonoBehaviour
{
    [SerializeField] private Sprite[] planeSprites; // Drag Player 1, Player 2, Player 3 sprites here

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