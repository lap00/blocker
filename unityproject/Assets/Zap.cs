using UnityEngine;
using System.Collections;

public class Zap : MonoBehaviour {

	public void OnCollisionEnter2D (Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Block"))
        {
            coll.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
