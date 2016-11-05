using UnityEngine;
using System.Collections;

public class HitGround : MonoBehaviour {

    
    public GameObject heightChecker;
    
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        heightChecker.GetComponent<HeightChecker>().Die();
    }
}
