using UnityEngine;

/// <summary>
/// Simple continuous single-sprite background scroller.
/// </summary>
public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 3f;
    [SerializeField] private float backgroundWidth = 12.8f;

    private void Update()
    {
        if (PauseManager.IsPaused || Mathf.Approximately(Time.timeScale, 0f)) return;

        transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime, Space.World);

        if (transform.position.x <= -backgroundWidth)
        {
            transform.position += new Vector3(backgroundWidth * 2f, 0f, 0f);
        }
    }
}