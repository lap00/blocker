using UnityEngine;

public class GlobalStateContainer : MonoBehaviour
{
    public static GlobalStateContainer instance { get; private set; }
    
    public void Awake()
    {
        if (instance == null) {
            DontDestroyOnLoad(gameObject);
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public string playerName;
}
