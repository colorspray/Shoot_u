using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GrandlineDelay : MonoBehaviour {
	
	//public float delayTime = 1.0f;

	public List<float> delayTimes;
    public List<float> dieTimes;
	public List<GameObject> animatedObjs;
	// Use this for initialization
	void Start () {
		if (animatedObjs.Count > 0)
		{
			for (int i = 0; i < animatedObjs.Count; i++) {
				if (animatedObjs[i] == null)
					continue;
				animatedObjs[i].SetActive(false);
				StartCoroutine(WaitAndPrint(animatedObjs[i], delayTimes[i]));
				//Invoke("DelayFunc", delayTimes[i]);
			}
		}
		// gameObject.SetActiveRecursively(false);
		// Invoke("DelayFunc", delayTime);
	}

    void OnEnable()
    {
		if (animatedObjs.Count > 0)
		{
			for (int i = 0; i < animatedObjs.Count; i++) {
				if (animatedObjs[i] == null)
					continue;
				animatedObjs[i].SetActive(false);
                if (delayTimes.Count > i)
				    StartCoroutine(WaitAndPrint(animatedObjs[i], delayTimes[i]));
                if (dieTimes.Count > i)
                    StartCoroutine(WaitAndHide(animatedObjs[i], dieTimes[i]));
                //Invoke("Diefunc", dieTimes[i]);
			}
			
		}
       

    }

   /*
    void Diefunc()
    {
        for (int i = 0; i <  animatedObjs.Count; i++)
        {
            animatedObjs[i].SetActive(false);
        }
    }*/
	IEnumerator WaitAndPrint(GameObject inputObj, float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		//Debug.Log(inputObj);
		inputObj.SetActive(true);
	}

    IEnumerator WaitAndHide(GameObject inputObj, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //Debug.Log(inputObj);
        inputObj.SetActive(false);
    }

}
