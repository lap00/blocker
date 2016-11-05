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
    public GameObject gameOverSplash;
    public int lives;
    public int startLives = 3;
    public GameObject[] hearts;

    GameObject bar;
    
    GameObject barNemesis;
    bool nemesisWaiting = false;
    float nemesisHeight = -1;

    GlobalStateContainer stateContainer;

    void Start()
    {
        PrintHeight();
        PrintBlockCount();
        PrintTotalMass();
        updateGameOverScoreText();
        stateContainer = GameObject.Find("StateKeeper").GetComponent<GlobalStateContainer>();
        bar = GameObject.Find("BarSpecial");
        barNemesis = GameObject.Find("BarNemesis");

        lives = startLives;
        gameOverSplash.SetActive(false);

        bar.SetActive(false);
        barNemesis.SetActive(false);
        GetComponent<HttpClient>().GetPersonalBest(stateContainer.playerName, RecieveUpdateMax);
    }
	
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

        // nemesis bar
        if (!nemesisWaiting && height / unitsPerMeter > nemesisHeight)
        {
            nemesisWaiting = true;
            GetComponent<HttpClient>().GetNemesis(height / unitsPerMeter, RecieveUpdateNemesis);
        }

        stateContainer.currentHeight = height / unitsPerMeter;
        stateContainer.currentCount = blockCount;
        stateContainer.currentWeight = totalMass;
    }

    void RecieveUpdateNemesis(HttpClient.State state)
    {
        nemesisHeight = state.height;// * unitsPerMeter;
        SetBar(barNemesis, state);
        nemesisWaiting = false;
        Debug.Log("Nemesis[" + state.name + ":" + state.height + "]");
    }

    void RecieveUpdateMax(HttpClient.State state)
    {
        SetBar(bar, state);
        Debug.Log("Max[" + state.name + ":" + state.height + "]");
    }

    void SetBar(GameObject bar, HttpClient.State state)
    {
        bar.GetComponentInChildren<TextMesh>().text = "" + state.name + " [" + (state.height).ToString("n2") + "m]"; ;
        bar.transform.position = new Vector3(bar.transform.position.x, state.height * unitsPerMeter, bar.transform.position.z);
        bar.SetActive(true);
    }

    void PrintHeight()
    {
        heightText.text = (height / unitsPerMeter).ToString("n2");
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
