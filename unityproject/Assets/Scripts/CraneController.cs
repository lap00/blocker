using UnityEngine;
using System.Collections;

public class CraneController : MonoBehaviour
{

    public GameObject[] blockTypes;
    GameObject currentBlock = null;
    public float xRange = 3.0f;
    public float yOffset = 3.0f; // distance from highest point to crane
    public HeightChecker heightChecker;
    LineRenderer lineRenderer;
    DistanceJoint2D distanceJoint;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(SpawnBox(0f));
        distanceJoint = this.GetComponent<DistanceJoint2D>();
        lineRenderer = this.GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, this.transform.position);
        lineRenderer.SetPosition(1, this.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = this.transform.position;

        // move crane horizontally
        float newX = xRange * Mathf.Cos(Time.time * 0.7f);

        // move crane vertically
        float newY = pos.y + 0.01f * (heightChecker.height + yOffset - pos.y);

        this.transform.position = new Vector3(newX, newY, pos.z);
        lineRenderer.SetPosition(0, this.transform.position);
        lineRenderer.SetPosition(1, this.transform.position);

        if (currentBlock != null)
        {
            lineRenderer.SetPosition(1, currentBlock.transform.position);
            if (Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.Space))
            {
                distanceJoint.connectedBody = null;
                currentBlock = null;
                StartCoroutine(SpawnBox(2f));
            }

        }
    }

    IEnumerator SpawnBox(float wait)
    {
        yield return new WaitForSeconds(wait);

        Vector3 pos = this.transform.position;
        GameObject nextBlock = (GameObject)Instantiate(blockTypes[Random.Range(0, blockTypes.Length)], new Vector3(pos.x - 1.0f, pos.y - 2.0f, pos.z), Quaternion.identity);
        lineRenderer.SetPosition(1, nextBlock.transform.position);
        distanceJoint.connectedBody = nextBlock.GetComponent<Rigidbody2D>();
        currentBlock = nextBlock;
    }
}
