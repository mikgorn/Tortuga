using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickup_script : MonoBehaviour {
    public Inventory inventory;
    public GameObject panel;
	// Use this for initialization
	void Start () {
        panel.SetActive(false);
	}
    private void OnTriggerEnter2D(Collider other)
    {
        panel.SetActive(true);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
