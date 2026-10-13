using UnityEngine;

/// <summary>
/// Controls continuous rotation animation for player shield visuals around the Z-axis.
/// </summary>
public class ShieldRotator : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 120f;

    private void Update()
    {
        if (PauseManager.IsPaused || Mathf.Approximately(Time.timeScale, 0f)) return;

        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}