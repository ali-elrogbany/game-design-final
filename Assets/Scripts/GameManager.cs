using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI References")]
    public GameObject gameOverScreen;  // Reference to the Game Over UI panel
    public TMP_Text currentScoreText;      // Reference to the Text showing current score
    public TMP_Text highScoreText;   

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

        //Calulate score
        int finalScore = GetScore();
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (finalScore > highScore)
        {
            highScore = finalScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }

        // Display Game Over UI
        gameOverScreen.SetActive(true);
        currentScoreText.text = "Score: " + finalScore;
        highScoreText.text = "High Score: " + highScore;

        Debug.Log($"Final Score: {finalScore}, High Score: {highScore}");

        // Debug.Log($"Final Score: {GetScore()}");

    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Resume time
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
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
