using System;
using System.Collections;
using System.Collections.Generic;
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
    public AudioSource EffectsSource;
    public AudioSource MusicSource;
    // Random pitch adjustment range.
    public float LowPitchRange = .95f;
    public float HighPitchRange = 1.05f;
    // Singleton instance.
    public static AudioManager Instance = null;
    private float oldEffectVolume = 1f;
    private float oldMusicVolume = 1f;
	
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
        // Load sound config
        SoundSettingModel soundConfig = SoundSettingModel.LoadData();
        this.SetEffectVolume(soundConfig.effectVolume);
        this.SetMusicVolume(soundConfig.musicVolume);
    }
    // Play a single clip through the sound effects source.
    public void Play(AudioClip clip)
    {
        // TODO: change to create new obj to play audio (for supporting multiple audio in one time)
        EffectsSource.clip = clip;
        EffectsSource.Play();
    }
    // Play a single clip through the music source.
    public void PlayMusic(AudioClip clip)
    {
        MusicSource.clip = clip;
        MusicSource.Play();
        
    }
    // Play a random clip from an array, and randomize the pitch slightly.
    public void RandomSoundEffect(params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(LowPitchRange, HighPitchRange);
        EffectsSource.pitch = randomPitch;
        EffectsSource.clip = clips[randomIndex];
        EffectsSource.Play();
    }
	
    public void SetEffectVolume(Single value)
    {
        EffectsSource.volume = value;
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
        if (EffectsSource.volume > 0)
        {
            oldEffectVolume = EffectsSource.volume;
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
        new SoundSettingModel(MusicSource.volume, EffectsSource.volume).Save();
    }
}