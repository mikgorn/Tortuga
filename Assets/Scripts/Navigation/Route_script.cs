using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route_script : MonoBehaviour {
    public int n;
	// Use this for initialization
	void Start () {
        n = transform.childCount;
    }
	public Vector3 get_next_point()
    {
        int k = (int) Random.Range(0,n-0.01f);
        return transform.GetChild(k).position;
    }
	// Update is called once per frame
	void Update () {
		
	}
}
