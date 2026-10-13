using UnityEngine;

public enum ItemType
{
    TripleShot,
    Shield,
    Rocket,
    Magnet
}

/// <summary>
/// Controls floating power-up items that grant temporary player enhancements upon collection.
/// </summary>
public class ItemPickup : MonoBehaviour
{
    [SerializeField] private ItemType itemType;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float activeDuration = 7f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float despawnX = -12f;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip pickupSFX;

    private void Update()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime, Space.World);
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);

        if (transform.position.x < despawnX)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Optimized with TryGetComponent for zero unnecessary memory allocations
            if (other.TryGetComponent(out PlayerPowerUps powerUps))
            {
                powerUps.ActivatePowerUp(itemType, activeDuration);
            }

            if (pickupSFX != null && Camera.main != null)
            {
                AudioSource.PlayClipAtPoint(pickupSFX, Camera.main.transform.position, 1f);
            }

            Destroy(gameObject);
        }
    }
}