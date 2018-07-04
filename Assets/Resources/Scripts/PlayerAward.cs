using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAward : MonoBehaviour {
  
    public float exitTime =20;//持续时间
    float startTimer = 0;

    Playermovement player;

    public GameObject fireGun;
    public GameObject iceGun;
    public GameObject eleGun;
    public GameObject normalGun;


    public GameObject normalGunLeverFx;
    public GameObject otherGunLeverFx;
    public GameObject getShoesFx;
  

    PlayerShooting playAwards;
    public float speedExitTime;

    public MeshFilter normalGunMesh;
    public Mesh[] normalGunmeshArray;
    private int normalGunmeshIndex = 0;

    void Awake()
    {
        player = GetComponent<Playermovement>();
        playAwards = GetComponentInChildren<PlayerShooting>();
      //  playAwards = GetComponent<PlayerShooting>();
    }
    void Update()
    {
        if (startTimer > 0)
        {
            startTimer -= Time.deltaTime;
            if (startTimer < 0)
            {
                TurnToNormalGun();
            }
        }
        if (startTimer > 0)
        {
            startTimer -= Time.deltaTime;
            if (startTimer < 0)
            {
                player.speed = 6;
            }
        }
    }
    public void GetAward(AwardType type)
    {
        if (type == AwardType.NormalGun)
        {
            GunLevelUp();
        }
        else if (type == AwardType.FireGun)
        {
            TurnToFireGun();
        }
        else if (type == AwardType.EleGun)
        {
            TurnToEleGun();
        }
        else if (type == AwardType.IceGun)
        {
           TurnToIceGun();
        }
        else if (type == AwardType.MoveSpeed)
        {
            PlayerSpeed();
        }
    }

    void TurnToNormalGun()
    {
        fireGun.SetActive(false);
        iceGun.SetActive(false);
        eleGun.SetActive(false);
        normalGun.SetActive(true);
        startTimer = exitTime;
        Instantiate(otherGunLeverFx, transform.position, transform.rotation, transform);
    }


    void PlayerSpeed()
    {
        player.speed = 9;
        Instantiate(otherGunLeverFx, transform.position, transform.rotation, transform);
        Instantiate(getShoesFx, transform.position, transform.rotation, transform);
        startTimer = speedExitTime;
    }

   
    void TurnToFireGun()
    {
        fireGun.SetActive(true);
        iceGun.SetActive(false);
        eleGun.SetActive(false);
        normalGun.SetActive(false);
        startTimer = exitTime;
        Instantiate(otherGunLeverFx, transform.position, transform.rotation, transform);
    }

    void TurnToEleGun()
    {
        fireGun.SetActive(false);
        iceGun.SetActive(false);
        eleGun.SetActive(true);
        normalGun.SetActive(false);
        startTimer = exitTime;
        Instantiate(otherGunLeverFx, transform.position, transform.rotation, transform);
    }

    void TurnToIceGun()
    {
        fireGun.SetActive(false);
        iceGun.SetActive(true);
        eleGun.SetActive(false);
        normalGun.SetActive(false);
        startTimer = exitTime;
        Instantiate(otherGunLeverFx, transform.position, transform.rotation, transform);
    }


    void GunLevelUp()
    {
        playAwards.BulletLevel++;
        normalGunmeshIndex++;
        Instantiate(normalGunLeverFx, transform.position, transform.rotation, transform);
        normalGunmeshIndex %= normalGunmeshArray.Length;
        normalGunMesh.sharedMesh = normalGunmeshArray[normalGunmeshIndex];
    }
  
}
