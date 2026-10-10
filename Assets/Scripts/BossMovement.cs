using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [Header("Entrance Settings")]
    [SerializeField] private float targetX = 6.5f;        // Where the boss stops on the right edge
    [SerializeField] private float entranceSpeed = 3f;     // Speed entering from off-screen

    [Header("Up & Down Hover Settings")]
    [SerializeField] private float verticalSpeed = 2f;     // How fast it moves up/down
    [SerializeField] private float verticalAmplitude = 3.2f; // How far up/down it travels

    private bool enteredScreen = false;
    private float centerY = 0f;
    private float hoverTimer = 0f; // Local timer for smooth start

    private void Start()
    {
        // Store the starting Y height as the center pivot line
        centerY = transform.position.y;
    }

    private void Update()
    {
        if (!enteredScreen)
        {
            // 1. Move horizontally from off-screen right to targetX
            float newX = Mathf.MoveTowards(transform.position.x, targetX, entranceSpeed * Time.deltaTime);
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);

            if (Mathf.Approximately(transform.position.x, targetX))
            {
                enteredScreen = true;
                hoverTimer = 0f; // Reset wave timer so sine wave starts cleanly at 0
            }
        }
        else
        {
            // 2. Increment wave timer
            hoverTimer += Time.deltaTime;

            // Sin(0) = 0, ensuring a smooth transition starting right from centerY
            float newY = centerY + Mathf.Sin(hoverTimer * verticalSpeed) * verticalAmplitude;
            transform.position = new Vector3(targetX, newY, transform.position.z);
        }
    }
}