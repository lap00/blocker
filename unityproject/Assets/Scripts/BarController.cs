using UnityEngine;
using System.Collections;

public class BarController : MonoBehaviour {

    GameObject[] bars;
    public GameObject barType;

	// Use this for initialization
	void Start () {
        float unitsPerMeter = 0.2f;
        float metersPerBar = 20.0f;
        bars = new GameObject[10];
        for (int i=0; i<bars.Length; i++)
        {
            float height = (i + 1) * metersPerBar * unitsPerMeter;
            bars[i] = (GameObject)Instantiate(barType, new Vector3(0f, height, 0), Quaternion.identity, this.transform);
            bars[i].GetComponentInChildren<TextMesh>().text = "" + ((int)((i + 1) * metersPerBar)) + "m";
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
