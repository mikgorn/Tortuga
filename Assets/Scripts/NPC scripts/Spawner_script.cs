using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_script : MonoBehaviour {
    public GameObject route;
    public GameObject ship_prefab;
    public int max_ships;
    private int ships = 0;
    public float cd = 10;
    private float left_time = 0;

	// Use this for initialization
	void Start () {
		
	}
	public void destroy_ship()
    {
        ships -= 1;
    }
	// Update is called once per frame
	void Update () {
        if (left_time > 0)
        {
            left_time -= Time.deltaTime;
        }
        if ((ships < max_ships)&&(left_time<=0))
        {
            GameObject ship = Instantiate(ship_prefab);
            ship.transform.SetParent(gameObject.transform);
            ship.GetComponent<NPC_target_script>().route = route;
            ship.transform.position = transform.position;
            ships += 1;
            left_time = cd;
        }
	}
}
