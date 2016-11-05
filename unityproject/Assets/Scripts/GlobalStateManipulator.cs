using UnityEngine;

public class GlobalStateManipulator : MonoBehaviour
{   
    public string playerName
    {
        set { GlobalStateContainer.instance.playerName = value; }
    }

    public void loadLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level");
    }
}
