using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    public Slider _musicSlider, _sfxSlider;

    [SerializeField] Button closeBtn;
    [SerializeField] GameObject pauseMenuCanvas;
    [SerializeField] GameObject settingsMenuCanvas;

    private AudioManager audioManager;

    private void Start()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        if (audioManager == null)
        {
            Debug.LogError("AudioManager not found in the scene.");
        }

        LoadVolumeSettings();
        closeBtn.onClick.AddListener(() => CloseBtn());
    }

    private void OnEnable()
    {
        LoadVolumeSettings();
        closeBtn.onClick.AddListener(() => CloseBtn());
    }

    private void LoadVolumeSettings()
    {
        if (audioManager != null)
        {
            _musicSlider.value = audioManager.musicSource.volume;
            _sfxSlider.value = audioManager.sfxSource.volume;
        }
    }

    private void CloseBtn()
    {
        pauseMenuCanvas.SetActive(true);
        settingsMenuCanvas.SetActive(false);
    }

    public void MusicVolume()
    {
        if (audioManager != null)
        {
            audioManager.musicVolume(_musicSlider.value);
        }
    }

    public void SFXVolume()
    {
        if (audioManager != null)
        {
            audioManager.sfxVolume(_sfxSlider.value);
        }
    }
}
