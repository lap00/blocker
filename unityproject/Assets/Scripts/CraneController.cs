using UnityEngine;
using System.Collections;

public class CraneController : MonoBehaviour {

    public GameObject[] blockTypes;
    GameObject currentBlock = null;

	// Use this for initialization
	void Start () {
        StartCoroutine(SpawnBox());
        this.GetComponent<LineRenderer>().SetPosition(0, this.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.GetComponent<DistanceJoint2D>().connectedBody = null;
            this.GetComponent<LineRenderer>().SetPosition(1, this.transform.position);
            StartCoroutine(SpawnBox());
        }
        if (currentBlock != null)
        {
            this.GetComponent<LineRenderer>().SetPosition(1, currentBlock.transform.position);
        }
    }

    IEnumerator SpawnBox() {
        yield return new WaitForSeconds(2.0f);

        Vector3 pos = this.transform.position;
        GameObject nextBlock = (GameObject)Instantiate(blockTypes[0], new Vector3(pos.x - 1.0f, pos.y - 2.0f, pos.z), Quaternion.identity);
        this.GetComponent<LineRenderer>().SetPosition(1, nextBlock.transform.position);
        nextBlock.GetComponent<Rigidbody2D>().mass = Random.Range(1.0f, 5.0f);
        this.GetComponent<DistanceJoint2D>().connectedBody = nextBlock.GetComponent<Rigidbody2D>();
        currentBlock = nextBlock;
    }
}
