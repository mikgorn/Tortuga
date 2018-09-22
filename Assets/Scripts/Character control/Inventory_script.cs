using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_script : MonoBehaviour {
    public Inventory inventory;
    public bool show_inventory = false;
    public GameObject panel_prefab;
    public GameObject item_prefab;
    public Text gold_label;
    public int gold = 0;
    GameObject inventory_panel;
	// Use this for initialization
	void Start () {
        gold_label.text = "Gold: " + gold;
        if (inventory == null)
        {
            inventory = ScriptableObject.CreateInstance<Inventory>();
        }
    }

    public void update_panel()
    {
        int shift = 0;
        if (inventory_panel)
        {
            Destroy(inventory_panel);

        }
        inventory_panel = Instantiate(panel_prefab);
        inventory_panel.transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform,false);

        foreach(Item item in inventory.items)
        {
            GameObject item_gameobject = Instantiate(item_prefab);
            item_gameobject.transform.SetParent(inventory_panel.transform.GetChild(0).GetChild(0).GetChild(0), false);
            item_gameobject.transform.position +=new Vector3(0,shift,0);
            item_gameobject.transform.GetChild(0).GetComponent<Text>().text = item.name;
            item_gameobject.transform.GetChild(1).GetComponent<Text>().text = item.amount.ToString();
            item_gameobject.transform.GetChild(2).GetComponent<Image>().sprite = item.image;

            item_gameobject.transform.GetChild(3).gameObject.SetActive(false);
            item_gameobject.transform.GetChild(4).gameObject.SetActive(false);
            shift -= 35;
        }
    }

    public void add_gold(int amount)
    {
        gold += amount;
        gold_label.text = "Gold: " + gold;
    }
    public void take_gold(int amount)
    {
        gold -= amount;
        gold_label.text = "Gold: " + gold;
    }
    // Update is called once per frame
    void Update () {
        if(Input.GetKeyDown(KeyCode.I)){
            show_inventory = !show_inventory;
            if (show_inventory)
            {
                update_panel();
            }
            else
            {
                Destroy(inventory_panel);
            }
        }
	}
}
