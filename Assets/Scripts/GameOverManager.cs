using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip loseSFX;

    public void ShowGameOverPanel()
    {
        if (gameOverPanel != null)
        {
            // 1. Stop background music on AudioManager
            AudioManager audioManager = FindFirstObjectByType<AudioManager>();
            if (audioManager != null)
            {
                AudioSource[] sources = audioManager.GetComponents<AudioSource>();
                foreach (AudioSource source in sources)
                {
                    if (source.isPlaying) source.Stop();
                }
            }

            // 2. Play Lose SFX
            if (loseSFX != null)
            {
                AudioSource.PlayClipAtPoint(loseSFX, Camera.main.transform.position);
            }

            // 3. Display panel and pause time
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void OnRestartButtonClicked()
    {
        Time.timeScale = 1f; // Unpause time before reloading
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload current level
    }

    public void OnMainMenuButtonClicked()
    {
        Time.timeScale = 1f; // Unpause time
        SceneManager.LoadScene("MainMenu Scene"); // Load main menu scene
    }
}