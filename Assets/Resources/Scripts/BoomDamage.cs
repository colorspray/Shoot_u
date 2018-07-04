using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomDamage : MonoBehaviour {
    public int demagePershor = 50;//攻击力
    bool enemyInrange;//是否在攻击范围内
    EnemyHealth enemyHealth;
    float timer;
    GameObject enemy;
    Vector3 hitPoint;
    bool isCollider;
    private SphereCollider boomCollider;
	// Use this for initialization
	void Awake () 
    {
        boomCollider = GetComponent<SphereCollider>();
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        enemyHealth = GetComponent<EnemyHealth>();
	}

    void OnTriggerEnter(Collider other)
    {
          isCollider = true;
          if (other.tag ==Tags.Enemy)
        {
            boomCollider.isTrigger = false;
            enemyInrange = true;
        }
        timer += Time.deltaTime;//计时器开始
        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
        if (enemyInrange && enemyHealth.currentHealth > 0)
        {
            enemyHealth.TakeDamage01(demagePershor);
        }
    }

    void OnTriggerExit(Collider other)
    {
        isCollider = false;
        if (other.gameObject == enemy)
        {
            enemyInrange = false;
        }
       
    }
	// Update is called once per frame
	void Update () {
       
	}
}
