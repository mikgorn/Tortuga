using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot_script : MonoBehaviour {
    public Inventory inventory;
    public GameObject lootbox_prefab;
    // Use this for initialization
    void Start () {
		
	}
	public void drop_items()
    {
        GameObject lootbox =Instantiate(lootbox_prefab);
        lootbox.transform.position = transform.position;
    }
	// Update is called once per frame
	void Update () {
		
	}
}
