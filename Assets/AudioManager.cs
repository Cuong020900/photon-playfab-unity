using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    // Speaker
    public GameObject speaker;
    public GameObject speakerSFX;

    private Speaker _speakerScript;
    private Speaker _speakerSFXScript;
    // Audio players components.
    public GameObject effectAudioPrefab;
    public AudioSource MusicSource;
    // Random pitch adjustment range.
    public float LowPitchRange = .95f;
    public float HighPitchRange = 1.05f;
    // Singleton instance.
    public static AudioManager Instance = null;
    private float oldEffectVolume = 1f;
    private float oldMusicVolume = 1f;

    public List<AudioSource> _effectsSources = new List<AudioSource>();
    private const int EFFECT_SOURCE_COUNT = 5;
	
    // Initialize the singleton instance.
    private void Awake()
    {
        // If there is not already an instance of SoundManager, set it to this.
        if (Instance == null)
        {
            Instance = this;
        }
        //If an instance already exists, destroy whatever this object is to enforce the singleton.
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad (gameObject);

        _speakerScript = speaker.GetComponent<Speaker>();
        _speakerSFXScript = speakerSFX.GetComponent<Speaker>();
    }

    private void Start()
    {
        
        // INIT effect source
        for (int i = 0; i < EFFECT_SOURCE_COUNT; i++)
        {
            // GameObject effectSource = Instantiate(effectAudioPrefab);
            // effectSource.gameObject.name = "EffectSource" + i;
            // effectSource.SetActive(true);
            
            GameObject effectSource = new GameObject();
            effectSource.name = "EffectSource" + i;
            AudioSource audioSource = effectSource.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.loop = false;
            DontDestroyOnLoad(effectSource);

            AudioSource effectSourceAudio = effectSource.GetComponent<AudioSource>();
            _effectsSources.Add(effectSourceAudio);
        }
        
        // Load sound config
        SoundSettingModel soundConfig = SoundSettingModel.LoadData();
        this.SetEffectVolume(soundConfig.effectVolume);
        this.SetMusicVolume(soundConfig.musicVolume);
    }

    // Play a single clip through the sound effects source.
    public void Play(AudioClip clip)
    {
        // get free sfxSource
        foreach (var source in _effectsSources.Where(source => !source.isPlaying))
        {
            source.clip = clip;
            source.Play();
            return;
        }
    }
    
    // Play a single clip through the music source.
    public void PlayMusic(AudioClip clip)
    {
        MusicSource.clip = clip;
        MusicSource.Play();
        
    }
    // Play a random clip from an array, and randomize the pitch slightly.
	
    public void SetEffectVolume(Single value)
    {
        foreach (var source in _effectsSources)
        {
            source.volume = value;
        }
        _speakerSFXScript.HandleRenderSpeakerButton(value > 0);
        this.SaveAudioConfigLocal();
    }
    
    public void SetMusicVolume(Single value)
    {
        MusicSource.volume = value;
        _speakerScript.HandleRenderSpeakerButton(value > 0);
        this.SaveAudioConfigLocal();
    }

    public void ToggleEffectSound()
    {
        Debug.Log(_effectsSources);
        if (_effectsSources.ElementAt(0).volume > 0)
        {
            oldEffectVolume = _effectsSources.ElementAt(0).volume;
            this.SetEffectVolume(0);
            return;
        }

        this.SetEffectVolume(oldEffectVolume);
    }
    
    public void ToggleMusicSound()
    {
        if (MusicSource.volume > 0)
        {
            oldMusicVolume = MusicSource.volume;
            this.SetMusicVolume(0);
            return;
        }

        this.SetMusicVolume(oldMusicVolume);
    }

    private void SaveAudioConfigLocal()
    {
        foreach (var item in _effectsSources)
        {
            Debug.Log(item.ToString());
        }

        new SoundSettingModel(MusicSource.volume, _effectsSources.ElementAt(0).volume).Save();
    }
}