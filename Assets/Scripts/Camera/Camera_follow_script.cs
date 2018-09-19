using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_follow_script : MonoBehaviour {
    public GameObject character;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var x = character.transform.position;
        x.z = -10;
        transform.position = x;
	}
}
