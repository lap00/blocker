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
    public float unitsPerMeter;
    public Text gameOverScoreText;

    void Start()
    {
        PrintHeight();
        PrintBlockCount();
        PrintTotalMass();
        updateGameOverScoreText();
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

            PrintHeight();

            blockCount++;

            PrintBlockCount();

            totalMass += block.GetComponent<Rigidbody2D>().mass;

            PrintTotalMass();

            updateGameOverScoreText();
        }
	}

    void PrintHeight()
    {
        heightText.text = "Height: " + (height / unitsPerMeter).ToString("n2");
    }

    void PrintBlockCount()
    {
        blockCountText.text = "Block count: " + blockCount.ToString();
    }

    void PrintTotalMass()
    {
        totalMassText.text = "Total mass: " + totalMass.ToString("n2");
    }

    void updateGameOverScoreText()
    {
        gameOverScoreText.text = "Height: " + (height / unitsPerMeter).ToString("n2") + "m \n Block count: " + blockCount.ToString() + " Total mass: " + totalMass.ToString("n2");
    }
}
