using UnityEngine;

public enum ItemType
{
    TripleShot,
    Shield,
    Rocket,
    Magnet
}

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private ItemType itemType;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float activeDuration = 7f;
    [SerializeField] private float rotationSpeed = 100f;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip pickupSFX;

    private void Update()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime, Space.World);
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);

        if (transform.position.x < -12f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPowerUps powerUps = other.GetComponent<PlayerPowerUps>();
            if (powerUps != null)
            {
                powerUps.ActivatePowerUp(itemType, activeDuration);
            }

            // Play pickup sound effect at position before destroying
            if (pickupSFX != null && Camera.main != null)
            {
                AudioSource.PlayClipAtPoint(pickupSFX, Camera.main.transform.position, 1f);
            }

            Destroy(gameObject);
        }
    }
}