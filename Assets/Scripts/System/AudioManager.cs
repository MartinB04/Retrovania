using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager sharedInstance;

    //private AudioSource audioSource;
    [SerializeField] AudioClip coinAudio;
    [SerializeField] AudioClip backgroundAudio;

    [SerializeField] AudioSource backgroundMusic;
    [SerializeField] AudioSource audioClips;

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

    public void PlayCoin()
    {
        this.audioClips.PlayOneShot(this.coinAudio);
    }

    public void SetMusicVolume(float vol)
    {
        this.backgroundMusic.volume = vol;
    }

    public void SetSfxVolume(float vol)
    {
        this.audioClips.volume = vol;
    }
}
