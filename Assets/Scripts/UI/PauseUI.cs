using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] Slider sliderMusic;
    [SerializeField] Slider sliderSfx;

    void Start()
    {
        LoadVolumeSettings();
        this.sliderMusic.onValueChanged.AddListener(SetMusicVolume);
        this.sliderSfx.onValueChanged.AddListener(SetSfxVolume);
    }

    private void SetMusicVolume(float vol)
    {
        AudioManager.sharedInstance.SetMusicVolume(vol);
    }

    private void SetSfxVolume(float vol)
    {
        AudioManager.sharedInstance.SetSfxVolume(vol);
    }

    private void LoadVolumeSettings()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            float musicVolume = PlayerPrefs.GetFloat("MusicVolume");
            this.sliderMusic.value = musicVolume;
        }

        if (PlayerPrefs.HasKey("SfxVolume"))
        {
            float sfxVolume = PlayerPrefs.GetFloat("SfxVolume");
            this.sliderSfx.value = sfxVolume;
        }
    }
}
