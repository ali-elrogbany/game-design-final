using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableController : SpawnableObjectController
{
    [SerializeField] private float scoreBonus = 10f;

    public override void OnCollision()
    {
        if (GameManager.instance)
        {
            GameManager.instance.IncrementScore(scoreBonus);
        }
        Destroy(gameObject);
    }
}