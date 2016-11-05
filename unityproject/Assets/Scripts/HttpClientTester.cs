using UnityEngine;

[RequireComponent(typeof(HttpClient))]
public class HttpClientTester : MonoBehaviour {

    public string token = System.Guid.NewGuid().ToString();
    public string player = "Jakob";
    public float height = 40f;
    public float weight = 30f;
    public int count = 20;

    HttpClient client;
    
    public void Start()
    {
        client = GetComponent<HttpClient>();
    }

    public void OnGUI()
    {
        if (GUI.Button(new Rect(50, 50, 200, 20), "Get highscores")) {
            client.GetHighscores(this.LogHighscores);
        }

        if (GUI.Button(new Rect(50, 100, 200, 20), "Get nemesis")) {
            client.GetNemesis(height, this.LogNemesis);
        }

        if (GUI.Button(new Rect(50, 150, 200, 20), "Post current state")) {
            client.PostState(new HttpClient.State(player, height, weight, count, token));
        }
        
        if (GUI.Button(new Rect(50, 200, 200, 20), "Get personal best")) {
            client.GetPersonalBest(player, this.LogPersonalBest);
        }
    }

    private void LogHighscores(HttpClient.HighscoreList list)
    {
        foreach (HttpClient.State s in list.highscores)
            Debug.Log(s.name + ": " + s.height);
    }

    private void LogNemesis(HttpClient.State nemesis)
    {
        Debug.Log(nemesis.name + ": " + nemesis.height);
    }
    
    private void LogPersonalBest(HttpClient.State best)
    {
        Debug.Log(best.name + ": " + best.height);
    }
}
