using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_target_script : MonoBehaviour {
    public Transform target = null;
    public bool attack_target = false;
    public bool no_target = true;
    public float cd_aggr = 10;
    public float aggr_radius = 7.5f;
    public float speed = 50f;
    public float angular_speed = 30;
    public NPC_cannon_script[] cannons;
    public Rigidbody2D ship;

    private Vector3 destination;

    public GameObject route;
    private Route_script route_script;
	// Use this for initialization
	void Start () {
        destination = transform.position;
        cannons = gameObject.GetComponentsInChildren<NPC_cannon_script>();
        ship = gameObject.GetComponent<Rigidbody2D>();
        route_script = route.GetComponent<Route_script>();
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != transform.tag && (!collision.isTrigger)&&(no_target))
        {
            attack_target = true;
            target = collision.transform;
            no_target = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag != transform.tag && (!collision.isTrigger) && (no_target))
        {
            attack_target = true;
            target = collision.transform;
            no_target = false;
        }
    }
    private void move_to(Vector3 dest)
    {
        Vector3 distance = dest - transform.position;



        float a = this.transform.eulerAngles.z;
        float x = Mathf.Sin(a / (180f / Mathf.PI));
        float y = -Mathf.Cos(a / (180f / Mathf.PI));

        //


        float target_a = Mathf.Atan2(distance.y, distance.x);
        target_a *= 180 / Mathf.PI;

        float delta_a = (target_a - a - 90)%360;
        if (delta_a > 180)
        {
            delta_a -= 360;
        }
        if (delta_a < -180)
        {
            delta_a += 360;
        }
        if (delta_a > 0)
        {
            transform.Rotate(new Vector3(0, 0, -Time.deltaTime * angular_speed));
        }
        if (delta_a < 0)
        {
            transform.Rotate(new Vector3(0, 0, +Time.deltaTime * angular_speed));
        }
        if (distance.magnitude > 1 )
        { ship.velocity = (new Vector3(x * speed / 100, y * speed / 100, 0)); }
        else
        {
            if (no_target)
            {
                destination = route_script.get_next_point();
            }
        }
    }
    // Update is called once per frame
    void Update () {
        if (!no_target)
        {
            if(attack_target && (target.position - transform.position).magnitude > aggr_radius)
            {
                cd_aggr -= Time.deltaTime;
            }

            destination = target.position;
            

            

            if (attack_target)
            {
                foreach(NPC_cannon_script cannon in cannons)
                {

                    cannon.try_shoot(target.position);
                }
            }
        }
        if (cd_aggr <= 0)
        {
            target = null;
            no_target = true;
        }
        if (no_target)
        {
            attack_target = false;
        }
        move_to(destination);
	}
}
