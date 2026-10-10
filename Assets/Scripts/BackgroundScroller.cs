using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 3f;
    [SerializeField] private float backgroundWidth = 12.8f; // Match width of your background in Unity units

    private void Update()
    {
        // Move background to the left
        transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);

        // Reset position when completely off-screen to the left
        if (transform.position.x <= -backgroundWidth)
        {
            transform.position += new Vector3(backgroundWidth * 2f, 0f, 0f);
        }
    }
}