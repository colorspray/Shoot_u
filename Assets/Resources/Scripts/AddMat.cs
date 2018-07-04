using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AddMat : MonoBehaviour {
    public static AddMat _instance;
    public Material Material;
    public Renderer renderParent;
    Material instanceMat;
    public float maxLifeTime = 1.0f;
    private float lifeTime = 1.0f;
    private bool isSetNull = false;

    void Awake()
    {
        _instance = this;
        lifeTime = maxLifeTime;
    }

    public void AddMaterial()
    {
        if (renderParent.materials.Length <2)
        {
            var materials = renderParent.sharedMaterials;
            var length = materials.Length + 1;
            var newMaterials = new Material[length];
            materials.CopyTo(newMaterials, 0);
            renderParent.material = Material;
            instanceMat = renderParent.material;
            newMaterials[length - 1] = instanceMat;
            renderParent.sharedMaterials = newMaterials;
            lifeTime = maxLifeTime;
            isSetNull = false;
        }
        else
        {
            return;
        }

    }
    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    AddMaterial();
        //}

        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0 && !isSetNull)
        {
            isSetNull = true;
            OnDestroy();
        }
        if (lifeTime > 0)
            lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
            lifeTime = 0;
        //Color c = instanceMat.GetColor("_TintColor");
        //c.a = lifeTime / maxLifeTime;
        //instanceMat.SetColor("_TintColor", c);
        //float atte = instanceMat.GetFloat("_Disslusion");
        //instanceMat.SetFloat("_Disslusion", 1 - lifeTime / maxLifeTime);


    }
    void OnDestroy()
    {
        if (renderParent == null)
            return;
        var materials = renderParent.sharedMaterials.ToList();
        materials.Remove(instanceMat);
        renderParent.sharedMaterials = materials.ToArray();
    }
}
