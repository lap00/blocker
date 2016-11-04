using UnityEngine;
using System.Collections;

public class Melt : MonoBehaviour {

    public float meltProperty;
    public float inActiveThreshold = 0.05F;

    void Start ()
    {
        meltProperty = 0.02F;
    }
    
	// Update is called once per frame
	void Update () {

        Vector2 scale = transform.localScale;

        Rigidbody2D rigidbody = gameObject.GetComponent<Rigidbody2D>();

        float mass = rigidbody.mass;

        float deltaTime = Time.deltaTime;

        float meltFactor = deltaTime * meltProperty;
        
        scale.Scale(new Vector2(1 - meltFactor, 1 - meltFactor));
        mass *= (1 - meltFactor);

        print("mass: " + mass);
        if (scale.x < inActiveThreshold)
        {
            gameObject.SetActive(false);
        }

        transform.localScale = scale;
        rigidbody.mass = mass;
	}
}
