using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_cannon_script : MonoBehaviour {
    public GameObject cannonball_prefab;
    public float cd = 2;
    public float left_cd = 0;

    
	// Use this for initialization
	void Start () {
		
	}
    public void try_shoot(Vector3 destination)
    {
        if (left_cd <= 0)
        {
            GameObject cannonball = Instantiate(cannonball_prefab);
            cannonball.transform.position = transform.position;
            Cannonball_script cb_script = cannonball.GetComponent<Cannonball_script>();
            Vector3 direction = destination;
            cb_script.set_direction(direction);
            cb_script.active = true;
            cb_script.owner = gameObject;
            cannonball.tag = transform.parent.tag;
            left_cd = cd;
            Debug.Log("shooting");
        }
    }
    // Update is called once per frame
    void Update () {
        if (left_cd > 0)
        {
            left_cd -= Time.deltaTime;
        }
	}
}
