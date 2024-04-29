using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;
using static SaveLoadManager;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; set; }

    public Button applyBtn;

    public Slider masterSlider;
    public GameObject masterValue;

    public Slider musicSlider;
    public GameObject musicValue;

    public Slider inGameMusicSlider;
    public GameObject inGameMusicValue;

    public Slider sfxSlider;
    public GameObject sfxValue;

    public Slider zombieSlider;
    public GameObject zombieValue;

    [SerializeField] private AudioMixer myMixer;

    float musicVolume;
    float masterVolume;
    float sfxVolume;
    float zombieVolume;
    float inGameMusicVolume;


    private void Start()
    {
        //If the volume has a "masterVolume" within the PlayerPrefs, Load and Apply Saved Values.
        if (PlayerPrefs.HasKey("masterVolume"))
        {
            StartCoroutine(LoadAndApplySettings());
        }
        //If key is not found, set sounds to default values.
        else
        {
            SetMusicVolume();
            SetInGameMusicVolume();
            SetMasterVolume();
            SetSFXVolume();
            SetZombieVolume();
        }
        //Apply settings and SaveVolumeSettings using SaveLoadManager to PlayerPrefs.
        applyBtn.onClick.AddListener(() =>
        {
            SaveLoadManager.Instance.SaveVolumeSettings(musicSlider.value, sfxSlider.value, masterSlider.value, zombieSlider.value, inGameMusicSlider.value);
        });
        
        //StartCoroutine(LoadAndApplySettings());
    }

    private IEnumerator LoadAndApplySettings()
    {
        //Load and Apply Saved Volume Settings
        LoadAndSetVolume();

        // Load GraphicsSettings
        // Load Key Bindings

        yield return new WaitForSeconds(0.1f);
    }

    public void LoadAndSetVolume()
    {
        VolumeSettings volumeSettings = SaveLoadManager.Instance.LoadVolumeSettings();

        masterSlider.value = volumeSettings.master;
        musicSlider.value = volumeSettings.music;
        inGameMusicSlider.value = volumeSettings.inGameMusic;
        sfxSlider.value = volumeSettings.sfx;
        zombieSlider.value = volumeSettings.zombie;

        print("Volume Settings are Loaded");
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    //Set textmeshprougui values for Options Menu UI sliders to slider values
    private void Update()
    {
        masterValue.GetComponent<TextMeshProUGUI>().text = ("" + (masterSlider.value * 100).ToString("F0") + "");
        musicValue.GetComponent<TextMeshProUGUI>().text = ("" + (musicSlider.value * 100).ToString("F0") + "");
        inGameMusicValue.GetComponent<TextMeshProUGUI>().text = ("" + (inGameMusicSlider.value * 100).ToString("F0") + "");
        sfxValue.GetComponent<TextMeshProUGUI>().text = ("" + (sfxSlider.value * 100).ToString("F0") + "");
        zombieValue.GetComponent<TextMeshProUGUI>().text = ("" + (zombieSlider.value * 100).ToString("F0") + "");
    }

    //Set all volumes via mixer making readibility within inspector easier.
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

    public void SetSFXVolume()
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
        inGameMusicVolume = inGameMusicSlider.value;
        myMixer.SetFloat("ingamemusic", Mathf.Log10(inGameMusicVolume) * 20);
    }
}
