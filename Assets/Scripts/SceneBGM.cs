using UnityEngine;

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