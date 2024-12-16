using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : SpawnableObjectController
{
    [SerializeField] private PowerUpType powerUpType;

    public override void OnCollision()
    {
        if (AudioManager.instance)
        {
            AudioManager.instance.PlayPickupTriggerAudioClip();
        }

        PlayerController.instance.ActivatePowerUp(powerUpType);
        Destroy(gameObject);
    }
}
