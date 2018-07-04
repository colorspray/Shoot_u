using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour {
    public enum Rotate { up, right , forward}
    public Rotate rot;
    public float  speed;
    
	// Use this for initialization
	void Awake () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (rot == Rotate.up)
        {
            transform.Rotate(Vector3.up * speed * Time.deltaTime);
        }
        if (rot == Rotate.right)
        {
            transform.Rotate(Vector3.right * speed * Time.deltaTime);
        }
        if (rot == Rotate.forward)
        {
            transform.Rotate(Vector3.forward * speed * Time.deltaTime);
        }
	}
}
