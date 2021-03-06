﻿using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{

    public GameObject EffBullet;
    public Transform  bulletTrans01;
    public GameObject BulletFire;

    public Transform bulletTrans02;
    public Transform bulletTrans03;
    public Transform bulletTrans04;

    public GameObject EffBoom;
    public Transform EffBoomTran;

    public float timeBetweenBullet = 0.2f;//子弹发射时间间隔
  
    float timer;//计时器
    Ray shootRay;//射线
    RaycastHit shootHit;//保存射线碰撞的结果信息
    int shootableMask;
    public float range = 100f; //开火范围，射程

    float effectsDisplayTime = 2.0f; //枪支开火时播放效果的持续时间的百分比
    private bool isFiring = false;
    // Use this for initialization
    void Awake()
    {
        shootableMask = LayerMask.GetMask("Ground");//地面，鼠标和地板2 出的一条射线
    }

    public int BulletLevel { get; set; }
    
    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            isFiring = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isFiring = false;
        }
        if (isFiring)
        {
          
            timer += Time.deltaTime;
            if (timer >= timeBetweenBullet && Time.timeScale != 0)
            {
                LevelNum(BulletLevel);
                timer = 0;
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Instantiate(EffBoom, EffBoomTran.position, EffBoomTran.rotation);
        }
    }

  
    //public void shoot()
    //{

    //    GameObject.Instantiate(BulletFire, bulletTrans.position, bulletTrans.rotation);
    //    GameObject.Instantiate(EffBullet, bulletTrans.position, bulletTrans.rotation);
       
    //}
    public void LevelNum(int level)
    {
        if (level == 0)
        {

            GameObject.Instantiate(BulletFire, bulletTrans01.position, bulletTrans01.rotation);
            GameObject.Instantiate(EffBullet, bulletTrans01.position, bulletTrans01.rotation);
        }
        if (level == 1)
        {
            GameObject.Instantiate(BulletFire, bulletTrans01.position, bulletTrans01.rotation);
         //   GameObject.Instantiate(EffBullet, bulletTrans.position, bulletTrans.rotation);
            GameObject.Instantiate(EffBullet, bulletTrans03.position, bulletTrans03.rotation);
            GameObject.Instantiate(EffBullet, bulletTrans02.position, bulletTrans02.rotation);
        }
        if (level == 2)
        {
            GameObject.Instantiate(BulletFire, bulletTrans01.position, bulletTrans01.rotation);
            GameObject.Instantiate(EffBullet, bulletTrans04.position, bulletTrans04.rotation);
            GameObject.Instantiate(EffBullet, bulletTrans02.position, bulletTrans02.rotation);
            GameObject.Instantiate(EffBullet, bulletTrans03.position, bulletTrans03.rotation);
        }
    }

}
