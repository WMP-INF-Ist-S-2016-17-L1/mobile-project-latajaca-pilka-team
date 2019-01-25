using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScripts : MonoBehaviour
{

    public void ChangeLevel(string sceneName)
    {
        Application.LoadLevel(sceneName);
        BallControlScript.hideUI();
    }

    public void ToMainMenu()
    {
        Application.LoadLevel("MainMenu");
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}