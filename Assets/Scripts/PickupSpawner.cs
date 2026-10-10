using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [Header("Pickup Prefabs")]
    [SerializeField] private GameObject[] pickupPrefabs; // Assign Pickup 1, 2, and 3 prefabs

    [Header("Spawn Settings")]
    [SerializeField] private float spawnInterval = 12f; // Seconds between item drops
    [SerializeField] private float spawnX = 10f;        // Off-screen right position
    [SerializeField] private float minY = -3.5f;        // Min vertical screen bound
    [SerializeField] private float maxY = 3.5f;         // Max vertical screen bound

    [Header("Spawn Settings")]
    [SerializeField] private float minSpawnInterval = 3f; // Minimum delay
    [SerializeField] private float maxSpawnInterval = 7f; // Maximum delay

    private float nextSpawnTime;

    private void Start()
    {
        // First pickup spawns after the initial interval
        nextSpawnTime = Time.time + spawnInterval;
    }

    private void Update()
    {
        // Don't count down or spawn while paused
        if (PauseManager.IsPaused || Time.timeScale == 0f) return;

        if (Time.time >= nextSpawnTime)
        {
            SpawnPickup();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    private void SpawnPickup()
    {
        if (pickupPrefabs == null || pickupPrefabs.Length == 0) return;

        int randomIndex = Random.Range(0, pickupPrefabs.Length);
        float randomY = Random.Range(minY, maxY);
        Vector3 spawnPosition = new Vector3(spawnX, randomY, 0f);

        Instantiate(pickupPrefabs[randomIndex], spawnPosition, Quaternion.identity);

        // Set next spawn time to a random delay
        nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
    }
}