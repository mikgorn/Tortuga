using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : ScriptableObject {
    public List<Item> items = new List<Item>();
    
    public void add_item(Item item)
    {
        int i = items.FindIndex(x => x.name==item.name);
        if (i >= 0)
        {
            items[i].amount += item.amount;
        }
        else
        {
            items.Add(item);
        }
    }

    public void remove_item(Item item)
    {
        int i = items.FindIndex(x => x.name == item.name);
       
            items[i].amount -= item.amount;
        if (items[i].amount <= 0)
        {
            items.RemoveAt(i);
        }
    }
}
