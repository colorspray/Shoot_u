using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalExplossionDamage : MonoBehaviour {

    public static NormalExplossionDamage _instance;
    public int fireExpDamage = 50;
    GameObject enemy;
    EnemyHealth enemyHealth;
    [HideInInspector]
    public bool isFireExpCol;
    SphereCollider TNTcollider;
    public float attactDistance = 10;
   
    float timer;
	// Use this for initialization
	void Awake () {

        enemy = GameObject.FindGameObjectWithTag("Enemy");
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
            TNTcollider.isTrigger = false;
            isFireExpCol = true;
        }
        if (isFireExpCol)
        {
            List<GameObject> enemyList = new List<GameObject>();
            foreach (GameObject go in SpawnManage._instance.enemyList)
            {
                float temp = Vector3.Distance(go.transform.position, transform.position);
                if (temp < attactDistance)
                {
                    enemyList.Add(go);
                    // go.GetComponent<AtkAndDamage>().TakeDamage(AttackDamage);
                }
            }
            foreach (GameObject go in enemyList)
            {
                go.GetComponent<EnemyHealth>().TakeDamageFireExplosion(fireExpDamage);
            }
        }
    }


    void OnTriggerExit(Collider col)
    {
        isFireExpCol = false;
    }
}
