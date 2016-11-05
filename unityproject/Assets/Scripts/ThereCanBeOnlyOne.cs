using UnityEngine;

public class ThereCanBeOnlyOne : MonoBehaviour
{
    static ThereCanBeOnlyOne leader = null;
    
    public void Awake()
    {
        if (leader == null) {
            DontDestroyOnLoad(gameObject);
            leader = this;
        } else {
            Destroy(gameObject);
        }
    }
}
