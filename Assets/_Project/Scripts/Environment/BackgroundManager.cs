using UnityEngine;

/// <summary>
/// Manages seamless looping of dual background layers for a continuous scrolling effect.
/// </summary>
public class BackgroundManager : MonoBehaviour
{
    [SerializeField] private GameObject background1;
    [SerializeField] private GameObject background2;
    [SerializeField] private float scrollSpeed = 3f;
    [SerializeField] private float fallbackSpriteWidth = 19.2f;

    private float spriteWidth;

    private void Start()
    {
        if (background1 == null || background2 == null)
        {
            Debug.LogWarning("BackgroundManager: Background references are not fully assigned!");
            return;
        }

        if (background1.TryGetComponent(out SpriteRenderer sr) && sr.bounds.size.x > 0f)
        {
            spriteWidth = sr.bounds.size.x;
        }
        else
        {
            spriteWidth = fallbackSpriteWidth;
            Debug.LogWarning($"BackgroundManager: SpriteRenderer missing or zero size on {background1.name}. Using fallback width: {spriteWidth}");
        }

        // Align background 2 precisely next to background 1
        background2.transform.position = new Vector3(
            background1.transform.position.x + spriteWidth,
            background1.transform.position.y,
            background1.transform.position.z
        );
    }

    private void Update()
    {
        if (PauseManager.IsPaused || Mathf.Approximately(Time.timeScale, 0f)) return;

        Vector3 moveDelta = Vector3.left * scrollSpeed * Time.deltaTime;
        background1.transform.Translate(moveDelta, Space.World);
        background2.transform.Translate(moveDelta, Space.World);

        CheckAndResetBackground(background1, background2);
        CheckAndResetBackground(background2, background1);
    }

    private void CheckAndResetBackground(GameObject currentBg, GameObject otherBg)
    {
        if (currentBg.transform.position.x <= -spriteWidth)
        {
            currentBg.transform.position = new Vector3(
                otherBg.transform.position.x + spriteWidth,
                currentBg.transform.position.y,
                currentBg.transform.position.z
            );
        }
    }
}