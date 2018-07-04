// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

//覆盖u3d的standard
Shader "Standard" {
	Properties {
		_MainColor("叠加颜色",color) = (1,1,1,1)
	}
	SubShader {
	      Pass {		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			uniform fixed4 _MainColor;
			struct VertexInput {
					fixed4 vertex : POSITION;

				};
				struct VertexOutput {
					fixed4 pos : SV_POSITION;
				};

			VertexOutput vert( VertexInput v ) {
				VertexOutput o = (VertexOutput)0;
				o.pos = UnityObjectToClipPos (v.vertex);
				return o;
			}

			fixed4 frag( VertexOutput i ) : COLOR{
				return _MainColor;
			}
				ENDCG
		}
	}
}
