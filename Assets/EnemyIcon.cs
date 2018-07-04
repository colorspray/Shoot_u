using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIcon : MonoBehaviour {
    private Transform icon;
    private Transform player;
	// Use this for initialization
	void Start () {
        if (this.tag == Tags.Enemy)
        {
            icon = MinMap._instance.GetEnemyIcon().transform;
        }
        player = GameObject.FindGameObjectWithTag(Tags.Player).transform;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 offset = transform.position - player.position;//怪物的位置减去主角 的位置得出一个位置差
        offset *= 8;
        icon.localPosition = new Vector3(offset.x, offset.z, 0);
      
	}
    void OnDestroy()
    {
        if (icon != null)
        {
            Destroy(icon.gameObject);
        }
    }
}
