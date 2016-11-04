using UnityEngine;
using System.Collections;

public class HeightChecker : MonoBehaviour {

    public float height;
	
	// Update is called once per frame
	void Update () {

        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");

        height = 0;

        foreach (GameObject block in blocks)
        {
            float blockHeight = block.transform.position.y;
            if (blockHeight > height)
            {
                height = blockHeight;
            }
        }
	}
}
