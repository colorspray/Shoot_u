using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : MonoBehaviour
{
    public static NormalBullet _instance;
    public int demagePershor = 20;//攻击力
    public float bulletSpeed = 100;
    public GameObject  bulletHit;
     [HideInInspector]
     public bool isNorCollider;
  
   // Enemy01Health enemyHealth = enemy.GetComponent<Enemy01Health>();

    
	// Use this for initialization
	void Awake () {
        
        _instance = this;
	}
	
	// Update is called once per frame
	void Update () {
        shoot();
	}
    void shoot()
    {
        Vector3 oriPos = transform.position;

        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);

        Vector3 direction = transform.position - oriPos;

        float length = (transform.position - oriPos).magnitude;

        // PlayerShooting EffBullet = bullet.GetComponent<PlayerShooting>();

        RaycastHit hitinfo;
        isNorCollider = Physics.Raycast(oriPos, direction, out hitinfo, length);
        if (isNorCollider)
        {
           
            EnemyHealth enemyHealth = hitinfo.collider.GetComponent<EnemyHealth>();
            if (enemyHealth != null)// 如果不为空，说明碰到敌人，让敌人受到伤害
            {
                enemyHealth.TakeDamageNormal(demagePershor, hitinfo.point);
            }
            Vector3 pos = hitinfo.point;//碰撞的位置
            GameObject.Instantiate(bulletHit, pos , transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
