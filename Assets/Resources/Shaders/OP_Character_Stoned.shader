// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "OP_Character/Stoned" {

	Properties
	{
		_StoneTex ("岩石纹理", 2D) = "gray" {}
		_LightDir ("伪光方向", Vector) = (1.0, 0.0, 0.0, 0.0)
		_FresnelRange ("边缘光范围", Range(0.0, 3.0)) = 0.0
		_LightColor ("亮部颜色", Color) = (1.0, 1.0, 1.0, 1.0)
		_DarkColor ("暗部颜色", Color) = (0.0, 0.0, 0.0, 1.0)

		_OutlineWidth ("描边宽度（高质量）", float) = 1.0
		_OutlineLerp ("描边宽度适应系数（高质量）", Range(0.0, 1.0)) = 0.5
		_OutlineColor ("描边颜色", Color) = (0.0, 0.0, 0.0, 0.0)
	}

	// 多边形描边
	Subshader {
		Tags {
			"Queue" = "Geometry"
			"RenderType" = "Opaque"
		}
		LOD 500

		Pass {
			Name "Outline"
			Tags {
			}
			Cull Front
			
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"
				#pragma multi_compile_fog

				uniform half _OutlineWidth;
				uniform fixed4 _OutlineColor;
				uniform fixed _OutlineLerp;

				struct VertexInput {
					float4 vertex : POSITION;
					float3 normal : NORMAL;
				};

				struct VertexOutput {
					float4 pos : SV_POSITION;
					UNITY_FOG_COORDS(0)
				};

				VertexOutput vert (VertexInput v) {
					VertexOutput o = (VertexOutput)0;
					float4 objPos = mul (unity_ObjectToWorld, float4(0, 0, 0, 1));
					half actualOutlineWidth = lerp(_OutlineWidth, (_OutlineWidth * distance(_WorldSpaceCameraPos, objPos.rgb)), _OutlineLerp);
					o.pos = UnityObjectToClipPos(float4(v.vertex.xyz + v.normal * actualOutlineWidth * 0.01, 1.0));
					return o;
				}

				fixed4 frag(VertexOutput i) : COLOR {
					_OutlineColor.a = 1.0;
					return _OutlineColor;
				}
			ENDCG
		}

		Pass {
			Name "ForwardExpand"
			Tags {
				"LightMode"="ForwardBase" 
			}
			Cull Back

			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"
				#define UNITY_PASS_FORWARDBASE
				#pragma multi_compile_fog

				uniform sampler2D _StoneTex;
				uniform fixed4 _LightDir;
				uniform fixed4 _LightColor;
				uniform fixed4 _DarkColor;

				uniform half _FresnelRange;



				struct VertexInput {
					float4 vertex : POSITION;
					float3 normal : NORMAL;
					float2 texcoord : TEXCOORD;
					float4 vertexColor : COLOR;
				};

				struct VertexOutput {
					float4 pos : SV_POSITION;
					float2 uv : TEXCOORD0;
					float3 normalDir : TEXCOORD1;
					float3 viewDir : TEXCOORD2;
					float4 vertexColor : COLOR0;
					#ifdef _LightProbe_ON
					float3 shLight : COLOR1;
					#endif
					UNITY_FOG_COORDS(3)
				};

				VertexOutput vert (VertexInput v) {
					VertexOutput o = (VertexOutput)0;
					o.pos = UnityObjectToClipPos(v.vertex );
					o.uv = v.texcoord;
					o.normalDir = normalize(UnityObjectToWorldNormal(v.normal));
					float3 posWorld = mul(unity_ObjectToWorld, v.vertex);
					o.viewDir = normalize(_WorldSpaceCameraPos.xyz - posWorld.xyz);
					o.vertexColor = v.vertexColor;
					#ifdef _LightProbe_ON
					float3 worldN = mul((float3x3) unity_ObjectToWorld, SCALED_NORMAL);
					o.shLight = ShadeSH9(float4(worldN,1.0));
					#endif
					UNITY_TRANSFER_FOG(o,o.pos);
					return o;
				}

				float4 frag(VertexOutput i) : COLOR {
					fixed4 _StoneTex_var = tex2D(_StoneTex, i.uv);

					fixed3 fresnel = (1.0 - saturate(dot(i.normalDir, i.viewDir))) * _FresnelRange;
					fresnel = min(ceil(fresnel - 0.5), 1.0) * _LightColor;

					fixed NdotL = saturate(dot(normalize(_LightDir), i.normalDir));
					fixed4 darkpart = min(ceil(NdotL - 0.2) + _DarkColor, 1.0);
					fresnel *= ceil(NdotL - 0.5);

					fixed4 finalRGBA;
					finalRGBA.rgb = (_StoneTex_var + fresnel) * darkpart;

					finalRGBA.a = 1.0;
					UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
					return finalRGBA;
				}
			ENDCG
		}
	}
	Subshader {
		Tags {
			"Queue" = "Geometry"
			"RenderType" = "Opaque"
		}
		LOD 100

		Pass {
			Name "ForwardExpand"
			Tags {
				"LightMode"="ForwardBase" 
			}
			Cull Back

			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"
				#define UNITY_PASS_FORWARDBASE


				uniform sampler2D _StoneTex;
				uniform fixed4 _LightDir;
				uniform fixed4 _LightColor;
				uniform fixed4 _DarkColor;

				uniform half _FresnelRange;



				struct VertexInput {
					float4 vertex : POSITION;
					float3 normal : NORMAL;
					float2 texcoord : TEXCOORD;
					float4 vertexColor : COLOR;
				};

				struct VertexOutput {
					float4 pos : SV_POSITION;
					float2 uv : TEXCOORD0;
					float3 normalDir : TEXCOORD1;
					float3 viewDir : TEXCOORD2;
					float4 vertexColor : COLOR0;


				};

				VertexOutput vert (VertexInput v) {
					VertexOutput o = (VertexOutput)0;
					o.pos = UnityObjectToClipPos(v.vertex );
					o.uv = v.texcoord;
					o.normalDir = normalize(UnityObjectToWorldNormal(v.normal));
					float3 posWorld = mul(unity_ObjectToWorld, v.vertex);
					o.viewDir = normalize(_WorldSpaceCameraPos.xyz - posWorld.xyz);
					o.vertexColor = v.vertexColor;
					return o;
				}

				float4 frag(VertexOutput i) : COLOR {
					fixed4 _StoneTex_var = tex2D(_StoneTex, i.uv);

					fixed3 fresnel = (1.0 - saturate(dot(i.normalDir, i.viewDir))) * _FresnelRange;
					fresnel = min(ceil(fresnel - 0.5), 1.0) * _LightColor;

					fixed NdotL = saturate(dot(normalize(_LightDir), i.normalDir));
					fixed4 darkpart = min(ceil(NdotL - 0.2) + _DarkColor, 1.0);
					fresnel *= ceil(NdotL - 0.5);

					fixed4 finalRGBA;
					finalRGBA.rgb = (_StoneTex_var + fresnel) * darkpart;

					finalRGBA.a = 1.0;

					return finalRGBA;
				}
			ENDCG
		}
	}
   Fallback "Unlit/Texture"
}