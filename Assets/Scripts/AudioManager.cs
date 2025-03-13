using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioClip[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

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
        }
    }

    private void Start()
    {
        LoadVolumeSettings();
    }

    private void OnEnable()
    {
        LoadVolumeSettings();
    }

    private void OnDisable()
    {
        SaveVolumeSettings();
    }

    public void PlayMusic(string name)
    {
        AudioClip s = Array.Find(musicSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Music Not Found");
        }
        else
        {
            musicSource.clip = s;
            musicSource.Play();
        }
    }

    public void StopMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }

    public void PlaySFX(string name)
    {
        AudioClip s = Array.Find(sfxSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("SFX Not Found");
        }
        else
        {
            sfxSource.clip = s;
            sfxSource.PlayOneShot(s);
        }
    }

    public void StopSFX()
    {
        if (sfxSource.isPlaying)
        {
            sfxSource.Stop();
        }
    }


    private void SaveVolumeSettings()
    {
        soundval.musicVolume = musicSource.volume;
        soundval.sfxVolume = sfxSource.volume;
    }
    private void LoadVolumeSettings()
    {
        musicSource.volume = soundval.musicVolume;
        sfxSource.volume = soundval.sfxVolume;
    }

    public void musicVolume(float volume)
    {
        musicSource.volume = volume;
        SaveVolumeSettings();
    }
    public void sfxVolume(float volume)
    {
        sfxSource.volume = volume;
        SaveVolumeSettings();
    }
}
