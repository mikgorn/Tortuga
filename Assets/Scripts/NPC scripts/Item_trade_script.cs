using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item_trade_script : MonoBehaviour {
    public Item item;
    public int buy_price;
    public int sell_price;
    public Inventory_script player_inventory;
    public Text amount_label;
    public int n;
	// Use this for initialization
	void Start () {
		
	}
	public void buy()
    {
        if (player_inventory.gold > buy_price)
        {
            player_inventory.inventory.add_item(item);
            player_inventory.take_gold(buy_price);
            n++;
            amount_label.text = n.ToString();
        }
    }
    public void sell()
    {
        int i = player_inventory.inventory.items.FindIndex(x => x.name == item.name);
        if (i>0)
        {
            player_inventory.inventory.remove_item(item);
            player_inventory.add_gold(sell_price);
            n--;
            amount_label.text = n.ToString();
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
