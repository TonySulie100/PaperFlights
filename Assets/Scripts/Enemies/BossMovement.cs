using UnityEngine;

/// <summary>
/// Controls the boss's entrance slide onto the screen and subsequent vertical hover oscillation.
/// </summary>
public class BossMovement : MonoBehaviour
{
    [Header("Entrance Settings")]
    [SerializeField] private float targetX = 6.5f;
    [SerializeField] private float entranceSpeed = 3f;

    [Header("Up & Down Hover Settings")]
    [SerializeField] private float verticalSpeed = 2f;
    [SerializeField] private float verticalAmplitude = 3.2f;

    private bool enteredScreen;
    private float centerY;
    private float hoverTimer;

    private void Start()
    {
        centerY = transform.position.y;
    }

    private void Update()
    {
        if (!enteredScreen)
        {
            HandleEntrance();
        }
        else
        {
            HandleHover();
        }
    }

    private void HandleEntrance()
    {
        float newX = Mathf.MoveTowards(transform.position.x, targetX, entranceSpeed * Time.deltaTime);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        if (Mathf.Approximately(transform.position.x, targetX))
        {
            enteredScreen = true;
            hoverTimer = 0f;
        }
    }

    private void HandleHover()
    {
        hoverTimer += Time.deltaTime;
        float newY = centerY + Mathf.Sin(hoverTimer * verticalSpeed) * verticalAmplitude;
        transform.position = new Vector3(targetX, newY, transform.position.z);
    }
}