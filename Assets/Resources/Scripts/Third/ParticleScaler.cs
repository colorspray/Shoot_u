using UnityEngine;
#if UNITY_EDITOR 
using UnityEditor;
#endif


/// <summary>
/// 粒子缩放
/// （仅Editor可用版）
/// </summary>
[ExecuteInEditMode]
public class ParticleScaler : MonoBehaviour
{
    public float particleScale = 1.0f;
    public bool alsoScaleGameobject = false;

    private float prevScale;

    /// <summary>
    /// 一堆开关
    /// </summary>
    public bool ScaleVelocityModule = true;
    public bool ScaleForceModule = true;
    public bool ScaleColorBySpeedModule = true;
    public bool ScaleSizeBySpeedModule = true;
    public bool ScaleRotationBySpeedModule = true;

    void Start()
    {
        prevScale = particleScale;
    }
#if UNITY_EDITOR
    void Update()
    {
        //check if we need to update
        if (prevScale != particleScale && particleScale > 0)
        {
            if (alsoScaleGameobject)
                transform.localScale = new Vector3(particleScale, particleScale, particleScale);

            float scaleFactor = particleScale / prevScale;

            //scale legacy particle systems
            ScaleLegacySystems(scaleFactor);

            //scale shuriken particle systems
            ScaleShurikenSystems(scaleFactor);

            //scale trail renders
            ScaleTrailRenderers(scaleFactor);

            prevScale = particleScale;
        }
    }
#endif
    void ScaleShurikenSystems(float scaleFactor)
    {
#if UNITY_EDITOR
        //get all shuriken systems we need to do scaling on
        ParticleSystem[] systems = GetComponentsInChildren<ParticleSystem>();

        foreach (ParticleSystem system in systems)
        {
            system.startSpeed *= scaleFactor;
            system.startSize *= scaleFactor;
            system.gravityModifier *= scaleFactor;

            //some variables cannot be accessed through regular script, we will acces them through a serialized object
            SerializedObject so = new SerializedObject(system);

            //unity 4.0 and onwards will already do this one for us
#if UNITY_3_5
			so.FindProperty("ShapeModule.radius").floatValue *= scaleFactor;
			so.FindProperty("ShapeModule.boxX").floatValue *= scaleFactor;
			so.FindProperty("ShapeModule.boxY").floatValue *= scaleFactor;
			so.FindProperty("ShapeModule.boxZ").floatValue *= scaleFactor;
#else
            if (alsoScaleGameobject == false)
            {
                //ShapeModule
                so.FindProperty("ShapeModule.radius").floatValue *= scaleFactor;
                so.FindProperty("ShapeModule.boxX").floatValue *= scaleFactor;
                so.FindProperty("ShapeModule.boxY").floatValue *= scaleFactor;
                so.FindProperty("ShapeModule.boxZ").floatValue *= scaleFactor;
            }
#endif

            if (ScaleVelocityModule)
            {
                //VelocityModule
                so.FindProperty("VelocityModule.x.scalar").floatValue *= scaleFactor;
                so.FindProperty("VelocityModule.y.scalar").floatValue *= scaleFactor;
                so.FindProperty("VelocityModule.z.scalar").floatValue *= scaleFactor;
                ScaleCurve(so.FindProperty("VelocityModule.x.minCurve").animationCurveValue, scaleFactor);
                ScaleCurve(so.FindProperty("VelocityModule.x.maxCurve").animationCurveValue, scaleFactor);
                ScaleCurve(so.FindProperty("VelocityModule.y.minCurve").animationCurveValue, scaleFactor);
                ScaleCurve(so.FindProperty("VelocityModule.y.maxCurve").animationCurveValue, scaleFactor);
                ScaleCurve(so.FindProperty("VelocityModule.z.minCurve").animationCurveValue, scaleFactor);
                ScaleCurve(so.FindProperty("VelocityModule.z.maxCurve").animationCurveValue, scaleFactor);

                //ClampVelocityModule
                so.FindProperty("ClampVelocityModule.magnitude.scalar").floatValue *= scaleFactor;
                so.FindProperty("ClampVelocityModule.x.scalar").floatValue *= scaleFactor;
                so.FindProperty("ClampVelocityModule.y.scalar").floatValue *= scaleFactor;
                so.FindProperty("ClampVelocityModule.z.scalar").floatValue *= scaleFactor;
                ScaleCurve(so.FindProperty("ClampVelocityModule.x.minCurve").animationCurveValue, scaleFactor);
                ScaleCurve(so.FindProperty("ClampVelocityModule.x.maxCurve").animationCurveValue, scaleFactor);
                ScaleCurve(so.FindProperty("ClampVelocityModule.y.minCurve").animationCurveValue, scaleFactor);
                ScaleCurve(so.FindProperty("ClampVelocityModule.y.maxCurve").animationCurveValue, scaleFactor);
                ScaleCurve(so.FindProperty("ClampVelocityModule.z.minCurve").animationCurveValue, scaleFactor);
                ScaleCurve(so.FindProperty("ClampVelocityModule.z.maxCurve").animationCurveValue, scaleFactor);
                ScaleCurve(so.FindProperty("ClampVelocityModule.magnitude.minCurve").animationCurveValue, scaleFactor);
                ScaleCurve(so.FindProperty("ClampVelocityModule.magnitude.maxCurve").animationCurveValue, scaleFactor);
            }


            if (ScaleForceModule)
            {
                //ForceModule
                so.FindProperty("ForceModule.x.scalar").floatValue *= scaleFactor;
                so.FindProperty("ForceModule.y.scalar").floatValue *= scaleFactor;
                so.FindProperty("ForceModule.z.scalar").floatValue *= scaleFactor;
                ScaleCurve(so.FindProperty("ForceModule.x.minCurve").animationCurveValue, scaleFactor);
                ScaleCurve(so.FindProperty("ForceModule.x.maxCurve").animationCurveValue, scaleFactor);
                ScaleCurve(so.FindProperty("ForceModule.y.minCurve").animationCurveValue, scaleFactor);
                ScaleCurve(so.FindProperty("ForceModule.y.maxCurve").animationCurveValue, scaleFactor);
                ScaleCurve(so.FindProperty("ForceModule.z.minCurve").animationCurveValue, scaleFactor);
                ScaleCurve(so.FindProperty("ForceModule.z.maxCurve").animationCurveValue, scaleFactor);
            }

            if (ScaleColorBySpeedModule)
            {
                so.FindProperty("ColorBySpeedModule.range").vector2Value *= scaleFactor;
            }
            if (ScaleSizeBySpeedModule)
            {
                so.FindProperty("SizeBySpeedModule.range").vector2Value *= scaleFactor;
            }
            if (ScaleRotationBySpeedModule)
            {
                so.FindProperty("RotationBySpeedModule.range").vector2Value *= scaleFactor;
            }

            so.ApplyModifiedProperties();
        }
#endif
    }


    void ScaleCurve(AnimationCurve curve, float scaleFactor)
    {
        for (int i = 0; i < curve.keys.Length; i++)
        {
            curve.keys[i].value *= scaleFactor;
        }
    }

    void ScaleLegacySystems(float scaleFactor)
    {
#if UNITY_EDITOR
        //get all emitters we need to do scaling on
        ParticleEmitter[] emitters = GetComponentsInChildren<ParticleEmitter>();

        //get all animators we need to do scaling on
        ParticleAnimator[] animators = GetComponentsInChildren<ParticleAnimator>();

        //apply scaling to emitters
        foreach (ParticleEmitter emitter in emitters)
        {
            emitter.minSize *= scaleFactor;
            emitter.maxSize *= scaleFactor;
            emitter.worldVelocity *= scaleFactor;
            emitter.localVelocity *= scaleFactor;
            emitter.rndVelocity *= scaleFactor;

            //some variables cannot be accessed through regular script, we will acces them through a serialized object
            SerializedObject so = new SerializedObject(emitter);

            so.FindProperty("m_Ellipsoid").vector3Value *= scaleFactor;
            so.FindProperty("tangentVelocity").vector3Value *= scaleFactor;
            so.ApplyModifiedProperties();
        }

        //apply scaling to animators
        foreach (ParticleAnimator animator in animators)
        {
            animator.force *= scaleFactor;
            animator.rndForce *= scaleFactor;
        }
#endif
    }

    void ScaleTrailRenderers(float scaleFactor)
    {
        //get all animators we need to do scaling on
        TrailRenderer[] trails = GetComponentsInChildren<TrailRenderer>();

        //apply scaling to animators
        foreach (TrailRenderer trail in trails)
        {
            trail.startWidth *= scaleFactor;
            trail.endWidth *= scaleFactor;
        }
    }
}
