using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField] private GameObject background1;
    [SerializeField] private GameObject background2;
    [SerializeField] private float scrollSpeed = 3f;

    private float spriteWidth;

    private void Start()
    {
        // Calculate the exact world-space width of the background sprite
        SpriteRenderer sr = background1.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            spriteWidth = sr.bounds.size.x;
        }

        // Align Background 2 directly to the right of Background 1
        background2.transform.position = new Vector3(
            background1.transform.position.x + spriteWidth,
            background1.transform.position.y,
            background1.transform.position.z
        );
    }

    private void Update()
    {
        if (PauseManager.IsPaused || Time.timeScale == 0f) return;

        // Move both backgrounds left
        background1.transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);
        background2.transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);

        // Reset background 1 when off-screen
        if (background1.transform.position.x <= -spriteWidth)
        {
            background1.transform.position = new Vector3(
                background2.transform.position.x + spriteWidth,
                background1.transform.position.y,
                background1.transform.position.z
            );
        }

        // Reset background 2 when off-screen
        if (background2.transform.position.x <= -spriteWidth)
        {
            background2.transform.position = new Vector3(
                background1.transform.position.x + spriteWidth,
                background2.transform.position.y,
                background2.transform.position.z
            );
        }
    }
}