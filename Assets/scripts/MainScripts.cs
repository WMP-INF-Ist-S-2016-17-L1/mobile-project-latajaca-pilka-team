using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Klasa zawierajaca główne funkcje gry
public class MainScripts : MonoBehaviour
{

	// Metoda zmieniająca poziom, na podstawie jego nazwy
    public void ChangeLevel(string sceneName)
    {
        Application.LoadLevel(sceneName);
        BallControlScript.hideUI();
    }

	// Metoda przenosząca nas do menu
    public void ToMainMenu()
    {
        Application.LoadLevel("MainMenu");
    }

	// Metoda zatrzymania gry
    public void PauseGame()
    {
        Time.timeScale = 0;
    }

	// Metoda wznowienia gry
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

	// Metoda wyjścia z gry
    public void QuitGame()
    {
        Application.Quit();
    }
}