using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball_script : MonoBehaviour {
    public int speed = 10;
    public float lifetime = 2f;
    public int damage =10;
    public Vector3 direction;
    public bool active = false;
    public GameObject owner;
	// Use this for initialization
	void Start () {
		
	}
	
    public void set_direction(Vector3 new_destination)
    {
        direction = new_destination-transform.position;
        direction = direction / direction.magnitude;
    }

    private bool is_enemy(string other_tag)
    {
        if(gameObject.tag!= other_tag)
        {
            return true;
        }
        return false;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag !=tag&&(!collider.isTrigger))
        {
            HP_script enemy_hp = collider.gameObject.GetComponent<HP_script>();
            enemy_hp.hit(10);
            Destroy(this.gameObject);
        }
    }
    // Update is called once per frame
    void Update () {
        if (active)
        {
            if (lifetime > 0f)
            {
                transform.position += direction*speed*Time.deltaTime;
                lifetime -= Time.deltaTime;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
	}
}
