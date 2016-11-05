using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HttpClient : MonoBehaviour
{
    public string host = "http://178.62.206.116:8080";

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

    public void PostState(State state)
    {
        StartCoroutine(postStateCoroutine(state));
    }

    public void GetNemesis(float height, Action<State> action)
    {
        StartCoroutine(getNemesisCoroutine(height, action));
    }


    public void GetPersonalBest(string name, Action<State> action)
    {
        StartCoroutine(getPersonalBestCoroutine(name, action));
    }

    IEnumerator getPersonalBestCoroutine(string name, Action<State> action)
    {
        WWW www = new WWW(host + "/highscores/" + name);

        yield return www;

        State result = JsonUtility.FromJson<State>(www.text);
        action(result);
    }

    IEnumerator getNemesisCoroutine(float height, Action<State> action)
    {
        WWW www = new WWW(host + "/highscores/nemesis/" + height.ToString("n2"));

        yield return www;

        State result = JsonUtility.FromJson<State>(www.text);
        action(result);
    }

    IEnumerator postStateCoroutine(State score)
    {
        WWW www = new WWW(
            host + "/states",
            System.Text.Encoding.ASCII.GetBytes(JsonUtility.ToJson(score)),
            headers
        );

        yield return www;
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
        public State[] highscores;
    }

    [Serializable]
    public class State
    {
        public string name;
        public float height;
        public float weight;
        public int count;
        public string token;

        public State(string name, float height, float weight, int count, string token = null)
        {
            this.name = name;
            this.height = height;
            this.weight = weight;
            this.count = count;
            this.token = token;
        }
    }
}
