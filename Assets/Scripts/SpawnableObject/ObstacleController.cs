using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : SpawnableObjectController
{
    public override void OnCollision()
    {
        if (PlayerController.instance && !PlayerController.instance.GetIsInvisible())
        {
            if (AudioManager.instance)
            {
                AudioManager.instance.PlayObstacleTriggerAudioClip();
            }

            if (GameManager.instance)
            {
                GameManager.instance.OnGameOver();
            }
        }
    }
}
