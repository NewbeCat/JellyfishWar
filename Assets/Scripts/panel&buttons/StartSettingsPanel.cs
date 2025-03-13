using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StartSettingsPanel : MonoBehaviour
{
    public Slider _musicSlider, _sfxSlider;

    [SerializeField] Button closeBtn;
    [SerializeField] GameObject settingsMenuCanvas;

    private void OnEnable()
    {
        LoadVolumeSettings();
        closeBtn.onClick.AddListener(() => CloseBtn());
    }
    private void LoadVolumeSettings()
    {
        _musicSlider.value = AudioManager.Instance.musicSource.volume;
        _sfxSlider.value = AudioManager.Instance.sfxSource.volume;
    }


    private void CloseBtn()
    {
        settingsMenuCanvas.SetActive(false);
    }

    public void MusicVolume()
    {
        AudioManager.Instance.musicVolume(_musicSlider.value);
    }
    public void SFXVolume()
    {
        AudioManager.Instance.musicVolume(_sfxSlider.value);
    }
}
