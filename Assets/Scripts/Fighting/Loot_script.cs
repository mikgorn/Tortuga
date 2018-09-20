using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot_script : MonoBehaviour {
    public Inventory inventory_asset;
    private Inventory inventory;
    public GameObject lootbox_prefab;
    public GameObject player;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        inventory = new Inventory();
        foreach (Item item in inventory_asset.items)
        {
            inventory.add_item(item);
        }
    }
	public void drop_items()
    {
        GameObject lootbox =Instantiate(lootbox_prefab);
        lootbox.transform.position = transform.position;
        Pickup_script lootbox_pickup = lootbox.GetComponent<Pickup_script>();
        lootbox_pickup.inventory = new Inventory();
        foreach(Item item in inventory.items)
        {
            lootbox_pickup.inventory.add_item(item);
        }
        lootbox_pickup.player = player;
    }
	// Update is called once per frame
	void Update () {
		
	}
}
