using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAndDirection : MonoBehaviour {
    GameObject tnt;
    GameObject player;
    private int tntIndex = 0;
  
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        tnt= GameObject.FindGameObjectWithTag("TNT");
	}
	
	// Update is called once per frame
	void Update ()
    {

        float temp = Vector3.Distance(player.transform.position, transform.position);
        if (Input.GetKeyDown(KeyCode.E) && temp<10)
        {
            tnt.transform.position = transform.TransformPoint(0, 0,0);
            tnt.transform.parent = transform;
            tnt.GetComponent<Rigidbody>().isKinematic = true;
        }
            if(Input.GetKeyDown(KeyCode.Q))
        {
            if (tnt.transform.parent == this.transform)
            {
                tnt.GetComponent<BoxCollider>().isTrigger = true;
                tnt.GetComponent<Rigidbody>().isKinematic = false;
                transform.DetachChildren();
                Vector3 direction = transform.TransformDirection(0, 3, 5);
                tnt.GetComponent<Rigidbody>().AddForce(direction, ForceMode.Impulse);
            }
        }
	}
}
