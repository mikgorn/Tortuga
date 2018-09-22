using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Town_script : MonoBehaviour {
    public Inventory inventory;
    public GameObject panel_prefab;
    private GameObject panel;

    public GameObject inventory_prefab;
    public GameObject repair_prefab;
    public GameObject item_prefab;
    public GameObject upgrade_prefab;
    public GameObject cannon_upgrade_prefab;
    public GameObject add_cannon_button_prefab;
    public GameObject hull_prefab;

    public Sprite[] cannon_images;

    public GameObject cannon_prefab;

    private GameObject inventory_panel;
    private GameObject repair_panel;
    private GameObject upgrade_panel;


    private Button trade_button;
    private Button repair_button;
    private Button upgrade_button;

    private GameObject player;
    private Inventory player_inventory;
    private Inventory_script player_inventory_script;
    private HP_script player_hp_script;

	// Use this for initialization
	void Start () {
        panel = Instantiate(panel_prefab);
        panel.transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform,false);
        panel.SetActive(false);
        panel.transform.GetChild(0).GetComponent<Text>().text = gameObject.name;

        trade_button = panel.transform.GetChild(1).GetComponent<Button>();
        trade_button.onClick.AddListener(open_trade);

        repair_button = panel.transform.GetChild(2).GetComponent<Button>();
        repair_button.onClick.AddListener(open_repair);

        upgrade_button = panel.transform.GetChild(3).GetComponent<Button>();
        upgrade_button.onClick.AddListener(open_upgrade);

        player = GameObject.FindGameObjectWithTag("Player");
        player_inventory_script = player.GetComponent<Inventory_script>();
        player_hp_script = player.GetComponent<HP_script>();
        player_inventory = player_inventory_script.inventory;
	}
    private void open_upgrade()
    {
        if (upgrade_panel)
        {
            Destroy(upgrade_panel);
        }
        upgrade_panel = Instantiate(upgrade_prefab);
        upgrade_panel.transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform, false);

        Cannon_script[] cannons = player.GetComponentsInChildren<Cannon_script>();
        int shift = 0;
        int i = 0;
        foreach(Cannon_script cannon in cannons)
        {

            i++;
            GameObject cannon_gameobject = Instantiate(cannon_upgrade_prefab);
            cannon_gameobject.transform.SetParent(upgrade_panel.transform, false);
            cannon_gameobject.transform.position += new Vector3(0,-shift,0);

            Image cannon_image = cannon_gameobject.transform.GetChild(0).GetComponent<Image>();
            cannon_image.sprite = cannon_images[cannon.level];

            cannon_gameobject.transform.GetChild(1).GetComponent<Text>().text = "Cannon " + i;
            cannon_gameobject.transform.GetChild(2).GetComponent<Text>().text = cannon.damage[cannon.level].ToString();
            cannon_gameobject.transform.GetChild(3).GetComponent<Text>().text = cannon.speed[cannon.level].ToString();
            cannon_gameobject.transform.GetChild(4).GetComponent<Text>().text = cannon.acc[cannon.level].ToString();

            if (cannon.level < 2)
            {
                cannon_gameobject.transform.GetChild(5).GetComponent<Button>().onClick.AddListener(cannon.upgrade);
                cannon_gameobject.transform.GetChild(5).GetComponent<Button>().onClick.AddListener(open_upgrade);
                cannon_gameobject.transform.GetChild(5).GetChild(0).GetComponent<Text>().text = "Upgrade ("+cannon.upgrade_cost[cannon.level].ToString()+")";
                if (player_inventory_script.gold < cannon.upgrade_cost[cannon.level])
                {
                    cannon_gameobject.transform.GetChild(5).GetComponent<Button>().enabled = false;
                }
            }
            else
            {
                cannon_gameobject.transform.GetChild(5).gameObject.SetActive(false);
            }
            shift += 35;
        }
        shift += 20;
        if (i < player_hp_script.max_cannons)
        {
            GameObject add_cannon_button = Instantiate(add_cannon_button_prefab);
            add_cannon_button.transform.SetParent(upgrade_panel.transform,false);
            add_cannon_button.transform.position += new Vector3(0, -shift, 0);

            add_cannon_button.GetComponent<Button>().onClick.AddListener(add_cannon);
            add_cannon_button.GetComponent<Button>().onClick.AddListener(open_upgrade);
            if (player_inventory_script.gold < 5000)
            {
                add_cannon_button.GetComponent<Button>().enabled = false;
            }
            
        }
    }
    private void add_cannon()
    {
        GameObject cannon = Instantiate(cannon_prefab);
        cannon.transform.SetParent(player.transform, false);
        player_inventory_script.take_gold(5000);
    }
    private void upgrade_hull_type()
    {
        player_hp_script.upgrade_hull_type();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        player_inventory = player_inventory_script.inventory;
        if (collision.gameObject.tag == "Player")
        {
            panel.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            panel.SetActive(false);
        }
    }
    private void open_repair()
    {
        if (repair_panel)
        {
            Destroy(repair_panel);
        }
        repair_panel = Instantiate(repair_prefab);
        repair_panel.transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform, false);

        Text repair_text = repair_panel.transform.GetChild(1).GetComponent<Text>();
        Button repair_button = repair_panel.transform.GetChild(2).GetComponent<Button>();

        int repair_cost = player_hp_script.max_hp - player_hp_script.hp;

        if (player_inventory_script.gold >= repair_cost)
        {
            repair_text.text = "Repair will cost " + repair_cost + " gold. Would you like to repair now?";
            repair_button.onClick.AddListener(repair_ship);
        }
        else
        {
            repair_text.text = "Repair cost " + repair_cost + "gold. You have not enough money!";
            repair_button.onClick.AddListener(close_repair_tab);
        }

    }
    private void repair_ship()
    {
        player_hp_script.repair();
        Destroy(repair_panel);
    }
    private void close_repair_tab()
    {
        Destroy(repair_panel);
    }
    private void open_trade()
    {
        int shift = 0;
        if (inventory_panel)
        {
            Destroy(inventory_panel);

        }
        inventory_panel = Instantiate(inventory_prefab);
        inventory_panel.transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform, false);

        foreach (Item item in inventory.items)
        {
            GameObject item_gameobject = Instantiate(item_prefab);
            item_gameobject.transform.SetParent(inventory_panel.transform.GetChild(0).GetChild(0).GetChild(0), false);
            item_gameobject.transform.position += new Vector3(0, shift, 0);
            item_gameobject.transform.GetChild(0).GetComponent<Text>().text = item.name;
            
            int i = player_inventory.items.FindIndex(x=>x.name == item.name);
            int k = 0;

            if (i >= 0)
            {
                k = player_inventory.items[i].amount;
            }
            item_gameobject.transform.GetChild(1).GetComponent<Text>().text =k.ToString();

            Item duplicate = item.Clone();
            duplicate.amount = 1;
            Item_trade_script item_trade_script = item_gameobject.GetComponent<Item_trade_script>();
            item_trade_script.item = duplicate;
            item_trade_script.buy_price = item.buy_price;
            item_trade_script.sell_price = item.sell_price;
            item_trade_script.player_inventory = player_inventory_script;
            item_trade_script.amount_label = item_gameobject.transform.GetChild(1).GetComponent<Text>();
            item_trade_script.n = k;


            Button buy_button = item_gameobject.transform.GetChild(3).GetComponent<Button>();
            item_gameobject.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = "Buy (" + item.buy_price + ")";
            Button sell_button = item_gameobject.transform.GetChild(4).GetComponent<Button>();
            item_gameobject.transform.GetChild(4).GetChild(0).GetComponent<Text>().text = "Sell (" + item.sell_price + ")";

            buy_button.onClick.AddListener(item_trade_script.buy);
            sell_button.onClick.AddListener(item_trade_script.sell);

            item_gameobject.transform.GetChild(2).GetComponent<Image>().sprite = item.image;

            
            shift -= 35;
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
