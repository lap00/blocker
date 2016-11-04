using UnityEngine;
using System.Collections;

[RequireComponent(typeof(HttpClient))]
public class HttpClientTester : MonoBehaviour {

    HttpClient client;

    public void Start()
    {
        this.client = GetComponent<HttpClient>();
    }

    public void OnGUI()
    {
        if (GUI.Button(new Rect(50, 50, 100, 20), "Get highscores")) {
            client.GetHighscores(this.LogHighscores);
        }
        
        if (GUI.Button(new Rect(50, 100, 100, 20), "Post highscore")) {
            client.PostHighscore(new HttpClient.Highscore("Jakob", 1000.5f));
        }
    }

    private void LogHighscores(HttpClient.HighscoreList list)
    {
        foreach (HttpClient.Highscore s in list.highscores)
            Debug.Log(s.name + ": " + s.height);
    }
}
