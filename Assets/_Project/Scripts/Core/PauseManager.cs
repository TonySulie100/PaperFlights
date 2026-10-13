using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

/// <summary>
/// Handles game pausing, unpausing, settings toggling, and scene reloads/exits.
/// </summary>
public class PauseManager : MonoBehaviour
{
    public static bool IsPaused { get; private set; }

    [Header("UI Panels")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject youWinPanel;
    [SerializeField] private GameObject bossHealthBar;

    private bool wasBossHealthBarActive;

    private void Awake()
    {
        // Ensure static state resets safely on load/awake to prevent cross-scene bugs
        IsPaused = false;
    }

    private void Start()
    {
        if (pausePanel != null) pausePanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(false);

        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (IsEndGameScreenActive()) return;

        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (IsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    private bool IsEndGameScreenActive()
    {
        bool isGameOverActive = gameOverPanel != null && gameOverPanel.activeSelf;
        bool isYouWinActive = youWinPanel != null && youWinPanel.activeSelf;
        return isGameOverActive || isYouWinActive;
    }

    public void PauseGame()
    {
        IsPaused = true;

        if (bossHealthBar != null)
        {
            wasBossHealthBarActive = bossHealthBar.activeSelf;
            bossHealthBar.SetActive(false);
        }

        if (pausePanel != null) pausePanel.SetActive(true);
        if (settingsPanel != null) settingsPanel.SetActive(false);

        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        IsPaused = false;

        if (pausePanel != null) pausePanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(false);

        if (bossHealthBar != null && wasBossHealthBarActive)
        {
            bossHealthBar.SetActive(true);
        }

        Time.timeScale = 1f;
    }

    public void OpenSettings()
    {
        if (pausePanel != null) pausePanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (pausePanel != null) pausePanel.SetActive(true);
    }

    public void RestartGame()
    {
        IsPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitToMainMenu()
    {
        IsPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayHoverSFX()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayHoverSFX();
        }
    }

    public void PlayClickSFX()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayClickSFX();
        }
    }
}