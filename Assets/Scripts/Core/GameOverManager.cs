using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages game over state, audio stops, lose sound playback, and scene navigation.
/// </summary>
public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip loseSFX;

    public void ShowGameOverPanel()
    {
        if (gameOverPanel == null) return;

        AudioManager audioManager = FindFirstObjectByType<AudioManager>();
        if (audioManager != null)
        {
            AudioSource[] sources = audioManager.GetComponents<AudioSource>();
            foreach (AudioSource source in sources)
            {
                if (source != null && source.isPlaying)
                {
                    source.Stop();
                }
            }
        }

        if (loseSFX != null && Camera.main != null)
        {
            AudioSource.PlayClipAtPoint(loseSFX, Camera.main.transform.position);
        }

        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OnRestartButtonClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnMainMenuButtonClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}