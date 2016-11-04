using UnityEngine;
using System.Collections;

public class CraneController : MonoBehaviour {

    public GameObject[] blockTypes;
    GameObject currentBlock = null;
    public float xRange = 3.0f;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(SpawnBox(0f));
        this.GetComponent<LineRenderer>().SetPosition(0, this.transform.position);
        this.GetComponent<LineRenderer>().SetPosition(1, this.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = this.transform.position;
        this.transform.position = new Vector3(xRange * Mathf.Cos(Time.realtimeSinceStartup * 0.7f), pos.y, pos.z);
        this.GetComponent<LineRenderer>().SetPosition(0, this.transform.position);
        this.GetComponent<LineRenderer>().SetPosition(1, this.transform.position);

        if (currentBlock != null)
        {
			if (Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.Space))
            {
                this.GetComponent<LineRenderer>().SetPosition(1, this.transform.position);
                this.GetComponent<DistanceJoint2D>().connectedBody = null;
                currentBlock = null;
                StartCoroutine(SpawnBox(2f));
            }
            this.GetComponent<LineRenderer>().SetPosition(1, currentBlock.transform.position);
        }
    }

    IEnumerator SpawnBox(float wait)
    {
        yield return new WaitForSeconds(wait);

        Vector3 pos = this.transform.position;
        GameObject nextBlock = (GameObject)Instantiate(blockTypes[Random.Range(0, blockTypes.Length)], new Vector3(pos.x - 1.0f, pos.y - 2.0f, pos.z), Quaternion.identity);
        this.GetComponent<LineRenderer>().SetPosition(1, nextBlock.transform.position);
        this.GetComponent<DistanceJoint2D>().connectedBody = nextBlock.GetComponent<Rigidbody2D>();
        currentBlock = nextBlock;
    }
}
