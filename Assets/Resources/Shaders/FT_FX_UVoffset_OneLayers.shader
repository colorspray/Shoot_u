// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "FT/FX/UVoffset/OneLayers" {
    Properties {
    	_MainColor("颜色",Color) = (1,1,1,1)
        _MainTex ("MainTex", 2D) = "white" {}
        _MainTexInt("亮度",float) = 1
        _offsetuv("位移XY UV1, ",Vector ) = (-0.2,0.2,0,0)

        [Enum(UnityEngine.Rendering.BlendMode)] _SourceBlend ("Source Blend Mode", Float) = 5  
		[Enum(UnityEngine.Rendering.BlendMode)] _DestBlend ("Dest Blend Mode", Float) = 10
		[Enum(UnityEngine.Rendering.CullMode)] _Cull ("Cull Mode", Float) = 0
		[Enum(Off,0,On,1)] _ZWrite ("ZWrite", Float) = 0
    }
Category {
Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
	Blend [_SourceBlend] [_DestBlend]
	AlphaTest Greater .01
	ColorMask RGB
	Cull [_Cull] 
	Lighting Off 
	ZWrite [_ZWrite]
    SubShader {
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            uniform fixed4 _TimeEditor;
            uniform sampler2D _MainTex; uniform fixed4 _MainTex_ST;
            uniform fixed4 _offsetuv;
             uniform fixed4 _MainColor;
             uniform fixed _MainTexInt;
            struct VertexInput {
                fixed4 vertex : POSITION;
                fixed2 texcoord0 : TEXCOORD0;
                fixed4 vertexColor : COLOR;
            };
            struct VertexOutput {
                fixed4 pos : SV_POSITION;
                fixed2 uv0 : TEXCOORD0;
                fixed4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                fixed4 TheTime = _Time + _TimeEditor;
                 o.uv0 = v.texcoord0 + TheTime.g*_offsetuv.xy;
                 o.vertexColor = v.vertexColor;
                 o.pos = UnityObjectToClipPos(v.vertex );
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                fixed4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex)) * _MainColor ;
                _MainTex_var.rgb = _MainTex_var.rgb * _MainTexInt * i.vertexColor.rgb;
                _MainTex_var.a = _MainTex_var.a * i.vertexColor.a;
                return _MainTex_var;
            }
            ENDCG
        }
    }
   }
	   Fallback "Unlit/Transparent"
}
