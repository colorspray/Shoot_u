using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceExplosionDamage : MonoBehaviour
{
    public static IceExplosionDamage _instance;
    public int iceExpDamage = 50;
    GameObject enemy;
    EnemyHealth enemyHealth;
    [HideInInspector]
    public bool isIceExpCol;
    SphereCollider TNTcollider;
    public float attactDistance = 10;
    float timer;
	// Use this for initialization
	void Awake () {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
       // enemy = GameObject.FindGameObjectWithTag("Enemy");
        enemyHealth = GetComponent<EnemyHealth>(); 
        TNTcollider = GetComponent<SphereCollider>();
		_instance=this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == Tags.Enemy)
        {
            isIceExpCol = true;
        }
     //   EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
        if (isIceExpCol)
        {
            List<GameObject> enemyList = new List<GameObject>();
            foreach (GameObject go in SpawnManage._instance.enemyList)
            {
                float temp = Vector3.Distance(go.transform.position, transform.position);
                if (temp < attactDistance)
                {
                    enemyList.Add(go);
                   // go.GetComponent<EnemyHealth>().TakeDamageIceExplosion(iceExpDamage);
                }
            }
            foreach (GameObject go in enemyList)
            {
                go.GetComponent<EnemyHealth>().TakeDamageIceExplosion(iceExpDamage);
            }
        }
        isIceExpCol = false;
    }

    void OnTriggerExit(Collider col)
    {
        isIceExpCol = false;
    }
}
