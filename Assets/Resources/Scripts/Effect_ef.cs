using UnityEngine;
using System.Collections;

public class Effect_ef : MonoBehaviour
{
    public float delayTime = 2.0f;
    public float  r_Speed;
    public float Lifetime = 2.0f;
    public GameObject explosionFx;
    public AnimationCurve transformCurveY;
    GameObject player;
    GameObject world;

    private float outputY;
    private float startTime = float.NaN;
    private bool shouldUpdate = false;
    private Vector3 startPos = Vector3.zero;
    private Vector3 lastPos = Vector3.zero;
    private Vector3 initForward = Vector3.zero;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        outputY = 0;
        Invoke("Delayfunc", delayTime);
        gameObject.SetActive(false);
    }

    void Delayfunc()
    {
        gameObject.SetActive(true);
        shouldUpdate = true;
        startTime = Time.realtimeSinceStartup;
        startPos = transform.position;
        lastPos = transform.position;
        initForward = transform.forward;
        Destroy(gameObject, Lifetime);
    }
    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            return;
        }
        else 
        {
            Instantiate(explosionFx, transform.position, Quaternion.identity);
        }
    }

    void Update()
    {
        if (!shouldUpdate) return;
        float delta = Time.realtimeSinceStartup - startTime;
        outputY = transformCurveY.Evaluate(delta);
        transform.position = startPos + initForward * delta * r_Speed + new Vector3(0, outputY, 0);
        Vector3 posDelta = (transform.position - lastPos).normalized;
        transform.forward = posDelta;
        lastPos = transform.position;
    }
}
