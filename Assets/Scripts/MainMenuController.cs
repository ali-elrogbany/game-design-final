using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        // Replace "GameScene" with the name of your gameplay scene
        SceneManager.LoadScene("EndlessRunner");
    }

    public void OpenSettings()
    {
        // This is a placeholder for settings functionality
        Debug.Log("Settings menu opened!");
    }

    public void QuitGame()
    {
        // Quit the application (works in built versions only)
        Application.Quit();
        Debug.Log("Game Quit");
    }
}