using UnityEngine;
using System.Collections;

public class Playermovement : MonoBehaviour {
    public float speed = 6.0f;
    Vector3 movement;
    Rigidbody playerRigidbody;
    Animator anim;

    int floorMask;
    float camRayLength = 100f;
	// Use this for initialization
    void Awake() {
        floorMask = LayerMask.GetMask("Ground");
        playerRigidbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void FixedUpdate() 
    {
       float h = Input.GetAxisRaw("Horizontal");
       float v = Input.GetAxisRaw("Vertical");
       Move(h,v);
       Animating(h,v);
       Turning();
    }
    void Move(float h , float v)
    {
        movement.Set(h, 0f, v);//设置movement的值
        movement = movement.normalized * speed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + movement);//通过MovePosition()方法让主角移动
    }

    void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0;
        anim.SetBool("IsWalking", walking);

    }
    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);//根据当前的鼠标位置，从摄像机发射一条射线，返回射线
        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(newRotation);

        }
    }

    void OnTriggerEnter(Collider other)
    {
        Prop_speed speedProp = other.GetComponent<Prop_speed>();
        if (speedProp)
        {
            StartCoroutine(UseProp(speedProp.durationTime));
        }
    }

    IEnumerator UseProp(float time)
    {
      
        yield return new WaitForSeconds(time);
      
    }
}
