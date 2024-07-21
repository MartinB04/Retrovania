using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager sharedInstance;

    [SerializeField] AudioClip coinAudio;
    [SerializeField] AudioClip initialMusic;
    [SerializeField] AudioClip gameplayMusic1;

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

    private void Update()
    {
        //SetTrackMusic();
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

    public void SetTrackMusic(GameState gameState)
    {
        AudioClip newClip = null;
        switch (gameState)
        {
            case GameState.menu:
                newClip = this.initialMusic;
                break;
            case GameState.inGame:
                newClip = this.gameplayMusic1;
                break;
        }

        if(this.audioSourceMusic.clip != newClip)
        {
            this.audioSourceMusic.clip = newClip;
            this.audioSourceMusic.loop = true;
            this.audioSourceMusic.Play();
        }

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
