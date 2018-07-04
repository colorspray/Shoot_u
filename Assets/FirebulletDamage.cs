using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebulletDamage : MonoBehaviour
{
    public static FirebulletDamage _instance;
    public int damage = 50;

    GameObject enemy;
    EnemyHealth enemyHealth;
    public GameObject fireBulletHit;
    [HideInInspector]
    public bool isCol;
    [HideInInspector]
    public  SphereCollider TNTcollider;
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
            isCol = true;
        }
        if (isCol)
        {
            TNTcollider.isTrigger = false;
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
                Instantiate(fireBulletHit, go.transform.position+Vector3.up , go.transform.rotation,go.transform);
                go.GetComponent<EnemyHealth>().TakeDamage01(damage);
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        isCol = false;
    }
}
