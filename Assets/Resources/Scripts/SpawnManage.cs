using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManage : MonoBehaviour {
    public static SpawnManage _instance;
    public EnemySpawn[] monsterSpawnArray;
    public EnemySpawn[] bossSpawnArray;

    public List<GameObject> enemyList = new List<GameObject>();

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        StartCoroutine(Spawn());
    }
    IEnumerator Spawn() 
    {
        //第一波敌人的生产
        foreach(EnemySpawn s in monsterSpawnArray)
        {
            enemyList.Add(s.Spawn());
        }

        while (enemyList.Count > 0)
        {
            yield return new WaitForSeconds(0.2f);
        }
        //第二波敌人的生产
        foreach (EnemySpawn s in monsterSpawnArray)
        {
            enemyList.Add(s.Spawn());
        }
        yield return new WaitForSeconds(0.5f);
        foreach (EnemySpawn s in monsterSpawnArray)
        {
            enemyList.Add(s.Spawn());
        }

        while (enemyList.Count > 0)
        {
            yield return new WaitForSeconds(0.2f);
        }
        //第三波敌人的生产
        foreach (EnemySpawn s in monsterSpawnArray)
        {
            enemyList.Add(s.Spawn());
        }

        yield return new WaitForSeconds(0.5f);

        foreach (EnemySpawn s in monsterSpawnArray)
        {
            enemyList.Add(s.Spawn());
        }
        yield return new WaitForSeconds(0.5f);

        foreach (EnemySpawn s in bossSpawnArray)
        {
            enemyList.Add(s.Spawn());
        }


    }
}
