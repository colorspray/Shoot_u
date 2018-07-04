using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNTtransform : MonoBehaviour {
   
    public float r_Speed;
    public GameObject explosionFx;
    public AnimationCurve transformCurveY;

    private float outputY;
    private float startTime = float.NaN;
    
    private Vector3 startPos = Vector3.zero;
    private Vector3 lastPos = Vector3.zero;
    private Vector3 initForward = Vector3.zero;
   
	// Use this for initialization
	void Awake () {
        outputY = 0;
        initForward = transform.forward;
        startTime = Time.realtimeSinceStartup;
        startPos = transform.position;
        lastPos = transform.position;
    }

	
	// Update is called once per frame
	void Update () {
      
        float delta = Time.realtimeSinceStartup - startTime;
        outputY = transformCurveY.Evaluate(delta);
        transform.position = startPos + initForward * delta * r_Speed + new Vector3(0, outputY, 0);
        Vector3 posDelta = (transform.position - lastPos).normalized;
        transform.forward = posDelta;
        lastPos = transform.position;
     
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.tag ==Tags.Ground)
        {
            Destroy(this.gameObject);
            Instantiate(explosionFx, transform.position, Quaternion.identity);
        }
    }
}
