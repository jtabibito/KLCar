/*
 * CarPaint2.SHADER is a part of MoDyEn (Motorsports Dynamics Engine v1.0)
 * You may not resell a part of MoDyEn,
 * Copyright (C) Ravel Tammeleht 2011 - 2012
 */
Shader "Car/CarPain2" {
	Properties {
		_Color("Diffuse Color", Color) = (1, 1, 1,1)
		_SpecColor("Specular Color", Color) = (1,1,1,1)		
		_AmbientColor("Metalic Color", Color) = (1,1,1, 1)
		_AmbientColor2("Candy Color", Color) = (1,1,1, 1)
		_ReflectionColor("Reflection Color", Color) = (1,1,1, 1)		
		_Shininess("Glossiness", Range(0.1,2) ) = 0.5		
		_MainTex("Diffuse", 2D) = "white" {}
		_FlakeTex("Flakes", 2D) = "white" {}
		_FlakeDens("Flakes Tile", Range(1,40) ) = 2
		_Cube("Reflection Cubemap", Cube) = "" { TexGen CubeReflect }		
		_FresnelScale ("Fresnel Intensity", Range(0,2) ) = 0
		_FresnelPower ("Fresnel Power", Range(0.1,3) ) = 0		
		_MetalicScale ("Metalic Intensity", Range(0.0,4.0)) = 0
		_MetalicPower ("Metalic Power", Range(0.0,20.0)) = 0		
		_CandyScale ("Candy Intensity", Range(0.0,4.0)) = 0
		_CandyPower ("Candy Power", Range(0.0,20.0)) = 0
	}
	SubShader {		
		Tags { "RenderType"="Opaque" }
		LOD 400			
		Cull Back
		ZWrite On
		ZTest Lequal
		ColorMask RGBA
		CGPROGRAM		
		#pragma surface surf BlinnPhong
		#pragma target 3.0		
		struct Input
		{
			float2 uv_MainTex;	
			float2 uv_FlakeTex;
			float3 worldRefl;
			float3 viewDir;
			float3 lightDir;
			float3 normal;
			INTERNAL_DATA
		};		
		sampler2D _MainTex;
		sampler2D _FlakeTex;
		samplerCUBE _Cube;		
		float4 _Color;		
	  	float4 _AmbientColor;
	  	float4 _AmbientColor2;
	  	float4 _ReflectionColor;	  	
		float _Shininess;
		float _FlakeDens;
		float _FresnelScale;
		float _FresnelPower;
		float _MetalicScale;		
		float _MetalicPower;
		float _CandyScale;		
		float _CandyPower;
		void surf (Input IN, inout SurfaceOutput o){		
				
			o.Normal = normalize(float3(0.0,0.0,1.0));
			// 0. Vectors
			float3 worldRefl = WorldReflectionVector (IN, o.Normal);
			float3 normEyeVec = normalize (IN.viewDir);
			float3 worldLight = normalize (IN.lightDir);
			float dotEyeVecNormal = abs(dot(normEyeVec, o.Normal));				
			
			// 1. Difuse (Texture + Color)
			float4 Tex1 = tex2D( _MainTex,IN.uv_MainTex );			
			float specularmask = Tex1.a ;
			float4 Diffuse = ((_Color* (specularmask) )* Tex1) + (Tex1*(1-specularmask));				

			// 2. GlossMap / Sparks			
			float4 Tex2 = tex2D( _FlakeTex, IN.uv_FlakeTex*_FlakeDens );	
			float4 Specular = (_Shininess * Tex2) + Tex2;	
			
			// 3. Emission (Reflections with Fresnel)		
			float4 TexCUBE = texCUBE( _Cube,worldRefl) * _ReflectionColor;
			float4 fresnel = (1.0 - dot( normEyeVec,o.Normal ));
			float4 emmission = _FresnelScale * TexCUBE * pow(fresnel,_FresnelPower);			
			
			// 4. Special (Metalic & Candy)
			float metalic = (specularmask*_MetalicScale) * pow(dotEyeVecNormal,_MetalicPower);
			float candy = _CandyScale * pow(dotEyeVecNormal,_CandyPower);
			
			o.Albedo = Diffuse * ((metalic+(1-specularmask)+((1-Tex2)*(specularmask))) + (_AmbientColor.rgb*specularmask)  ) + (candy * _AmbientColor2.rgb * specularmask*(1-Tex2));
			
			o.Specular = Specular;						
			o.Gloss = _SpecColor * specularmask;				
			o.Emission = emmission * emmission * specularmask;
		}		
		ENDCG
	} 
	FallBack "Reflective/Diffuse"
}
