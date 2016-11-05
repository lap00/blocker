using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    
    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("NameScene");
    }

    public void HighScores()
    {
        SceneManager.LoadScene("Highscore");
    }

    public void Settings()
    {
        print("Settings");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
