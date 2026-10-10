using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [Header("UI References")]
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private TMP_Text scoreText;

    private int sessionCoins = 0;
    private int sessionScore = 0;

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
        UpdateCoinUI();
        UpdateScoreUI();
    }

    public void AddCoin(int amount = 1)
    {
        sessionCoins += amount;
        UpdateCoinUI();

        // Save persistently using "TotalCoins" so ShopManager reads it
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        PlayerPrefs.SetInt("TotalCoins", totalCoins + amount);
        PlayerPrefs.Save();
    }

    public void AddScore(int amount)
    {
        sessionScore += amount;
        UpdateScoreUI();

        // Check and save Highscore
        int currentHighScore = PlayerPrefs.GetInt("HighScore", 0);
        if (sessionScore > currentHighScore)
        {
            PlayerPrefs.SetInt("HighScore", sessionScore);
            PlayerPrefs.Save();
        }
    }

    private void UpdateCoinUI()
    {
        if (coinText != null)
        {
            coinText.text = $"x{sessionCoins}";
        }
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score:{sessionScore}";
        }
    }
}