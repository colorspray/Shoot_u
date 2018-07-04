using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
    public Transform target;
    public float smoothing = 5f;

    Vector3 offset;
	// Use this for initialization
	void Start () {
        offset = transform.position - target.position;//求出摄像机与目标物体的相对位置
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void FixedUpdate() {
        Vector3 targetCamPos = target.position + offset;//保持摄像机与目标的相对偏移
        transform.position = Vector3.Lerp(transform.position,targetCamPos,smoothing*Time.deltaTime);//让摄像机从自身的位置到目标位置进行移动
    }
}
