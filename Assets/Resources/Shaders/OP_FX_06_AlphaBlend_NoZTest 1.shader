Shader "OP_FX/06_AlphaBlend_NoZTest" {
Properties {
 _MainTex ("Particle Texture", 2D) = "white" {}
}

SubShader { 
 	Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" }
 	Pass {
 			Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" }
  			BindChannels {
   				Bind "vertex", Vertex
   				Bind "color", Color
   				Bind "texcoord", TexCoord
 										  }
  	ZWrite Off
  	ZTest Off
 	Cull Off
 	Fog {
   Color (0,0,0,0)
  }
  Blend SrcAlpha OneMinusSrcAlpha
  SetTexture [_MainTex] { combine texture * primary }
 }
}
}