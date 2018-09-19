using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon_script : MonoBehaviour {
    int cooldown = 1;
    int speed = 10;
    int damage = 10;
    public float distance = 50f;

    public float cd = 1;
    public float left_cd = 0;

    public GameObject cannonball_prefab;
    // Use this for initialization
    void Start () {
	
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
