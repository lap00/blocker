using UnityEngine;

public class GlobalStateManipulator : MonoBehaviour
{    
    GlobalStateContainer container;

	public void Start () {
        container = FindObjectOfType<GlobalStateContainer>();
    }

    public string playerName
    {
        set { container.playerName = value; }
    }

    public void loadLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level");
    }
}
