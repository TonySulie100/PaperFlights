using UnityEngine;

public class ShieldRotator : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 120f; // Degrees per second

    private void Update()
    {
        // Don't rotate while game is paused
        if (PauseManager.IsPaused || Time.timeScale == 0f) return;

        // Spin continuously around the Z-axis
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}