using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the win state UI, halts background music, plays victory audio, and handles scene navigation.
/// </summary>
public class WinManager : MonoBehaviour
{
    [SerializeField] private GameObject youWinPanel;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip winSFX;

    public void ShowWinPanel()
    {
        if (youWinPanel == null) return;

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

        GameObject bgmObj = GameObject.Find("SceneBGM");
        if (bgmObj != null)
        {
            AudioSource bgmAudio = bgmObj.GetComponent<AudioSource>();
            if (bgmAudio != null && bgmAudio.isPlaying)
            {
                bgmAudio.Stop();
            }
        }

        if (winSFX != null && Camera.main != null)
        {
            AudioSource.PlayClipAtPoint(winSFX, Camera.main.transform.position);
        }

        youWinPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OnNextButtonClicked()
    {
        Time.timeScale = 1f;
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    public void OnMenuButtonClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}