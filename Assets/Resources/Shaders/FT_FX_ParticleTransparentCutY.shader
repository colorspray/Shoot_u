// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "FT/FX/Particle/TransparentCutY"
{
	Properties 
	{
		_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_MainTex ("Particle Texture", 2D) = "white" {}
		_InvFade ("Soft Particles Factor", Range(0.01,3.0)) = 1.0
		_Ratio ("Ratio", float) = 1.0
		_CutoutPos("裁剪高度",float) = 0
	}

	Category 
	{
		LOD 500
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
		ColorMask RGB
		Cull Off Lighting Off ZWrite Off

		SubShader 
		{
			Pass 
			{
			
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_particles
           	    #pragma multi_compile_fog
       	   	    #include "UnityCG.cginc"
       		    #include "FT_sceneCG.cginc"

				sampler2D _MainTex;
				fixed4 _TintColor;
				
				struct appdata_t 
				{
					fixed4 vertex : POSITION;
					fixed4 color : COLOR;
					fixed2 texcoord : TEXCOORD0;
				};

				struct v2f 
				{
					fixed4 vertex : SV_POSITION;
					fixed4 color : COLOR;
					fixed2 texcoord : TEXCOORD0;
					half fog :  TEXCOORD1;
					fixed4  posWorld : TEXCOORD2;//position of vertex in world;
					#ifdef SOFTPARTICLES_ON
					fixed4 projPos : TEXCOORD3;
					#endif
				};
				
				fixed4 _MainTex_ST;

				v2f vert (appdata_t v)
				{
					v2f o;
					UNITY_INITIALIZE_OUTPUT(v2f, o); 
					o.vertex = UnityObjectToClipPos(v.vertex);
					#ifdef SOFTPARTICLES_ON
					o.projPos = ComputeScreenPos (o.vertex);
					COMPUTE_EYEDEPTH(o.projPos.z);
					#endif
					o.color = v.color;
					o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
					o.posWorld.xyz = mul(unity_ObjectToWorld, v.vertex);
					o.fog = FT_linearFog(v.vertex);
					return o;
				}

				sampler2D_float _CameraDepthTexture;
				fixed _InvFade;
				fixed _Ratio;
				fixed _CutoutPos;

				fixed4 frag (v2f i) : SV_Target
				{
					if (i.posWorld.y < _CutoutPos)  
		            {  
		                discard;   
		            }  
					#ifdef SOFTPARTICLES_ON
					fixed sceneZ = LinearEyeDepth (SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)));
					fixed partZ = i.projPos.z;
					fixed fade = saturate (_InvFade * (sceneZ-partZ));
					i.color.a *= fade;
					#endif
					fixed4 Finalcolor = i.color * _TintColor * tex2D(_MainTex, i.texcoord) * _Ratio;
	                #ifdef UNITY_PASS_FORWARDADD
						UNITY_APPLY_FOG_COLOR(i.fog, Finalcolor, float4(0,0,0,0));
					#else
						UNITY_APPLY_FOG_COLOR(i.fog, Finalcolor, unity_FogColor);
					#endif
					return Finalcolor;
				}
				ENDCG 
			}
		}	
	}
	Category 
	{
		LOD 100
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
		ColorMask RGB
		Cull Off Lighting Off ZWrite Off

		SubShader 
		{
			Pass 
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_particles
				#include "UnityCG.cginc"

				sampler2D _MainTex;
				fixed4 _TintColor;
				
				struct appdata_t 
				{
					fixed4 vertex : POSITION;
					fixed4 color : COLOR;
					fixed2 texcoord : TEXCOORD0;
				};

				struct v2f 
				{
					fixed4 vertex : SV_POSITION;
					fixed4 color : COLOR;
					fixed2 texcoord : TEXCOORD0;
					fixed4  posWorld : TEXCOORD1;//position of vertex in world;
				};
				
				fixed4 _MainTex_ST;

				v2f vert (appdata_t v)
				{
					v2f o;
					UNITY_INITIALIZE_OUTPUT(v2f, o); 
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.color = v.color;
					o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
					o.posWorld.xyz = mul(unity_ObjectToWorld, v.vertex);
					return o;
				}

				sampler2D_float _CameraDepthTexture;
				fixed _InvFade;
				fixed _Ratio;
				fixed _CutoutPos;

				fixed4 frag (v2f i) : SV_Target
				{
					if (i.posWorld.y < _CutoutPos)  
		            {  
		                discard;   
		            }  
					fixed4 col = i.color * _TintColor * tex2D(_MainTex, i.texcoord) * _Ratio;
					return col;
				}
				ENDCG 
			}
		}	
	}
}
