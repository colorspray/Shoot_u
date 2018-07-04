using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;//初始生命值
    public int currentHealth ; //当前生命值
   
    Playermovement playerMovement;
    Animator anim;

    public Slider healthSlider;//生命值UI滑块
    public Image damageImage;
    public float flashSpeed = 5f;
    public Color flashColor = new Color(1f,0f,0f,0.1f);
    public GameObject hitEffect;
    
    bool isDead;//是否死亡
    bool damaged;//是否受到伤害
    public AudioClip deathClip;
    AudioSource playerAudio;
	// Use this for initialization
	void Awake () {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<Playermovement>();
        currentHealth = startingHealth;//把初始生命值置为当前生命值	
	}
	// Update is called once per frame
	void Update () {
        if (damaged)
        {
            damageImage.color = flashColor;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);// Color.clear清楚颜色表色Rg(0,0,0,0).
        }
        damaged = false;
	}

    public void TakeDamage(int amount) {
        damaged = true;
        currentHealth -= amount;// 如果受到伤害，生命值等于当前生命值-伤害。
        healthSlider.value = currentHealth;
        playerAudio.Play();
        Instantiate(hitEffect, transform.position, Quaternion.identity);
        if (currentHealth <= 0 && !isDead)
           Death();
    }
    void Death()
    {
        isDead = true;
        anim.SetTrigger("die");
        playerAudio.clip = deathClip;
        playerAudio.Play();

        playerMovement.enabled = false; //禁用掉玩家的控制
    }
    public void RestartLevel()
    {
            Application.LoadLevel(Application.loadedLevel);//调入当前已经调用的场景
        
    }
     
}
