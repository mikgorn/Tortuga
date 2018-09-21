using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item  {
    public string name;
    public Sprite image;
    public int amount;
    public int buy_price;
    public int sell_price;
	
    public Item Clone()
    {
        Item clone = new Item();

        clone.name = name;
        clone.image = image;
        clone.amount = amount;

        return clone;
    }
}
