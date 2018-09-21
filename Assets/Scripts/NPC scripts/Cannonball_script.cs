using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball_script : MonoBehaviour {
    public float speed = 10;
    public float lifetime = 1.5f;
    public int damage =10;
    public float acc = 20f;
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

        float x = direction.x;
        float y = direction.y;
        float a = Random.Range(-acc / 2f, acc / 2f)*Mathf.PI/180;
        Vector3 acc_factor = new Vector3(x*Mathf.Cos(a)-y*Mathf.Sin(a),y * Mathf.Cos(a) + x * Mathf.Sin(a), 0);
        direction = acc_factor;
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
