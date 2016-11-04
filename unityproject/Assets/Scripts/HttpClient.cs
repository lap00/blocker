using System;
using System.Collections;
using UnityEngine;

public class HttpClient : MonoBehaviour
{

    public string host = "http://jk.internal:8080";

    public void GetHighscores(Action<HighscoreList> action)
    {
        StartCoroutine(this.getHighscores(action));
    }

    IEnumerator getHighscores(Action<HighscoreList> action)
    {
        WWW www = new WWW(host + "/highscores");

        yield return www;

        HighscoreList result = JsonUtility.FromJson<HighscoreList>(www.text);
        action(result);
    }

    [Serializable]
    public class HighscoreList
    {
        public Highscore[] highscores;
    }

    [Serializable]
    public class Highscore
    {
        public string name;
        public float height;
    }
}