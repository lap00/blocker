using UnityEngine;
using System.Collections;

public class MarkAsFallen : MonoBehaviour {

    

    void OnCollisionEnter2D(Collision2D coll)
    {
        gameObject.tag = "Block";
    }
}
