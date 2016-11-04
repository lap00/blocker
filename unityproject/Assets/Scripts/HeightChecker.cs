using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HeightChecker : MonoBehaviour {

    public float height;
    public Text heightText;
    public int blockCount;
    public Text blockCountText;
    public float totalMass;
    public Text totalMassText;

    void Start()
    {
        heightText.text = "asdsad";
    }
	
	// Update is called once per frame
	void Update () {

        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");

        height = 0;
        blockCount = 0;
        totalMass = 0;

        foreach (GameObject block in blocks)
        {
            float blockHeight = block.transform.position.y;
            if (blockHeight > height)
            {
                height = blockHeight;
            }

            heightText.text = "Height: " + height.ToString() + " m";

            blockCount++;

            blockCountText.text = "Block count: " + blockCount.ToString();

            totalMass += block.GetComponent<Rigidbody2D>().mass;

            totalMassText.text = "Total mass: " + totalMass.ToString();
        }
	}
}
