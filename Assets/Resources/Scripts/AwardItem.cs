using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AwardType { NormalGun, EleGun, FireGun, IceGun,MoveSpeed }


public class AwardItem : MonoBehaviour {

    public AwardType type;
    bool startMove = false;
    private Transform player;
    public float itemSpeed = 8.0f;
    Rigidbody rig;
    bool isCollider=false;
    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }
    void Update()
    {
        //if (isCollider)
        //{
        //    transform.position = Vector3.Lerp(transform.position, player.position , itemSpeed * Time.deltaTime);
        //    if (Vector3.Distance(transform.position, player.position ) < 0.6f)
        //    {
        //        player.GetComponent<PlayerAward>().GetAward(type);
        //        Destroy(this.gameObject);
        //    }
        //}
        if (isCollider)
        {
            transform.position = Vector3.Lerp(transform.position, player.position, itemSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, player.position) < 0.6f)
            {
                player.GetComponent<PlayerAward>().GetAward(type);
                Destroy(this.gameObject);
            }
        }
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.tag == Tags.Player)
        {
            player = col.transform;
            isCollider = true;
        }
    }

}
