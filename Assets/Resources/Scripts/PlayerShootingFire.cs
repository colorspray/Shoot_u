using UnityEngine;
using System.Collections;

public class PlayerShootingFire : MonoBehaviour
{

    public GameObject EffBullet;
    public Transform  bulletTrans01;
    public GameObject EffBulletDie;

   

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
        EffBulletDie.SetActive(false);
        EffBullet.SetActive(false);
        shootableMask = LayerMask.GetMask("Ground");//地面，鼠标和地板2 出的一条射线
    }

    public int BulletLevel { get; set; }
    
    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            EffBulletDie.SetActive(false);
            EffBullet.SetActive(true);
        }
      
        if (Input.GetMouseButtonUp(0))
        {

            FirebulletDamage._instance.TNTcollider.isTrigger = true;
            Invoke("DelayDestroy", 0.4f);
            EffBulletDie.SetActive(true);
        }
      
    }

  
 
    public void Shoot()
    {
        EffBullet.SetActive(true);
    }
    void DelayDestroy()
    {
        EffBullet.SetActive(false);
    }
}
