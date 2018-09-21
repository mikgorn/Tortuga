using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Close_panel : MonoBehaviour {
    Inventory_script player_inventory;
	// Use this for initialization
	void Start () {
		Button exit_button = gameObject.GetComponent<Button>();
        exit_button.onClick.AddListener(close_panel);
        player_inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory_script>();
	}
	private void close_panel()
    {
        player_inventory.show_inventory = false;
        Destroy(transform.parent.gameObject);
        
    }
	// Update is called once per frame
	void Update () {
		
	}
}
