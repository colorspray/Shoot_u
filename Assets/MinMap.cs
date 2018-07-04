using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMap : MonoBehaviour
{
    public GameObject enemyIconPrafab;
    public static MinMap _instance;
    private Transform playerIcon;
	// Use this for initialization
	void Awake () {
        _instance = this;
        playerIcon = transform.Find("PlayerIcon");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public Transform  GetPlayerIcon()
    {
        return playerIcon;
    }

     
    public GameObject GetEnemyIcon()
    {
        GameObject go = Instantiate(enemyIconPrafab, transform.position, transform.rotation, playerIcon.transform);
        return go;
    }
}
