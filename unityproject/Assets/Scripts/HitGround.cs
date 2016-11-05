using UnityEngine;

public class HitGround : MonoBehaviour {
    
    public GameObject heightChecker;
    public float waterY = -1f;
    public GameObject splashPrefab;
    
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (splashPrefab != null) {
            Vector3 pos = coll.transform.position;
            pos.y = waterY;

            GameObject splash = Instantiate(splashPrefab);
            splash.transform.position = pos;
        }

        heightChecker.GetComponent<HeightChecker>().Die();
    }
}
