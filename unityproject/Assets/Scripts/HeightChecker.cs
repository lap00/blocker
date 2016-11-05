using UnityEngine;
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
    public GameObject nameContainer;
    public GameObject gameOverSplash;
    public string playerName;
    public string guid;
    public int lives;
    public int startLives = 3;
    public GameObject[] hearts;
    public GameObject bar;
    static float maxHeight;

    void Start()
    {
        PrintHeight();
        PrintBlockCount();
        PrintTotalMass();
        updateGameOverScoreText();
        nameContainer = GameObject.Find("NameContainer");
        bar = GameObject.Find("BarSpecial");
        if (nameContainer != null)
        {
            playerName = nameContainer.GetComponent<GlobalStateContainer>().playerName;
        } 
        else
        {
            playerName = "Jimmy";
        }
        
        guid = System.Guid.NewGuid().ToString();
        lives = startLives;
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

        if (height > maxHeight)
        {
            maxHeight = height;
        }
        bar.GetComponentInChildren<TextMesh>().text = playerName + " - " + (height / unitsPerMeter).ToString("n2") + "m";
        bar.transform.position = new Vector3(bar.transform.position.x, maxHeight, bar.transform.position.z);
        bar.SetActive(maxHeight >= 5f);
	}

    void PrintHeight()
    {
        heightText.text = "Height: " + (height / unitsPerMeter).ToString("n2");
    }

    void PrintBlockCount()
    {
        blockCountText.text = "Blocks: " + blockCount.ToString();
    }

    void PrintTotalMass()
    {
        totalMassText.text = "Tonnage: " + totalMass.ToString("n2");
    }

    void updateGameOverScoreText()
    {
        gameOverScoreText.text = "Height: " + (height / unitsPerMeter).ToString("n2") + "m\nTonnage: " + totalMass.ToString("n2") + "\n Blocks: " + blockCount.ToString();
    }

    public void Die()
    {
        lives--;

        for (int i=0; i<hearts.Length; i++)
        {
            hearts[i].GetComponent<SpriteRenderer>().enabled = (lives > i);
        }

        if (lives == 0)
        {
            gameOverSplash.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
