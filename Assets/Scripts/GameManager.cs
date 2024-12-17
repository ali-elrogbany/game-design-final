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
    private int activeDoubleScorePowerUps = 0;

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

            if (UIManager.instance)
                UIManager.instance.UpdateScoreText(score);
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

        if (FloorMovement.instance)
        {
            FloorMovement.instance.StopMovement();
        }

        if (PlayerController.instance)
        {
            PlayerController.instance.OnGameOver();
        }

        SpawnableObjectController[] spawnableObjects = FindObjectsOfType<SpawnableObjectController>();
        foreach (var obj in spawnableObjects)
        {
            obj.DisableCanMove();
        }

        Debug.Log($"Final Score: {GetScore()}");
    }

    public int GetScore()
    {
        return Mathf.FloorToInt(score);
    }

    public void IncrementScore(float score)
    {
        if (score < 0)
            return;
        this.score += score;

        if (UIManager.instance)
            UIManager.instance.UpdateScoreText(score);
    }

    public void ActivateDoubleScore()
    {
        activeDoubleScorePowerUps++;
        UpdateScoreMultiplier();

        StartCoroutine(HandleDoubleScore());
    }

    private IEnumerator HandleDoubleScore()
    {
        yield return new WaitForSeconds(10f);

        activeDoubleScorePowerUps--;
        UpdateScoreMultiplier();
    }

    private void UpdateScoreMultiplier()
    {
        if (activeDoubleScorePowerUps > 0)
        {
            scoreMultiplier = 2f * (scoreMultiplier / Mathf.Max(1, activeDoubleScorePowerUps));
        }
        else
        {
            scoreMultiplier = 1f;
        }
    }
}
