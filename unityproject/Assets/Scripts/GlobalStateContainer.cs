using System.Collections;
using UnityEngine;

[RequireComponent(typeof(HttpClient))]
public class GlobalStateContainer : MonoBehaviour
{
    public static GlobalStateContainer instance { get; private set; }

    HttpClient client;
    string currentToken;
    
    public string playerName;
    public float currentWeight;
    public float currentHeight;
    public int currentCount;

    public void Awake()
    {
        if (instance == null) {
            DontDestroyOnLoad(gameObject);
            instance = this;
        } else {
            Destroy(gameObject);
        }

        client = GetComponent<HttpClient>();
    }
    
    public void Start()
    {
        currentToken = System.Guid.NewGuid().ToString();
    }

    public void OnLevelWasLoaded()
    {
        StartCoroutine(postState());
    }
    
    IEnumerator postState()
    {
        while (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Level") {
            client.PostState(new HttpClient.State(
                playerName,
                currentHeight,
                currentWeight,
                currentCount,
                currentToken
            ));
            
            yield return new WaitForSecondsRealtime(1f);
        }
    }

}
