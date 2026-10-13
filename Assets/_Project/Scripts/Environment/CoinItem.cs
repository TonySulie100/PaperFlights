using UnityEngine;

/// <summary>
/// Controls collectible coin pickups, including magnetic pull behavior toward the player and score incrementing.
/// </summary>
public class CoinItem : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float scrollSpeed = 3f;
    [SerializeField] private float magnetPullSpeed = 12f;
    [SerializeField] private float lifetime = 8f;

    [Header("Coin Value & Audio")]
    [SerializeField] private int coinValue = 1;
    [SerializeField] private AudioClip coinSFX;

    private Transform playerTransform;
    private PlayerPowerUps playerPowerUps;

    private void Start()
    {
        Destroy(gameObject, lifetime);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
            player.TryGetComponent(out playerPowerUps);
        }
    }

    private void Update()
    {
        if (playerPowerUps != null && playerPowerUps.IsMagnetActive && playerTransform != null)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                playerTransform.position,
                magnetPullSpeed * Time.deltaTime
            );
        }
        else
        {
            transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime, Space.World);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddCoin(coinValue);
            }

            if (coinSFX != null && Camera.main != null)
            {
                AudioSource.PlayClipAtPoint(coinSFX, Camera.main.transform.position, 1.0f);
            }

            Destroy(gameObject);
        }
    }
}