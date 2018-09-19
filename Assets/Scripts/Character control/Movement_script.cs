using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement_script : MonoBehaviour {
    public Button forward_button,left_button,right_button;
    public int speed = 10;
    public int angular_speed = 30;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis("Vertical") > 0 )
        {
            float a = this.transform.eulerAngles.z;
            float x = Mathf.Sin(a/(180f/Mathf.PI));
            float y = -Mathf.Cos(a  / (180f / Mathf.PI));

            this.gameObject.GetComponent<Rigidbody2D>().velocity = (new Vector3(x * speed/100, y * speed/100, 0));
        }
        if (Input.GetAxis("Horizontal")>0)
        {
            this.transform.Rotate(new Vector3(0,0,-Time.deltaTime*angular_speed));
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            this.transform.Rotate(new Vector3(0, 0, +Time.deltaTime * angular_speed));
        }
    }
}
