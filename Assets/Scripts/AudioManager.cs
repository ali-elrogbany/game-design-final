using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
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
}
