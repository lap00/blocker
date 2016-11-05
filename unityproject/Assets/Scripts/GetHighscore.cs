using UnityEngine;
using System.Collections;


public class GetHighscore : MonoBehaviour {

	HttpClient client;

	public UnityEngine.UI.Text[] nameTexts;
	public UnityEngine.UI.Text[] scoreTexts;

	// Use this for initialization
	void Start () {
		client = GetComponent<HttpClient>();
		client.GetHighscores(this.LogHighscores);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	private void LogHighscores(HttpClient.HighscoreList list)
	{
		for (int i = 0; i < Mathf.Min(5, list.highscores.Length); i++)
		{
			nameTexts[i].text = list.highscores[i].name;
			scoreTexts[i].text = list.highscores[i].height.ToString();
			
		}
	}
}

