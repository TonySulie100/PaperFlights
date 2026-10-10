using System.Collections;
using UnityEngine;
using TMPro;

[System.Serializable]
public class EnemySpawnConfig
{
    public string enemyName = "Enemy";
    public GameObject enemyPrefab;

    [Header("Randomized Spawn Interval (Seconds)")]
    public float minInterval = 2.5f; // e.g. around 3s
    public float maxInterval = 3.5f;
}

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }

    [Header("Regular Enemy Settings")]
    [SerializeField] private EnemySpawnConfig[] enemyConfigs; // Assign Enemy 1, Enemy 2, etc. with custom times
    [SerializeField] private int maxEnemiesToSpawn = 10;

    [Header("Boss Settings")]
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private float bossSpawnDelay = 10f;
    [SerializeField] private Transform bossSpawnPoint;

    [Header("Countdown UI Settings")]
    [SerializeField] private TMP_Text countdownText;

    [Header("Random Y Spawn Area")]
    [SerializeField] private float spawnX = 10f;
    [SerializeField] private float minY = -3.5f;
    [SerializeField] private float maxY = 3.5f;

    private int spawnedCount = 0;
    private int activeEnemyCount = 0;

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
        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(false);
        }

        StartCoroutine(MainWaveRoutine());
    }

    private IEnumerator MainWaveRoutine()
    {
        // 1. Launch independent spawning loops for each enemy config concurrently
        foreach (var config in enemyConfigs)
        {
            StartCoroutine(SpawnEnemyTypeRoutine(config));
        }

        // Wait until total spawned enemies reach max wave cap
        while (spawnedCount < maxEnemiesToSpawn)
        {
            yield return null;
        }

        // 2. Wait until ALL active regular enemies are destroyed/despawned
        while (activeEnemyCount > 0)
        {
            yield return null;
        }

        // 3. Countdown timer on screen
        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(true);
            int countdownInt = Mathf.RoundToInt(bossSpawnDelay);

            for (int i = countdownInt; i > 0; i--)
            {
                countdownText.text = i.ToString();
                yield return new WaitForSeconds(1f);
            }

            countdownText.gameObject.SetActive(false);
        }
        else
        {
            yield return new WaitForSeconds(bossSpawnDelay);
        }

        // 4. Spawn the Boss
        SpawnBoss();
    }

    private IEnumerator SpawnEnemyTypeRoutine(EnemySpawnConfig config)
    {
        while (spawnedCount < maxEnemiesToSpawn)
        {
            // Pick a randomized interval between min and max
            float randomDelay = Random.Range(config.minInterval, config.maxInterval);
            yield return new WaitForSeconds(randomDelay);

            // Double check total wave limit before instantiating
            if (spawnedCount < maxEnemiesToSpawn && config.enemyPrefab != null)
            {
                SpawnSingleEnemy(config.enemyPrefab);
            }
        }
    }

    private void SpawnSingleEnemy(GameObject prefab)
    {
        float randomY = Random.Range(minY, maxY);
        Vector3 spawnPosition = new Vector3(spawnX, randomY, 0f);

        Instantiate(prefab, spawnPosition, Quaternion.identity);
        spawnedCount++;
        activeEnemyCount++;
    }

    public void OnEnemyDestroyed()
    {
        activeEnemyCount--;
        if (activeEnemyCount < 0) activeEnemyCount = 0;
    }

    private void SpawnBoss()
    {
        if (bossPrefab == null) return;

        Vector3 spawnPos = (bossSpawnPoint != null)
            ? bossSpawnPoint.position
            : new Vector3(spawnX, 0f, 0f);

        Instantiate(bossPrefab, spawnPos, Quaternion.identity);
    }
}