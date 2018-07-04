using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

    public int startingHealth = 100;//初始生命值
    public int currentHealth; //当前生命值
    public float sinkSpeed =2.5f;
    public AudioClip deathClip;
    CapsuleCollider capsuleCollider; //胶囊体
    bool isDead;
    bool isSinking;
    Animator ani;
    AudioSource enemyAudio;
    Rigidbody rig;
    UnityEngine.AI.NavMeshAgent nav;
    AddMat addMaterial;

    public float iceTime = 3f; 
    float timer;//计时器
    bool startMove;
 

	// Use this for initialization
	void Awake () {
        addMaterial = GetComponent<AddMat>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        rig = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        ani = GetComponent<Animator>();
        currentHealth = startingHealth;
    
        enemyAudio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
      

        if (startMove)
        {
            timer += Time.deltaTime;
            if (timer >= iceTime && Time.timeScale != 0)
            {
                
                ani.speed = 1f;
                nav.speed = 3;
                timer = 0;
                AddMat._instance.maxLifeTime = 0.5f;
            }
        }
        if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
	
	}
    public void TakeDamageNormal(int amount, Vector3 hitPoint) {
        if (isDead)
            return;

        if (NormalBullet._instance.isNorCollider)
        {
            addMaterial.AddMaterial();
            currentHealth -= amount;//怪物受到攻击 减少生命值
            ani.SetTrigger("hurt");
            enemyAudio.Play();
        }

          
      
        if (currentHealth <= 0)
        {
            Death();
        }
    }
    public void TakeDamageIce(int amount, Vector3 hitPoint)
    {
        if (isDead)
            return;

        if (IceBullet._instance.isIceCollider == true)
        {
            startMove = false;
        }
        if (IceBullet._instance.isIceCollider == false)
        {
            startMove = true;
        }

        if (IceBullet._instance.isIceCollider)
        {
            currentHealth -= amount;//怪物受到攻击 减少生命值
            ani.SetTrigger("hurt");
            enemyAudio.Play();


            AddMat._instance.maxLifeTime = 3;
            addMaterial.AddMaterial();
            ani.speed = 0.5f;
            nav.speed = 2;
            IceBullet._instance.isIceCollider = false;
        }

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public void TakeDamageIceExplosion(int amount)
    {
        if (isDead)
            return;

        if (IceExplosionDamage._instance.isIceExpCol)
        {
            currentHealth -= amount;//怪物受到攻击 减少生命值
            ani.SetTrigger("hurt");
            enemyAudio.Play();

            addMaterial.AddMaterial();
            ani.speed = 0f;
            nav.speed = 0;
        }

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public void TakeDamageFireExplosion(int amount)
    {
        if (isDead)
            return;

        if (NormalExplossionDamage._instance.isFireExpCol)
        {
            currentHealth -= amount;//怪物受到攻击 减少生命值
            ani.SetTrigger("hurt");
            enemyAudio.Play();
        }

        if (currentHealth <= 0)
        {
            Death();
        }
    }




    public void TakeDamage01(int amount)
    {
        if (isDead)
            return;

        amount = 20;
        currentHealth -= amount;//怪物受到攻击 减少生命值
      
        enemyAudio.Play();
        if (currentHealth <= 0)
        {
            Death();
        }
    }


    void Death()
    {
        isDead = true;
        capsuleCollider.enabled = false;//变成触发器，不会挡住子弹的弹道
        ani.SetTrigger("Die"); //播放死亡动画
        enemyAudio.clip = deathClip;
        enemyAudio.Play();
        SpawnManage._instance.enemyList.Remove(this.gameObject);
    }

    public void startSinking()
    {
        rig.useGravity = true;
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled =false;//导航效果去除
        isSinking = true;
        Destroy(gameObject,2f);//销毁物体
    }
}
