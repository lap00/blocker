using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class HeightChecker : MonoBehaviour {

    public float height;
    public Text heightText;
    public int blockCount;
    public Text blockCountText;
    public float totalMass;
    public Text totalMassText;
    public float unitsPerMeter;
    public Text gameOverScoreText;
    public GameObject nameContainer;
    public GameObject gameOverSplash;
    public string playerName;
    public string guid;
    public int lives;

    void Start()
    {
        PrintHeight();
        PrintBlockCount();
        PrintTotalMass();
        updateGameOverScoreText();
        nameContainer = GameObject.Find("NameContainer");
        if (nameContainer != null)
        {
            playerName = nameContainer.GetComponent<NameContainer>().playerName;
        } 
        else
        {
            playerName = "Jimmy";
        }
        
        guid = System.Guid.NewGuid().ToString();
        lives = 3;
        gameOverSplash.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
        
        height = 0;
        blockCount = 0;
        totalMass = 0;
        
        foreach (GameObject block in blocks)
        {
            BoxCollider2D collider = block.GetComponent<BoxCollider2D>();
            if (collider != null && collider.bounds.max.y > height)
            {
                height = collider.bounds.max.y;
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

    public void Die()
    {
        lives--;

        if (lives == 0)
        {
            gameOverSplash.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
