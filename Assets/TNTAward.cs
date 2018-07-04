using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNTAward : MonoBehaviour {

    public GameObject explosionFx;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == Tags.Ground)
        {
            Destroy(this.gameObject);
            Instantiate(explosionFx, transform.position, transform.rotation);
        }

    }

}
