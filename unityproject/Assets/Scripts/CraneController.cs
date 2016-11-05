using UnityEngine;
using System.Collections;

public class CraneController : MonoBehaviour
{

    public GameObject[] blockTypes;
    float blockWeight;
    GameObject currentBlock = null;
    public float xRange = 3.0f;
    public float yOffset = 3.0f; // distance from highest point to crane
    public HeightChecker heightChecker;
    LineRenderer lineRenderer;
    DistanceJoint2D distanceJoint;

    Vector3 ropeTarget;
    float ropeFactor = 0f;
    public float ropeSpeed = 4.0f;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(SpawnBox(0f));
        distanceJoint = this.GetComponent<DistanceJoint2D>();
        lineRenderer = this.GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, this.transform.position);
        lineRenderer.SetPosition(1, this.transform.position);
        blockWeight = 0f;
        for(int i=0; i<blockTypes.Length; i++)
        {
            blockWeight += blockTypes[i].GetComponent<MarkAsFallen>().weight;
        }
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

        if (currentBlock != null)
        {
            ropeFactor = Mathf.Min(1.0f, ropeFactor + ropeSpeed * Time.deltaTime);
            ropeTarget = currentBlock.transform.position;
            if ((Input.GetMouseButtonDown(0) && Input.mousePosition.normalized.y < 0.8f) || Input.GetKeyDown(KeyCode.Space))
            {
                distanceJoint.connectedBody = null;
                currentBlock = null;
                StartCoroutine(SpawnBox(2f));
            }
        } 
        else
        {
            ropeFactor = Mathf.Max(0.0f, ropeFactor - ropeSpeed * Time.deltaTime);
        }
        
        lineRenderer.SetPosition(1, Vector3.Lerp(pos, ropeTarget, ropeFactor));
    }

    IEnumerator SpawnBox(float wait)
    {
        yield return new WaitForSeconds(wait);

        float rand = Random.Range(0.0f, blockWeight);
        int index = 0;
        while(index < blockTypes.Length - 1 && rand > blockTypes[index].GetComponent<MarkAsFallen>().weight)
        {
            rand -= blockTypes[index++].GetComponent<MarkAsFallen>().weight;
        }


        Vector3 pos = this.transform.position;
        float spawnAngle = 4.7f + Random.Range(-0.5f, 0.5f);
        float distance = 2.0f;
        Vector3 startPos = new Vector3(pos.x + distance * Mathf.Cos(spawnAngle), pos.y + distance * Mathf.Sin(spawnAngle), pos.z);
        GameObject nextBlock = (GameObject)Instantiate(blockTypes[index], startPos, Quaternion.identity);
        distanceJoint.connectedBody = nextBlock.GetComponent<Rigidbody2D>();
        currentBlock = nextBlock;
    }
}
