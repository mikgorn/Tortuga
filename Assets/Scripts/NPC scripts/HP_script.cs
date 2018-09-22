using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HP_script : MonoBehaviour {
    public int hp = 100;
    public int max_hp = 100;
    public int max_cannons = 3;
    public GameObject hp_bar;
    public int hull_type = 1;
    public Sprite hull_sprite_2;
    public bool npc = true;
    private SpriteRenderer hull_sprite;
    private Image hp_bar_body;
    private RectTransform hp_body_rect;
    private RectTransform hp_right_rect;
    private Text hp_label;
    private RectTransform hp_label_rect;
    private int max_width;
    private Spawner_script spawner;
	// Use this for initialization
	void Start () {
        hull_sprite = gameObject.GetComponent<SpriteRenderer>();
        if (hp_bar)
        {
            hp_bar_body = hp_bar.transform.GetChild(2).GetComponent<Image>();
            hp_body_rect = hp_bar.transform.GetChild(2).GetComponent<RectTransform>();
            hp_right_rect = hp_bar.transform.GetChild(3).GetComponent<RectTransform>();
            hp_label = hp_bar.transform.GetChild(4).GetComponent<Text>();
            hp_label_rect = hp_bar.transform.GetChild(4).GetComponent<RectTransform>();
            hp_label.text = hp.ToString();
            
        }
        if (npc)
        {
            spawner = transform.parent.GetComponent<Spawner_script>();
        }
	}
	public void upgrade_hull_type()
    {
        hull_sprite.sprite = hull_sprite_2;
    }
    public void hit(int damage)
    {
        hp -= damage;
        if (hp_bar)
        {
            hp_label.text = hp.ToString();
            hp_bar_body.fillAmount = (float) hp / max_hp;
            Vector3 new_pos = hp_body_rect.position;
            new_pos.x += hp_body_rect.rect.width*hp_bar_body.fillAmount;
            hp_right_rect.position = new_pos;
            new_pos.x -= hp_body_rect.rect.width * hp_bar_body.fillAmount/2f;
            hp_label_rect.position = new_pos;
        }
    }
    public void repair(int repair_hp = 888)
    {
        hp += repair_hp;
        if (hp > max_hp)
        {
            hp = max_hp;
        }
        if (hp_bar)
        {
            hp_label.text = hp.ToString();
        }
    }
    // Update is called once per frame
    void Update () {
        if (hp <= 0)
        {
            Loot_script loot_script = gameObject.GetComponent<Loot_script>();
            if (loot_script)
            {
                //loot_script.drop_items();
            }
            spawner.destroy_ship();
            Destroy(gameObject);
        }
	}
}
