using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NameContainer : MonoBehaviour {

    public string playerName;

    public Text playerNameText;

    public void Start()
    {
        Object.DontDestroyOnLoad(gameObject);
    }


    public void NameEntered(string playerNameEntered)
    {
        playerName = playerNameText.text;
        SceneManager.LoadScene("Main");
    }
}
