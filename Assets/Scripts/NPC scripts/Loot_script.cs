using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot_script : MonoBehaviour {
    public Inventory inventory_asset;
    private Inventory inventory;
    public GameObject lootbox_prefab;
    public GameObject player;
    public int gold;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        inventory = ScriptableObject.CreateInstance<Inventory>();
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
        lootbox_pickup.inventory = ScriptableObject.CreateInstance<Inventory>();
        lootbox_pickup.gold = gold;
        foreach(Item item in inventory.items)
        {
            float received = Random.Range(0f, 1f);
            if (received <= item.drop_chance)
            {
                if (item.amount > 1)
                {
                    item.amount = (int)( item.amount / 2 + Random.Range(0, item.amount/2));

                    lootbox_pickup.inventory.add_item(item);
                }
                else
                {
                    lootbox_pickup.inventory.add_item(item);
                }
            }
        }
        lootbox_pickup.player = player;
    }
	// Update is called once per frame
	void Update () {
		
	}
}
