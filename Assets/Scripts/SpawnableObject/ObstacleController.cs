using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : SpawnableObjectController
{
    public override void OnCollision()
    {
        if (GameManager.instance)
        {
            GameManager.instance.OnGameOver();
        }
    }
}
