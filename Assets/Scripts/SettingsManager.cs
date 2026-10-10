using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void OnEnable()
    {
        // Refresh slider positions whenever this panel is activated
        LoadSavedSettings();
    }

    private void Start()
    {
        // Bind slider change listeners
        if (musicSlider != null)
        {
            musicSlider.onValueChanged.AddListener(SetMusicVolume);
        }

        if (sfxSlider != null)
        {
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        }
    }

    public void LoadSavedSettings()
    {
        float savedMusic = PlayerPrefs.GetFloat("MusicVolume", 100f);
        float savedSFX = PlayerPrefs.GetFloat("SFXVolume", 100f);

        if (musicSlider != null)
        {
            musicSlider.value = savedMusic;
        }

        if (sfxSlider != null)
        {
            sfxSlider.value = savedSFX;
        }
    }

    public void SetMusicVolume(float value)
    {
        PlayerPrefs.SetFloat("MusicVolume", value);
        PlayerPrefs.Save();

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetMusicVolume(value);
        }
    }

    public void SetSFXVolume(float value)
    {
        PlayerPrefs.SetFloat("SFXVolume", value);
        PlayerPrefs.Save();

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetSFXVolume(value);
        }
    }
}