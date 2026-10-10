using UnityEngine;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour
{
    [SerializeField] private GameObject youWinPanel;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip winSFX;

    public void ShowWinPanel()
    {
        if (youWinPanel != null)
        {
            // 1. Stop BGM on AudioManager
            AudioManager audioManager = FindFirstObjectByType<AudioManager>();
            if (audioManager != null)
            {
                AudioSource[] sources = audioManager.GetComponents<AudioSource>();
                foreach (AudioSource source in sources)
                {
                    if (source.isPlaying)
                    {
                        source.Stop();
                    }
                }
            }

            // Also check for a standalone SceneBGM object just in case
            GameObject bgmObj = GameObject.Find("SceneBGM");
            if (bgmObj != null)
            {
                AudioSource bgmAudio = bgmObj.GetComponent<AudioSource>();
                if (bgmAudio != null && bgmAudio.isPlaying)
                {
                    bgmAudio.Stop();
                }
            }

            // 2. Play the Win SFX
            if (winSFX != null)
            {
                AudioSource.PlayClipAtPoint(winSFX, Camera.main.transform.position);
            }

            // 3. Show panel and pause game action
            youWinPanel.SetActive(true);
            Time.timeScale = 0f;
        }
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
        SceneManager.LoadScene("Main Menu Scene");
    }
}