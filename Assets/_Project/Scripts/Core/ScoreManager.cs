using UnityEngine;
using TMPro;

/// <summary>
/// Manages session score and currency accumulation, UI text updates, and persistent high score/coin saving.
/// </summary>
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [Header("UI References")]
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private TMP_Text scoreText;

    private int sessionCoins;
    private int sessionScore;

    public const string TotalCoinsKey = "TotalCoins";
    public const string HighScoreKey = "HighScore";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        UpdateCoinUI();
        UpdateScoreUI();
    }

    public void ResetSession()
    {
        sessionCoins = 0;
        sessionScore = 0;
        UpdateCoinUI();
        UpdateScoreUI();
    }

    public void AddCoin(int amount = 1)
    {
        sessionCoins += amount;
        UpdateCoinUI();

        int totalCoins = PlayerPrefs.GetInt(TotalCoinsKey, 0);
        PlayerPrefs.SetInt(TotalCoinsKey, totalCoins + amount);
        PlayerPrefs.Save();
    }

    public void AddScore(int amount)
    {
        sessionScore += amount;
        UpdateScoreUI();

        int currentHighScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        if (sessionScore > currentHighScore)
        {
            PlayerPrefs.SetInt(HighScoreKey, sessionScore);
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