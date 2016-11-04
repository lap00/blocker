using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour
{
    public GameObject target;
    public float offset;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        transform.position = new Vector3(pos.x, Mathf.Max(3.5f, target.transform.position.y + offset), pos.z);
    }
}
