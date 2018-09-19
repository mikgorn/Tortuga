using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_script : MonoBehaviour {
    public int hp = 100;
    public int max_hp = 100;
	// Use this for initialization
	void Start () {
		
	}
	
    public void hit(int damage)
    {
        hp -= damage;
    }
    public void repair(int repair_hp = 888)
    {
        hp += repair_hp;
        if (hp > max_hp)
        {
            hp = max_hp;
        }
    }
    // Update is called once per frame
    void Update () {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
	}
}
