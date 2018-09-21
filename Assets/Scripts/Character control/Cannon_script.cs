using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon_script : MonoBehaviour {
    public int[] damage = {10,15,20 };
    public float[] speed = {5f,6f,8f };
    public float[] acc = {35f,20f,10f };
    public int level = 0;
    public int[] upgrade_cost = {1000,2000 };

    public float cd = 1;
    public float left_cd = 0;
    Inventory_script player_inventory_script;
    public void upgrade()
    {
        player_inventory_script.take_gold(upgrade_cost[level]);
        level++;
    }
    public GameObject cannonball_prefab;
    // Use this for initialization
    void Start () {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player_inventory_script = player.GetComponent<Inventory_script>();
	}
	public void try_shoot()
    {
        if (left_cd <= 0)
        {
            GameObject cannonball = Instantiate(cannonball_prefab);
            cannonball.transform.position = transform.position;
            Cannonball_script cb_script = cannonball.GetComponent<Cannonball_script>();
            Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction.z = 0;
            cb_script.set_direction(direction);
            cb_script.active = true;
            cb_script.owner = gameObject;

            cb_script.damage = damage[level];
            cb_script.speed = speed[level];
            cb_script.acc = acc[level];

            cannonball.tag = transform.parent.tag;
            left_cd = cd;
        }
    }
	// Update is called once per frame
	void Update () {
        if( Input.GetMouseButtonDown(0)){

            try_shoot();
        }
        if (left_cd > 0)
        {
            left_cd -= Time.deltaTime;
        }
	}
}
