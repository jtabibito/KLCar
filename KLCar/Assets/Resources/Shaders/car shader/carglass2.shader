/*
 * CarGlass2.SHADER is a part of MoDyEn (Motorsports Dynamics Engine v1.0)
 * You may not resell a part of MoDyEn,
 * Copyright (C) Ravel Tammeleht 2011 - 2012
 */
Shader "Car/CarGlass2" {
	Properties {
		_Color("Diffuse Color", Color) = (1, 1, 1,1)
		_SpecColor("Specular Color", Color) = (1,1,1,1)		
		_AmbientColor("Metalic Color", Color) = (1,1,1, 1)
		_AmbientColor2("Candy Color", Color) = (1,1,1, 1)
		_ReflectionColor("Reflection Color", Color) = (1,1,1, 1)			
		_Shininess("Glossiness", Range(0.01,2) ) = 0.5	
		_Reflect("Reflection", Range(0.0,.5) ) = 0 	
		_MainTex("Diffuse", 2D) = "white" {}	
		_Cube("Reflection Cubemap", Cube) = "" { TexGen CubeReflect }		
		_FresnelScale ("Fresnel Intensity", Range(0,4) ) = 0
		_FresnelPower ("Fresnel Power", Range(0.1,3) ) = 0		
		_MetalicScale ("Metalic Intensity", Range(0.0,4.0)) = 0
		_MetalicPower ("Metalic Power", Range(0.0,20.0)) = 0		
		_CandyScale ("Candy Intensity", Range(0.0,4.0)) = 0
		_CandyPower ("Candy Power", Range(0.0,20.0)) = 0
	}
	SubShader {		
		//Tags {"Queue"="Transparent" "IgnoreProjector"="False" "RenderType"="Transparent" }
		Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="Transparent" }
		LOD 400			
		Cull Back
		ZWrite Off
		ZTest Lequal
		Blend SrcAlpha OneMinusSrcAlpha
		ColorMask RGBA
		CGPROGRAM		
		//#pragma surface surf BlinnPhong
		#pragma surface surf BlinnPhong alpha vertex:vert fullforwardshadows approxview
		#pragma target 3.0		
		struct v2f { 
			V2F_SHADOW_CASTER; 
			float2 uv : TEXCOORD1;
		};		
		struct Input
		{
			float2 uv_MainTex;	
			float3 worldRefl;
			float3 viewDir;
			float3 normal;
			INTERNAL_DATA
		};
		v2f vert (inout appdata_full v) { 
			v2f o; 
			return o; 
		} 
		sampler2D _MainTex;
		samplerCUBE _Cube;		
		float4 _Color;		
	  	fixed4 _AmbientColor;
	  	fixed4 _AmbientColor2;
	  	float4 _ReflectionColor;	  	
		float _Shininess;
		float _Reflect;
		float _FresnelScale;
		float _FresnelPower;
		float _MetalicScale;		
		float _MetalicPower;
		float _CandyScale;		
		float _CandyPower;
		void surf (Input IN, inout SurfaceOutput o)
		{		
		
			o.Normal = normalize(float3(0.0,0.0,1.0));		
			// 0. Vectors
			float3 worldRefl = WorldReflectionVector (IN, o.Normal);
			float3 normEyeVec = normalize (IN.viewDir);
			float dotEyeVecNormal = abs(dot(normEyeVec, o.Normal));				
			
			// 1. Difuse (Texture + Color)
			float4 Tex1 = tex2D( _MainTex,IN.uv_MainTex );			
			float4 specularmask = (1-Tex1.a)+(_Reflect);
			//float4 Diffuse = ((_Color* (1-specularmask) )* Tex1) + (Tex1*(specularmask));	
			float4 Diffuse = _Color * Tex1;				
			
			// 3. Emission (Reflections with Fresnel)	
			float4 TexCUBE = texCUBE( _Cube,worldRefl) * _ReflectionColor;
			float4 fresnel = (1.0 - dot( normEyeVec,o.Normal ));
			float4 emmission = _FresnelScale * TexCUBE * pow(fresnel,_FresnelPower);			
			
			// 4. Special (Metalic & Candy)
			float metalic = (specularmask*_MetalicScale) * pow(dotEyeVecNormal,_MetalicPower);
			float candy = _CandyScale * pow(dotEyeVecNormal,_CandyPower);
			
			o.Albedo = Diffuse * ((metalic+(1-specularmask)) + (_AmbientColor.rgb*specularmask)  ) + (candy * _AmbientColor2.rgb * (specularmask));
			o.Specular = _Shininess * specularmask;						
			o.Gloss = _SpecColor * specularmask;		
			o.Alpha = Tex1.a*_Color.a;			
			o.Emission = emmission * emmission * specularmask;
		}		
		ENDCG

	} 
	FallBack "Transparent"	
}
