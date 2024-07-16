using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{

    [SerializeField] Slider sliderMusic;
    [SerializeField] Slider sliderSfx;
    // Start is called before the first frame update
    void Start()
    {
        this.sliderMusic.onValueChanged.AddListener(SetMusicVolume);
        this.sliderSfx.onValueChanged.AddListener(SetSfxVolume);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetMusicVolume(float vol)
    {
        AudioManager.sharedInstance.SetMusicVolume(vol);
    }

    private void SetSfxVolume(float vol)
    {
        AudioManager.sharedInstance.SetSfxVolume(vol);
    }
}
