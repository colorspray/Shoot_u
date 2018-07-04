using UnityEngine;
using System.Collections;

public class Enemymovement : MonoBehaviour {
    Transform player; //怪物导航的目标
    UnityEngine.AI.NavMeshAgent nav;
    EnemyHealth enemyHealth;
    PlayerHealth playerHealth;
    public GameObject slowEffect;
	// Use this for initialization
	void Awake () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        enemyHealth = GetComponent<EnemyHealth>();
        playerHealth = player.GetComponent<PlayerHealth>();
        slowEffect.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
           
            nav.SetDestination(player.position);//设置怪物自动导航的目标
            if (nav.speed < 2.2 && nav.speed>0)
            {
                slowEffect.SetActive(true);
            }
            else
            {
                slowEffect.SetActive(false);
            }
        }
        else
        {
            nav.enabled = false;
        }
	}
}
