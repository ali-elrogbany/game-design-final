using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip obstacleTriggerAudioClip;
    [SerializeField] private AudioClip collectableTriggerAudioClip;
    [SerializeField] private AudioClip pickupTriggerAudioClip;

    [Header("Sliders")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
    }

    private void Start()
    {
        musicSlider.value = musicAudioSource.volume;
        sfxSlider.value = sfxAudioSource.volume;
    }

    public void PlayObstacleTriggerAudioClip()
    {
        sfxAudioSource.PlayOneShot(obstacleTriggerAudioClip);
    }

    public void PlayCollectableTriggerAudioClip()
    {
        sfxAudioSource.PlayOneShot(collectableTriggerAudioClip);
    }

    public void PlayPickupTriggerAudioClip()
    {
        sfxAudioSource.PlayOneShot(pickupTriggerAudioClip);
    }

    public void SetMusicVolume()
    {
        musicAudioSource.volume = musicSlider.value;
    }

    public void SetSFXVolume()
    {
        sfxAudioSource.volume = sfxSlider.value;
    }
}
