using UnityEngine;
using System.Collections;

public class MarkAsFallen : MonoBehaviour {

    public float weight;    

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (gameObject.tag != "Block")
        {
            float mass = this.GetComponent<Rigidbody2D>().mass;
            if (mass > 1.5f)
            {
                Follow.shake = 0.02f * mass;
            }            
            gameObject.tag = "Block";
        }
    }
}
