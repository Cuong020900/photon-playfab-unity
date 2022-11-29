using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
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
    public AudioSource musicSource;

    // Singleton instance.
    public static AudioManager Instance = null;
    private float _oldEffectVolume = 1f;
    private float _oldMusicVolume = 1f;

    public List<AudioSource> effectsSources;
    private const int INIT_EFFECT_SOURCE_COUNT = 5;
    private int _effectSourceCount = INIT_EFFECT_SOURCE_COUNT;
	
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
        for (var i = 0; i < _effectSourceCount; i++)
        {
            _CreateEffectSource("EffectSource" + i);
        }
        
        // Load sound config
        var soundConfig = SoundSettingModel.LoadData();
        this.SetEffectVolume(soundConfig.effectVolume);
        this.SetMusicVolume(soundConfig.musicVolume);
    }

    // Play a single clip through the sound effects source.
    public void Play(AudioClip clip)
    {
        // get free sfxSource
        foreach (var source in effectsSources.Where(source => !source.isPlaying))
        {
            source.clip = clip;
            source.Play();
            return;
        }

        _CreateEffectSource("EffectSource" + _effectSourceCount++);
    }
    
    // Play a single clip through the music source.
    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
        
    }
	
    public void SetEffectVolume(Single value)
    {
        foreach (var source in effectsSources)
        {
            source.volume = value;
        }
        _speakerSFXScript.HandleRenderSpeakerButton(value > 0);
        this.SaveAudioConfigLocal();
    }
    
    public void SetMusicVolume(Single value)
    {
        musicSource.volume = value;
        _speakerScript.HandleRenderSpeakerButton(value > 0);
        this.SaveAudioConfigLocal();
    }

    public void ToggleEffectSound()
    {
        Debug.Log(effectsSources);
        if (effectsSources.ElementAt(0).volume > 0)
        {
            _oldEffectVolume = effectsSources.ElementAt(0).volume;
            this.SetEffectVolume(0);
            return;
        }

        this.SetEffectVolume(_oldEffectVolume);
    }
    
    public void ToggleMusicSound()
    {
        if (musicSource.volume > 0)
        {
            _oldMusicVolume = musicSource.volume;
            this.SetMusicVolume(0);
            return;
        }

        this.SetMusicVolume(_oldMusicVolume);
    }

    private void SaveAudioConfigLocal()
    {
        foreach (var item in effectsSources)
        {
            Debug.Log(item.ToString());
        }

        new SoundSettingModel(musicSource.volume, effectsSources.ElementAt(0).volume).Save();
    }

    private void _CreateEffectSource(string effectSourceName)
    {
        var effectSource = Instantiate(effectAudioPrefab);
        effectSource.gameObject.name = effectSourceName;
        effectSource.SetActive(true);
        DontDestroyOnLoad(effectSource);
            
        var effectSourceAudio = effectSource.GetComponent<AudioSource>();
        effectsSources.Add(effectSourceAudio);
    }
}