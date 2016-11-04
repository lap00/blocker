using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HttpClient : MonoBehaviour
{
    public string host = "http://jk.internal:8080";

    Dictionary<string, string> headers;

    public void Awake()
    {
        headers = new Dictionary<string, string>();
        headers["Content-Type"] = "application/json";
    }

    public void GetHighscores(Action<HighscoreList> action)
    {
        StartCoroutine(getHighscores(action));
    }

    public void PostHighscore(Highscore score)
    {
        StartCoroutine(postHighscore(score));        
    }

    IEnumerator postHighscore(Highscore score)
    {
        WWW www = new WWW(
            host + "/highscores",
            System.Text.Encoding.ASCII.GetBytes(JsonUtility.ToJson(score)),
            headers
        );

        yield return www;

        Debug.Log(www.text);
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

        public Highscore(string name, float height)
        {
            this.name = name;
            this.height = height;
        }
    }
}