using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static Weapon;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; set; }

    [Header("Audio Source")]
    public AudioSource shootingChannel;
    public AudioSource emptyMagChannel;
    public AudioSource reloadMagChannel;
    public AudioSource musicSource;
    public AudioSource inGameMusicChannel;
    public AudioSource throwablesChannel;
    public AudioSource zombieChannel;
    public AudioSource playerChannel;
    public AudioSource sfxChannel;

    [Header("Audio Clip")]
    public AudioClip aK47Shot;
    public AudioClip m1911Shot;
    public AudioClip m4_8Shot;
    public AudioClip uziShot;
    public AudioClip uziReloadSound;
    public AudioClip m4_8ReloadSound;
    public AudioClip m1911ReloadSound;
    public AudioClip m4_8EmptyMagSound;
    public AudioClip m1911EmptyMagSound;
    public AudioClip heGrenadeSound;
    public AudioClip inGameMusic;
    public AudioClip menuMusic;
    public AudioClip zombieWalkSound;
    public AudioClip zombieAttackSound;
    public AudioClip zombieDeathSound;
    public AudioClip zombieHurtSound;
    public AudioClip zombieChaseSound;
    public AudioClip playerHurtSound;
    public AudioClip playerDeathSound;
    public AudioClip gameOverMusic;

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

    public void PlaySFX(AudioClip clip)
    {
        sfxChannel.PlayOneShot(clip);
    }
    public void PlayInGameMusic(AudioClip clip)
    {
        inGameMusicChannel.PlayOneShot(clip);
    }
    public void PlayMenuMusic(AudioClip clip)
    {
        musicSource.PlayOneShot(clip);
    }

    public void PlayShootingSound(WeaponType weapon)
    {
        switch(weapon)
        {
            case WeaponType.Pistol1911:
                shootingChannel.PlayOneShot(m1911Shot); 
                break;
            case WeaponType.AK_47:
                shootingChannel.PlayOneShot(aK47Shot);
                break;
            case WeaponType.M4_8:
                shootingChannel.PlayOneShot(m4_8Shot);
                break;
            case WeaponType.Uzi:
                shootingChannel.PlayOneShot(uziShot);
                break;
        }
    }

    public void PlayReloadSound(WeaponType weapon)
    {
        switch (weapon)
        {
            case WeaponType.Uzi:
                reloadMagChannel.PlayOneShot(uziReloadSound); 
                break;
            case WeaponType.AK_47:
                reloadMagChannel.PlayOneShot(m4_8ReloadSound);
                break;
            case WeaponType.M4_8:
                reloadMagChannel.PlayOneShot(m4_8ReloadSound);
                break;
            case WeaponType.Pistol1911:
                reloadMagChannel.PlayOneShot(m1911ReloadSound);
                break;
        }
    }

    public void PlayEmptyMagSound(WeaponType weapon)
    {
        switch (weapon)
        {
            case WeaponType.Pistol1911:
                shootingChannel.PlayOneShot(m1911EmptyMagSound);
                break;
            case WeaponType.AK_47:
                shootingChannel.PlayOneShot(m4_8EmptyMagSound);
                break;
            case WeaponType.M4_8:
                shootingChannel.PlayOneShot(m4_8EmptyMagSound);
                break;
            case WeaponType.Uzi:
                shootingChannel.PlayOneShot(m1911EmptyMagSound);
                break;
        }
    }
}
