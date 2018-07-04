using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop_speed : MonoBehaviour {

    public GameObject GetProp;
    public Transform playerTrans;
    GameObject player;
    bool isColliderProp;
    float timer;
    public float durationTime=5;
	// Use this for initialization
	void Awake () {
        player = GameObject.FindGameObjectWithTag("Player");
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            isColliderProp = true;
            GetProp.SetActive(true);
            Destroy(this.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            isColliderProp = false;
        }
    }



    //void Update()
    //{
    //    timer += Time.deltaTime;
    //    if (isColliderProp)  
    //    {
    //        if (timer <= durationTime && Time.timeScale != 0)
    //        {

    //        }
    //    }
    //    if (!isColliderProp)
    //    {
    //        if (timer > durationTime  && Time.timeScale != 0)
    //        {
    //            speedEffect.SetActive(false);
    //            player.GetComponent<Playermovement>().speed = 3;
    //        }
           
    //    }
    //}
}