using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Reference to the instruction page (UI Panel)
    public GameObject instructionPage;

    public GameObject StartButton;
    public GameObject InstructButton;
    public GameObject QuitButton;

    public void StartGame()
    {
        // Replace "EndlessRunner" with the name of your gameplay scene
        SceneManager.LoadScene("EndlessRunner");
    }

    public void OpenSettings()
    {
        // This is a placeholder for settings functionality
        Debug.Log("Settings menu opened!");
    }

    // Open Instructions (Show instruction page)
    public void OpenInstructions()
    {
        // Show the instruction page
        instructionPage.SetActive(true);

        // Hide other buttons
        StartButton.SetActive(false);
        InstructButton.SetActive(false);
        QuitButton.SetActive(false);

        // Log to the console
        Debug.Log("Instructions page opened!");
    }

    // Close Instructions (Hide instruction page)
    public void CloseInstructions()
    {
        // Hide the instruction page
        instructionPage.SetActive(false);

        // Show other buttons
        StartButton.SetActive(true);
        InstructButton.SetActive(true);
        QuitButton.SetActive(true);

        // Log to the console
        Debug.Log("Instructions page closed!");
    }

    public void QuitGame()
    {
        // Quit the application (works in built versions only)
        Application.Quit();
        Debug.Log("Game Quit");
    }
}
