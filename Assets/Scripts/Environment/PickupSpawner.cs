using UnityEngine;

/// <summary>
/// Periodically spawns randomized power-up items across the screen.
/// </summary>
public class PickupSpawner : MonoBehaviour
{
    [Header("Pickup Prefabs")]
    [SerializeField] private GameObject[] pickupPrefabs;

    [Header("Spawn Bounds & Position")]
    [SerializeField] private float spawnX = 10f;
    [SerializeField] private float minY = -3.5f;
    [SerializeField] private float maxY = 3.5f;

    [Header("Spawn Intervals")]
    [SerializeField] private float initialSpawnDelay = 12f;
    [SerializeField] private float minSpawnInterval = 3f;
    [SerializeField] private float maxSpawnInterval = 7f;

    private float nextSpawnTime;

    private void Start()
    {
        nextSpawnTime = Time.time + initialSpawnDelay;
    }

    private void Update()
    {
        if (PauseManager.IsPaused || Mathf.Approximately(Time.timeScale, 0f)) return;

        if (Time.time >= nextSpawnTime)
        {
            SpawnPickup();
        }
    }

    private void SpawnPickup()
    {
        if (pickupPrefabs == null || pickupPrefabs.Length == 0) return;

        int randomIndex = Random.Range(0, pickupPrefabs.Length);
        if (pickupPrefabs[randomIndex] != null)
        {
            float randomY = Random.Range(minY, maxY);
            Vector3 spawnPosition = new Vector3(spawnX, randomY, 0f);

            Instantiate(pickupPrefabs[randomIndex], spawnPosition, Quaternion.identity);
        }

        nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
    }
}