using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject playMenuPanel;
    [SerializeField] private GameObject questionsPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject aboutUsPanel;
    [SerializeField] private GameObject versionsPanel;

    [Header("Play Menu Data UI")]
    [SerializeField] private TMP_Text highscoreText;
    [SerializeField] private TMP_Text coinText;

    [Header("Scenes")]
    [SerializeField] private string gameSceneName = "Gameplay Scene";

    private const string COINS_KEY = "TotalCoins";
    private const string HIGHSCORE_KEY = "HighScore";

    private void Start()
    {
        OpenMainMenu();
    }

    // Helper method to ensure no two panels overlap
    private void HideAllPanels()
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
        if (playMenuPanel != null) playMenuPanel.SetActive(false);
        if (questionsPanel != null) questionsPanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (aboutUsPanel != null) aboutUsPanel.SetActive(false);
        if (versionsPanel != null) versionsPanel.SetActive(false);
    }

    public void OpenMainMenu()
    {
        HideAllPanels();
        if (mainMenuPanel != null) mainMenuPanel.SetActive(true);
    }

    public void OpenPlayMenu()
    {
        HideAllPanels();
        if (playMenuPanel != null) playMenuPanel.SetActive(true);

        // Update Highscore and Coins display
        UpdatePlayMenuData();
    }

    public void UpdatePlayMenuData()
    {
        int highscore = PlayerPrefs.GetInt(HIGHSCORE_KEY, 0);
        if (highscoreText != null) highscoreText.text = $"Highscore :{highscore}";

        UpdateCoinUI();
    }

    public void UpdateCoinUI()
    {
        int totalCoins = PlayerPrefs.GetInt(COINS_KEY, 0);
        if (coinText != null)
        {
            coinText.text = $"x{totalCoins}";
        }
    }

    public void ClosePlayMenu()
    {
        OpenMainMenu();
    }

    public void OpenQuestions()
    {
        HideAllPanels();
        if (questionsPanel != null) questionsPanel.SetActive(true);
    }

    public void CloseQuestions()
    {
        OpenMainMenu();
    }

    public void OpenSettings()
    {
        HideAllPanels();
        if (settingsPanel != null) settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        OpenMainMenu();
    }

    public void OpenAboutUs()
    {
        HideAllPanels();
        if (aboutUsPanel != null) aboutUsPanel.SetActive(true);
    }

    public void CloseAboutUs()
    {
        OpenMainMenu();
    }

    public void OpenVersions()
    {
        HideAllPanels();
        if (versionsPanel != null) versionsPanel.SetActive(true);
    }

    public void CloseVersions()
    {
        OpenMainMenu();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}