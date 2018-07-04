Shader "OP_FX/05_Additive_NoZTest" {
Properties {
	//_Color("Color",Color) = (1,1,1)
	_MainTex ("Particle Texture", 2D) = "white" {}
}

Category {
	Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
	Blend SrcAlpha One
	ZTest Off
	Cull Off Lighting Off ZWrite Off Fog { Color (0,0,0,0) }
	
	BindChannels {
		Bind "Color", color
		Bind "Vertex", vertex
		Bind "TexCoord", texcoord
	}
	
	SubShader {
		Pass {
			Color[color]
			SetTexture [_MainTex] {
				//constantColor[_Color]
				combine texture * previous
			}
		}
	}
}
}