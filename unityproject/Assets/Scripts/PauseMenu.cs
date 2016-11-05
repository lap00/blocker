using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	public string quit;

	public bool isPaused;

	public GameObject pausedMenuCanvas;

	// Update is called once per frame
	void Update () {

		if (isPaused) {
			pausedMenuCanvas.SetActive (true);
		} else {
			pausedMenuCanvas.SetActive (false);
		}

		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			isPaused = !isPaused;
		}
	}

	public void Menu ()
	{
		isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0f;

        }
        else
        {
            Time.timeScale = 1f;
        }
    }
	public void Resume ()
	{
		isPaused = false;
        Time.timeScale = 1f;
	}

	public void Restart ()
	{
        Time.timeScale = 1f;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
	
}
