using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundControlManager : MonoBehaviour
{
    /// <summary>
    /// REMOVE THIS SCRIPT
    /// </summary>

    [SerializeField]
    private AudioMixer myMixer;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider zombieSlider;
    [SerializeField] Slider activeGameMusicSlider;

    float musicVolume;
    float masterVolume;
    float sfxVolume;
    float zombieVolume;
    float inGameMusicVolume;

    void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume") || PlayerPrefs.HasKey("masterVolume") || PlayerPrefs.HasKey("sfxVolume") || PlayerPrefs.HasKey("zombieVolume") || PlayerPrefs.HasKey("inGameMusicVolume"))
        {
            SettingsManager.Instance.LoadAndSetVolume();
        }
        else
        {
            SetMusicVolume();
            SetInGameMusicVolume();
            SetMasterVolume();
            SetEffectsVolume();
            SetZombieVolume();
        }
    }

    public void SetMusicVolume()
    {
        musicVolume = musicSlider.value;
        myMixer.SetFloat("music", Mathf.Log10(musicVolume) * 20);
    }

    public void SetMasterVolume()
    {
        masterVolume = masterSlider.value;
        myMixer.SetFloat("master", Mathf.Log10(masterVolume) * 20);
    }

    public void SetEffectsVolume()
    {
        sfxVolume = sfxSlider.value;
        myMixer.SetFloat("sfx", Mathf.Log10(sfxVolume) * 20);
    }

    public void SetZombieVolume()
    {
        zombieVolume = zombieSlider.value;
        myMixer.SetFloat("zombie", Mathf.Log10(zombieVolume) * 20);
    }

    public void SetInGameMusicVolume()
    {
        inGameMusicVolume = activeGameMusicSlider.value;
        myMixer.SetFloat("ingamemusic", Mathf.Log10(inGameMusicVolume) * 20);
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.SetFloat("masterVolume", masterVolume);
        PlayerPrefs.SetFloat("sfxVolume", sfxVolume);
        PlayerPrefs.SetFloat("zombieVolume", zombieVolume);
        PlayerPrefs.SetFloat("inGameMusicVolume", inGameMusicVolume);
    }

    public void ChangeVolume()
    {
        AudioListener.volume = musicSlider.value;
    }
}
