using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenuUI; // Assign the Pause Menu UI GameObject in the Inspector.
    public Button pauseButton;    // Assign the Pause Button in the Inspector.
    public Button continueButton; // Assign the Continue Button in the Inspector.
    public Button restartButton;  // Assign the Restart Button in the Inspector.

    private bool isPaused = false;

    void Start()
    {
        // Assign button click events
        pauseButton.onClick.AddListener(TogglePause); // Link Pause Button to TogglePause
        continueButton.onClick.AddListener(ResumeGame);
        restartButton.onClick.AddListener(RestartGame);

        // Ensure the pause menu is hidden on start
        pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        // Toggle pause with Esc key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (GameManager.instance && GameManager.instance.GetIsGameActive())
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        pauseMenuUI.SetActive(true); // Show pause menu
        Time.timeScale = 0f;        // Freeze game time
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenuUI.SetActive(false); // Hide pause menu
        Time.timeScale = 1f;         // Resume game time
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Reset time scale before restarting
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
        );
    }
}
