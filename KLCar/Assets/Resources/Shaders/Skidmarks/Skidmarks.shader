Shader "KL/Skidmarks" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
}

Category {
	Offset -4, -4
	ZWrite Off
	Alphatest Greater 0
	Tags {"Queue"="Geometry+5"  "RenderType"="Transparent"}
	SubShader {
		ColorMaterial AmbientAndDiffuse
		Lighting Off
		Blend SrcAlpha OneMinusSrcAlpha 
		Pass {
			ColorMask RGBA
			SetTexture [_MainTex] {
				Combine texture, texture * primary
				// * primary
			} 
			SetTexture [_MainTex] {
				constantColor [_Color] Combine constant * previous
			} 
		}
	} 
}

// Fallback to Alpha Vertex Lit
Fallback "Transparent/VertexLit", 2

}