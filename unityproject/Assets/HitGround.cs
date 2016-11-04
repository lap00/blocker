using UnityEngine;
using System.Collections;

public class HitGround : MonoBehaviour {

    public GameObject gameOverSplash;

	// Use this for initialization
	void Start () {
        gameOverSplash.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        gameOverSplash.SetActive(true);
        Time.timeScale = 0f;
    }
}
