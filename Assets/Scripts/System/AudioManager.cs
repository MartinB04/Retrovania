using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager sharedInstance;

    private AudioSource audioSource;
    [SerializeField] AudioClip coinAudio;

    private void Awake()
    {

        if (sharedInstance != null && sharedInstance != this)
        {
            Destroy(gameObject);
            return;
        }

        sharedInstance = this;
        DontDestroyOnLoad(gameObject);

        this.audioSource = GetComponent<AudioSource>();

    }
    // Start is called before the first frame update
    void Start()
    {
        if (this.audioSource == null)
            Debug.LogWarning("AudioManager - AudioSource Nulo");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayCoin()
    {
        this.audioSource.PlayOneShot(this.coinAudio);
    }
}
