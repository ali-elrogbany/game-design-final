using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Settings")]
    [SerializeField] private float scoreIncrementRate = 10f;
    [SerializeField] private float scoreMultiplier = 1f;

    [Header("Local Variables")]
    private bool isGameActive = true;
    private float score = 0f;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    void Update()
    {
        if (isGameActive)
        {
            score += scoreIncrementRate * scoreMultiplier * Time.deltaTime;
        }
    }

    public void OnGameOver()
    {
        Debug.Log("Game Over");

        isGameActive = false;

        if (Spawner.instance)
        {
            Spawner.instance.StopSpawining();
        }

        Debug.Log($"Final Score: {GetScore()}");
    }

    public int GetScore()
    {
        return Mathf.FloorToInt(score);
    }
}
