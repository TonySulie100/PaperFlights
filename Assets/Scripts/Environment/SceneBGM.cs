using UnityEngine;

/// <summary>
/// Triggers scene-specific background music playback via the global audio manager on start.
/// </summary>
public class SceneBGM : MonoBehaviour
{
    [SerializeField] private AudioClip sceneBGM;

    private void Start()
    {
        if (AudioManager.Instance != null && sceneBGM != null)
        {
            AudioManager.Instance.PlayMusic(sceneBGM);
        }
    }
}