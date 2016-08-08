// Unlit alpha-blended shader.
// - no lighting
// - no lightmap support
// - no per-material color

Shader "My3DFont" {
Properties {

	// 添加色彩属性,主要用来提供alpha控制
	_Color ("Main Color", Color) = (1,1,1,0.5)
	_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
}

SubShader {
	Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
	LOD 100
	
	ZWrite Off
	Blend SrcAlpha OneMinusSrcAlpha 

	Pass {
	 // 在材质中添加对色彩属性的支持
	 Material {
                Diffuse [_Color]
            }
            
        // 总是显示在最前面
		Lighting Off Cull Off ZTest Always ZWrite Off Fog { Mode Off }
		
		SetTexture [_MainTex] { 
		constantColor [_Color]
		combine  texture * constant } 
	}
}
}
