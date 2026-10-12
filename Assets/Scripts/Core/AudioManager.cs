using UnityEngine;

/// <summary>
/// Persistent singleton manager handling global background music, sound effects volume, and UI audio feedback.
/// </summary>
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("SFX Clips")]
    [SerializeField] private AudioClip hoverSound;
    [SerializeField] private AudioClip clickSound;

    public const string MusicVolumeKey = "MusicVolume";
    public const string SFXVolumeKey = "SFXVolume";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        float musicVol = PlayerPrefs.GetFloat(MusicVolumeKey, 100f);
        float sfxVol = PlayerPrefs.GetFloat(SFXVolumeKey, 100f);

        SetMusicVolume(musicVol);
        SetSFXVolume(sfxVol);
    }

    public void PlayMusic(AudioClip newClip)
    {
        if (newClip == null || musicSource == null) return;
        if (musicSource.clip == newClip && musicSource.isPlaying) return;

        musicSource.clip = newClip;
        musicSource.loop = true;
        musicSource.Play();
    }

    // Centralized audio stop for Win/Game Over screens
    public void StopAllAudio()
    {
        if (musicSource != null && musicSource.isPlaying)
        {
            musicSource.Stop();
        }

        if (sfxSource != null && sfxSource.isPlaying)
        {
            sfxSource.Stop();
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
    }

    public void PlayHoverSFX()
    {
        if (sfxSource != null && hoverSound != null)
        {
            sfxSource.PlayOneShot(hoverSound);
        }
    }

    public void PlayClickSFX()
    {
        if (sfxSource != null && clickSound != null)
        {
            sfxSource.PlayOneShot(clickSound);
        }
    }

    public void SetMusicVolume(float value)
    {
        if (musicSource != null)
        {
            musicSource.volume = Mathf.Clamp01(value / 100f);
        }
    }

    public void SetSFXVolume(float value)
    {
        if (sfxSource != null)
        {
            sfxSource.volume = Mathf.Clamp01(value / 100f);
        }
    }
}