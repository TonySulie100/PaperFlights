using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("SFX Clips")]
    [SerializeField] private AudioClip hoverSound;
    [SerializeField] private AudioClip clickSound;

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
            return;
        }
    }

    private void Start()
    {
        // Apply saved volume settings
        float musicVol = PlayerPrefs.GetFloat("MusicVolume", 100f) / 100f;
        float sfxVol = PlayerPrefs.GetFloat("SFXVolume", 100f) / 100f;

        SetMusicVolume(musicVol * 100f);
        SetSFXVolume(sfxVol * 100f);
    }

    public void PlayMusic(AudioClip newClip)
    {
        if (newClip == null || musicSource == null) return;

        // If the requested track is already playing, let it keep playing
        if (musicSource.clip == newClip && musicSource.isPlaying) return;

        musicSource.clip = newClip;
        musicSource.loop = true;
        musicSource.Play();
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
            musicSource.volume = value / 100f;
        }
    }

    public void SetSFXVolume(float value)
    {
        if (sfxSource != null)
        {
            sfxSource.volume = value / 100f;
        }
    }
}