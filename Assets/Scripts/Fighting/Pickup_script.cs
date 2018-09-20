using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickup_script : MonoBehaviour {
    public Inventory inventory;
    public GameObject panel_prefab;
    public GameObject panel;
    public Canvas canvas;
    private Button button;
    public GameObject player;
	// Use this for initialization
	void Start () {
        if (inventory == null)
        {
            inventory = new Inventory();
        }
        canvas = GameObject.FindObjectOfType<Canvas>();
        panel = Instantiate(panel_prefab);
        panel.transform.SetParent(canvas.transform,false);
        panel.SetActive(false);
        button = panel.GetComponentInChildren<Button>();
        button.onClick.AddListener(loot_items);
	}
    private void OnTriggerEnter2D(Collider2D other)
    {
        panel.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        panel.SetActive(false);
    }

    private void loot_items()
    {
        Inventory_script player_inventory_script = player.GetComponent<Inventory_script>();
        foreach(Item item in inventory.items)
        {
            player_inventory_script.inventory.add_item(item);
        }
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
