using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Back : MonoBehaviour {

	public void BackButton()
	{
		SceneManager.LoadScene("MainMenu");
	}
}
