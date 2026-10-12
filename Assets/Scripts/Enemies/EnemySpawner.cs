using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }

    [Header("Spawner Settings")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float spawnInterval = 2.0f;
    [SerializeField] private int enemiesPerWave = 10;

    [Header("Boss Encounter")]
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private float timeBeforeBoss = 30f;

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
        if (enemyPrefabs.Length == 0) return;
        int randomIndex = Random.Range(0, enemyPrefabs.Length);

        float randomX = Random.Range(-8f, 8f);
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
            Debug.Log("Boss Spawned! Triggering boss health UI and music transition.");
        }
    }

    public void OnEnemyDestroyed()
    {
        activeEnemyCount = Mathf.Max(0, activeEnemyCount - 1);
    }
}