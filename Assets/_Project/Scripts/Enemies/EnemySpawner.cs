using System.Collections;
using UnityEngine;

/// <summary>
/// Manages enemy wave spawning cycles, boss encounter triggers, and active enemy counts.
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }

    [Header("Spawner Settings")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float spawnInterval = 2.0f;
    [SerializeField] private int enemiesPerWave = 10;

    [Header("Spawn Bounds")]
    [SerializeField] private float minSpawnX = -8f;
    [SerializeField] private float maxSpawnX = 8f;

    [Header("Boss Encounter")]
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private float timeBeforeBoss = 10f;

    private int spawnedCount = 0;
    private int activeEnemyCount = 0;
    private bool isBossSpawned = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        StartCoroutine(SpawnWaveRoutine());
    }

    private IEnumerator SpawnWaveRoutine()
    {
        while (!isBossSpawned && spawnedCount < enemiesPerWave)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnRandomEnemy();
            spawnedCount++;
        }

        if (!isBossSpawned)
        {
            StartCoroutine(SpawnBossRoutine());
        }
    }

    private void SpawnRandomEnemy()
    {
        if (enemyPrefabs == null || enemyPrefabs.Length == 0) return;

        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        float randomX = Random.Range(minSpawnX, maxSpawnX);
        Vector3 spawnPosition = new Vector3(randomX, transform.position.y, 0f);

        Instantiate(enemyPrefabs[randomIndex], spawnPosition, Quaternion.identity);
        activeEnemyCount++;
    }

    private IEnumerator SpawnBossRoutine()
    {
        isBossSpawned = true;
        yield return new WaitForSeconds(timeBeforeBoss);

        if (bossPrefab != null)
        {
            Instantiate(bossPrefab, transform.position, Quaternion.identity);
            Debug.Log("Boss Spawned!");
        }
    }

    public void OnEnemyDestroyed()
    {
        activeEnemyCount = Mathf.Max(0, activeEnemyCount - 1);
    }
}