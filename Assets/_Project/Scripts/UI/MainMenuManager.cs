using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// Manages main menu panel navigation, highscore/coin data displays, and scene transitions.
/// </summary>
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
    [SerializeField] private string gameSceneName = "Gameplay";

    private const string CoinsKey = "TotalCoins";
    private const string HighscoreKey = "HighScore";

    private void Start()
    {
        OpenMainMenu();
    }

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
        UpdatePlayMenuData();
    }

    public void UpdatePlayMenuData()
    {
        int highscore = PlayerPrefs.GetInt(HighscoreKey, 0);
        if (highscoreText != null) highscoreText.text = $"Highscore :{highscore}";

        UpdateCoinUI();
    }

    public void UpdateCoinUI()
    {
        int totalCoins = PlayerPrefs.GetInt(CoinsKey, 0);
        if (coinText != null)
        {
            coinText.text = $"x{totalCoins}";
        }
    }

    public void ClosePlayMenu() => OpenMainMenu();
    public void CloseQuestions() => OpenMainMenu();
    public void CloseSettings() => OpenMainMenu();
    public void CloseAboutUs() => OpenMainMenu();
    public void CloseVersions() => OpenMainMenu();

    public void OpenQuestions()
    {
        HideAllPanels();
        if (questionsPanel != null) questionsPanel.SetActive(true);
    }

    public void OpenSettings()
    {
        HideAllPanels();
        if (settingsPanel != null) settingsPanel.SetActive(true);
    }

    public void OpenAboutUs()
    {
        HideAllPanels();
        if (aboutUsPanel != null) aboutUsPanel.SetActive(true);
    }

    public void OpenVersions()
    {
        HideAllPanels();
        if (versionsPanel != null) versionsPanel.SetActive(true);
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