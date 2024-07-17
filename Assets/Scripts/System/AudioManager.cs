using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager sharedInstance;

    [SerializeField] AudioClip coinAudio;
    [SerializeField] AudioClip backgroundAudio;

    [SerializeField] AudioSource audioSourceMusic;
    [SerializeField] AudioSource audioSourceSfx;

    private void Awake()
    {
        if (sharedInstance != null && sharedInstance != this)
        {
            Destroy(gameObject);
            return;
        }
        sharedInstance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        LoadVolumeSettings();
    }

    public void PlayCoin()
    {
        this.audioSourceSfx.PlayOneShot(this.coinAudio);
    }

    public void SetMusicVolume(float vol)
    {
        this.audioSourceMusic.volume = vol;
        PlayerPrefs.SetFloat("MusicVolume", vol);
    }

    public void SetSfxVolume(float vol)
    {
        this.audioSourceSfx.volume = vol;
        PlayerPrefs.SetFloat("SfxVolume", vol);
    }

    private void LoadVolumeSettings()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            float musicVolume = PlayerPrefs.GetFloat("MusicVolume");
            this.audioSourceMusic.volume = musicVolume;
        }

        if (PlayerPrefs.HasKey("SfxVolume"))
        {
            float sfxVolume = PlayerPrefs.GetFloat("SfxVolume");
            this.audioSourceSfx.volume = sfxVolume;
        }
    }
}
