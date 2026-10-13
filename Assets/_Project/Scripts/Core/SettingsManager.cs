using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages audio settings sliders, volume preferences persistence, and real-time audio adjustments.
/// </summary>
public class SettingsManager : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void OnEnable()
    {
        LoadSavedSettings();
    }

    private void Start()
    {
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
        float savedMusic = PlayerPrefs.GetFloat(AudioManager.MusicVolumeKey, 100f);
        float savedSFX = PlayerPrefs.GetFloat(AudioManager.SFXVolumeKey, 100f);

        if (musicSlider != null) musicSlider.value = savedMusic;
        if (sfxSlider != null) sfxSlider.value = savedSFX;
    }

    public void SetMusicVolume(float value)
    {
        PlayerPrefs.SetFloat(AudioManager.MusicVolumeKey, value);
        PlayerPrefs.Save();

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetMusicVolume(value);
        }
    }

    public void SetSFXVolume(float value)
    {
        PlayerPrefs.SetFloat(AudioManager.SFXVolumeKey, value);
        PlayerPrefs.Save();

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetSFXVolume(value);
        }
    }
}