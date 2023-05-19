Shader "KL/CarPaint" {
   Properties {
	  _MainTex ("Diffuse Texture", 2D) = "white" {} 
	  _Cube("Reflection Map", Cube) = "" {}
  
	  
   }
SubShader {
   Tags { "QUEUE"="Geometry" "RenderType"="Opaque" " IgnoreProjector"="False"}	  
      Pass {  
      
         Tags { "LightMode" = "ForwardBase" } // pass for 
            // 4 vertex lights, ambient light & first pixel light
 
         Program "vp" {
// Vertex combos: 8
//   opengl - ALU: 35 to 96
//   d3d9 - ALU: 35 to 96
SubProgram "opengl " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "tangent" ATTR14
Vector 13 [_WorldSpaceCameraPos]
Matrix 5 [_Object2World]
Matrix 9 [_World2Object]
"3.0-!!ARBvp1.0
# 35 ALU
PARAM c[14] = { { 0 },
		state.matrix.mvp,
		program.local[5..13] };
TEMP R0;
TEMP R1;
MOV R1.w, c[0].x;
MOV R1.xyz, vertex.attrib[14];
DP4 R0.z, R1, c[7];
DP4 R0.y, R1, c[6];
DP4 R0.x, R1, c[5];
DP3 R0.w, R0, R0;
RSQ R0.w, R0.w;
MUL result.texcoord[6].xyz, R0.w, R0;
MUL R1.xyz, vertex.normal.y, c[10];
MAD R1.xyz, vertex.normal.x, c[9], R1;
MAD R1.xyz, vertex.normal.z, c[11], R1;
ADD R1.xyz, R1, c[0].x;
DP4 R0.w, vertex.position, c[8];
DP4 R0.z, vertex.position, c[7];
DP4 R0.x, vertex.position, c[5];
DP4 R0.y, vertex.position, c[6];
MOV result.texcoord[0], R0;
ADD R0.xyz, R0, -c[13];
DP3 R0.w, R1, R1;
RSQ R0.w, R0.w;
DP3 R1.w, R0, R0;
MUL result.texcoord[1].xyz, R0.w, R1;
RSQ R0.w, R1.w;
MUL result.texcoord[4].xyz, R0.w, R0;
MOV R0.w, c[0].x;
MOV R0.xyz, vertex.normal;
MOV result.texcoord[3], vertex.texcoord[0];
DP4 result.texcoord[5].z, R0, c[7];
DP4 result.texcoord[5].y, R0, c[6];
DP4 result.texcoord[5].x, R0, c[5];
MOV result.texcoord[2].xyz, c[0].x;
DP4 result.position.w, vertex.position, c[4];
DP4 result.position.z, vertex.position, c[3];
DP4 result.position.y, vertex.position, c[2];
DP4 result.position.x, vertex.position, c[1];
END
# 35 instructions, 2 R-regs
"
}

SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "tangent" TexCoord2
Matrix 0 [glstate_matrix_mvp]
Vector 12 [_WorldSpaceCameraPos]
Matrix 4 [_Object2World]
Matrix 8 [_World2Object]
"vs_3_0
; 35 ALU
dcl_position o0
dcl_texcoord0 o1
dcl_texcoord1 o2
dcl_texcoord2 o3
dcl_texcoord3 o4
dcl_texcoord4 o5
dcl_texcoord5 o6
dcl_texcoord6 o7
def c13, 0.00000000, 0, 0, 0
dcl_position0 v0
dcl_normal0 v1
dcl_texcoord0 v2
dcl_tangent0 v3
mov r1.w, c13.x
mov r1.xyz, v3
dp4 r0.z, r1, c6
dp4 r0.y, r1, c5
dp4 r0.x, r1, c4
dp3 r0.w, r0, r0
rsq r0.w, r0.w
mul o7.xyz, r0.w, r0
mul r1.xyz, v1.y, c9
mad r1.xyz, v1.x, c8, r1
mad r1.xyz, v1.z, c10, r1
add r1.xyz, r1, c13.x
dp4 r0.w, v0, c7
dp4 r0.z, v0, c6
dp4 r0.x, v0, c4
dp4 r0.y, v0, c5
mov o1, r0
add r0.xyz, r0, -c12
dp3 r0.w, r1, r1
rsq r0.w, r0.w
dp3 r1.w, r0, r0
mul o2.xyz, r0.w, r1
rsq r0.w, r1.w
mul o5.xyz, r0.w, r0
mov r0.w, c13.x
mov r0.xyz, v1
mov o4, v2
dp4 o6.z, r0, c6
dp4 o6.y, r0, c5
dp4 o6.x, r0, c4
mov o3.xyz, c13.x
dp4 o0.w, v0, c3
dp4 o0.z, v0, c2
dp4 o0.y, v0, c1
dp4 o0.x, v0, c0
"
}

SubProgram "gles " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec3 xlv_TEXCOORD6;
varying highp vec3 xlv_TEXCOORD5;
varying highp vec3 xlv_TEXCOORD4;
varying highp vec4 xlv_TEXCOORD3;
varying highp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;

uniform highp vec3 _WorldSpaceCameraPos;
uniform highp mat4 _World2Object;
uniform highp mat4 _Object2World;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec3 tmpvar_1;
  tmpvar_1 = normalize (_glesNormal);
  vec4 tmpvar_2;
  tmpvar_2.xyz = normalize (_glesTANGENT.xyz);
  tmpvar_2.w = _glesTANGENT.w;
  highp vec4 tmpvar_3;
  tmpvar_3.w = 0.0;
  tmpvar_3.xyz = tmpvar_1;
  highp vec4 tmpvar_4;
  tmpvar_4.w = 0.0;
  tmpvar_4.xyz = tmpvar_1;
  highp vec4 tmpvar_5;
  tmpvar_5.w = 1.0;
  tmpvar_5.xyz = _WorldSpaceCameraPos;
  highp vec4 tmpvar_6;
  tmpvar_6.w = 0.0;
  tmpvar_6.xyz = tmpvar_2.xyz;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = (_Object2World * _glesVertex);
  xlv_TEXCOORD1 = normalize ((tmpvar_4 * _World2Object).xyz);
  xlv_TEXCOORD2 = vec3(0.0, 0.0, 0.0);
  xlv_TEXCOORD3 = _glesMultiTexCoord0;
  xlv_TEXCOORD4 = normalize (((_Object2World * _glesVertex) - tmpvar_5).xyz);
  xlv_TEXCOORD5 = (_Object2World * tmpvar_3).xyz;
  xlv_TEXCOORD6 = normalize ((_Object2World * tmpvar_6).xyz);
}



#endif
#ifdef FRAGMENT
#define unity_LightColor0 _glesLightSource[0].diffuse
#define unity_LightColor1 _glesLightSource[1].diffuse
#define unity_LightColor2 _glesLightSource[2].diffuse
#define unity_LightColor3 _glesLightSource[3].diffuse
#define unity_LightPosition0 _glesLightSource[0].position
#define unity_LightPosition1 _glesLightSource[1].position
#define unity_LightPosition2 _glesLightSource[2].position
#define unity_LightPosition3 _glesLightSource[3].position
#define glstate_light0_spotDirection _glesLightSource[0].spotDirection
#define glstate_light1_spotDirection _glesLightSource[1].spotDirection
#define glstate_light2_spotDirection _glesLightSource[2].spotDirection
#define glstate_light3_spotDirection _glesLightSource[3].spotDirection
#define unity_LightAtten0 _glesLightSource[0].atten
#define unity_LightAtten1 _glesLightSource[1].atten
#define unity_LightAtten2 _glesLightSource[2].atten
#define unity_LightAtten3 _glesLightSource[3].atten
#define glstate_lightmodel_ambient _glesLightModel.ambient
#define gl_LightSource _glesLightSource
#define gl_LightSourceParameters _glesLightSourceParameters
struct _glesLightSourceParameters {
  vec4 diffuse;
  vec4 position;
  vec3 spotDirection;
  vec4 atten;
};
uniform _glesLightSourceParameters _glesLightSource[4];
#define gl_LightModel _glesLightModel
#define gl_LightModelParameters _glesLightModelParameters
struct _glesLightModelParameters {
  vec4 ambient;
};
uniform _glesLightModelParameters _glesLightModel;
#define gl_FrontMaterial _glesFrontMaterial
#define gl_BackMaterial _glesFrontMaterial
#define gl_MaterialParameters _glesMaterialParameters
struct _glesMaterialParameters {
  vec4 emission;
  vec4 ambient;
  vec4 diffuse;
  vec4 specular;
  float shininess;
};
uniform _glesMaterialParameters _glesFrontMaterial;

varying highp vec3 xlv_TEXCOORD4;
varying highp vec4 xlv_TEXCOORD3;
varying highp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;

uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp vec3 _WorldSpaceCameraPos;
uniform sampler2D _MainTex;
uniform highp vec4 _LightColor0;
uniform samplerCube _Cube;
void main ()
{
  highp vec4 tmpvar_1;
  lowp vec4 color;
  lowp float SurfAngle;
  highp vec4 reflTex;
  highp vec3 specularReflection;
  highp vec3 lightDirection;
  highp vec4 textureColor;
  highp vec3 tmpvar_2;
  tmpvar_2 = normalize (xlv_TEXCOORD1);
  highp vec3 tmpvar_3;
  tmpvar_3 = normalize ((_WorldSpaceCameraPos - xlv_TEXCOORD0.xyz));
  highp vec2 tmpvar_4;
  tmpvar_4 = xlv_TEXCOORD3.xy;
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_MainTex, tmpvar_4);
  textureColor = tmpvar_5;
  if ((0.0 == _WorldSpaceLightPos0.w)) {
    lowp vec3 tmpvar_6;
    tmpvar_6 = normalize (_WorldSpaceLightPos0.xyz);
    lightDirection = tmpvar_6;
  } else {
    lightDirection = normalize ((_WorldSpaceLightPos0 - xlv_TEXCOORD0).xyz);
  };
  highp vec3 tmpvar_7;
  tmpvar_7 = (_LightColor0.xyz * max (0.0, dot (tmpvar_2, lightDirection)));
  highp float tmpvar_8;
  tmpvar_8 = dot (tmpvar_2, lightDirection);
  if ((tmpvar_8 < 0.0)) {
    specularReflection = vec3(0.0, 0.0, 0.0);
  } else {
    specularReflection = (_LightColor0.xyz * pow (max (0.0, dot (reflect (-(lightDirection), tmpvar_2), tmpvar_3)), 0.06));
  };
  highp vec3 tmpvar_9;
  tmpvar_9 = (specularReflection * 0.1);
  specularReflection = tmpvar_9;
  highp vec3 tmpvar_10;
  tmpvar_10 = reflect (xlv_TEXCOORD4, normalize (xlv_TEXCOORD1));
  lowp vec4 tmpvar_11;
  tmpvar_11 = textureCube (_Cube, tmpvar_10);
  reflTex = tmpvar_11;
  highp float tmpvar_12;
  tmpvar_12 = clamp (abs (dot (tmpvar_10, xlv_TEXCOORD1)), 0.0, 1.0);
  SurfAngle = tmpvar_12;
  lowp float tmpvar_13;
  tmpvar_13 = (pow ((1.0 - SurfAngle), 2.51) * 0.64);
  lowp float tmpvar_14;
  tmpvar_14 = clamp ((0.07 + tmpvar_13), 0.0, 1.0);
  reflTex.xyz = (reflTex.xyz * tmpvar_14);
  highp vec4 tmpvar_15;
  tmpvar_15.w = 1.0;
  tmpvar_15.xyz = ((textureColor.xyz * clamp (((xlv_TEXCOORD2 + gl_LightModel.ambient.xyz) + tmpvar_7), 0.0, 1.0)) + tmpvar_9);
  color = tmpvar_15;
  highp vec4 tmpvar_16;
  tmpvar_16 = (color + reflTex);
  color = tmpvar_16;
  highp vec4 tmpvar_17;
  tmpvar_17 = (color + (tmpvar_13 * reflTex));
  color = tmpvar_17;
  tmpvar_1 = color;
  gl_FragData[0] = tmpvar_1;
}



#endif"
}

SubProgram "glesdesktop " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec3 xlv_TEXCOORD6;
varying highp vec3 xlv_TEXCOORD5;
varying highp vec3 xlv_TEXCOORD4;
varying highp vec4 xlv_TEXCOORD3;
varying highp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;

uniform highp vec3 _WorldSpaceCameraPos;
uniform highp mat4 _World2Object;
uniform highp mat4 _Object2World;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec3 tmpvar_1;
  tmpvar_1 = normalize (_glesNormal);
  vec4 tmpvar_2;
  tmpvar_2.xyz = normalize (_glesTANGENT.xyz);
  tmpvar_2.w = _glesTANGENT.w;
  highp vec4 tmpvar_3;
  tmpvar_3.w = 0.0;
  tmpvar_3.xyz = tmpvar_1;
  highp vec4 tmpvar_4;
  tmpvar_4.w = 0.0;
  tmpvar_4.xyz = tmpvar_1;
  highp vec4 tmpvar_5;
  tmpvar_5.w = 1.0;
  tmpvar_5.xyz = _WorldSpaceCameraPos;
  highp vec4 tmpvar_6;
  tmpvar_6.w = 0.0;
  tmpvar_6.xyz = tmpvar_2.xyz;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = (_Object2World * _glesVertex);
  xlv_TEXCOORD1 = normalize ((tmpvar_4 * _World2Object).xyz);
  xlv_TEXCOORD2 = vec3(0.0, 0.0, 0.0);
  xlv_TEXCOORD3 = _glesMultiTexCoord0;
  xlv_TEXCOORD4 = normalize (((_Object2World * _glesVertex) - tmpvar_5).xyz);
  xlv_TEXCOORD5 = (_Object2World * tmpvar_3).xyz;
  xlv_TEXCOORD6 = normalize ((_Object2World * tmpvar_6).xyz);
}



#endif
#ifdef FRAGMENT
#define unity_LightColor0 _glesLightSource[0].diffuse
#define unity_LightColor1 _glesLightSource[1].diffuse
#define unity_LightColor2 _glesLightSource[2].diffuse
#define unity_LightColor3 _glesLightSource[3].diffuse
#define unity_LightPosition0 _glesLightSource[0].position
#define unity_LightPosition1 _glesLightSource[1].position
#define unity_LightPosition2 _glesLightSource[2].position
#define unity_LightPosition3 _glesLightSource[3].position
#define glstate_light0_spotDirection _glesLightSource[0].spotDirection
#define glstate_light1_spotDirection _glesLightSource[1].spotDirection
#define glstate_light2_spotDirection _glesLightSource[2].spotDirection
#define glstate_light3_spotDirection _glesLightSource[3].spotDirection
#define unity_LightAtten0 _glesLightSource[0].atten
#define unity_LightAtten1 _glesLightSource[1].atten
#define unity_LightAtten2 _glesLightSource[2].atten
#define unity_LightAtten3 _glesLightSource[3].atten
#define glstate_lightmodel_ambient _glesLightModel.ambient
#define gl_LightSource _glesLightSource
#define gl_LightSourceParameters _glesLightSourceParameters
struct _glesLightSourceParameters {
  vec4 diffuse;
  vec4 position;
  vec3 spotDirection;
  vec4 atten;
};
uniform _glesLightSourceParameters _glesLightSource[4];
#define gl_LightModel _glesLightModel
#define gl_LightModelParameters _glesLightModelParameters
struct _glesLightModelParameters {
  vec4 ambient;
};
uniform _glesLightModelParameters _glesLightModel;
#define gl_FrontMaterial _glesFrontMaterial
#define gl_BackMaterial _glesFrontMaterial
#define gl_MaterialParameters _glesMaterialParameters
struct _glesMaterialParameters {
  vec4 emission;
  vec4 ambient;
  vec4 diffuse;
  vec4 specular;
  float shininess;
};
uniform _glesMaterialParameters _glesFrontMaterial;

varying highp vec3 xlv_TEXCOORD4;
varying highp vec4 xlv_TEXCOORD3;
varying highp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;

uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp vec3 _WorldSpaceCameraPos;
uniform sampler2D _MainTex;
uniform highp vec4 _LightColor0;
uniform samplerCube _Cube;
void main ()
{
  highp vec4 tmpvar_1;
  lowp vec4 color;
  lowp float SurfAngle;
  highp vec4 reflTex;
  highp vec3 specularReflection;
  highp vec3 lightDirection;
  highp vec4 textureColor;
  highp vec3 tmpvar_2;
  tmpvar_2 = normalize (xlv_TEXCOORD1);
  highp vec3 tmpvar_3;
  tmpvar_3 = normalize ((_WorldSpaceCameraPos - xlv_TEXCOORD0.xyz));
  highp vec2 tmpvar_4;
  tmpvar_4 = xlv_TEXCOORD3.xy;
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_MainTex, tmpvar_4);
  textureColor = tmpvar_5;
  if ((0.0 == _WorldSpaceLightPos0.w)) {
    lowp vec3 tmpvar_6;
    tmpvar_6 = normalize (_WorldSpaceLightPos0.xyz);
    lightDirection = tmpvar_6;
  } else {
    lightDirection = normalize ((_WorldSpaceLightPos0 - xlv_TEXCOORD0).xyz);
  };
  highp vec3 tmpvar_7;
  tmpvar_7 = (_LightColor0.xyz * max (0.0, dot (tmpvar_2, lightDirection)));
  highp float tmpvar_8;
  tmpvar_8 = dot (tmpvar_2, lightDirection);
  if ((tmpvar_8 < 0.0)) {
    specularReflection = vec3(0.0, 0.0, 0.0);
  } else {
    specularReflection = (_LightColor0.xyz * pow (max (0.0, dot (reflect (-(lightDirection), tmpvar_2), tmpvar_3)), 0.06));
  };
  highp vec3 tmpvar_9;
  tmpvar_9 = (specularReflection * 0.1);
  specularReflection = tmpvar_9;
  highp vec3 tmpvar_10;
  tmpvar_10 = reflect (xlv_TEXCOORD4, normalize (xlv_TEXCOORD1));
  lowp vec4 tmpvar_11;
  tmpvar_11 = textureCube (_Cube, tmpvar_10);
  reflTex = tmpvar_11;
  highp float tmpvar_12;
  tmpvar_12 = clamp (abs (dot (tmpvar_10, xlv_TEXCOORD1)), 0.0, 1.0);
  SurfAngle = tmpvar_12;
  lowp float tmpvar_13;
  tmpvar_13 = (pow ((1.0 - SurfAngle), 2.51) * 0.64);
  lowp float tmpvar_14;
  tmpvar_14 = clamp ((0.07 + tmpvar_13), 0.0, 1.0);
  reflTex.xyz = (reflTex.xyz * tmpvar_14);
  highp vec4 tmpvar_15;
  tmpvar_15.w = 1.0;
  tmpvar_15.xyz = ((textureColor.xyz * clamp (((xlv_TEXCOORD2 + gl_LightModel.ambient.xyz) + tmpvar_7), 0.0, 1.0)) + tmpvar_9);
  color = tmpvar_15;
  highp vec4 tmpvar_16;
  tmpvar_16 = (color + reflTex);
  color = tmpvar_16;
  highp vec4 tmpvar_17;
  tmpvar_17 = (color + (tmpvar_13 * reflTex));
  color = tmpvar_17;
  tmpvar_1 = color;
  gl_FragData[0] = tmpvar_1;
}



#endif"
}

SubProgram "opengl " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "tangent" ATTR14
Vector 13 [_WorldSpaceCameraPos]
Matrix 5 [_Object2World]
Matrix 9 [_World2Object]
"3.0-!!ARBvp1.0
# 35 ALU
PARAM c[14] = { { 0 },
		state.matrix.mvp,
		program.local[5..13] };
TEMP R0;
TEMP R1;
MOV R1.w, c[0].x;
MOV R1.xyz, vertex.attrib[14];
DP4 R0.z, R1, c[7];
DP4 R0.y, R1, c[6];
DP4 R0.x, R1, c[5];
DP3 R0.w, R0, R0;
RSQ R0.w, R0.w;
MUL result.texcoord[6].xyz, R0.w, R0;
MUL R1.xyz, vertex.normal.y, c[10];
MAD R1.xyz, vertex.normal.x, c[9], R1;
MAD R1.xyz, vertex.normal.z, c[11], R1;
ADD R1.xyz, R1, c[0].x;
DP4 R0.w, vertex.position, c[8];
DP4 R0.z, vertex.position, c[7];
DP4 R0.x, vertex.position, c[5];
DP4 R0.y, vertex.position, c[6];
MOV result.texcoord[0], R0;
ADD R0.xyz, R0, -c[13];
DP3 R0.w, R1, R1;
RSQ R0.w, R0.w;
DP3 R1.w, R0, R0;
MUL result.texcoord[1].xyz, R0.w, R1;
RSQ R0.w, R1.w;
MUL result.texcoord[4].xyz, R0.w, R0;
MOV R0.w, c[0].x;
MOV R0.xyz, vertex.normal;
MOV result.texcoord[3], vertex.texcoord[0];
DP4 result.texcoord[5].z, R0, c[7];
DP4 result.texcoord[5].y, R0, c[6];
DP4 result.texcoord[5].x, R0, c[5];
MOV result.texcoord[2].xyz, c[0].x;
DP4 result.position.w, vertex.position, c[4];
DP4 result.position.z, vertex.position, c[3];
DP4 result.position.y, vertex.position, c[2];
DP4 result.position.x, vertex.position, c[1];
END
# 35 instructions, 2 R-regs
"
}

SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "tangent" TexCoord2
Matrix 0 [glstate_matrix_mvp]
Vector 12 [_WorldSpaceCameraPos]
Matrix 4 [_Object2World]
Matrix 8 [_World2Object]
"vs_3_0
; 35 ALU
dcl_position o0
dcl_texcoord0 o1
dcl_texcoord1 o2
dcl_texcoord2 o3
dcl_texcoord3 o4
dcl_texcoord4 o5
dcl_texcoord5 o6
dcl_texcoord6 o7
def c13, 0.00000000, 0, 0, 0
dcl_position0 v0
dcl_normal0 v1
dcl_texcoord0 v2
dcl_tangent0 v3
mov r1.w, c13.x
mov r1.xyz, v3
dp4 r0.z, r1, c6
dp4 r0.y, r1, c5
dp4 r0.x, r1, c4
dp3 r0.w, r0, r0
rsq r0.w, r0.w
mul o7.xyz, r0.w, r0
mul r1.xyz, v1.y, c9
mad r1.xyz, v1.x, c8, r1
mad r1.xyz, v1.z, c10, r1
add r1.xyz, r1, c13.x
dp4 r0.w, v0, c7
dp4 r0.z, v0, c6
dp4 r0.x, v0, c4
dp4 r0.y, v0, c5
mov o1, r0
add r0.xyz, r0, -c12
dp3 r0.w, r1, r1
rsq r0.w, r0.w
dp3 r1.w, r0, r0
mul o2.xyz, r0.w, r1
rsq r0.w, r1.w
mul o5.xyz, r0.w, r0
mov r0.w, c13.x
mov r0.xyz, v1
mov o4, v2
dp4 o6.z, r0, c6
dp4 o6.y, r0, c5
dp4 o6.x, r0, c4
mov o3.xyz, c13.x
dp4 o0.w, v0, c3
dp4 o0.z, v0, c2
dp4 o0.y, v0, c1
dp4 o0.x, v0, c0
"
}

SubProgram "gles " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec3 xlv_TEXCOORD6;
varying highp vec3 xlv_TEXCOORD5;
varying highp vec3 xlv_TEXCOORD4;
varying highp vec4 xlv_TEXCOORD3;
varying highp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;

uniform highp vec3 _WorldSpaceCameraPos;
uniform highp mat4 _World2Object;
uniform highp mat4 _Object2World;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec3 tmpvar_1;
  tmpvar_1 = normalize (_glesNormal);
  vec4 tmpvar_2;
  tmpvar_2.xyz = normalize (_glesTANGENT.xyz);
  tmpvar_2.w = _glesTANGENT.w;
  highp vec4 tmpvar_3;
  tmpvar_3.w = 0.0;
  tmpvar_3.xyz = tmpvar_1;
  highp vec4 tmpvar_4;
  tmpvar_4.w = 0.0;
  tmpvar_4.xyz = tmpvar_1;
  highp vec4 tmpvar_5;
  tmpvar_5.w = 1.0;
  tmpvar_5.xyz = _WorldSpaceCameraPos;
  highp vec4 tmpvar_6;
  tmpvar_6.w = 0.0;
  tmpvar_6.xyz = tmpvar_2.xyz;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = (_Object2World * _glesVertex);
  xlv_TEXCOORD1 = normalize ((tmpvar_4 * _World2Object).xyz);
  xlv_TEXCOORD2 = vec3(0.0, 0.0, 0.0);
  xlv_TEXCOORD3 = _glesMultiTexCoord0;
  xlv_TEXCOORD4 = normalize (((_Object2World * _glesVertex) - tmpvar_5).xyz);
  xlv_TEXCOORD5 = (_Object2World * tmpvar_3).xyz;
  xlv_TEXCOORD6 = normalize ((_Object2World * tmpvar_6).xyz);
}



#endif
#ifdef FRAGMENT
#define unity_LightColor0 _glesLightSource[0].diffuse
#define unity_LightColor1 _glesLightSource[1].diffuse
#define unity_LightColor2 _glesLightSource[2].diffuse
#define unity_LightColor3 _glesLightSource[3].diffuse
#define unity_LightPosition0 _glesLightSource[0].position
#define unity_LightPosition1 _glesLightSource[1].position
#define unity_LightPosition2 _glesLightSource[2].position
#define unity_LightPosition3 _glesLightSource[3].position
#define glstate_light0_spotDirection _glesLightSource[0].spotDirection
#define glstate_light1_spotDirection _glesLightSource[1].spotDirection
#define glstate_light2_spotDirection _glesLightSource[2].spotDirection
#define glstate_light3_spotDirection _glesLightSource[3].spotDirection
#define unity_LightAtten0 _glesLightSource[0].atten
#define unity_LightAtten1 _glesLightSource[1].atten
#define unity_LightAtten2 _glesLightSource[2].atten
#define unity_LightAtten3 _glesLightSource[3].atten
#define glstate_lightmodel_ambient _glesLightModel.ambient
#define gl_LightSource _glesLightSource
#define gl_LightSourceParameters _glesLightSourceParameters
struct _glesLightSourceParameters {
  vec4 diffuse;
  vec4 position;
  vec3 spotDirection;
  vec4 atten;
};
uniform _glesLightSourceParameters _glesLightSource[4];
#define gl_LightModel _glesLightModel
#define gl_LightModelParameters _glesLightModelParameters
struct _glesLightModelParameters {
  vec4 ambient;
};
uniform _glesLightModelParameters _glesLightModel;
#define gl_FrontMaterial _glesFrontMaterial
#define gl_BackMaterial _glesFrontMaterial
#define gl_MaterialParameters _glesMaterialParameters
struct _glesMaterialParameters {
  vec4 emission;
  vec4 ambient;
  vec4 diffuse;
  vec4 specular;
  float shininess;
};
uniform _glesMaterialParameters _glesFrontMaterial;

varying highp vec3 xlv_TEXCOORD4;
varying highp vec4 xlv_TEXCOORD3;
varying highp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;

uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp vec3 _WorldSpaceCameraPos;
uniform sampler2D _MainTex;
uniform highp vec4 _LightColor0;
uniform samplerCube _Cube;
void main ()
{
  highp vec4 tmpvar_1;
  lowp vec4 color;
  lowp float SurfAngle;
  highp vec4 reflTex;
  highp vec3 specularReflection;
  highp vec3 lightDirection;
  highp vec4 textureColor;
  highp vec3 tmpvar_2;
  tmpvar_2 = normalize (xlv_TEXCOORD1);
  highp vec3 tmpvar_3;
  tmpvar_3 = normalize ((_WorldSpaceCameraPos - xlv_TEXCOORD0.xyz));
  highp vec2 tmpvar_4;
  tmpvar_4 = xlv_TEXCOORD3.xy;
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_MainTex, tmpvar_4);
  textureColor = tmpvar_5;
  if ((0.0 == _WorldSpaceLightPos0.w)) {
    lowp vec3 tmpvar_6;
    tmpvar_6 = normalize (_WorldSpaceLightPos0.xyz);
    lightDirection = tmpvar_6;
  } else {
    lightDirection = normalize ((_WorldSpaceLightPos0 - xlv_TEXCOORD0).xyz);
  };
  highp vec3 tmpvar_7;
  tmpvar_7 = (_LightColor0.xyz * max (0.0, dot (tmpvar_2, lightDirection)));
  highp float tmpvar_8;
  tmpvar_8 = dot (tmpvar_2, lightDirection);
  if ((tmpvar_8 < 0.0)) {
    specularReflection = vec3(0.0, 0.0, 0.0);
  } else {
    specularReflection = (_LightColor0.xyz * pow (max (0.0, dot (reflect (-(lightDirection), tmpvar_2), tmpvar_3)), 0.06));
  };
  highp vec3 tmpvar_9;
  tmpvar_9 = (specularReflection * 0.1);
  specularReflection = tmpvar_9;
  highp vec3 tmpvar_10;
  tmpvar_10 = reflect (xlv_TEXCOORD4, normalize (xlv_TEXCOORD1));
  lowp vec4 tmpvar_11;
  tmpvar_11 = textureCube (_Cube, tmpvar_10);
  reflTex = tmpvar_11;
  highp float tmpvar_12;
  tmpvar_12 = clamp (abs (dot (tmpvar_10, xlv_TEXCOORD1)), 0.0, 1.0);
  SurfAngle = tmpvar_12;
  lowp float tmpvar_13;
  tmpvar_13 = (pow ((1.0 - SurfAngle), 2.51) * 0.64);
  lowp float tmpvar_14;
  tmpvar_14 = clamp ((0.07 + tmpvar_13), 0.0, 1.0);
  reflTex.xyz = (reflTex.xyz * tmpvar_14);
  highp vec4 tmpvar_15;
  tmpvar_15.w = 1.0;
  tmpvar_15.xyz = ((textureColor.xyz * clamp (((xlv_TEXCOORD2 + gl_LightModel.ambient.xyz) + tmpvar_7), 0.0, 1.0)) + tmpvar_9);
  color = tmpvar_15;
  highp vec4 tmpvar_16;
  tmpvar_16 = (color + reflTex);
  color = tmpvar_16;
  highp vec4 tmpvar_17;
  tmpvar_17 = (color + (tmpvar_13 * reflTex));
  color = tmpvar_17;
  tmpvar_1 = color;
  gl_FragData[0] = tmpvar_1;
}



#endif"
}

SubProgram "glesdesktop " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec3 xlv_TEXCOORD6;
varying highp vec3 xlv_TEXCOORD5;
varying highp vec3 xlv_TEXCOORD4;
varying highp vec4 xlv_TEXCOORD3;
varying highp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;

uniform highp vec3 _WorldSpaceCameraPos;
uniform highp mat4 _World2Object;
uniform highp mat4 _Object2World;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec3 tmpvar_1;
  tmpvar_1 = normalize (_glesNormal);
  vec4 tmpvar_2;
  tmpvar_2.xyz = normalize (_glesTANGENT.xyz);
  tmpvar_2.w = _glesTANGENT.w;
  highp vec4 tmpvar_3;
  tmpvar_3.w = 0.0;
  tmpvar_3.xyz = tmpvar_1;
  highp vec4 tmpvar_4;
  tmpvar_4.w = 0.0;
  tmpvar_4.xyz = tmpvar_1;
  highp vec4 tmpvar_5;
  tmpvar_5.w = 1.0;
  tmpvar_5.xyz = _WorldSpaceCameraPos;
  highp vec4 tmpvar_6;
  tmpvar_6.w = 0.0;
  tmpvar_6.xyz = tmpvar_2.xyz;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = (_Object2World * _glesVertex);
  xlv_TEXCOORD1 = normalize ((tmpvar_4 * _World2Object).xyz);
  xlv_TEXCOORD2 = vec3(0.0, 0.0, 0.0);
  xlv_TEXCOORD3 = _glesMultiTexCoord0;
  xlv_TEXCOORD4 = normalize (((_Object2World * _glesVertex) - tmpvar_5).xyz);
  xlv_TEXCOORD5 = (_Object2World * tmpvar_3).xyz;
  xlv_TEXCOORD6 = normalize ((_Object2World * tmpvar_6).xyz);
}



#endif
#ifdef FRAGMENT
#define unity_LightColor0 _glesLightSource[0].diffuse
#define unity_LightColor1 _glesLightSource[1].diffuse
#define unity_LightColor2 _glesLightSource[2].diffuse
#define unity_LightColor3 _glesLightSource[3].diffuse
#define unity_LightPosition0 _glesLightSource[0].position
#define unity_LightPosition1 _glesLightSource[1].position
#define unity_LightPosition2 _glesLightSource[2].position
#define unity_LightPosition3 _glesLightSource[3].position
#define glstate_light0_spotDirection _glesLightSource[0].spotDirection
#define glstate_light1_spotDirection _glesLightSource[1].spotDirection
#define glstate_light2_spotDirection _glesLightSource[2].spotDirection
#define glstate_light3_spotDirection _glesLightSource[3].spotDirection
#define unity_LightAtten0 _glesLightSource[0].atten
#define unity_LightAtten1 _glesLightSource[1].atten
#define unity_LightAtten2 _glesLightSource[2].atten
#define unity_LightAtten3 _glesLightSource[3].atten
#define glstate_lightmodel_ambient _glesLightModel.ambient
#define gl_LightSource _glesLightSource
#define gl_LightSourceParameters _glesLightSourceParameters
struct _glesLightSourceParameters {
  vec4 diffuse;
  vec4 position;
  vec3 spotDirection;
  vec4 atten;
};
uniform _glesLightSourceParameters _glesLightSource[4];
#define gl_LightModel _glesLightModel
#define gl_LightModelParameters _glesLightModelParameters
struct _glesLightModelParameters {
  vec4 ambient;
};
uniform _glesLightModelParameters _glesLightModel;
#define gl_FrontMaterial _glesFrontMaterial
#define gl_BackMaterial _glesFrontMaterial
#define gl_MaterialParameters _glesMaterialParameters
struct _glesMaterialParameters {
  vec4 emission;
  vec4 ambient;
  vec4 diffuse;
  vec4 specular;
  float shininess;
};
uniform _glesMaterialParameters _glesFrontMaterial;

varying highp vec3 xlv_TEXCOORD4;
varying highp vec4 xlv_TEXCOORD3;
varying highp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;

uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp vec3 _WorldSpaceCameraPos;
uniform sampler2D _MainTex;
uniform highp vec4 _LightColor0;
uniform samplerCube _Cube;
void main ()
{
  highp vec4 tmpvar_1;
  lowp vec4 color;
  lowp float SurfAngle;
  highp vec4 reflTex;
  highp vec3 specularReflection;
  highp vec3 lightDirection;
  highp vec4 textureColor;
  highp vec3 tmpvar_2;
  tmpvar_2 = normalize (xlv_TEXCOORD1);
  highp vec3 tmpvar_3;
  tmpvar_3 = normalize ((_WorldSpaceCameraPos - xlv_TEXCOORD0.xyz));
  highp vec2 tmpvar_4;
  tmpvar_4 = xlv_TEXCOORD3.xy;
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_MainTex, tmpvar_4);
  textureColor = tmpvar_5;
  if ((0.0 == _WorldSpaceLightPos0.w)) {
    lowp vec3 tmpvar_6;
    tmpvar_6 = normalize (_WorldSpaceLightPos0.xyz);
    lightDirection = tmpvar_6;
  } else {
    lightDirection = normalize ((_WorldSpaceLightPos0 - xlv_TEXCOORD0).xyz);
  };
  highp vec3 tmpvar_7;
  tmpvar_7 = (_LightColor0.xyz * max (0.0, dot (tmpvar_2, lightDirection)));
  highp float tmpvar_8;
  tmpvar_8 = dot (tmpvar_2, lightDirection);
  if ((tmpvar_8 < 0.0)) {
    specularReflection = vec3(0.0, 0.0, 0.0);
  } else {
    specularReflection = (_LightColor0.xyz * pow (max (0.0, dot (reflect (-(lightDirection), tmpvar_2), tmpvar_3)), 0.06));
  };
  highp vec3 tmpvar_9;
  tmpvar_9 = (specularReflection * 0.1);
  specularReflection = tmpvar_9;
  highp vec3 tmpvar_10;
  tmpvar_10 = reflect (xlv_TEXCOORD4, normalize (xlv_TEXCOORD1));
  lowp vec4 tmpvar_11;
  tmpvar_11 = textureCube (_Cube, tmpvar_10);
  reflTex = tmpvar_11;
  highp float tmpvar_12;
  tmpvar_12 = clamp (abs (dot (tmpvar_10, xlv_TEXCOORD1)), 0.0, 1.0);
  SurfAngle = tmpvar_12;
  lowp float tmpvar_13;
  tmpvar_13 = (pow ((1.0 - SurfAngle), 2.51) * 0.64);
  lowp float tmpvar_14;
  tmpvar_14 = clamp ((0.07 + tmpvar_13), 0.0, 1.0);
  reflTex.xyz = (reflTex.xyz * tmpvar_14);
  highp vec4 tmpvar_15;
  tmpvar_15.w = 1.0;
  tmpvar_15.xyz = ((textureColor.xyz * clamp (((xlv_TEXCOORD2 + gl_LightModel.ambient.xyz) + tmpvar_7), 0.0, 1.0)) + tmpvar_9);
  color = tmpvar_15;
  highp vec4 tmpvar_16;
  tmpvar_16 = (color + reflTex);
  color = tmpvar_16;
  highp vec4 tmpvar_17;
  tmpvar_17 = (color + (tmpvar_13 * reflTex));
  color = tmpvar_17;
  tmpvar_1 = color;
  gl_FragData[0] = tmpvar_1;
}



#endif"
}

SubProgram "opengl " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "SHADOWS_OFF" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "tangent" ATTR14
Vector 13 [_WorldSpaceCameraPos]
Matrix 5 [_Object2World]
Matrix 9 [_World2Object]
"3.0-!!ARBvp1.0
# 35 ALU
PARAM c[14] = { { 0 },
		state.matrix.mvp,
		program.local[5..13] };
TEMP R0;
TEMP R1;
MOV R1.w, c[0].x;
MOV R1.xyz, vertex.attrib[14];
DP4 R0.z, R1, c[7];
DP4 R0.y, R1, c[6];
DP4 R0.x, R1, c[5];
DP3 R0.w, R0, R0;
RSQ R0.w, R0.w;
MUL result.texcoord[6].xyz, R0.w, R0;
MUL R1.xyz, vertex.normal.y, c[10];
MAD R1.xyz, vertex.normal.x, c[9], R1;
MAD R1.xyz, vertex.normal.z, c[11], R1;
ADD R1.xyz, R1, c[0].x;
DP4 R0.w, vertex.position, c[8];
DP4 R0.z, vertex.position, c[7];
DP4 R0.x, vertex.position, c[5];
DP4 R0.y, vertex.position, c[6];
MOV result.texcoord[0], R0;
ADD R0.xyz, R0, -c[13];
DP3 R0.w, R1, R1;
RSQ R0.w, R0.w;
DP3 R1.w, R0, R0;
MUL result.texcoord[1].xyz, R0.w, R1;
RSQ R0.w, R1.w;
MUL result.texcoord[4].xyz, R0.w, R0;
MOV R0.w, c[0].x;
MOV R0.xyz, vertex.normal;
MOV result.texcoord[3], vertex.texcoord[0];
DP4 result.texcoord[5].z, R0, c[7];
DP4 result.texcoord[5].y, R0, c[6];
DP4 result.texcoord[5].x, R0, c[5];
MOV result.texcoord[2].xyz, c[0].x;
DP4 result.position.w, vertex.position, c[4];
DP4 result.position.z, vertex.position, c[3];
DP4 result.position.y, vertex.position, c[2];
DP4 result.position.x, vertex.position, c[1];
END
# 35 instructions, 2 R-regs
"
}

SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "SHADOWS_OFF" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "tangent" TexCoord2
Matrix 0 [glstate_matrix_mvp]
Vector 12 [_WorldSpaceCameraPos]
Matrix 4 [_Object2World]
Matrix 8 [_World2Object]
"vs_3_0
; 35 ALU
dcl_position o0
dcl_texcoord0 o1
dcl_texcoord1 o2
dcl_texcoord2 o3
dcl_texcoord3 o4
dcl_texcoord4 o5
dcl_texcoord5 o6
dcl_texcoord6 o7
def c13, 0.00000000, 0, 0, 0
dcl_position0 v0
dcl_normal0 v1
dcl_texcoord0 v2
dcl_tangent0 v3
mov r1.w, c13.x
mov r1.xyz, v3
dp4 r0.z, r1, c6
dp4 r0.y, r1, c5
dp4 r0.x, r1, c4
dp3 r0.w, r0, r0
rsq r0.w, r0.w
mul o7.xyz, r0.w, r0
mul r1.xyz, v1.y, c9
mad r1.xyz, v1.x, c8, r1
mad r1.xyz, v1.z, c10, r1
add r1.xyz, r1, c13.x
dp4 r0.w, v0, c7
dp4 r0.z, v0, c6
dp4 r0.x, v0, c4
dp4 r0.y, v0, c5
mov o1, r0
add r0.xyz, r0, -c12
dp3 r0.w, r1, r1
rsq r0.w, r0.w
dp3 r1.w, r0, r0
mul o2.xyz, r0.w, r1
rsq r0.w, r1.w
mul o5.xyz, r0.w, r0
mov r0.w, c13.x
mov r0.xyz, v1
mov o4, v2
dp4 o6.z, r0, c6
dp4 o6.y, r0, c5
dp4 o6.x, r0, c4
mov o3.xyz, c13.x
dp4 o0.w, v0, c3
dp4 o0.z, v0, c2
dp4 o0.y, v0, c1
dp4 o0.x, v0, c0
"
}

SubProgram "gles " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "SHADOWS_OFF" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec3 xlv_TEXCOORD6;
varying highp vec3 xlv_TEXCOORD5;
varying highp vec3 xlv_TEXCOORD4;
varying highp vec4 xlv_TEXCOORD3;
varying highp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;

uniform highp vec3 _WorldSpaceCameraPos;
uniform highp mat4 _World2Object;
uniform highp mat4 _Object2World;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec3 tmpvar_1;
  tmpvar_1 = normalize (_glesNormal);
  vec4 tmpvar_2;
  tmpvar_2.xyz = normalize (_glesTANGENT.xyz);
  tmpvar_2.w = _glesTANGENT.w;
  highp vec4 tmpvar_3;
  tmpvar_3.w = 0.0;
  tmpvar_3.xyz = tmpvar_1;
  highp vec4 tmpvar_4;
  tmpvar_4.w = 0.0;
  tmpvar_4.xyz = tmpvar_1;
  highp vec4 tmpvar_5;
  tmpvar_5.w = 1.0;
  tmpvar_5.xyz = _WorldSpaceCameraPos;
  highp vec4 tmpvar_6;
  tmpvar_6.w = 0.0;
  tmpvar_6.xyz = tmpvar_2.xyz;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = (_Object2World * _glesVertex);
  xlv_TEXCOORD1 = normalize ((tmpvar_4 * _World2Object).xyz);
  xlv_TEXCOORD2 = vec3(0.0, 0.0, 0.0);
  xlv_TEXCOORD3 = _glesMultiTexCoord0;
  xlv_TEXCOORD4 = normalize (((_Object2World * _glesVertex) - tmpvar_5).xyz);
  xlv_TEXCOORD5 = (_Object2World * tmpvar_3).xyz;
  xlv_TEXCOORD6 = normalize ((_Object2World * tmpvar_6).xyz);
}



#endif
#ifdef FRAGMENT
#define unity_LightColor0 _glesLightSource[0].diffuse
#define unity_LightColor1 _glesLightSource[1].diffuse
#define unity_LightColor2 _glesLightSource[2].diffuse
#define unity_LightColor3 _glesLightSource[3].diffuse
#define unity_LightPosition0 _glesLightSource[0].position
#define unity_LightPosition1 _glesLightSource[1].position
#define unity_LightPosition2 _glesLightSource[2].position
#define unity_LightPosition3 _glesLightSource[3].position
#define glstate_light0_spotDirection _glesLightSource[0].spotDirection
#define glstate_light1_spotDirection _glesLightSource[1].spotDirection
#define glstate_light2_spotDirection _glesLightSource[2].spotDirection
#define glstate_light3_spotDirection _glesLightSource[3].spotDirection
#define unity_LightAtten0 _glesLightSource[0].atten
#define unity_LightAtten1 _glesLightSource[1].atten
#define unity_LightAtten2 _glesLightSource[2].atten
#define unity_LightAtten3 _glesLightSource[3].atten
#define glstate_lightmodel_ambient _glesLightModel.ambient
#define gl_LightSource _glesLightSource
#define gl_LightSourceParameters _glesLightSourceParameters
struct _glesLightSourceParameters {
  vec4 diffuse;
  vec4 position;
  vec3 spotDirection;
  vec4 atten;
};
uniform _glesLightSourceParameters _glesLightSource[4];
#define gl_LightModel _glesLightModel
#define gl_LightModelParameters _glesLightModelParameters
struct _glesLightModelParameters {
  vec4 ambient;
};
uniform _glesLightModelParameters _glesLightModel;
#define gl_FrontMaterial _glesFrontMaterial
#define gl_BackMaterial _glesFrontMaterial
#define gl_MaterialParameters _glesMaterialParameters
struct _glesMaterialParameters {
  vec4 emission;
  vec4 ambient;
  vec4 diffuse;
  vec4 specular;
  float shininess;
};
uniform _glesMaterialParameters _glesFrontMaterial;

varying highp vec3 xlv_TEXCOORD4;
varying highp vec4 xlv_TEXCOORD3;
varying highp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;

uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp vec3 _WorldSpaceCameraPos;
uniform sampler2D _MainTex;
uniform highp vec4 _LightColor0;
uniform samplerCube _Cube;
void main ()
{
  highp vec4 tmpvar_1;
  lowp vec4 color;
  lowp float SurfAngle;
  highp vec4 reflTex;
  highp vec3 specularReflection;
  highp vec3 lightDirection;
  highp vec4 textureColor;
  highp vec3 tmpvar_2;
  tmpvar_2 = normalize (xlv_TEXCOORD1);
  highp vec3 tmpvar_3;
  tmpvar_3 = normalize ((_WorldSpaceCameraPos - xlv_TEXCOORD0.xyz));
  highp vec2 tmpvar_4;
  tmpvar_4 = xlv_TEXCOORD3.xy;
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_MainTex, tmpvar_4);
  textureColor = tmpvar_5;
  if ((0.0 == _WorldSpaceLightPos0.w)) {
    lowp vec3 tmpvar_6;
    tmpvar_6 = normalize (_WorldSpaceLightPos0.xyz);
    lightDirection = tmpvar_6;
  } else {
    lightDirection = normalize ((_WorldSpaceLightPos0 - xlv_TEXCOORD0).xyz);
  };
  highp vec3 tmpvar_7;
  tmpvar_7 = (_LightColor0.xyz * max (0.0, dot (tmpvar_2, lightDirection)));
  highp float tmpvar_8;
  tmpvar_8 = dot (tmpvar_2, lightDirection);
  if ((tmpvar_8 < 0.0)) {
    specularReflection = vec3(0.0, 0.0, 0.0);
  } else {
    specularReflection = (_LightColor0.xyz * pow (max (0.0, dot (reflect (-(lightDirection), tmpvar_2), tmpvar_3)), 0.06));
  };
  highp vec3 tmpvar_9;
  tmpvar_9 = (specularReflection * 0.1);
  specularReflection = tmpvar_9;
  highp vec3 tmpvar_10;
  tmpvar_10 = reflect (xlv_TEXCOORD4, normalize (xlv_TEXCOORD1));
  lowp vec4 tmpvar_11;
  tmpvar_11 = textureCube (_Cube, tmpvar_10);
  reflTex = tmpvar_11;
  highp float tmpvar_12;
  tmpvar_12 = clamp (abs (dot (tmpvar_10, xlv_TEXCOORD1)), 0.0, 1.0);
  SurfAngle = tmpvar_12;
  lowp float tmpvar_13;
  tmpvar_13 = (pow ((1.0 - SurfAngle), 2.51) * 0.64);
  lowp float tmpvar_14;
  tmpvar_14 = clamp ((0.07 + tmpvar_13), 0.0, 1.0);
  reflTex.xyz = (reflTex.xyz * tmpvar_14);
  highp vec4 tmpvar_15;
  tmpvar_15.w = 1.0;
  tmpvar_15.xyz = ((textureColor.xyz * clamp (((xlv_TEXCOORD2 + gl_LightModel.ambient.xyz) + tmpvar_7), 0.0, 1.0)) + tmpvar_9);
  color = tmpvar_15;
  highp vec4 tmpvar_16;
  tmpvar_16 = (color + reflTex);
  color = tmpvar_16;
  highp vec4 tmpvar_17;
  tmpvar_17 = (color + (tmpvar_13 * reflTex));
  color = tmpvar_17;
  tmpvar_1 = color;
  gl_FragData[0] = tmpvar_1;
}



#endif"
}

SubProgram "glesdesktop " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "SHADOWS_OFF" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec3 xlv_TEXCOORD6;
varying highp vec3 xlv_TEXCOORD5;
varying highp vec3 xlv_TEXCOORD4;
varying highp vec4 xlv_TEXCOORD3;
varying highp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;

uniform highp vec3 _WorldSpaceCameraPos;
uniform highp mat4 _World2Object;
uniform highp mat4 _Object2World;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec3 tmpvar_1;
  tmpvar_1 = normalize (_glesNormal);
  vec4 tmpvar_2;
  tmpvar_2.xyz = normalize (_glesTANGENT.xyz);
  tmpvar_2.w = _glesTANGENT.w;
  highp vec4 tmpvar_3;
  tmpvar_3.w = 0.0;
  tmpvar_3.xyz = tmpvar_1;
  highp vec4 tmpvar_4;
  tmpvar_4.w = 0.0;
  tmpvar_4.xyz = tmpvar_1;
  highp vec4 tmpvar_5;
  tmpvar_5.w = 1.0;
  tmpvar_5.xyz = _WorldSpaceCameraPos;
  highp vec4 tmpvar_6;
  tmpvar_6.w = 0.0;
  tmpvar_6.xyz = tmpvar_2.xyz;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = (_Object2World * _glesVertex);
  xlv_TEXCOORD1 = normalize ((tmpvar_4 * _World2Object).xyz);
  xlv_TEXCOORD2 = vec3(0.0, 0.0, 0.0);
  xlv_TEXCOORD3 = _glesMultiTexCoord0;
  xlv_TEXCOORD4 = normalize (((_Object2World * _glesVertex) - tmpvar_5).xyz);
  xlv_TEXCOORD5 = (_Object2World * tmpvar_3).xyz;
  xlv_TEXCOORD6 = normalize ((_Object2World * tmpvar_6).xyz);
}



#endif
#ifdef FRAGMENT
#define unity_LightColor0 _glesLightSource[0].diffuse
#define unity_LightColor1 _glesLightSource[1].diffuse
#define unity_LightColor2 _glesLightSource[2].diffuse
#define unity_LightColor3 _glesLightSource[3].diffuse
#define unity_LightPosition0 _glesLightSource[0].position
#define unity_LightPosition1 _glesLightSource[1].position
#define unity_LightPosition2 _glesLightSource[2].position
#define unity_LightPosition3 _glesLightSource[3].position
#define glstate_light0_spotDirection _glesLightSource[0].spotDirection
#define glstate_light1_spotDirection _glesLightSource[1].spotDirection
#define glstate_light2_spotDirection _glesLightSource[2].spotDirection
#define glstate_light3_spotDirection _glesLightSource[3].spotDirection
#define unity_LightAtten0 _glesLightSource[0].atten
#define unity_LightAtten1 _glesLightSource[1].atten
#define unity_LightAtten2 _glesLightSource[2].atten
#define unity_LightAtten3 _glesLightSource[3].atten
#define glstate_lightmodel_ambient _glesLightModel.ambient
#define gl_LightSource _glesLightSource
#define gl_LightSourceParameters _glesLightSourceParameters
struct _glesLightSourceParameters {
  vec4 diffuse;
  vec4 position;
  vec3 spotDirection;
  vec4 atten;
};
uniform _glesLightSourceParameters _glesLightSource[4];
#define gl_LightModel _glesLightModel
#define gl_LightModelParameters _glesLightModelParameters
struct _glesLightModelParameters {
  vec4 ambient;
};
uniform _glesLightModelParameters _glesLightModel;
#define gl_FrontMaterial _glesFrontMaterial
#define gl_BackMaterial _glesFrontMaterial
#define gl_MaterialParameters _glesMaterialParameters
struct _glesMaterialParameters {
  vec4 emission;
  vec4 ambient;
  vec4 diffuse;
  vec4 specular;
  float shininess;
};
uniform _glesMaterialParameters _glesFrontMaterial;

varying highp vec3 xlv_TEXCOORD4;
varying highp vec4 xlv_TEXCOORD3;
varying highp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;

uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp vec3 _WorldSpaceCameraPos;
uniform sampler2D _MainTex;
uniform highp vec4 _LightColor0;
uniform samplerCube _Cube;
void main ()
{
  highp vec4 tmpvar_1;
  lowp vec4 color;
  lowp float SurfAngle;
  highp vec4 reflTex;
  highp vec3 specularReflection;
  highp vec3 lightDirection;
  highp vec4 textureColor;
  highp vec3 tmpvar_2;
  tmpvar_2 = normalize (xlv_TEXCOORD1);
  highp vec3 tmpvar_3;
  tmpvar_3 = normalize ((_WorldSpaceCameraPos - xlv_TEXCOORD0.xyz));
  highp vec2 tmpvar_4;
  tmpvar_4 = xlv_TEXCOORD3.xy;
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_MainTex, tmpvar_4);
  textureColor = tmpvar_5;
  if ((0.0 == _WorldSpaceLightPos0.w)) {
    lowp vec3 tmpvar_6;
    tmpvar_6 = normalize (_WorldSpaceLightPos0.xyz);
    lightDirection = tmpvar_6;
  } else {
    lightDirection = normalize ((_WorldSpaceLightPos0 - xlv_TEXCOORD0).xyz);
  };
  highp vec3 tmpvar_7;
  tmpvar_7 = (_LightColor0.xyz * max (0.0, dot (tmpvar_2, lightDirection)));
  highp float tmpvar_8;
  tmpvar_8 = dot (tmpvar_2, lightDirection);
  if ((tmpvar_8 < 0.0)) {
    specularReflection = vec3(0.0, 0.0, 0.0);
  } else {
    specularReflection = (_LightColor0.xyz * pow (max (0.0, dot (reflect (-(lightDirection), tmpvar_2), tmpvar_3)), 0.06));
  };
  highp vec3 tmpvar_9;
  tmpvar_9 = (specularReflection * 0.1);
  specularReflection = tmpvar_9;
  highp vec3 tmpvar_10;
  tmpvar_10 = reflect (xlv_TEXCOORD4, normalize (xlv_TEXCOORD1));
  lowp vec4 tmpvar_11;
  tmpvar_11 = textureCube (_Cube, tmpvar_10);
  reflTex = tmpvar_11;
  highp float tmpvar_12;
  tmpvar_12 = clamp (abs (dot (tmpvar_10, xlv_TEXCOORD1)), 0.0, 1.0);
  SurfAngle = tmpvar_12;
  lowp float tmpvar_13;
  tmpvar_13 = (pow ((1.0 - SurfAngle), 2.51) * 0.64);
  lowp float tmpvar_14;
  tmpvar_14 = clamp ((0.07 + tmpvar_13), 0.0, 1.0);
  reflTex.xyz = (reflTex.xyz * tmpvar_14);
  highp vec4 tmpvar_15;
  tmpvar_15.w = 1.0;
  tmpvar_15.xyz = ((textureColor.xyz * clamp (((xlv_TEXCOORD2 + gl_LightModel.ambient.xyz) + tmpvar_7), 0.0, 1.0)) + tmpvar_9);
  color = tmpvar_15;
  highp vec4 tmpvar_16;
  tmpvar_16 = (color + reflTex);
  color = tmpvar_16;
  highp vec4 tmpvar_17;
  tmpvar_17 = (color + (tmpvar_13 * reflTex));
  color = tmpvar_17;
  tmpvar_1 = color;
  gl_FragData[0] = tmpvar_1;
}



#endif"
}

SubProgram "opengl " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "tangent" ATTR14
Vector 13 [_ProjectionParams]
Vector 14 [_WorldSpaceCameraPos]
Matrix 5 [_Object2World]
Matrix 9 [_World2Object]
"3.0-!!ARBvp1.0
# 40 ALU
PARAM c[15] = { { 0, 0.5 },
		state.matrix.mvp,
		program.local[5..14] };
TEMP R0;
TEMP R1;
TEMP R2;
MOV R1.w, c[0].x;
MOV R1.xyz, vertex.attrib[14];
DP4 R0.z, R1, c[7];
DP4 R0.x, R1, c[5];
DP4 R0.y, R1, c[6];
DP3 R0.w, R0, R0;
RSQ R0.w, R0.w;
MUL result.texcoord[6].xyz, R0.w, R0;
MUL R0.xyz, vertex.normal.y, c[10];
MAD R0.xyz, vertex.normal.x, c[9], R0;
MAD R0.xyz, vertex.normal.z, c[11], R0;
DP4 R2.w, vertex.position, c[4];
DP4 R2.z, vertex.position, c[3];
DP4 R2.x, vertex.position, c[1];
DP4 R2.y, vertex.position, c[2];
MUL R1.xyz, R2.xyww, c[0].y;
MUL R1.y, R1, c[13].x;
ADD result.texcoord[7].xy, R1, R1.z;
ADD R1.xyz, R0, c[0].x;
DP4 R0.w, vertex.position, c[8];
DP4 R0.z, vertex.position, c[7];
DP4 R0.x, vertex.position, c[5];
DP4 R0.y, vertex.position, c[6];
MOV result.texcoord[0], R0;
ADD R0.xyz, R0, -c[14];
DP3 R0.w, R1, R1;
RSQ R0.w, R0.w;
DP3 R1.w, R0, R0;
MUL result.texcoord[1].xyz, R0.w, R1;
RSQ R0.w, R1.w;
MUL result.texcoord[4].xyz, R0.w, R0;
MOV R0.w, c[0].x;
MOV R0.xyz, vertex.normal;
MOV result.position, R2;
MOV result.texcoord[3], vertex.texcoord[0];
DP4 result.texcoord[5].z, R0, c[7];
DP4 result.texcoord[5].y, R0, c[6];
DP4 result.texcoord[5].x, R0, c[5];
MOV result.texcoord[7].zw, R2;
MOV result.texcoord[2].xyz, c[0].x;
END
# 40 instructions, 3 R-regs
"
}

SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "tangent" TexCoord2
Matrix 0 [glstate_matrix_mvp]
Vector 12 [_ProjectionParams]
Vector 13 [_ScreenParams]
Vector 14 [_WorldSpaceCameraPos]
Matrix 4 [_Object2World]
Matrix 8 [_World2Object]
"vs_3_0
; 40 ALU
dcl_position o0
dcl_texcoord0 o1
dcl_texcoord1 o2
dcl_texcoord2 o3
dcl_texcoord3 o4
dcl_texcoord4 o5
dcl_texcoord5 o6
dcl_texcoord6 o7
dcl_texcoord7 o8
def c15, 0.00000000, 0.50000000, 0, 0
dcl_position0 v0
dcl_normal0 v1
dcl_texcoord0 v2
dcl_tangent0 v3
mov r1.w, c15.x
mov r1.xyz, v3
dp4 r0.z, r1, c6
dp4 r0.x, r1, c4
dp4 r0.y, r1, c5
dp3 r0.w, r0, r0
rsq r0.w, r0.w
mul o7.xyz, r0.w, r0
mul r0.xyz, v1.y, c9
mad r0.xyz, v1.x, c8, r0
mad r0.xyz, v1.z, c10, r0
dp4 r2.w, v0, c3
dp4 r2.z, v0, c2
dp4 r2.x, v0, c0
dp4 r2.y, v0, c1
mul r1.xyz, r2.xyww, c15.y
mul r1.y, r1, c12.x
mad o8.xy, r1.z, c13.zwzw, r1
add r1.xyz, r0, c15.x
dp4 r0.w, v0, c7
dp4 r0.z, v0, c6
dp4 r0.x, v0, c4
dp4 r0.y, v0, c5
mov o1, r0
add r0.xyz, r0, -c14
dp3 r0.w, r1, r1
rsq r0.w, r0.w
dp3 r1.w, r0, r0
mul o2.xyz, r0.w, r1
rsq r0.w, r1.w
mul o5.xyz, r0.w, r0
mov r0.w, c15.x
mov r0.xyz, v1
mov o0, r2
mov o4, v2
dp4 o6.z, r0, c6
dp4 o6.y, r0, c5
dp4 o6.x, r0, c4
mov o8.zw, r2
mov o3.xyz, c15.x
"
}

SubProgram "gles " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec4 xlv_TEXCOORD7;
varying highp vec3 xlv_TEXCOORD6;
varying highp vec3 xlv_TEXCOORD5;
varying highp vec3 xlv_TEXCOORD4;
varying highp vec4 xlv_TEXCOORD3;
varying highp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;

uniform highp vec3 _WorldSpaceCameraPos;
uniform highp mat4 _World2Object;
uniform highp vec4 _ProjectionParams;
uniform highp mat4 _Object2World;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec3 tmpvar_1;
  tmpvar_1 = normalize (_glesNormal);
  vec4 tmpvar_2;
  tmpvar_2.xyz = normalize (_glesTANGENT.xyz);
  tmpvar_2.w = _glesTANGENT.w;
  highp vec4 tmpvar_3;
  tmpvar_3.w = 0.0;
  tmpvar_3.xyz = tmpvar_1;
  highp vec4 tmpvar_4;
  tmpvar_4.w = 0.0;
  tmpvar_4.xyz = tmpvar_1;
  highp vec4 tmpvar_5;
  tmpvar_5 = (gl_ModelViewProjectionMatrix * _glesVertex);
  highp vec4 tmpvar_6;
  tmpvar_6.w = 1.0;
  tmpvar_6.xyz = _WorldSpaceCameraPos;
  highp vec4 tmpvar_7;
  tmpvar_7.w = 0.0;
  tmpvar_7.xyz = tmpvar_2.xyz;
  highp vec4 o_i0;
  highp vec4 tmpvar_8;
  tmpvar_8 = (tmpvar_5 * 0.5);
  o_i0 = tmpvar_8;
  highp vec2 tmpvar_9;
  tmpvar_9.x = tmpvar_8.x;
  tmpvar_9.y = (tmpvar_8.y * _ProjectionParams.x);
  o_i0.xy = (tmpvar_9 + tmpvar_8.w);
  o_i0.zw = tmpvar_5.zw;
  gl_Position = tmpvar_5;
  xlv_TEXCOORD0 = (_Object2World * _glesVertex);
  xlv_TEXCOORD1 = normalize ((tmpvar_4 * _World2Object).xyz);
  xlv_TEXCOORD2 = vec3(0.0, 0.0, 0.0);
  xlv_TEXCOORD3 = _glesMultiTexCoord0;
  xlv_TEXCOORD4 = normalize (((_Object2World * _glesVertex) - tmpvar_6).xyz);
  xlv_TEXCOORD5 = (_Object2World * tmpvar_3).xyz;
  xlv_TEXCOORD6 = normalize ((_Object2World * tmpvar_7).xyz);
  xlv_TEXCOORD7 = o_i0;
}



#endif
#ifdef FRAGMENT
#define unity_LightColor0 _glesLightSource[0].diffuse
#define unity_LightColor1 _glesLightSource[1].diffuse
#define unity_LightColor2 _glesLightSource[2].diffuse
#define unity_LightColor3 _glesLightSource[3].diffuse
#define unity_LightPosition0 _glesLightSource[0].position
#define unity_LightPosition1 _glesLightSource[1].position
#define unity_LightPosition2 _glesLightSource[2].position
#define unity_LightPosition3 _glesLightSource[3].position
#define glstate_light0_spotDirection _glesLightSource[0].spotDirection
#define glstate_light1_spotDirection _glesLightSource[1].spotDirection
#define glstate_light2_spotDirection _glesLightSource[2].spotDirection
#define glstate_light3_spotDirection _glesLightSource[3].spotDirection
#define unity_LightAtten0 _glesLightSource[0].atten
#define unity_LightAtten1 _glesLightSource[1].atten
#define unity_LightAtten2 _glesLightSource[2].atten
#define unity_LightAtten3 _glesLightSource[3].atten
#define glstate_lightmodel_ambient _glesLightModel.ambient
#define gl_LightSource _glesLightSource
#define gl_LightSourceParameters _glesLightSourceParameters
struct _glesLightSourceParameters {
  vec4 diffuse;
  vec4 position;
  vec3 spotDirection;
  vec4 atten;
};
uniform _glesLightSourceParameters _glesLightSource[4];
#define gl_LightModel _glesLightModel
#define gl_LightModelParameters _glesLightModelParameters
struct _glesLightModelParameters {
  vec4 ambient;
};
uniform _glesLightModelParameters _glesLightModel;
#define gl_FrontMaterial _glesFrontMaterial
#define gl_BackMaterial _glesFrontMaterial
#define gl_MaterialParameters _glesMaterialParameters
struct _glesMaterialParameters {
  vec4 emission;
  vec4 ambient;
  vec4 diffuse;
  vec4 specular;
  float shininess;
};
uniform _glesMaterialParameters _glesFrontMaterial;

varying highp vec4 xlv_TEXCOORD7;
varying highp vec3 xlv_TEXCOORD4;
varying highp vec4 xlv_TEXCOORD3;
varying highp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;

uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp vec3 _WorldSpaceCameraPos;
uniform sampler2D _ShadowMapTexture;
uniform sampler2D _MainTex;
uniform highp vec4 _LightColor0;
uniform samplerCube _Cube;
void main ()
{
  highp vec4 tmpvar_1;
  lowp vec4 color;
  lowp float SurfAngle;
  highp vec4 reflTex;
  highp vec3 specularReflection;
  highp vec3 lightDirection;
  highp float attenuation;
  highp vec4 textureColor;
  highp vec3 tmpvar_2;
  tmpvar_2 = normalize (xlv_TEXCOORD1);
  highp vec3 tmpvar_3;
  tmpvar_3 = normalize ((_WorldSpaceCameraPos - xlv_TEXCOORD0.xyz));
  highp vec2 tmpvar_4;
  tmpvar_4 = xlv_TEXCOORD3.xy;
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_MainTex, tmpvar_4);
  textureColor = tmpvar_5;
  if ((0.0 == _WorldSpaceLightPos0.w)) {
    attenuation = 1.0;
    lowp vec3 tmpvar_6;
    tmpvar_6 = normalize (_WorldSpaceLightPos0.xyz);
    lightDirection = tmpvar_6;
  } else {
    highp vec3 tmpvar_7;
    tmpvar_7 = (_WorldSpaceLightPos0 - xlv_TEXCOORD0).xyz;
    attenuation = (1.0/(length (tmpvar_7)));
    lightDirection = normalize (tmpvar_7);
  };
  lowp float tmpvar_8;
  tmpvar_8 = texture2DProj (_ShadowMapTexture, xlv_TEXCOORD7).x;
  attenuation = tmpvar_8;
  highp vec3 tmpvar_9;
  tmpvar_9 = ((attenuation * _LightColor0.xyz) * max (0.0, dot (tmpvar_2, lightDirection)));
  highp float tmpvar_10;
  tmpvar_10 = dot (tmpvar_2, lightDirection);
  if ((tmpvar_10 < 0.0)) {
    specularReflection = vec3(0.0, 0.0, 0.0);
  } else {
    specularReflection = ((attenuation * _LightColor0.xyz) * pow (max (0.0, dot (reflect (-(lightDirection), tmpvar_2), tmpvar_3)), 0.06));
  };
  highp vec3 tmpvar_11;
  tmpvar_11 = (specularReflection * 0.1);
  specularReflection = tmpvar_11;
  highp vec3 tmpvar_12;
  tmpvar_12 = reflect (xlv_TEXCOORD4, normalize (xlv_TEXCOORD1));
  lowp vec4 tmpvar_13;
  tmpvar_13 = textureCube (_Cube, tmpvar_12);
  reflTex = tmpvar_13;
  highp float tmpvar_14;
  tmpvar_14 = clamp (abs (dot (tmpvar_12, xlv_TEXCOORD1)), 0.0, 1.0);
  SurfAngle = tmpvar_14;
  lowp float tmpvar_15;
  tmpvar_15 = (pow ((1.0 - SurfAngle), 2.51) * 0.64);
  lowp float tmpvar_16;
  tmpvar_16 = clamp ((0.07 + tmpvar_15), 0.0, 1.0);
  reflTex.xyz = (reflTex.xyz * tmpvar_16);
  highp vec4 tmpvar_17;
  tmpvar_17.w = 1.0;
  tmpvar_17.xyz = ((textureColor.xyz * clamp (((xlv_TEXCOORD2 + gl_LightModel.ambient.xyz) + tmpvar_9), 0.0, 1.0)) + tmpvar_11);
  color = tmpvar_17;
  highp vec4 tmpvar_18;
  tmpvar_18 = (color + reflTex);
  color = tmpvar_18;
  highp vec4 tmpvar_19;
  tmpvar_19 = (color + (tmpvar_15 * reflTex));
  color = tmpvar_19;
  tmpvar_1 = color;
  gl_FragData[0] = tmpvar_1;
}



#endif"
}

SubProgram "glesdesktop " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec4 xlv_TEXCOORD7;
varying highp vec3 xlv_TEXCOORD6;
varying highp vec3 xlv_TEXCOORD5;
varying highp vec3 xlv_TEXCOORD4;
varying highp vec4 xlv_TEXCOORD3;
varying highp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;

uniform highp vec3 _WorldSpaceCameraPos;
uniform highp mat4 _World2Object;
uniform highp vec4 _ProjectionParams;
uniform highp mat4 _Object2World;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec3 tmpvar_1;
  tmpvar_1 = normalize (_glesNormal);
  vec4 tmpvar_2;
  tmpvar_2.xyz = normalize (_glesTANGENT.xyz);
  tmpvar_2.w = _glesTANGENT.w;
  highp vec4 tmpvar_3;
  tmpvar_3.w = 0.0;
  tmpvar_3.xyz = tmpvar_1;
  highp vec4 tmpvar_4;
  tmpvar_4.w = 0.0;
  tmpvar_4.xyz = tmpvar_1;
  highp vec4 tmpvar_5;
  tmpvar_5 = (gl_ModelViewProjectionMatrix * _glesVertex);
  highp vec4 tmpvar_6;
  tmpvar_6.w = 1.0;
  tmpvar_6.xyz = _WorldSpaceCameraPos;
  highp vec4 tmpvar_7;
  tmpvar_7.w = 0.0;
  tmpvar_7.xyz = tmpvar_2.xyz;
  highp vec4 o_i0;
  highp vec4 tmpvar_8;
  tmpvar_8 = (tmpvar_5 * 0.5);
  o_i0 = tmpvar_8;
  highp vec2 tmpvar_9;
  tmpvar_9.x = tmpvar_8.x;
  tmpvar_9.y = (tmpvar_8.y * _ProjectionParams.x);
  o_i0.xy = (tmpvar_9 + tmpvar_8.w);
  o_i0.zw = tmpvar_5.zw;
  gl_Position = tmpvar_5;
  xlv_TEXCOORD0 = (_Object2World * _glesVertex);
  xlv_TEXCOORD1 = normalize ((tmpvar_4 * _World2Object).xyz);
  xlv_TEXCOORD2 = vec3(0.0, 0.0, 0.0);
  xlv_TEXCOORD3 = _glesMultiTexCoord0;
  xlv_TEXCOORD4 = normalize (((_Object2World * _glesVertex) - tmpvar_6).xyz);
  xlv_TEXCOORD5 = (_Object2World * tmpvar_3).xyz;
  xlv_TEXCOORD6 = normalize ((_Object2World * tmpvar_7).xyz);
  xlv_TEXCOORD7 = o_i0;
}



#endif
#ifdef FRAGMENT
#define unity_LightColor0 _glesLightSource[0].diffuse
#define unity_LightColor1 _glesLightSource[1].diffuse
#define unity_LightColor2 _glesLightSource[2].diffuse
#define unity_LightColor3 _glesLightSource[3].diffuse
#define unity_LightPosition0 _glesLightSource[0].position
#define unity_LightPosition1 _glesLightSource[1].position
#define unity_LightPosition2 _glesLightSource[2].position
#define unity_LightPosition3 _glesLightSource[3].position
#define glstate_light0_spotDirection _glesLightSource[0].spotDirection
#define glstate_light1_spotDirection _glesLightSource[1].spotDirection
#define glstate_light2_spotDirection _glesLightSource[2].spotDirection
#define glstate_light3_spotDirection _glesLightSource[3].spotDirection
#define unity_LightAtten0 _glesLightSource[0].atten
#define unity_LightAtten1 _glesLightSource[1].atten
#define unity_LightAtten2 _glesLightSource[2].atten
#define unity_LightAtten3 _glesLightSource[3].atten
#define glstate_lightmodel_ambient _glesLightModel.ambient
#define gl_LightSource _glesLightSource
#define gl_LightSourceParameters _glesLightSourceParameters
struct _glesLightSourceParameters {
  vec4 diffuse;
  vec4 position;
  vec3 spotDirection;
  vec4 atten;
};
uniform _glesLightSourceParameters _glesLightSource[4];
#define gl_LightModel _glesLightModel
#define gl_LightModelParameters _glesLightModelParameters
struct _glesLightModelParameters {
  vec4 ambient;
};
uniform _glesLightModelParameters _glesLightModel;
#define gl_FrontMaterial _glesFrontMaterial
#define gl_BackMaterial _glesFrontMaterial
#define gl_MaterialParameters _glesMaterialParameters
struct _glesMaterialParameters {
  vec4 emission;
  vec4 ambient;
  vec4 diffuse;
  vec4 specular;
  float shininess;
};
uniform _glesMaterialParameters _glesFrontMaterial;

varying highp vec4 xlv_TEXCOORD7;
varying highp vec3 xlv_TEXCOORD4;
varying highp vec4 xlv_TEXCOORD3;
varying highp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;

uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp vec3 _WorldSpaceCameraPos;
uniform sampler2D _ShadowMapTexture;
uniform sampler2D _MainTex;
uniform highp vec4 _LightColor0;
uniform samplerCube _Cube;
void main ()
{
  highp vec4 tmpvar_1;
  lowp vec4 color;
  lowp float SurfAngle;
  highp vec4 reflTex;
  highp vec3 specularReflection;
  highp vec3 lightDirection;
  highp float attenuation;
  highp vec4 textureColor;
  highp vec3 tmpvar_2;
  tmpvar_2 = normalize (xlv_TEXCOORD1);
  highp vec3 tmpvar_3;
  tmpvar_3 = normalize ((_WorldSpaceCameraPos - xlv_TEXCOORD0.xyz));
  highp vec2 tmpvar_4;
  tmpvar_4 = xlv_TEXCOORD3.xy;
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_MainTex, tmpvar_4);
  textureColor = tmpvar_5;
  if ((0.0 == _WorldSpaceLightPos0.w)) {
    attenuation = 1.0;
    lowp vec3 tmpvar_6;
    tmpvar_6 = normalize (_WorldSpaceLightPos0.xyz);
    lightDirection = tmpvar_6;
  } else {
    highp vec3 tmpvar_7;
    tmpvar_7 = (_WorldSpaceLightPos0 - xlv_TEXCOORD0).xyz;
    attenuation = (1.0/(length (tmpvar_7)));
    lightDirection = normalize (tmpvar_7);
  };
  lowp float tmpvar_8;
  tmpvar_8 = texture2DProj (_ShadowMapTexture, xlv_TEXCOORD7).x;
  attenuation = tmpvar_8;
  highp vec3 tmpvar_9;
  tmpvar_9 = ((attenuation * _LightColor0.xyz) * max (0.0, dot (tmpvar_2, lightDirection)));
  highp float tmpvar_10;
  tmpvar_10 = dot (tmpvar_2, lightDirection);
  if ((tmpvar_10 < 0.0)) {
    specularReflection = vec3(0.0, 0.0, 0.0);
  } else {
    specularReflection = ((attenuation * _LightColor0.xyz) * pow (max (0.0, dot (reflect (-(lightDirection), tmpvar_2), tmpvar_3)), 0.06));
  };
  highp vec3 tmpvar_11;
  tmpvar_11 = (specularReflection * 0.1);
  specularReflection = tmpvar_11;
  highp vec3 tmpvar_12;
  tmpvar_12 = reflect (xlv_TEXCOORD4, normalize (xlv_TEXCOORD1));
  lowp vec4 tmpvar_13;
  tmpvar_13 = textureCube (_Cube, tmpvar_12);
  reflTex = tmpvar_13;
  highp float tmpvar_14;
  tmpvar_14 = clamp (abs (dot (tmpvar_12, xlv_TEXCOORD1)), 0.0, 1.0);
  SurfAngle = tmpvar_14;
  lowp float tmpvar_15;
  tmpvar_15 = (pow ((1.0 - SurfAngle), 2.51) * 0.64);
  lowp float tmpvar_16;
  tmpvar_16 = clamp ((0.07 + tmpvar_15), 0.0, 1.0);
  reflTex.xyz = (reflTex.xyz * tmpvar_16);
  highp vec4 tmpvar_17;
  tmpvar_17.w = 1.0;
  tmpvar_17.xyz = ((textureColor.xyz * clamp (((xlv_TEXCOORD2 + gl_LightModel.ambient.xyz) + tmpvar_9), 0.0, 1.0)) + tmpvar_11);
  color = tmpvar_17;
  highp vec4 tmpvar_18;
  tmpvar_18 = (color + reflTex);
  color = tmpvar_18;
  highp vec4 tmpvar_19;
  tmpvar_19 = (color + (tmpvar_15 * reflTex));
  color = tmpvar_19;
  tmpvar_1 = color;
  gl_FragData[0] = tmpvar_1;
}



#endif"
}

SubProgram "opengl " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "tangent" ATTR14
Vector 13 [_ProjectionParams]
Vector 14 [_WorldSpaceCameraPos]
Matrix 5 [_Object2World]
Matrix 9 [_World2Object]
"3.0-!!ARBvp1.0
# 40 ALU
PARAM c[15] = { { 0, 0.5 },
		state.matrix.mvp,
		program.local[5..14] };
TEMP R0;
TEMP R1;
TEMP R2;
MOV R1.w, c[0].x;
MOV R1.xyz, vertex.attrib[14];
DP4 R0.z, R1, c[7];
DP4 R0.x, R1, c[5];
DP4 R0.y, R1, c[6];
DP3 R0.w, R0, R0;
RSQ R0.w, R0.w;
MUL result.texcoord[6].xyz, R0.w, R0;
MUL R0.xyz, vertex.normal.y, c[10];
MAD R0.xyz, vertex.normal.x, c[9], R0;
MAD R0.xyz, vertex.normal.z, c[11], R0;
DP4 R2.w, vertex.position, c[4];
DP4 R2.z, vertex.position, c[3];
DP4 R2.x, vertex.position, c[1];
DP4 R2.y, vertex.position, c[2];
MUL R1.xyz, R2.xyww, c[0].y;
MUL R1.y, R1, c[13].x;
ADD result.texcoord[7].xy, R1, R1.z;
ADD R1.xyz, R0, c[0].x;
DP4 R0.w, vertex.position, c[8];
DP4 R0.z, vertex.position, c[7];
DP4 R0.x, vertex.position, c[5];
DP4 R0.y, vertex.position, c[6];
MOV result.texcoord[0], R0;
ADD R0.xyz, R0, -c[14];
DP3 R0.w, R1, R1;
RSQ R0.w, R0.w;
DP3 R1.w, R0, R0;
MUL result.texcoord[1].xyz, R0.w, R1;
RSQ R0.w, R1.w;
MUL result.texcoord[4].xyz, R0.w, R0;
MOV R0.w, c[0].x;
MOV R0.xyz, vertex.normal;
MOV result.position, R2;
MOV result.texcoord[3], vertex.texcoord[0];
DP4 result.texcoord[5].z, R0, c[7];
DP4 result.texcoord[5].y, R0, c[6];
DP4 result.texcoord[5].x, R0, c[5];
MOV result.texcoord[7].zw, R2;
MOV result.texcoord[2].xyz, c[0].x;
END
# 40 instructions, 3 R-regs
"
}

SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "tangent" TexCoord2
Matrix 0 [glstate_matrix_mvp]
Vector 12 [_ProjectionParams]
Vector 13 [_ScreenParams]
Vector 14 [_WorldSpaceCameraPos]
Matrix 4 [_Object2World]
Matrix 8 [_World2Object]
"vs_3_0
; 40 ALU
dcl_position o0
dcl_texcoord0 o1
dcl_texcoord1 o2
dcl_texcoord2 o3
dcl_texcoord3 o4
dcl_texcoord4 o5
dcl_texcoord5 o6
dcl_texcoord6 o7
dcl_texcoord7 o8
def c15, 0.00000000, 0.50000000, 0, 0
dcl_position0 v0
dcl_normal0 v1
dcl_texcoord0 v2
dcl_tangent0 v3
mov r1.w, c15.x
mov r1.xyz, v3
dp4 r0.z, r1, c6
dp4 r0.x, r1, c4
dp4 r0.y, r1, c5
dp3 r0.w, r0, r0
rsq r0.w, r0.w
mul o7.xyz, r0.w, r0
mul r0.xyz, v1.y, c9
mad r0.xyz, v1.x, c8, r0
mad r0.xyz, v1.z, c10, r0
dp4 r2.w, v0, c3
dp4 r2.z, v0, c2
dp4 r2.x, v0, c0
dp4 r2.y, v0, c1
mul r1.xyz, r2.xyww, c15.y
mul r1.y, r1, c12.x
mad o8.xy, r1.z, c13.zwzw, r1
add r1.xyz, r0, c15.x
dp4 r0.w, v0, c7
dp4 r0.z, v0, c6
dp4 r0.x, v0, c4
dp4 r0.y, v0, c5
mov o1, r0
add r0.xyz, r0, -c14
dp3 r0.w, r1, r1
rsq r0.w, r0.w
dp3 r1.w, r0, r0
mul o2.xyz, r0.w, r1
rsq r0.w, r1.w
mul o5.xyz, r0.w, r0
mov r0.w, c15.x
mov r0.xyz, v1
mov o0, r2
mov o4, v2
dp4 o6.z, r0, c6
dp4 o6.y, r0, c5
dp4 o6.x, r0, c4
mov o8.zw, r2
mov o3.xyz, c15.x
"
}

SubProgram "gles " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec4 xlv_TEXCOORD7;
varying highp vec3 xlv_TEXCOORD6;
varying highp vec3 xlv_TEXCOORD5;
varying highp vec3 xlv_TEXCOORD4;
varying highp vec4 xlv_TEXCOORD3;
varying highp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;

uniform highp vec3 _WorldSpaceCameraPos;
uniform highp mat4 _World2Object;
uniform highp vec4 _ProjectionParams;
uniform highp mat4 _Object2World;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec3 tmpvar_1;
  tmpvar_1 = normalize (_glesNormal);
  vec4 tmpvar_2;
  tmpvar_2.xyz = normalize (_glesTANGENT.xyz);
  tmpvar_2.w = _glesTANGENT.w;
  highp vec4 tmpvar_3;
  tmpvar_3.w = 0.0;
  tmpvar_3.xyz = tmpvar_1;
  highp vec4 tmpvar_4;
  tmpvar_4.w = 0.0;
  tmpvar_4.xyz = tmpvar_1;
  highp vec4 tmpvar_5;
  tmpvar_5 = (gl_ModelViewProjectionMatrix * _glesVertex);
  highp vec4 tmpvar_6;
  tmpvar_6.w = 1.0;
  tmpvar_6.xyz = _WorldSpaceCameraPos;
  highp vec4 tmpvar_7;
  tmpvar_7.w = 0.0;
  tmpvar_7.xyz = tmpvar_2.xyz;
  highp vec4 o_i0;
  highp vec4 tmpvar_8;
  tmpvar_8 = (tmpvar_5 * 0.5);
  o_i0 = tmpvar_8;
  highp vec2 tmpvar_9;
  tmpvar_9.x = tmpvar_8.x;
  tmpvar_9.y = (tmpvar_8.y * _ProjectionParams.x);
  o_i0.xy = (tmpvar_9 + tmpvar_8.w);
  o_i0.zw = tmpvar_5.zw;
  gl_Position = tmpvar_5;
  xlv_TEXCOORD0 = (_Object2World * _glesVertex);
  xlv_TEXCOORD1 = normalize ((tmpvar_4 * _World2Object).xyz);
  xlv_TEXCOORD2 = vec3(0.0, 0.0, 0.0);
  xlv_TEXCOORD3 = _glesMultiTexCoord0;
  xlv_TEXCOORD4 = normalize (((_Object2World * _glesVertex) - tmpvar_6).xyz);
  xlv_TEXCOORD5 = (_Object2World * tmpvar_3).xyz;
  xlv_TEXCOORD6 = normalize ((_Object2World * tmpvar_7).xyz);
  xlv_TEXCOORD7 = o_i0;
}



#endif
#ifdef FRAGMENT
#define unity_LightColor0 _glesLightSource[0].diffuse
#define unity_LightColor1 _glesLightSource[1].diffuse
#define unity_LightColor2 _glesLightSource[2].diffuse
#define unity_LightColor3 _glesLightSource[3].diffuse
#define unity_LightPosition0 _glesLightSource[0].position
#define unity_LightPosition1 _glesLightSource[1].position
#define unity_LightPosition2 _glesLightSource[2].position
#define unity_LightPosition3 _glesLightSource[3].position
#define glstate_light0_spotDirection _glesLightSource[0].spotDirection
#define glstate_light1_spotDirection _glesLightSource[1].spotDirection
#define glstate_light2_spotDirection _glesLightSource[2].spotDirection
#define glstate_light3_spotDirection _glesLightSource[3].spotDirection
#define unity_LightAtten0 _glesLightSource[0].atten
#define unity_LightAtten1 _glesLightSource[1].atten
#define unity_LightAtten2 _glesLightSource[2].atten
#define unity_LightAtten3 _glesLightSource[3].atten
#define glstate_lightmodel_ambient _glesLightModel.ambient
#define gl_LightSource _glesLightSource
#define gl_LightSourceParameters _glesLightSourceParameters
struct _glesLightSourceParameters {
  vec4 diffuse;
  vec4 position;
  vec3 spotDirection;
  vec4 atten;
};
uniform _glesLightSourceParameters _glesLightSource[4];
#define gl_LightModel _glesLightModel
#define gl_LightModelParameters _glesLightModelParameters
struct _glesLightModelParameters {
  vec4 ambient;
};
uniform _glesLightModelParameters _glesLightModel;
#define gl_FrontMaterial _glesFrontMaterial
#define gl_BackMaterial _glesFrontMaterial
#define gl_MaterialParameters _glesMaterialParameters
struct _glesMaterialParameters {
  vec4 emission;
  vec4 ambient;
  vec4 diffuse;
  vec4 specular;
  float shininess;
};
uniform _glesMaterialParameters _glesFrontMaterial;

varying highp vec4 xlv_TEXCOORD7;
varying highp vec3 xlv_TEXCOORD4;
varying highp vec4 xlv_TEXCOORD3;
varying highp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;

uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp vec3 _WorldSpaceCameraPos;
uniform sampler2D _ShadowMapTexture;
uniform sampler2D _MainTex;
uniform highp vec4 _LightColor0;
uniform samplerCube _Cube;
void main ()
{
  highp vec4 tmpvar_1;
  lowp vec4 color;
  lowp float SurfAngle;
  highp vec4 reflTex;
  highp vec3 specularReflection;
  highp vec3 lightDirection;
  highp float attenuation;
  highp vec4 textureColor;
  highp vec3 tmpvar_2;
  tmpvar_2 = normalize (xlv_TEXCOORD1);
  highp vec3 tmpvar_3;
  tmpvar_3 = normalize ((_WorldSpaceCameraPos - xlv_TEXCOORD0.xyz));
  highp vec2 tmpvar_4;
  tmpvar_4 = xlv_TEXCOORD3.xy;
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_MainTex, tmpvar_4);
  textureColor = tmpvar_5;
  if ((0.0 == _WorldSpaceLightPos0.w)) {
    attenuation = 1.0;
    lowp vec3 tmpvar_6;
    tmpvar_6 = normalize (_WorldSpaceLightPos0.xyz);
    lightDirection = tmpvar_6;
  } else {
    highp vec3 tmpvar_7;
    tmpvar_7 = (_WorldSpaceLightPos0 - xlv_TEXCOORD0).xyz;
    attenuation = (1.0/(length (tmpvar_7)));
    lightDirection = normalize (tmpvar_7);
  };
  lowp float tmpvar_8;
  tmpvar_8 = texture2DProj (_ShadowMapTexture, xlv_TEXCOORD7).x;
  attenuation = tmpvar_8;
  highp vec3 tmpvar_9;
  tmpvar_9 = ((attenuation * _LightColor0.xyz) * max (0.0, dot (tmpvar_2, lightDirection)));
  highp float tmpvar_10;
  tmpvar_10 = dot (tmpvar_2, lightDirection);
  if ((tmpvar_10 < 0.0)) {
    specularReflection = vec3(0.0, 0.0, 0.0);
  } else {
    specularReflection = ((attenuation * _LightColor0.xyz) * pow (max (0.0, dot (reflect (-(lightDirection), tmpvar_2), tmpvar_3)), 0.06));
  };
  highp vec3 tmpvar_11;
  tmpvar_11 = (specularReflection * 0.1);
  specularReflection = tmpvar_11;
  highp vec3 tmpvar_12;
  tmpvar_12 = reflect (xlv_TEXCOORD4, normalize (xlv_TEXCOORD1));
  lowp vec4 tmpvar_13;
  tmpvar_13 = textureCube (_Cube, tmpvar_12);
  reflTex = tmpvar_13;
  highp float tmpvar_14;
  tmpvar_14 = clamp (abs (dot (tmpvar_12, xlv_TEXCOORD1)), 0.0, 1.0);
  SurfAngle = tmpvar_14;
  lowp float tmpvar_15;
  tmpvar_15 = (pow ((1.0 - SurfAngle), 2.51) * 0.64);
  lowp float tmpvar_16;
  tmpvar_16 = clamp ((0.07 + tmpvar_15), 0.0, 1.0);
  reflTex.xyz = (reflTex.xyz * tmpvar_16);
  highp vec4 tmpvar_17;
  tmpvar_17.w = 1.0;
  tmpvar_17.xyz = ((textureColor.xyz * clamp (((xlv_TEXCOORD2 + gl_LightModel.ambient.xyz) + tmpvar_9), 0.0, 1.0)) + tmpvar_11);
  color = tmpvar_17;
  highp vec4 tmpvar_18;
  tmpvar_18 = (color + reflTex);
  color = tmpvar_18;
  highp vec4 tmpvar_19;
  tmpvar_19 = (color + (tmpvar_15 * reflTex));
  color = tmpvar_19;
  tmpvar_1 = color;
  gl_FragData[0] = tmpvar_1;
}



#endif"
}

SubProgram "glesdesktop " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec4 xlv_TEXCOORD7;
varying highp vec3 xlv_TEXCOORD6;
varying highp vec3 xlv_TEXCOORD5;
varying highp vec3 xlv_TEXCOORD4;
varying highp vec4 xlv_TEXCOORD3;
varying highp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;

uniform highp vec3 _WorldSpaceCameraPos;
uniform highp mat4 _World2Object;
uniform highp vec4 _ProjectionParams;
uniform highp mat4 _Object2World;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec3 tmpvar_1;
  tmpvar_1 = normalize (_glesNormal);
  vec4 tmpvar_2;
  tmpvar_2.xyz = normalize (_glesTANGENT.xyz);
  tmpvar_2.w = _glesTANGENT.w;
  highp vec4 tmpvar_3;
  tmpvar_3.w = 0.0;
  tmpvar_3.xyz = tmpvar_1;
  highp vec4 tmpvar_4;
  tmpvar_4.w = 0.0;
  tmpvar_4.xyz = tmpvar_1;
  highp vec4 tmpvar_5;
  tmpvar_5 = (gl_ModelViewProjectionMatrix * _glesVertex);
  highp vec4 tmpvar_6;
  tmpvar_6.w = 1.0;
  tmpvar_6.xyz = _WorldSpaceCameraPos;
  highp vec4 tmpvar_7;
  tmpvar_7.w = 0.0;
  tmpvar_7.xyz = tmpvar_2.xyz;
  highp vec4 o_i0;
  highp vec4 tmpvar_8;
  tmpvar_8 = (tmpvar_5 * 0.5);
  o_i0 = tmpvar_8;
  highp vec2 tmpvar_9;
  tmpvar_9.x = tmpvar_8.x;
  tmpvar_9.y = (tmpvar_8.y * _ProjectionParams.x);
  o_i0.xy = (tmpvar_9 + tmpvar_8.w);
  o_i0.zw = tmpvar_5.zw;
  gl_Position = tmpvar_5;
  xlv_TEXCOORD0 = (_Object2World * _glesVertex);
  xlv_TEXCOORD1 = normalize ((tmpvar_4 * _World2Object).xyz);
  xlv_TEXCOORD2 = vec3(0.0, 0.0, 0.0);
  xlv_TEXCOORD3 = _glesMultiTexCoord0;
  xlv_TEXCOORD4 = normalize (((_Object2World * _glesVertex) - tmpvar_6).xyz);
  xlv_TEXCOORD5 = (_Object2World * tmpvar_3).xyz;
  xlv_TEXCOORD6 = normalize ((_Object2World * tmpvar_7).xyz);
  xlv_TEXCOORD7 = o_i0;
}



#endif
#ifdef FRAGMENT
#define unity_LightColor0 _glesLightSource[0].diffuse
#define unity_LightColor1 _glesLightSource[1].diffuse
#define unity_LightColor2 _glesLightSource[2].diffuse
#define unity_LightColor3 _glesLightSource[3].diffuse
#define unity_LightPosition0 _glesLightSource[0].position
#define unity_LightPosition1 _glesLightSource[1].position
#define unity_LightPosition2 _glesLightSource[2].position
#define unity_LightPosition3 _glesLightSource[3].position
#define glstate_light0_spotDirection _glesLightSource[0].spotDirection
#define glstate_light1_spotDirection _glesLightSource[1].spotDirection
#define glstate_light2_spotDirection _glesLightSource[2].spotDirection
#define glstate_light3_spotDirection _glesLightSource[3].spotDirection
#define unity_LightAtten0 _glesLightSource[0].atten
#define unity_LightAtten1 _glesLightSource[1].atten
#define unity_LightAtten2 _glesLightSource[2].atten
#define unity_LightAtten3 _glesLightSource[3].atten
#define glstate_lightmodel_ambient _glesLightModel.ambient
#define gl_LightSource _glesLightSource
#define gl_LightSourceParameters _glesLightSourceParameters
struct _glesLightSourceParameters {
  vec4 diffuse;
  vec4 position;
  vec3 spotDirection;
  vec4 atten;
};
uniform _glesLightSourceParameters _glesLightSource[4];
#define gl_LightModel _glesLightModel
#define gl_LightModelParameters _glesLightModelParameters
struct _glesLightModelParameters {
  vec4 ambient;
};
uniform _glesLightModelParameters _glesLightModel;
#define gl_FrontMaterial _glesFrontMaterial
#define gl_BackMaterial _glesFrontMaterial
#define gl_MaterialParameters _glesMaterialParameters
struct _glesMaterialParameters {
  vec4 emission;
  vec4 ambient;
  vec4 diffuse;
  vec4 specular;
  float shininess;
};
uniform _glesMaterialParameters _glesFrontMaterial;

varying highp vec4 xlv_TEXCOORD7;
varying highp vec3 xlv_TEXCOORD4;
varying highp vec4 xlv_TEXCOORD3;
varying highp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;

uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp vec3 _WorldSpaceCameraPos;
uniform sampler2D _ShadowMapTexture;
uniform sampler2D _MainTex;
uniform highp vec4 _LightColor0;
uniform samplerCube _Cube;
void main ()
{
  highp vec4 tmpvar_1;
  lowp vec4 color;
  lowp float SurfAngle;
  highp vec4 reflTex;
  highp vec3 specularReflection;
  highp vec3 lightDirection;
  highp float attenuation;
  highp vec4 textureColor;
  highp vec3 tmpvar_2;
  tmpvar_2 = normalize (xlv_TEXCOORD1);
  highp vec3 tmpvar_3;
  tmpvar_3 = normalize ((_WorldSpaceCameraPos - xlv_TEXCOORD0.xyz));
  highp vec2 tmpvar_4;
  tmpvar_4 = xlv_TEXCOORD3.xy;
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_MainTex, tmpvar_4);
  textureColor = tmpvar_5;
  if ((0.0 == _WorldSpaceLightPos0.w)) {
    attenuation = 1.0;
    lowp vec3 tmpvar_6;
    tmpvar_6 = normalize (_WorldSpaceLightPos0.xyz);
    lightDirection = tmpvar_6;
  } else {
    highp vec3 tmpvar_7;
    tmpvar_7 = (_WorldSpaceLightPos0 - xlv_TEXCOORD0).xyz;
    attenuation = (1.0/(length (tmpvar_7)));
    lightDirection = normalize (tmpvar_7);
  };
  lowp float tmpvar_8;
  tmpvar_8 = texture2DProj (_ShadowMapTexture, xlv_TEXCOORD7).x;
  attenuation = tmpvar_8;
  highp vec3 tmpvar_9;
  tmpvar_9 = ((attenuation * _LightColor0.xyz) * max (0.0, dot (tmpvar_2, lightDirection)));
  highp float tmpvar_10;
  tmpvar_10 = dot (tmpvar_2, lightDirection);
  if ((tmpvar_10 < 0.0)) {
    specularReflection = vec3(0.0, 0.0, 0.0);
  } else {
    specularReflection = ((attenuation * _LightColor0.xyz) * pow (max (0.0, dot (reflect (-(lightDirection), tmpvar_2), tmpvar_3)), 0.06));
  };
  highp vec3 tmpvar_11;
  tmpvar_11 = (specularReflection * 0.1);
  specularReflection = tmpvar_11;
  highp vec3 tmpvar_12;
  tmpvar_12 = reflect (xlv_TEXCOORD4, normalize (xlv_TEXCOORD1));
  lowp vec4 tmpvar_13;
  tmpvar_13 = textureCube (_Cube, tmpvar_12);
  reflTex = tmpvar_13;
  highp float tmpvar_14;
  tmpvar_14 = clamp (abs (dot (tmpvar_12, xlv_TEXCOORD1)), 0.0, 1.0);
  SurfAngle = tmpvar_14;
  lowp float tmpvar_15;
  tmpvar_15 = (pow ((1.0 - SurfAngle), 2.51) * 0.64);
  lowp float tmpvar_16;
  tmpvar_16 = clamp ((0.07 + tmpvar_15), 0.0, 1.0);
  reflTex.xyz = (reflTex.xyz * tmpvar_16);
  highp vec4 tmpvar_17;
  tmpvar_17.w = 1.0;
  tmpvar_17.xyz = ((textureColor.xyz * clamp (((xlv_TEXCOORD2 + gl_LightModel.ambient.xyz) + tmpvar_9), 0.0, 1.0)) + tmpvar_11);
  color = tmpvar_17;
  highp vec4 tmpvar_18;
  tmpvar_18 = (color + reflTex);
  color = tmpvar_18;
  highp vec4 tmpvar_19;
  tmpvar_19 = (color + (tmpvar_15 * reflTex));
  color = tmpvar_19;
  tmpvar_1 = color;
  gl_FragData[0] = tmpvar_1;
}



#endif"
}

SubProgram "opengl " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "SHADOWS_SCREEN" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "tangent" ATTR14
Vector 13 [_ProjectionParams]
Vector 14 [_WorldSpaceCameraPos]
Matrix 5 [_Object2World]
Matrix 9 [_World2Object]
"3.0-!!ARBvp1.0
# 40 ALU
PARAM c[15] = { { 0, 0.5 },
		state.matrix.mvp,
		program.local[5..14] };
TEMP R0;
TEMP R1;
TEMP R2;
MOV R1.w, c[0].x;
MOV R1.xyz, vertex.attrib[14];
DP4 R0.z, R1, c[7];
DP4 R0.x, R1, c[5];
DP4 R0.y, R1, c[6];
DP3 R0.w, R0, R0;
RSQ R0.w, R0.w;
MUL result.texcoord[6].xyz, R0.w, R0;
MUL R0.xyz, vertex.normal.y, c[10];
MAD R0.xyz, vertex.normal.x, c[9], R0;
MAD R0.xyz, vertex.normal.z, c[11], R0;
DP4 R2.w, vertex.position, c[4];
DP4 R2.z, vertex.position, c[3];
DP4 R2.x, vertex.position, c[1];
DP4 R2.y, vertex.position, c[2];
MUL R1.xyz, R2.xyww, c[0].y;
MUL R1.y, R1, c[13].x;
ADD result.texcoord[7].xy, R1, R1.z;
ADD R1.xyz, R0, c[0].x;
DP4 R0.w, vertex.position, c[8];
DP4 R0.z, vertex.position, c[7];
DP4 R0.x, vertex.position, c[5];
DP4 R0.y, vertex.position, c[6];
MOV result.texcoord[0], R0;
ADD R0.xyz, R0, -c[14];
DP3 R0.w, R1, R1;
RSQ R0.w, R0.w;
DP3 R1.w, R0, R0;
MUL result.texcoord[1].xyz, R0.w, R1;
RSQ R0.w, R1.w;
MUL result.texcoord[4].xyz, R0.w, R0;
MOV R0.w, c[0].x;
MOV R0.xyz, vertex.normal;
MOV result.position, R2;
MOV result.texcoord[3], vertex.texcoord[0];
DP4 result.texcoord[5].z, R0, c[7];
DP4 result.texcoord[5].y, R0, c[6];
DP4 result.texcoord[5].x, R0, c[5];
MOV result.texcoord[7].zw, R2;
MOV result.texcoord[2].xyz, c[0].x;
END
# 40 instructions, 3 R-regs
"
}

SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "SHADOWS_SCREEN" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "tangent" TexCoord2
Matrix 0 [glstate_matrix_mvp]
Vector 12 [_ProjectionParams]
Vector 13 [_ScreenParams]
Vector 14 [_WorldSpaceCameraPos]
Matrix 4 [_Object2World]
Matrix 8 [_World2Object]
"vs_3_0
; 40 ALU
dcl_position o0
dcl_texcoord0 o1
dcl_texcoord1 o2
dcl_texcoord2 o3
dcl_texcoord3 o4
dcl_texcoord4 o5
dcl_texcoord5 o6
dcl_texcoord6 o7
dcl_texcoord7 o8
def c15, 0.00000000, 0.50000000, 0, 0
dcl_position0 v0
dcl_normal0 v1
dcl_texcoord0 v2
dcl_tangent0 v3
mov r1.w, c15.x
mov r1.xyz, v3
dp4 r0.z, r1, c6
dp4 r0.x, r1, c4
dp4 r0.y, r1, c5
dp3 r0.w, r0, r0
rsq r0.w, r0.w
mul o7.xyz, r0.w, r0
mul r0.xyz, v1.y, c9
mad r0.xyz, v1.x, c8, r0
mad r0.xyz, v1.z, c10, r0
dp4 r2.w, v0, c3
dp4 r2.z, v0, c2
dp4 r2.x, v0, c0
dp4 r2.y, v0, c1
mul r1.xyz, r2.xyww, c15.y
mul r1.y, r1, c12.x
mad o8.xy, r1.z, c13.zwzw, r1
add r1.xyz, r0, c15.x
dp4 r0.w, v0, c7
dp4 r0.z, v0, c6
dp4 r0.x, v0, c4
dp4 r0.y, v0, c5
mov o1, r0
add r0.xyz, r0, -c14
dp3 r0.w, r1, r1
rsq r0.w, r0.w
dp3 r1.w, r0, r0
mul o2.xyz, r0.w, r1
rsq r0.w, r1.w
mul o5.xyz, r0.w, r0
mov r0.w, c15.x
mov r0.xyz, v1
mov o0, r2
mov o4, v2
dp4 o6.z, r0, c6
dp4 o6.y, r0, c5
dp4 o6.x, r0, c4
mov o8.zw, r2
mov o3.xyz, c15.x
"
}

SubProgram "gles " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "SHADOWS_SCREEN" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec4 xlv_TEXCOORD7;
varying highp vec3 xlv_TEXCOORD6;
varying highp vec3 xlv_TEXCOORD5;
varying highp vec3 xlv_TEXCOORD4;
varying highp vec4 xlv_TEXCOORD3;
varying highp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;

uniform highp vec3 _WorldSpaceCameraPos;
uniform highp mat4 _World2Object;
uniform highp vec4 _ProjectionParams;
uniform highp mat4 _Object2World;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec3 tmpvar_1;
  tmpvar_1 = normalize (_glesNormal);
  vec4 tmpvar_2;
  tmpvar_2.xyz = normalize (_glesTANGENT.xyz);
  tmpvar_2.w = _glesTANGENT.w;
  highp vec4 tmpvar_3;
  tmpvar_3.w = 0.0;
  tmpvar_3.xyz = tmpvar_1;
  highp vec4 tmpvar_4;
  tmpvar_4.w = 0.0;
  tmpvar_4.xyz = tmpvar_1;
  highp vec4 tmpvar_5;
  tmpvar_5 = (gl_ModelViewProjectionMatrix * _glesVertex);
  highp vec4 tmpvar_6;
  tmpvar_6.w = 1.0;
  tmpvar_6.xyz = _WorldSpaceCameraPos;
  highp vec4 tmpvar_7;
  tmpvar_7.w = 0.0;
  tmpvar_7.xyz = tmpvar_2.xyz;
  highp vec4 o_i0;
  highp vec4 tmpvar_8;
  tmpvar_8 = (tmpvar_5 * 0.5);
  o_i0 = tmpvar_8;
  highp vec2 tmpvar_9;
  tmpvar_9.x = tmpvar_8.x;
  tmpvar_9.y = (tmpvar_8.y * _ProjectionParams.x);
  o_i0.xy = (tmpvar_9 + tmpvar_8.w);
  o_i0.zw = tmpvar_5.zw;
  gl_Position = tmpvar_5;
  xlv_TEXCOORD0 = (_Object2World * _glesVertex);
  xlv_TEXCOORD1 = normalize ((tmpvar_4 * _World2Object).xyz);
  xlv_TEXCOORD2 = vec3(0.0, 0.0, 0.0);
  xlv_TEXCOORD3 = _glesMultiTexCoord0;
  xlv_TEXCOORD4 = normalize (((_Object2World * _glesVertex) - tmpvar_6).xyz);
  xlv_TEXCOORD5 = (_Object2World * tmpvar_3).xyz;
  xlv_TEXCOORD6 = normalize ((_Object2World * tmpvar_7).xyz);
  xlv_TEXCOORD7 = o_i0;
}



#endif
#ifdef FRAGMENT
#define unity_LightColor0 _glesLightSource[0].diffuse
#define unity_LightColor1 _glesLightSource[1].diffuse
#define unity_LightColor2 _glesLightSource[2].diffuse
#define unity_LightColor3 _glesLightSource[3].diffuse
#define unity_LightPosition0 _glesLightSource[0].position
#define unity_LightPosition1 _glesLightSource[1].position
#define unity_LightPosition2 _glesLightSource[2].position
#define unity_LightPosition3 _glesLightSource[3].position
#define glstate_light0_spotDirection _glesLightSource[0].spotDirection
#define glstate_light1_spotDirection _glesLightSource[1].spotDirection
#define glstate_light2_spotDirection _glesLightSource[2].spotDirection
#define glstate_light3_spotDirection _glesLightSource[3].spotDirection
#define unity_LightAtten0 _glesLightSource[0].atten
#define unity_LightAtten1 _glesLightSource[1].atten
#define unity_LightAtten2 _glesLightSource[2].atten
#define unity_LightAtten3 _glesLightSource[3].atten
#define glstate_lightmodel_ambient _glesLightModel.ambient
#define gl_LightSource _glesLightSource
#define gl_LightSourceParameters _glesLightSourceParameters
struct _glesLightSourceParameters {
  vec4 diffuse;
  vec4 position;
  vec3 spotDirection;
  vec4 atten;
};
uniform _glesLightSourceParameters _glesLightSource[4];
#define gl_LightModel _glesLightModel
#define gl_LightModelParameters _glesLightModelParameters
struct _glesLightModelParameters {
  vec4 ambient;
};
uniform _glesLightModelParameters _glesLightModel;
#define gl_FrontMaterial _glesFrontMaterial
#define gl_BackMaterial _glesFrontMaterial
#define gl_MaterialParameters _glesMaterialParameters
struct _glesMaterialParameters {
  vec4 emission;
  vec4 ambient;
  vec4 diffuse;
  vec4 specular;
  float shininess;
};
uniform _glesMaterialParameters _glesFrontMaterial;

varying highp vec4 xlv_TEXCOORD7;
varying highp vec3 xlv_TEXCOORD4;
varying highp vec4 xlv_TEXCOORD3;
varying highp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;

uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp vec3 _WorldSpaceCameraPos;
uniform sampler2D _ShadowMapTexture;
uniform sampler2D _MainTex;
uniform highp vec4 _LightColor0;
uniform samplerCube _Cube;
void main ()
{
  highp vec4 tmpvar_1;
  lowp vec4 color;
  lowp float SurfAngle;
  highp vec4 reflTex;
  highp vec3 specularReflection;
  highp vec3 lightDirection;
  highp float attenuation;
  highp vec4 textureColor;
  highp vec3 tmpvar_2;
  tmpvar_2 = normalize (xlv_TEXCOORD1);
  highp vec3 tmpvar_3;
  tmpvar_3 = normalize ((_WorldSpaceCameraPos - xlv_TEXCOORD0.xyz));
  highp vec2 tmpvar_4;
  tmpvar_4 = xlv_TEXCOORD3.xy;
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_MainTex, tmpvar_4);
  textureColor = tmpvar_5;
  if ((0.0 == _WorldSpaceLightPos0.w)) {
    attenuation = 1.0;
    lowp vec3 tmpvar_6;
    tmpvar_6 = normalize (_WorldSpaceLightPos0.xyz);
    lightDirection = tmpvar_6;
  } else {
    highp vec3 tmpvar_7;
    tmpvar_7 = (_WorldSpaceLightPos0 - xlv_TEXCOORD0).xyz;
    attenuation = (1.0/(length (tmpvar_7)));
    lightDirection = normalize (tmpvar_7);
  };
  lowp float tmpvar_8;
  tmpvar_8 = texture2DProj (_ShadowMapTexture, xlv_TEXCOORD7).x;
  attenuation = tmpvar_8;
  highp vec3 tmpvar_9;
  tmpvar_9 = ((attenuation * _LightColor0.xyz) * max (0.0, dot (tmpvar_2, lightDirection)));
  highp float tmpvar_10;
  tmpvar_10 = dot (tmpvar_2, lightDirection);
  if ((tmpvar_10 < 0.0)) {
    specularReflection = vec3(0.0, 0.0, 0.0);
  } else {
    specularReflection = ((attenuation * _LightColor0.xyz) * pow (max (0.0, dot (reflect (-(lightDirection), tmpvar_2), tmpvar_3)), 0.06));
  };
  highp vec3 tmpvar_11;
  tmpvar_11 = (specularReflection * 0.1);
  specularReflection = tmpvar_11;
  highp vec3 tmpvar_12;
  tmpvar_12 = reflect (xlv_TEXCOORD4, normalize (xlv_TEXCOORD1));
  lowp vec4 tmpvar_13;
  tmpvar_13 = textureCube (_Cube, tmpvar_12);
  reflTex = tmpvar_13;
  highp float tmpvar_14;
  tmpvar_14 = clamp (abs (dot (tmpvar_12, xlv_TEXCOORD1)), 0.0, 1.0);
  SurfAngle = tmpvar_14;
  lowp float tmpvar_15;
  tmpvar_15 = (pow ((1.0 - SurfAngle), 2.51) * 0.64);
  lowp float tmpvar_16;
  tmpvar_16 = clamp ((0.07 + tmpvar_15), 0.0, 1.0);
  reflTex.xyz = (reflTex.xyz * tmpvar_16);
  highp vec4 tmpvar_17;
  tmpvar_17.w = 1.0;
  tmpvar_17.xyz = ((textureColor.xyz * clamp (((xlv_TEXCOORD2 + gl_LightModel.ambient.xyz) + tmpvar_9), 0.0, 1.0)) + tmpvar_11);
  color = tmpvar_17;
  highp vec4 tmpvar_18;
  tmpvar_18 = (color + reflTex);
  color = tmpvar_18;
  highp vec4 tmpvar_19;
  tmpvar_19 = (color + (tmpvar_15 * reflTex));
  color = tmpvar_19;
  tmpvar_1 = color;
  gl_FragData[0] = tmpvar_1;
}



#endif"
}

SubProgram "glesdesktop " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "SHADOWS_SCREEN" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec4 xlv_TEXCOORD7;
varying highp vec3 xlv_TEXCOORD6;
varying highp vec3 xlv_TEXCOORD5;
varying highp vec3 xlv_TEXCOORD4;
varying highp vec4 xlv_TEXCOORD3;
varying highp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;

uniform highp vec3 _WorldSpaceCameraPos;
uniform highp mat4 _World2Object;
uniform highp vec4 _ProjectionParams;
uniform highp mat4 _Object2World;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec3 tmpvar_1;
  tmpvar_1 = normalize (_glesNormal);
  vec4 tmpvar_2;
  tmpvar_2.xyz = normalize (_glesTANGENT.xyz);
  tmpvar_2.w = _glesTANGENT.w;
  highp vec4 tmpvar_3;
  tmpvar_3.w = 0.0;
  tmpvar_3.xyz = tmpvar_1;
  highp vec4 tmpvar_4;
  tmpvar_4.w = 0.0;
  tmpvar_4.xyz = tmpvar_1;
  highp vec4 tmpvar_5;
  tmpvar_5 = (gl_ModelViewProjectionMatrix * _glesVertex);
  highp vec4 tmpvar_6;
  tmpvar_6.w = 1.0;
  tmpvar_6.xyz = _WorldSpaceCameraPos;
  highp vec4 tmpvar_7;
  tmpvar_7.w = 0.0;
  tmpvar_7.xyz = tmpvar_2.xyz;
  highp vec4 o_i0;
  highp vec4 tmpvar_8;
  tmpvar_8 = (tmpvar_5 * 0.5);
  o_i0 = tmpvar_8;
  highp vec2 tmpvar_9;
  tmpvar_9.x = tmpvar_8.x;
  tmpvar_9.y = (tmpvar_8.y * _ProjectionParams.x);
  o_i0.xy = (tmpvar_9 + tmpvar_8.w);
  o_i0.zw = tmpvar_5.zw;
  gl_Position = tmpvar_5;
  xlv_TEXCOORD0 = (_Object2World * _glesVertex);
  xlv_TEXCOORD1 = normalize ((tmpvar_4 * _World2Object).xyz);
  xlv_TEXCOORD2 = vec3(0.0, 0.0, 0.0);
  xlv_TEXCOORD3 = _glesMultiTexCoord0;
  xlv_TEXCOORD4 = normalize (((_Object2World * _glesVertex) - tmpvar_6).xyz);
  xlv_TEXCOORD5 = (_Object2World * tmpvar_3).xyz;
  xlv_TEXCOORD6 = normalize ((_Object2World * tmpvar_7).xyz);
  xlv_TEXCOORD7 = o_i0;
}



#endif
#ifdef FRAGMENT
#define unity_LightColor0 _glesLightSource[0].diffuse
#define unity_LightColor1 _glesLightSource[1].diffuse
#define unity_LightColor2 _glesLightSource[2].diffuse
#define unity_LightColor3 _glesLightSource[3].diffuse
#define unity_LightPosition0 _glesLightSource[0].position
#define unity_LightPosition1 _glesLightSource[1].position
#define unity_LightPosition2 _glesLightSource[2].position
#define unity_LightPosition3 _glesLightSource[3].position
#define glstate_light0_spotDirection _glesLightSource[0].spotDirection
#define glstate_light1_spotDirection _glesLightSource[1].spotDirection
#define glstate_light2_spotDirection _glesLightSource[2].spotDirection
#define glstate_light3_spotDirection _glesLightSource[3].spotDirection
#define unity_LightAtten0 _glesLightSource[0].atten
#define unity_LightAtten1 _glesLightSource[1].atten
#define unity_LightAtten2 _glesLightSource[2].atten
#define unity_LightAtten3 _glesLightSource[3].atten
#define glstate_lightmodel_ambient _glesLightModel.ambient
#define gl_LightSource _glesLightSource
#define gl_LightSourceParameters _glesLightSourceParameters
struct _glesLightSourceParameters {
  vec4 diffuse;
  vec4 position;
  vec3 spotDirection;
  vec4 atten;
};
uniform _glesLightSourceParameters _glesLightSource[4];
#define gl_LightModel _glesLightModel
#define gl_LightModelParameters _glesLightModelParameters
struct _glesLightModelParameters {
  vec4 ambient;
};
uniform _glesLightModelParameters _glesLightModel;
#define gl_FrontMaterial _glesFrontMaterial
#define gl_BackMaterial _glesFrontMaterial
#define gl_MaterialParameters _glesMaterialParameters
struct _glesMaterialParameters {
  vec4 emission;
  vec4 ambient;
  vec4 diffuse;
  vec4 specular;
  float shininess;
};
uniform _glesMaterialParameters _glesFrontMaterial;

varying highp vec4 xlv_TEXCOORD7;
varying highp vec3 xlv_TEXCOORD4;
varying highp vec4 xlv_TEXCOORD3;
varying highp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;

uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp vec3 _WorldSpaceCameraPos;
uniform sampler2D _ShadowMapTexture;
uniform sampler2D _MainTex;
uniform highp vec4 _LightColor0;
uniform samplerCube _Cube;
void main ()
{
  highp vec4 tmpvar_1;
  lowp vec4 color;
  lowp float SurfAngle;
  highp vec4 reflTex;
  highp vec3 specularReflection;
  highp vec3 lightDirection;
  highp float attenuation;
  highp vec4 textureColor;
  highp vec3 tmpvar_2;
  tmpvar_2 = normalize (xlv_TEXCOORD1);
  highp vec3 tmpvar_3;
  tmpvar_3 = normalize ((_WorldSpaceCameraPos - xlv_TEXCOORD0.xyz));
  highp vec2 tmpvar_4;
  tmpvar_4 = xlv_TEXCOORD3.xy;
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_MainTex, tmpvar_4);
  textureColor = tmpvar_5;
  if ((0.0 == _WorldSpaceLightPos0.w)) {
    attenuation = 1.0;
    lowp vec3 tmpvar_6;
    tmpvar_6 = normalize (_WorldSpaceLightPos0.xyz);
    lightDirection = tmpvar_6;
  } else {
    highp vec3 tmpvar_7;
    tmpvar_7 = (_WorldSpaceLightPos0 - xlv_TEXCOORD0).xyz;
    attenuation = (1.0/(length (tmpvar_7)));
    lightDirection = normalize (tmpvar_7);
  };
  lowp float tmpvar_8;
  tmpvar_8 = texture2DProj (_ShadowMapTexture, xlv_TEXCOORD7).x;
  attenuation = tmpvar_8;
  highp vec3 tmpvar_9;
  tmpvar_9 = ((attenuation * _LightColor0.xyz) * max (0.0, dot (tmpvar_2, lightDirection)));
  highp float tmpvar_10;
  tmpvar_10 = dot (tmpvar_2, lightDirection);
  if ((tmpvar_10 < 0.0)) {
    specularReflection = vec3(0.0, 0.0, 0.0);
  } else {
    specularReflection = ((attenuation * _LightColor0.xyz) * pow (max (0.0, dot (reflect (-(lightDirection), tmpvar_2), tmpvar_3)), 0.06));
  };
  highp vec3 tmpvar_11;
  tmpvar_11 = (specularReflection * 0.1);
  specularReflection = tmpvar_11;
  highp vec3 tmpvar_12;
  tmpvar_12 = reflect (xlv_TEXCOORD4, normalize (xlv_TEXCOORD1));
  lowp vec4 tmpvar_13;
  tmpvar_13 = textureCube (_Cube, tmpvar_12);
  reflTex = tmpvar_13;
  highp float tmpvar_14;
  tmpvar_14 = clamp (abs (dot (tmpvar_12, xlv_TEXCOORD1)), 0.0, 1.0);
  SurfAngle = tmpvar_14;
  lowp float tmpvar_15;
  tmpvar_15 = (pow ((1.0 - SurfAngle), 2.51) * 0.64);
  lowp float tmpvar_16;
  tmpvar_16 = clamp ((0.07 + tmpvar_15), 0.0, 1.0);
  reflTex.xyz = (reflTex.xyz * tmpvar_16);
  highp vec4 tmpvar_17;
  tmpvar_17.w = 1.0;
  tmpvar_17.xyz = ((textureColor.xyz * clamp (((xlv_TEXCOORD2 + gl_LightModel.ambient.xyz) + tmpvar_9), 0.0, 1.0)) + tmpvar_11);
  color = tmpvar_17;
  highp vec4 tmpvar_18;
  tmpvar_18 = (color + reflTex);
  color = tmpvar_18;
  highp vec4 tmpvar_19;
  tmpvar_19 = (color + (tmpvar_15 * reflTex));
  color = tmpvar_19;
  tmpvar_1 = color;
  gl_FragData[0] = tmpvar_1;
}



#endif"
}

SubProgram "opengl " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" "VERTEXLIGHT_ON" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "tangent" ATTR14
Vector 13 [_WorldSpaceCameraPos]
Matrix 5 [_Object2World]
Matrix 9 [_World2Object]
Vector 14 [unity_4LightPosX0]
Vector 15 [unity_4LightPosY0]
Vector 16 [unity_4LightPosZ0]
Vector 17 [unity_4LightAtten0]
Vector 18 [unity_LightColor0]
Vector 19 [unity_LightColor1]
Vector 20 [unity_LightColor2]
Vector 21 [unity_LightColor3]
"3.0-!!ARBvp1.0
# 91 ALU
PARAM c[22] = { { 0, 1 },
		state.matrix.mvp,
		program.local[5..21] };
TEMP R0;
TEMP R1;
TEMP R2;
TEMP R3;
TEMP R4;
DP4 R0.z, vertex.position, c[7];
DP4 R0.y, vertex.position, c[6];
DP4 R0.x, vertex.position, c[5];
MOV R1.x, c[14];
MOV R1.z, c[16].x;
MOV R1.y, c[15].x;
ADD R2.xyz, -R0, R1;
DP3 R0.w, R2, R2;
RSQ R1.w, R0.w;
MUL R1.xyz, vertex.normal.y, c[10];
MAD R1.xyz, vertex.normal.x, c[9], R1;
MAD R1.xyz, vertex.normal.z, c[11], R1;
MUL R0.w, R0, c[17].x;
ADD R0.w, R0, c[0].y;
MUL R2.xyz, R1.w, R2;
ADD R1.xyz, R1, c[0].x;
DP3 R1.w, R1, R1;
RSQ R1.w, R1.w;
MUL R1.xyz, R1.w, R1;
DP3 R1.w, R1, R2;
MAX R1.w, R1, c[0].x;
RCP R0.w, R0.w;
MOV R3.x, c[14].y;
MOV R3.z, c[16].y;
MOV R3.y, c[15];
ADD R3.xyz, -R0, R3;
DP3 R2.w, R3, R3;
RSQ R3.w, R2.w;
MUL R2.xyz, R3.w, R3;
DP3 R2.y, R1, R2;
MUL R2.x, R2.w, c[17].y;
ADD R2.x, R2, c[0].y;
MAX R2.w, R2.y, c[0].x;
RCP R2.x, R2.x;
MUL R2.xyz, R2.x, c[19];
MUL R4.xyz, R2, R2.w;
MOV R3.w, c[0].x;
MOV R2.x, c[14].w;
MOV R2.z, c[16].w;
MOV R2.y, c[15].w;
ADD R3.xyz, -R0, R2;
MUL R2.xyz, R0.w, c[18];
MAD R4.xyz, R2, R1.w, R4;
DP3 R0.w, R3, R3;
RSQ R2.w, R0.w;
MUL R3.xyz, R2.w, R3;
MOV R2.x, c[14].z;
MOV R2.z, c[16];
MOV R2.y, c[15].z;
ADD R2.xyz, -R0, R2;
DP3 R1.w, R2, R2;
RSQ R2.w, R1.w;
MUL R2.xyz, R2.w, R2;
DP3 R2.x, R1, R2;
MUL R1.w, R1, c[17].z;
ADD R1.w, R1, c[0].y;
MAX R2.w, R2.x, c[0].x;
RCP R1.w, R1.w;
MUL R2.xyz, R1.w, c[20];
MAD R2.xyz, R2, R2.w, R4;
DP3 R2.w, R1, R3;
MOV R3.xyz, vertex.attrib[14];
MUL R1.w, R0, c[17];
MAX R0.w, R2, c[0].x;
ADD R2.w, R1, c[0].y;
DP4 R4.z, R3, c[7];
DP4 R4.x, R3, c[5];
DP4 R4.y, R3, c[6];
DP3 R1.w, R4, R4;
RCP R2.w, R2.w;
RSQ R1.w, R1.w;
MUL R3.xyz, R2.w, c[21];
MAD result.texcoord[2].xyz, R3, R0.w, R2;
DP4 R0.w, vertex.position, c[8];
MOV result.texcoord[0], R0;
ADD R2.xyz, R0, -c[13];
DP3 R0.x, R2, R2;
RSQ R0.x, R0.x;
MUL result.texcoord[4].xyz, R0.x, R2;
MOV R0.w, c[0].x;
MOV R0.xyz, vertex.normal;
MUL result.texcoord[6].xyz, R1.w, R4;
MOV result.texcoord[1].xyz, R1;
MOV result.texcoord[3], vertex.texcoord[0];
DP4 result.texcoord[5].z, R0, c[7];
DP4 result.texcoord[5].y, R0, c[6];
DP4 result.texcoord[5].x, R0, c[5];
DP4 result.position.w, vertex.position, c[4];
DP4 result.position.z, vertex.position, c[3];
DP4 result.position.y, vertex.position, c[2];
DP4 result.position.x, vertex.position, c[1];
END
# 91 instructions, 5 R-regs
"
}

SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" "VERTEXLIGHT_ON" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "tangent" TexCoord2
Matrix 0 [glstate_matrix_mvp]
Vector 12 [_WorldSpaceCameraPos]
Matrix 4 [_Object2World]
Matrix 8 [_World2Object]
Vector 13 [unity_4LightPosX0]
Vector 14 [unity_4LightPosY0]
Vector 15 [unity_4LightPosZ0]
Vector 16 [unity_4LightAtten0]
Vector 17 [unity_LightColor0]
Vector 18 [unity_LightColor1]
Vector 19 [unity_LightColor2]
Vector 20 [unity_LightColor3]
"vs_3_0
; 91 ALU
dcl_position o0
dcl_texcoord0 o1
dcl_texcoord1 o2
dcl_texcoord2 o3
dcl_texcoord3 o4
dcl_texcoord4 o5
dcl_texcoord5 o6
dcl_texcoord6 o7
def c21, 0.00000000, 1.00000000, 0, 0
dcl_position0 v0
dcl_normal0 v1
dcl_texcoord0 v2
dcl_tangent0 v3
dp4 r0.z, v0, c6
dp4 r0.y, v0, c5
dp4 r0.x, v0, c4
mov r1.x, c13
mov r1.z, c15.x
mov r1.y, c14.x
add r2.xyz, -r0, r1
dp3 r0.w, r2, r2
rsq r1.w, r0.w
mul r1.xyz, v1.y, c9
mad r1.xyz, v1.x, c8, r1
mad r1.xyz, v1.z, c10, r1
mul r0.w, r0, c16.x
add r0.w, r0, c21.y
mul r2.xyz, r1.w, r2
add r1.xyz, r1, c21.x
dp3 r1.w, r1, r1
rsq r1.w, r1.w
mul r1.xyz, r1.w, r1
dp3 r1.w, r1, r2
max r1.w, r1, c21.x
rcp r0.w, r0.w
mov r3.x, c13.y
mov r3.z, c15.y
mov r3.y, c14
add r3.xyz, -r0, r3
dp3 r2.w, r3, r3
rsq r3.w, r2.w
mul r2.xyz, r3.w, r3
dp3 r2.y, r1, r2
mul r2.x, r2.w, c16.y
add r2.x, r2, c21.y
max r2.w, r2.y, c21.x
rcp r2.x, r2.x
mul r2.xyz, r2.x, c18
mul r4.xyz, r2, r2.w
mov r3.w, c21.x
mov r2.x, c13.w
mov r2.z, c15.w
mov r2.y, c14.w
add r3.xyz, -r0, r2
mul r2.xyz, r0.w, c17
mad r4.xyz, r2, r1.w, r4
dp3 r0.w, r3, r3
rsq r2.w, r0.w
mul r3.xyz, r2.w, r3
mov r2.x, c13.z
mov r2.z, c15
mov r2.y, c14.z
add r2.xyz, -r0, r2
dp3 r1.w, r2, r2
rsq r2.w, r1.w
mul r2.xyz, r2.w, r2
dp3 r2.x, r1, r2
mul r1.w, r1, c16.z
add r1.w, r1, c21.y
max r2.w, r2.x, c21.x
rcp r1.w, r1.w
mul r2.xyz, r1.w, c19
mad r2.xyz, r2, r2.w, r4
dp3 r2.w, r1, r3
mov r3.xyz, v3
mul r1.w, r0, c16
max r0.w, r2, c21.x
add r2.w, r1, c21.y
dp4 r4.z, r3, c6
dp4 r4.x, r3, c4
dp4 r4.y, r3, c5
dp3 r1.w, r4, r4
rcp r2.w, r2.w
rsq r1.w, r1.w
mul r3.xyz, r2.w, c20
mad o3.xyz, r3, r0.w, r2
dp4 r0.w, v0, c7
mov o1, r0
add r2.xyz, r0, -c12
dp3 r0.x, r2, r2
rsq r0.x, r0.x
mul o5.xyz, r0.x, r2
mov r0.w, c21.x
mov r0.xyz, v1
mul o7.xyz, r1.w, r4
mov o2.xyz, r1
mov o4, v2
dp4 o6.z, r0, c6
dp4 o6.y, r0, c5
dp4 o6.x, r0, c4
dp4 o0.w, v0, c3
dp4 o0.z, v0, c2
dp4 o0.y, v0, c1
dp4 o0.x, v0, c0
"
}

SubProgram "gles " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" "VERTEXLIGHT_ON" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec3 xlv_TEXCOORD6;
varying highp vec3 xlv_TEXCOORD5;
varying highp vec3 xlv_TEXCOORD4;
varying highp vec4 xlv_TEXCOORD3;
varying highp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 unity_LightColor[4];
uniform highp vec4 unity_4LightPosZ0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightAtten0;

uniform highp vec3 _WorldSpaceCameraPos;
uniform highp mat4 _World2Object;
uniform highp mat4 _Object2World;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec3 tmpvar_1;
  tmpvar_1 = normalize (_glesNormal);
  vec4 tmpvar_2;
  tmpvar_2.xyz = normalize (_glesTANGENT.xyz);
  tmpvar_2.w = _glesTANGENT.w;
  highp vec3 tmpvar_3;
  highp vec4 tmpvar_4;
  tmpvar_4 = (_Object2World * _glesVertex);
  highp vec4 tmpvar_5;
  tmpvar_5.w = 0.0;
  tmpvar_5.xyz = tmpvar_1;
  highp vec4 tmpvar_6;
  tmpvar_6.w = 0.0;
  tmpvar_6.xyz = tmpvar_1;
  highp vec3 tmpvar_7;
  tmpvar_7 = normalize ((tmpvar_6 * _World2Object).xyz);
  highp vec4 tmpvar_8;
  tmpvar_8.w = 1.0;
  tmpvar_8.xyz = _WorldSpaceCameraPos;
  highp vec4 tmpvar_9;
  tmpvar_9.w = 0.0;
  tmpvar_9.xyz = tmpvar_2.xyz;
  highp vec4 tmpvar_10;
  tmpvar_10.w = 1.0;
  tmpvar_10.x = unity_4LightPosX0.x;
  tmpvar_10.y = unity_4LightPosY0.x;
  tmpvar_10.z = unity_4LightPosZ0.x;
  highp vec3 tmpvar_11;
  tmpvar_11 = (tmpvar_10 - tmpvar_4).xyz;
  tmpvar_3 = (((1.0/((1.0 + (unity_4LightAtten0.x * dot (tmpvar_11, tmpvar_11))))) * unity_LightColor[0].xyz) * max (0.0, dot (tmpvar_7, normalize (tmpvar_11))));
  highp vec4 tmpvar_12;
  tmpvar_12.w = 1.0;
  tmpvar_12.x = unity_4LightPosX0.y;
  tmpvar_12.y = unity_4LightPosY0.y;
  tmpvar_12.z = unity_4LightPosZ0.y;
  highp vec3 tmpvar_13;
  tmpvar_13 = (tmpvar_12 - tmpvar_4).xyz;
  tmpvar_3 = (tmpvar_3 + (((1.0/((1.0 + (unity_4LightAtten0.y * dot (tmpvar_13, tmpvar_13))))) * unity_LightColor[1].xyz) * max (0.0, dot (tmpvar_7, normalize (tmpvar_13)))));
  highp vec4 tmpvar_14;
  tmpvar_14.w = 1.0;
  tmpvar_14.x = unity_4LightPosX0.z;
  tmpvar_14.y = unity_4LightPosY0.z;
  tmpvar_14.z = unity_4LightPosZ0.z;
  highp vec3 tmpvar_15;
  tmpvar_15 = (tmpvar_14 - tmpvar_4).xyz;
  tmpvar_3 = (tmpvar_3 + (((1.0/((1.0 + (unity_4LightAtten0.z * dot (tmpvar_15, tmpvar_15))))) * unity_LightColor[2].xyz) * max (0.0, dot (tmpvar_7, normalize (tmpvar_15)))));
  highp vec4 tmpvar_16;
  tmpvar_16.w = 1.0;
  tmpvar_16.x = unity_4LightPosX0.w;
  tmpvar_16.y = unity_4LightPosY0.w;
  tmpvar_16.z = unity_4LightPosZ0.w;
  highp vec3 tmpvar_17;
  tmpvar_17 = (tmpvar_16 - tmpvar_4).xyz;
  tmpvar_3 = (tmpvar_3 + (((1.0/((1.0 + (unity_4LightAtten0.w * dot (tmpvar_17, tmpvar_17))))) * unity_LightColor[3].xyz) * max (0.0, dot (tmpvar_7, normalize (tmpvar_17)))));
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_4;
  xlv_TEXCOORD1 = tmpvar_7;
  xlv_TEXCOORD2 = tmpvar_3;
  xlv_TEXCOORD3 = _glesMultiTexCoord0;
  xlv_TEXCOORD4 = normalize (((_Object2World * _glesVertex) - tmpvar_8).xyz);
  xlv_TEXCOORD5 = (_Object2World * tmpvar_5).xyz;
  xlv_TEXCOORD6 = normalize ((_Object2World * tmpvar_9).xyz);
}



#endif
#ifdef FRAGMENT
#define unity_LightColor0 _glesLightSource[0].diffuse
#define unity_LightColor1 _glesLightSource[1].diffuse
#define unity_LightColor2 _glesLightSource[2].diffuse
#define unity_LightColor3 _glesLightSource[3].diffuse
#define unity_LightPosition0 _glesLightSource[0].position
#define unity_LightPosition1 _glesLightSource[1].position
#define unity_LightPosition2 _glesLightSource[2].position
#define unity_LightPosition3 _glesLightSource[3].position
#define glstate_light0_spotDirection _glesLightSource[0].spotDirection
#define glstate_light1_spotDirection _glesLightSource[1].spotDirection
#define glstate_light2_spotDirection _glesLightSource[2].spotDirection
#define glstate_light3_spotDirection _glesLightSource[3].spotDirection
#define unity_LightAtten0 _glesLightSource[0].atten
#define unity_LightAtten1 _glesLightSource[1].atten
#define unity_LightAtten2 _glesLightSource[2].atten
#define unity_LightAtten3 _glesLightSource[3].atten
#define glstate_lightmodel_ambient _glesLightModel.ambient
#define gl_LightSource _glesLightSource
#define gl_LightSourceParameters _glesLightSourceParameters
struct _glesLightSourceParameters {
  vec4 diffuse;
  vec4 position;
  vec3 spotDirection;
  vec4 atten;
};
uniform _glesLightSourceParameters _glesLightSource[4];
#define gl_LightModel _glesLightModel
#define gl_LightModelParameters _glesLightModelParameters
struct _glesLightModelParameters {
  vec4 ambient;
};
uniform _glesLightModelParameters _glesLightModel;
#define gl_FrontMaterial _glesFrontMaterial
#define gl_BackMaterial _glesFrontMaterial
#define gl_MaterialParameters _glesMaterialParameters
struct _glesMaterialParameters {
  vec4 emission;
  vec4 ambient;
  vec4 diffuse;
  vec4 specular;
  float shininess;
};
uniform _glesMaterialParameters _glesFrontMaterial;

varying highp vec3 xlv_TEXCOORD4;
varying highp vec4 xlv_TEXCOORD3;
varying highp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;

uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp vec3 _WorldSpaceCameraPos;
uniform sampler2D _MainTex;
uniform highp vec4 _LightColor0;
uniform samplerCube _Cube;
void main ()
{
  highp vec4 tmpvar_1;
  lowp vec4 color;
  lowp float SurfAngle;
  highp vec4 reflTex;
  highp vec3 specularReflection;
  highp vec3 lightDirection;
  highp vec4 textureColor;
  highp vec3 tmpvar_2;
  tmpvar_2 = normalize (xlv_TEXCOORD1);
  highp vec3 tmpvar_3;
  tmpvar_3 = normalize ((_WorldSpaceCameraPos - xlv_TEXCOORD0.xyz));
  highp vec2 tmpvar_4;
  tmpvar_4 = xlv_TEXCOORD3.xy;
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_MainTex, tmpvar_4);
  textureColor = tmpvar_5;
  if ((0.0 == _WorldSpaceLightPos0.w)) {
    lowp vec3 tmpvar_6;
    tmpvar_6 = normalize (_WorldSpaceLightPos0.xyz);
    lightDirection = tmpvar_6;
  } else {
    lightDirection = normalize ((_WorldSpaceLightPos0 - xlv_TEXCOORD0).xyz);
  };
  highp vec3 tmpvar_7;
  tmpvar_7 = (_LightColor0.xyz * max (0.0, dot (tmpvar_2, lightDirection)));
  highp float tmpvar_8;
  tmpvar_8 = dot (tmpvar_2, lightDirection);
  if ((tmpvar_8 < 0.0)) {
    specularReflection = vec3(0.0, 0.0, 0.0);
  } else {
    specularReflection = (_LightColor0.xyz * pow (max (0.0, dot (reflect (-(lightDirection), tmpvar_2), tmpvar_3)), 0.06));
  };
  highp vec3 tmpvar_9;
  tmpvar_9 = (specularReflection * 0.1);
  specularReflection = tmpvar_9;
  highp vec3 tmpvar_10;
  tmpvar_10 = reflect (xlv_TEXCOORD4, normalize (xlv_TEXCOORD1));
  lowp vec4 tmpvar_11;
  tmpvar_11 = textureCube (_Cube, tmpvar_10);
  reflTex = tmpvar_11;
  highp float tmpvar_12;
  tmpvar_12 = clamp (abs (dot (tmpvar_10, xlv_TEXCOORD1)), 0.0, 1.0);
  SurfAngle = tmpvar_12;
  lowp float tmpvar_13;
  tmpvar_13 = (pow ((1.0 - SurfAngle), 2.51) * 0.64);
  lowp float tmpvar_14;
  tmpvar_14 = clamp ((0.07 + tmpvar_13), 0.0, 1.0);
  reflTex.xyz = (reflTex.xyz * tmpvar_14);
  highp vec4 tmpvar_15;
  tmpvar_15.w = 1.0;
  tmpvar_15.xyz = ((textureColor.xyz * clamp (((xlv_TEXCOORD2 + gl_LightModel.ambient.xyz) + tmpvar_7), 0.0, 1.0)) + tmpvar_9);
  color = tmpvar_15;
  highp vec4 tmpvar_16;
  tmpvar_16 = (color + reflTex);
  color = tmpvar_16;
  highp vec4 tmpvar_17;
  tmpvar_17 = (color + (tmpvar_13 * reflTex));
  color = tmpvar_17;
  tmpvar_1 = color;
  gl_FragData[0] = tmpvar_1;
}



#endif"
}

SubProgram "glesdesktop " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" "VERTEXLIGHT_ON" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec3 xlv_TEXCOORD6;
varying highp vec3 xlv_TEXCOORD5;
varying highp vec3 xlv_TEXCOORD4;
varying highp vec4 xlv_TEXCOORD3;
varying highp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 unity_LightColor[4];
uniform highp vec4 unity_4LightPosZ0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightAtten0;

uniform highp vec3 _WorldSpaceCameraPos;
uniform highp mat4 _World2Object;
uniform highp mat4 _Object2World;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec3 tmpvar_1;
  tmpvar_1 = normalize (_glesNormal);
  vec4 tmpvar_2;
  tmpvar_2.xyz = normalize (_glesTANGENT.xyz);
  tmpvar_2.w = _glesTANGENT.w;
  highp vec3 tmpvar_3;
  highp vec4 tmpvar_4;
  tmpvar_4 = (_Object2World * _glesVertex);
  highp vec4 tmpvar_5;
  tmpvar_5.w = 0.0;
  tmpvar_5.xyz = tmpvar_1;
  highp vec4 tmpvar_6;
  tmpvar_6.w = 0.0;
  tmpvar_6.xyz = tmpvar_1;
  highp vec3 tmpvar_7;
  tmpvar_7 = normalize ((tmpvar_6 * _World2Object).xyz);
  highp vec4 tmpvar_8;
  tmpvar_8.w = 1.0;
  tmpvar_8.xyz = _WorldSpaceCameraPos;
  highp vec4 tmpvar_9;
  tmpvar_9.w = 0.0;
  tmpvar_9.xyz = tmpvar_2.xyz;
  highp vec4 tmpvar_10;
  tmpvar_10.w = 1.0;
  tmpvar_10.x = unity_4LightPosX0.x;
  tmpvar_10.y = unity_4LightPosY0.x;
  tmpvar_10.z = unity_4LightPosZ0.x;
  highp vec3 tmpvar_11;
  tmpvar_11 = (tmpvar_10 - tmpvar_4).xyz;
  tmpvar_3 = (((1.0/((1.0 + (unity_4LightAtten0.x * dot (tmpvar_11, tmpvar_11))))) * unity_LightColor[0].xyz) * max (0.0, dot (tmpvar_7, normalize (tmpvar_11))));
  highp vec4 tmpvar_12;
  tmpvar_12.w = 1.0;
  tmpvar_12.x = unity_4LightPosX0.y;
  tmpvar_12.y = unity_4LightPosY0.y;
  tmpvar_12.z = unity_4LightPosZ0.y;
  highp vec3 tmpvar_13;
  tmpvar_13 = (tmpvar_12 - tmpvar_4).xyz;
  tmpvar_3 = (tmpvar_3 + (((1.0/((1.0 + (unity_4LightAtten0.y * dot (tmpvar_13, tmpvar_13))))) * unity_LightColor[1].xyz) * max (0.0, dot (tmpvar_7, normalize (tmpvar_13)))));
  highp vec4 tmpvar_14;
  tmpvar_14.w = 1.0;
  tmpvar_14.x = unity_4LightPosX0.z;
  tmpvar_14.y = unity_4LightPosY0.z;
  tmpvar_14.z = unity_4LightPosZ0.z;
  highp vec3 tmpvar_15;
  tmpvar_15 = (tmpvar_14 - tmpvar_4).xyz;
  tmpvar_3 = (tmpvar_3 + (((1.0/((1.0 + (unity_4LightAtten0.z * dot (tmpvar_15, tmpvar_15))))) * unity_LightColor[2].xyz) * max (0.0, dot (tmpvar_7, normalize (tmpvar_15)))));
  highp vec4 tmpvar_16;
  tmpvar_16.w = 1.0;
  tmpvar_16.x = unity_4LightPosX0.w;
  tmpvar_16.y = unity_4LightPosY0.w;
  tmpvar_16.z = unity_4LightPosZ0.w;
  highp vec3 tmpvar_17;
  tmpvar_17 = (tmpvar_16 - tmpvar_4).xyz;
  tmpvar_3 = (tmpvar_3 + (((1.0/((1.0 + (unity_4LightAtten0.w * dot (tmpvar_17, tmpvar_17))))) * unity_LightColor[3].xyz) * max (0.0, dot (tmpvar_7, normalize (tmpvar_17)))));
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_4;
  xlv_TEXCOORD1 = tmpvar_7;
  xlv_TEXCOORD2 = tmpvar_3;
  xlv_TEXCOORD3 = _glesMultiTexCoord0;
  xlv_TEXCOORD4 = normalize (((_Object2World * _glesVertex) - tmpvar_8).xyz);
  xlv_TEXCOORD5 = (_Object2World * tmpvar_5).xyz;
  xlv_TEXCOORD6 = normalize ((_Object2World * tmpvar_9).xyz);
}



#endif
#ifdef FRAGMENT
#define unity_LightColor0 _glesLightSource[0].diffuse
#define unity_LightColor1 _glesLightSource[1].diffuse
#define unity_LightColor2 _glesLightSource[2].diffuse
#define unity_LightColor3 _glesLightSource[3].diffuse
#define unity_LightPosition0 _glesLightSource[0].position
#define unity_LightPosition1 _glesLightSource[1].position
#define unity_LightPosition2 _glesLightSource[2].position
#define unity_LightPosition3 _glesLightSource[3].position
#define glstate_light0_spotDirection _glesLightSource[0].spotDirection
#define glstate_light1_spotDirection _glesLightSource[1].spotDirection
#define glstate_light2_spotDirection _glesLightSource[2].spotDirection
#define glstate_light3_spotDirection _glesLightSource[3].spotDirection
#define unity_LightAtten0 _glesLightSource[0].atten
#define unity_LightAtten1 _glesLightSource[1].atten
#define unity_LightAtten2 _glesLightSource[2].atten
#define unity_LightAtten3 _glesLightSource[3].atten
#define glstate_lightmodel_ambient _glesLightModel.ambient
#define gl_LightSource _glesLightSource
#define gl_LightSourceParameters _glesLightSourceParameters
struct _glesLightSourceParameters {
  vec4 diffuse;
  vec4 position;
  vec3 spotDirection;
  vec4 atten;
};
uniform _glesLightSourceParameters _glesLightSource[4];
#define gl_LightModel _glesLightModel
#define gl_LightModelParameters _glesLightModelParameters
struct _glesLightModelParameters {
  vec4 ambient;
};
uniform _glesLightModelParameters _glesLightModel;
#define gl_FrontMaterial _glesFrontMaterial
#define gl_BackMaterial _glesFrontMaterial
#define gl_MaterialParameters _glesMaterialParameters
struct _glesMaterialParameters {
  vec4 emission;
  vec4 ambient;
  vec4 diffuse;
  vec4 specular;
  float shininess;
};
uniform _glesMaterialParameters _glesFrontMaterial;

varying highp vec3 xlv_TEXCOORD4;
varying highp vec4 xlv_TEXCOORD3;
varying highp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;

uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp vec3 _WorldSpaceCameraPos;
uniform sampler2D _MainTex;
uniform highp vec4 _LightColor0;
uniform samplerCube _Cube;
void main ()
{
  highp vec4 tmpvar_1;
  lowp vec4 color;
  lowp float SurfAngle;
  highp vec4 reflTex;
  highp vec3 specularReflection;
  highp vec3 lightDirection;
  highp vec4 textureColor;
  highp vec3 tmpvar_2;
  tmpvar_2 = normalize (xlv_TEXCOORD1);
  highp vec3 tmpvar_3;
  tmpvar_3 = normalize ((_WorldSpaceCameraPos - xlv_TEXCOORD0.xyz));
  highp vec2 tmpvar_4;
  tmpvar_4 = xlv_TEXCOORD3.xy;
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_MainTex, tmpvar_4);
  textureColor = tmpvar_5;
  if ((0.0 == _WorldSpaceLightPos0.w)) {
    lowp vec3 tmpvar_6;
    tmpvar_6 = normalize (_WorldSpaceLightPos0.xyz);
    lightDirection = tmpvar_6;
  } else {
    lightDirection = normalize ((_WorldSpaceLightPos0 - xlv_TEXCOORD0).xyz);
  };
  highp vec3 tmpvar_7;
  tmpvar_7 = (_LightColor0.xyz * max (0.0, dot (tmpvar_2, lightDirection)));
  highp float tmpvar_8;
  tmpvar_8 = dot (tmpvar_2, lightDirection);
  if ((tmpvar_8 < 0.0)) {
    specularReflection = vec3(0.0, 0.0, 0.0);
  } else {
    specularReflection = (_LightColor0.xyz * pow (max (0.0, dot (reflect (-(lightDirection), tmpvar_2), tmpvar_3)), 0.06));
  };
  highp vec3 tmpvar_9;
  tmpvar_9 = (specularReflection * 0.1);
  specularReflection = tmpvar_9;
  highp vec3 tmpvar_10;
  tmpvar_10 = reflect (xlv_TEXCOORD4, normalize (xlv_TEXCOORD1));
  lowp vec4 tmpvar_11;
  tmpvar_11 = textureCube (_Cube, tmpvar_10);
  reflTex = tmpvar_11;
  highp float tmpvar_12;
  tmpvar_12 = clamp (abs (dot (tmpvar_10, xlv_TEXCOORD1)), 0.0, 1.0);
  SurfAngle = tmpvar_12;
  lowp float tmpvar_13;
  tmpvar_13 = (pow ((1.0 - SurfAngle), 2.51) * 0.64);
  lowp float tmpvar_14;
  tmpvar_14 = clamp ((0.07 + tmpvar_13), 0.0, 1.0);
  reflTex.xyz = (reflTex.xyz * tmpvar_14);
  highp vec4 tmpvar_15;
  tmpvar_15.w = 1.0;
  tmpvar_15.xyz = ((textureColor.xyz * clamp (((xlv_TEXCOORD2 + gl_LightModel.ambient.xyz) + tmpvar_7), 0.0, 1.0)) + tmpvar_9);
  color = tmpvar_15;
  highp vec4 tmpvar_16;
  tmpvar_16 = (color + reflTex);
  color = tmpvar_16;
  highp vec4 tmpvar_17;
  tmpvar_17 = (color + (tmpvar_13 * reflTex));
  color = tmpvar_17;
  tmpvar_1 = color;
  gl_FragData[0] = tmpvar_1;
}



#endif"
}

SubProgram "opengl " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" "VERTEXLIGHT_ON" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "tangent" ATTR14
Vector 13 [_ProjectionParams]
Vector 14 [_WorldSpaceCameraPos]
Matrix 5 [_Object2World]
Matrix 9 [_World2Object]
Vector 15 [unity_4LightPosX0]
Vector 16 [unity_4LightPosY0]
Vector 17 [unity_4LightPosZ0]
Vector 18 [unity_4LightAtten0]
Vector 19 [unity_LightColor0]
Vector 20 [unity_LightColor1]
Vector 21 [unity_LightColor2]
Vector 22 [unity_LightColor3]
"3.0-!!ARBvp1.0
# 96 ALU
PARAM c[23] = { { 0, 1, 0.5 },
		state.matrix.mvp,
		program.local[5..22] };
TEMP R0;
TEMP R1;
TEMP R2;
TEMP R3;
TEMP R4;
MUL R0.xyz, vertex.normal.y, c[10];
MAD R0.xyz, vertex.normal.x, c[9], R0;
MAD R2.xyz, vertex.normal.z, c[11], R0;
ADD R2.xyz, R2, c[0].x;
DP3 R1.w, R2, R2;
RSQ R1.w, R1.w;
MUL R2.xyz, R1.w, R2;
DP4 R0.z, vertex.position, c[7];
DP4 R0.y, vertex.position, c[6];
DP4 R0.x, vertex.position, c[5];
MOV R1.x, c[15].z;
MOV R1.z, c[17];
MOV R1.y, c[16].z;
ADD R1.xyz, -R0, R1;
DP3 R0.w, R1, R1;
RSQ R2.w, R0.w;
MUL R1.xyz, R2.w, R1;
DP3 R1.w, R2, R1;
MOV R1.x, c[15];
MOV R1.z, c[17].x;
MOV R1.y, c[16].x;
ADD R3.xyz, -R0, R1;
DP3 R2.w, R3, R3;
RSQ R4.y, R2.w;
MUL R3.xyz, R4.y, R3;
DP3 R3.x, R2, R3;
MAX R1.w, R1, c[0].x;
MUL R0.w, R0, c[18].z;
MOV R1.x, c[15].y;
MOV R1.z, c[17].y;
MOV R1.y, c[16];
ADD R1.xyz, -R0, R1;
DP3 R3.w, R1, R1;
RSQ R4.x, R3.w;
MUL R1.xyz, R4.x, R1;
DP3 R1.x, R2, R1;
MAX R4.x, R3, c[0];
MAX R3.x, R1, c[0];
MUL R1.x, R2.w, c[18];
ADD R1.x, R1, c[0].y;
MUL R1.y, R3.w, c[18];
ADD R1.y, R1, c[0];
RCP R2.w, R1.x;
RCP R1.y, R1.y;
MUL R1.xyz, R1.y, c[20];
MUL R3.xyz, R1, R3.x;
MUL R1.xyz, R2.w, c[19];
MAD R4.xyz, R1, R4.x, R3;
ADD R2.w, R0, c[0].y;
RCP R3.x, R2.w;
MUL R3.xyz, R3.x, c[21];
MAD R3.xyz, R3, R1.w, R4;
MOV R1.w, c[0].x;
MOV R1.x, c[15].w;
MOV R1.z, c[17].w;
MOV R1.y, c[16].w;
ADD R1.xyz, -R0, R1;
DP3 R0.w, R1, R1;
RSQ R2.w, R0.w;
MUL R1.xyz, R2.w, R1;
DP3 R1.y, R2, R1;
MUL R0.w, R0, c[18];
ADD R1.x, R0.w, c[0].y;
MAX R0.w, R1.y, c[0].x;
RCP R2.w, R1.x;
MOV R1.xyz, vertex.attrib[14];
DP4 R4.z, R1, c[7];
DP4 R4.x, R1, c[5];
DP4 R4.y, R1, c[6];
MUL R1.xyz, R2.w, c[22];
MAD result.texcoord[2].xyz, R1, R0.w, R3;
DP3 R1.w, R4, R4;
RSQ R0.w, R1.w;
MUL result.texcoord[6].xyz, R0.w, R4;
DP4 R0.w, vertex.position, c[8];
MOV result.texcoord[0], R0;
DP4 R1.w, vertex.position, c[4];
DP4 R1.z, vertex.position, c[3];
MOV R0.w, c[0].x;
DP4 R1.x, vertex.position, c[1];
DP4 R1.y, vertex.position, c[2];
MUL R3.xyz, R1.xyww, c[0].z;
MUL R3.y, R3, c[13].x;
ADD result.texcoord[7].xy, R3, R3.z;
ADD R3.xyz, R0, -c[14];
DP3 R0.x, R3, R3;
RSQ R0.x, R0.x;
MUL result.texcoord[4].xyz, R0.x, R3;
MOV R0.xyz, vertex.normal;
MOV result.position, R1;
MOV result.texcoord[1].xyz, R2;
MOV result.texcoord[3], vertex.texcoord[0];
DP4 result.texcoord[5].z, R0, c[7];
DP4 result.texcoord[5].y, R0, c[6];
DP4 result.texcoord[5].x, R0, c[5];
MOV result.texcoord[7].zw, R1;
END
# 96 instructions, 5 R-regs
"
}

SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" "VERTEXLIGHT_ON" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "tangent" TexCoord2
Matrix 0 [glstate_matrix_mvp]
Vector 12 [_ProjectionParams]
Vector 13 [_ScreenParams]
Vector 14 [_WorldSpaceCameraPos]
Matrix 4 [_Object2World]
Matrix 8 [_World2Object]
Vector 15 [unity_4LightPosX0]
Vector 16 [unity_4LightPosY0]
Vector 17 [unity_4LightPosZ0]
Vector 18 [unity_4LightAtten0]
Vector 19 [unity_LightColor0]
Vector 20 [unity_LightColor1]
Vector 21 [unity_LightColor2]
Vector 22 [unity_LightColor3]
"vs_3_0
; 96 ALU
dcl_position o0
dcl_texcoord0 o1
dcl_texcoord1 o2
dcl_texcoord2 o3
dcl_texcoord3 o4
dcl_texcoord4 o5
dcl_texcoord5 o6
dcl_texcoord6 o7
dcl_texcoord7 o8
def c23, 0.00000000, 1.00000000, 0.50000000, 0
dcl_position0 v0
dcl_normal0 v1
dcl_texcoord0 v2
dcl_tangent0 v3
mul r0.xyz, v1.y, c9
mad r0.xyz, v1.x, c8, r0
mad r2.xyz, v1.z, c10, r0
add r2.xyz, r2, c23.x
dp3 r1.w, r2, r2
rsq r1.w, r1.w
mul r2.xyz, r1.w, r2
dp4 r0.z, v0, c6
dp4 r0.y, v0, c5
dp4 r0.x, v0, c4
mov r1.x, c15.z
mov r1.z, c17
mov r1.y, c16.z
add r1.xyz, -r0, r1
dp3 r0.w, r1, r1
rsq r2.w, r0.w
mul r1.xyz, r2.w, r1
dp3 r1.w, r2, r1
mov r1.x, c15
mov r1.z, c17.x
mov r1.y, c16.x
add r3.xyz, -r0, r1
dp3 r2.w, r3, r3
rsq r4.y, r2.w
mul r3.xyz, r4.y, r3
dp3 r3.x, r2, r3
max r1.w, r1, c23.x
mul r0.w, r0, c18.z
mov r1.x, c15.y
mov r1.z, c17.y
mov r1.y, c16
add r1.xyz, -r0, r1
dp3 r3.w, r1, r1
rsq r4.x, r3.w
mul r1.xyz, r4.x, r1
dp3 r1.x, r2, r1
max r4.x, r3, c23
max r3.x, r1, c23
mul r1.x, r2.w, c18
add r1.x, r1, c23.y
mul r1.y, r3.w, c18
add r1.y, r1, c23
rcp r2.w, r1.x
rcp r1.y, r1.y
mul r1.xyz, r1.y, c20
mul r3.xyz, r1, r3.x
mul r1.xyz, r2.w, c19
mad r4.xyz, r1, r4.x, r3
add r2.w, r0, c23.y
rcp r3.x, r2.w
mul r3.xyz, r3.x, c21
mad r3.xyz, r3, r1.w, r4
mov r1.w, c23.x
mov r1.x, c15.w
mov r1.z, c17.w
mov r1.y, c16.w
add r1.xyz, -r0, r1
dp3 r0.w, r1, r1
rsq r2.w, r0.w
mul r1.xyz, r2.w, r1
dp3 r1.y, r2, r1
mul r0.w, r0, c18
add r1.x, r0.w, c23.y
max r0.w, r1.y, c23.x
rcp r2.w, r1.x
mov r1.xyz, v3
dp4 r4.z, r1, c6
dp4 r4.x, r1, c4
dp4 r4.y, r1, c5
mul r1.xyz, r2.w, c22
mad o3.xyz, r1, r0.w, r3
dp3 r1.w, r4, r4
rsq r0.w, r1.w
mul o7.xyz, r0.w, r4
dp4 r0.w, v0, c7
mov o1, r0
dp4 r1.w, v0, c3
dp4 r1.z, v0, c2
mov r0.w, c23.x
dp4 r1.x, v0, c0
dp4 r1.y, v0, c1
mul r3.xyz, r1.xyww, c23.z
mul r3.y, r3, c12.x
mad o8.xy, r3.z, c13.zwzw, r3
add r3.xyz, r0, -c14
dp3 r0.x, r3, r3
rsq r0.x, r0.x
mul o5.xyz, r0.x, r3
mov r0.xyz, v1
mov o0, r1
mov o2.xyz, r2
mov o4, v2
dp4 o6.z, r0, c6
dp4 o6.y, r0, c5
dp4 o6.x, r0, c4
mov o8.zw, r1
"
}

SubProgram "gles " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" "VERTEXLIGHT_ON" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec4 xlv_TEXCOORD7;
varying highp vec3 xlv_TEXCOORD6;
varying highp vec3 xlv_TEXCOORD5;
varying highp vec3 xlv_TEXCOORD4;
varying highp vec4 xlv_TEXCOORD3;
varying highp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 unity_LightColor[4];
uniform highp vec4 unity_4LightPosZ0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightAtten0;

uniform highp vec3 _WorldSpaceCameraPos;
uniform highp mat4 _World2Object;
uniform highp vec4 _ProjectionParams;
uniform highp mat4 _Object2World;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec3 tmpvar_1;
  tmpvar_1 = normalize (_glesNormal);
  vec4 tmpvar_2;
  tmpvar_2.xyz = normalize (_glesTANGENT.xyz);
  tmpvar_2.w = _glesTANGENT.w;
  highp vec3 tmpvar_3;
  highp vec4 tmpvar_4;
  tmpvar_4 = (_Object2World * _glesVertex);
  highp vec4 tmpvar_5;
  tmpvar_5.w = 0.0;
  tmpvar_5.xyz = tmpvar_1;
  highp vec4 tmpvar_6;
  tmpvar_6.w = 0.0;
  tmpvar_6.xyz = tmpvar_1;
  highp vec3 tmpvar_7;
  tmpvar_7 = normalize ((tmpvar_6 * _World2Object).xyz);
  highp vec4 tmpvar_8;
  tmpvar_8 = (gl_ModelViewProjectionMatrix * _glesVertex);
  highp vec4 tmpvar_9;
  tmpvar_9.w = 1.0;
  tmpvar_9.xyz = _WorldSpaceCameraPos;
  highp vec4 tmpvar_10;
  tmpvar_10.w = 0.0;
  tmpvar_10.xyz = tmpvar_2.xyz;
  highp vec4 tmpvar_11;
  tmpvar_11.w = 1.0;
  tmpvar_11.x = unity_4LightPosX0.x;
  tmpvar_11.y = unity_4LightPosY0.x;
  tmpvar_11.z = unity_4LightPosZ0.x;
  highp vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_11 - tmpvar_4).xyz;
  tmpvar_3 = (((1.0/((1.0 + (unity_4LightAtten0.x * dot (tmpvar_12, tmpvar_12))))) * unity_LightColor[0].xyz) * max (0.0, dot (tmpvar_7, normalize (tmpvar_12))));
  highp vec4 tmpvar_13;
  tmpvar_13.w = 1.0;
  tmpvar_13.x = unity_4LightPosX0.y;
  tmpvar_13.y = unity_4LightPosY0.y;
  tmpvar_13.z = unity_4LightPosZ0.y;
  highp vec3 tmpvar_14;
  tmpvar_14 = (tmpvar_13 - tmpvar_4).xyz;
  tmpvar_3 = (tmpvar_3 + (((1.0/((1.0 + (unity_4LightAtten0.y * dot (tmpvar_14, tmpvar_14))))) * unity_LightColor[1].xyz) * max (0.0, dot (tmpvar_7, normalize (tmpvar_14)))));
  highp vec4 tmpvar_15;
  tmpvar_15.w = 1.0;
  tmpvar_15.x = unity_4LightPosX0.z;
  tmpvar_15.y = unity_4LightPosY0.z;
  tmpvar_15.z = unity_4LightPosZ0.z;
  highp vec3 tmpvar_16;
  tmpvar_16 = (tmpvar_15 - tmpvar_4).xyz;
  tmpvar_3 = (tmpvar_3 + (((1.0/((1.0 + (unity_4LightAtten0.z * dot (tmpvar_16, tmpvar_16))))) * unity_LightColor[2].xyz) * max (0.0, dot (tmpvar_7, normalize (tmpvar_16)))));
  highp vec4 tmpvar_17;
  tmpvar_17.w = 1.0;
  tmpvar_17.x = unity_4LightPosX0.w;
  tmpvar_17.y = unity_4LightPosY0.w;
  tmpvar_17.z = unity_4LightPosZ0.w;
  highp vec3 tmpvar_18;
  tmpvar_18 = (tmpvar_17 - tmpvar_4).xyz;
  tmpvar_3 = (tmpvar_3 + (((1.0/((1.0 + (unity_4LightAtten0.w * dot (tmpvar_18, tmpvar_18))))) * unity_LightColor[3].xyz) * max (0.0, dot (tmpvar_7, normalize (tmpvar_18)))));
  highp vec4 o_i0;
  highp vec4 tmpvar_19;
  tmpvar_19 = (tmpvar_8 * 0.5);
  o_i0 = tmpvar_19;
  highp vec2 tmpvar_20;
  tmpvar_20.x = tmpvar_19.x;
  tmpvar_20.y = (tmpvar_19.y * _ProjectionParams.x);
  o_i0.xy = (tmpvar_20 + tmpvar_19.w);
  o_i0.zw = tmpvar_8.zw;
  gl_Position = tmpvar_8;
  xlv_TEXCOORD0 = tmpvar_4;
  xlv_TEXCOORD1 = tmpvar_7;
  xlv_TEXCOORD2 = tmpvar_3;
  xlv_TEXCOORD3 = _glesMultiTexCoord0;
  xlv_TEXCOORD4 = normalize (((_Object2World * _glesVertex) - tmpvar_9).xyz);
  xlv_TEXCOORD5 = (_Object2World * tmpvar_5).xyz;
  xlv_TEXCOORD6 = normalize ((_Object2World * tmpvar_10).xyz);
  xlv_TEXCOORD7 = o_i0;
}



#endif
#ifdef FRAGMENT
#define unity_LightColor0 _glesLightSource[0].diffuse
#define unity_LightColor1 _glesLightSource[1].diffuse
#define unity_LightColor2 _glesLightSource[2].diffuse
#define unity_LightColor3 _glesLightSource[3].diffuse
#define unity_LightPosition0 _glesLightSource[0].position
#define unity_LightPosition1 _glesLightSource[1].position
#define unity_LightPosition2 _glesLightSource[2].position
#define unity_LightPosition3 _glesLightSource[3].position
#define glstate_light0_spotDirection _glesLightSource[0].spotDirection
#define glstate_light1_spotDirection _glesLightSource[1].spotDirection
#define glstate_light2_spotDirection _glesLightSource[2].spotDirection
#define glstate_light3_spotDirection _glesLightSource[3].spotDirection
#define unity_LightAtten0 _glesLightSource[0].atten
#define unity_LightAtten1 _glesLightSource[1].atten
#define unity_LightAtten2 _glesLightSource[2].atten
#define unity_LightAtten3 _glesLightSource[3].atten
#define glstate_lightmodel_ambient _glesLightModel.ambient
#define gl_LightSource _glesLightSource
#define gl_LightSourceParameters _glesLightSourceParameters
struct _glesLightSourceParameters {
  vec4 diffuse;
  vec4 position;
  vec3 spotDirection;
  vec4 atten;
};
uniform _glesLightSourceParameters _glesLightSource[4];
#define gl_LightModel _glesLightModel
#define gl_LightModelParameters _glesLightModelParameters
struct _glesLightModelParameters {
  vec4 ambient;
};
uniform _glesLightModelParameters _glesLightModel;
#define gl_FrontMaterial _glesFrontMaterial
#define gl_BackMaterial _glesFrontMaterial
#define gl_MaterialParameters _glesMaterialParameters
struct _glesMaterialParameters {
  vec4 emission;
  vec4 ambient;
  vec4 diffuse;
  vec4 specular;
  float shininess;
};
uniform _glesMaterialParameters _glesFrontMaterial;

varying highp vec4 xlv_TEXCOORD7;
varying highp vec3 xlv_TEXCOORD4;
varying highp vec4 xlv_TEXCOORD3;
varying highp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;

uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp vec3 _WorldSpaceCameraPos;
uniform sampler2D _ShadowMapTexture;
uniform sampler2D _MainTex;
uniform highp vec4 _LightColor0;
uniform samplerCube _Cube;
void main ()
{
  highp vec4 tmpvar_1;
  lowp vec4 color;
  lowp float SurfAngle;
  highp vec4 reflTex;
  highp vec3 specularReflection;
  highp vec3 lightDirection;
  highp float attenuation;
  highp vec4 textureColor;
  highp vec3 tmpvar_2;
  tmpvar_2 = normalize (xlv_TEXCOORD1);
  highp vec3 tmpvar_3;
  tmpvar_3 = normalize ((_WorldSpaceCameraPos - xlv_TEXCOORD0.xyz));
  highp vec2 tmpvar_4;
  tmpvar_4 = xlv_TEXCOORD3.xy;
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_MainTex, tmpvar_4);
  textureColor = tmpvar_5;
  if ((0.0 == _WorldSpaceLightPos0.w)) {
    attenuation = 1.0;
    lowp vec3 tmpvar_6;
    tmpvar_6 = normalize (_WorldSpaceLightPos0.xyz);
    lightDirection = tmpvar_6;
  } else {
    highp vec3 tmpvar_7;
    tmpvar_7 = (_WorldSpaceLightPos0 - xlv_TEXCOORD0).xyz;
    attenuation = (1.0/(length (tmpvar_7)));
    lightDirection = normalize (tmpvar_7);
  };
  lowp float tmpvar_8;
  tmpvar_8 = texture2DProj (_ShadowMapTexture, xlv_TEXCOORD7).x;
  attenuation = tmpvar_8;
  highp vec3 tmpvar_9;
  tmpvar_9 = ((attenuation * _LightColor0.xyz) * max (0.0, dot (tmpvar_2, lightDirection)));
  highp float tmpvar_10;
  tmpvar_10 = dot (tmpvar_2, lightDirection);
  if ((tmpvar_10 < 0.0)) {
    specularReflection = vec3(0.0, 0.0, 0.0);
  } else {
    specularReflection = ((attenuation * _LightColor0.xyz) * pow (max (0.0, dot (reflect (-(lightDirection), tmpvar_2), tmpvar_3)), 0.06));
  };
  highp vec3 tmpvar_11;
  tmpvar_11 = (specularReflection * 0.1);
  specularReflection = tmpvar_11;
  highp vec3 tmpvar_12;
  tmpvar_12 = reflect (xlv_TEXCOORD4, normalize (xlv_TEXCOORD1));
  lowp vec4 tmpvar_13;
  tmpvar_13 = textureCube (_Cube, tmpvar_12);
  reflTex = tmpvar_13;
  highp float tmpvar_14;
  tmpvar_14 = clamp (abs (dot (tmpvar_12, xlv_TEXCOORD1)), 0.0, 1.0);
  SurfAngle = tmpvar_14;
  lowp float tmpvar_15;
  tmpvar_15 = (pow ((1.0 - SurfAngle), 2.51) * 0.64);
  lowp float tmpvar_16;
  tmpvar_16 = clamp ((0.07 + tmpvar_15), 0.0, 1.0);
  reflTex.xyz = (reflTex.xyz * tmpvar_16);
  highp vec4 tmpvar_17;
  tmpvar_17.w = 1.0;
  tmpvar_17.xyz = ((textureColor.xyz * clamp (((xlv_TEXCOORD2 + gl_LightModel.ambient.xyz) + tmpvar_9), 0.0, 1.0)) + tmpvar_11);
  color = tmpvar_17;
  highp vec4 tmpvar_18;
  tmpvar_18 = (color + reflTex);
  color = tmpvar_18;
  highp vec4 tmpvar_19;
  tmpvar_19 = (color + (tmpvar_15 * reflTex));
  color = tmpvar_19;
  tmpvar_1 = color;
  gl_FragData[0] = tmpvar_1;
}



#endif"
}

SubProgram "glesdesktop " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" "VERTEXLIGHT_ON" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec4 xlv_TEXCOORD7;
varying highp vec3 xlv_TEXCOORD6;
varying highp vec3 xlv_TEXCOORD5;
varying highp vec3 xlv_TEXCOORD4;
varying highp vec4 xlv_TEXCOORD3;
varying highp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 unity_LightColor[4];
uniform highp vec4 unity_4LightPosZ0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightAtten0;

uniform highp vec3 _WorldSpaceCameraPos;
uniform highp mat4 _World2Object;
uniform highp vec4 _ProjectionParams;
uniform highp mat4 _Object2World;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec3 tmpvar_1;
  tmpvar_1 = normalize (_glesNormal);
  vec4 tmpvar_2;
  tmpvar_2.xyz = normalize (_glesTANGENT.xyz);
  tmpvar_2.w = _glesTANGENT.w;
  highp vec3 tmpvar_3;
  highp vec4 tmpvar_4;
  tmpvar_4 = (_Object2World * _glesVertex);
  highp vec4 tmpvar_5;
  tmpvar_5.w = 0.0;
  tmpvar_5.xyz = tmpvar_1;
  highp vec4 tmpvar_6;
  tmpvar_6.w = 0.0;
  tmpvar_6.xyz = tmpvar_1;
  highp vec3 tmpvar_7;
  tmpvar_7 = normalize ((tmpvar_6 * _World2Object).xyz);
  highp vec4 tmpvar_8;
  tmpvar_8 = (gl_ModelViewProjectionMatrix * _glesVertex);
  highp vec4 tmpvar_9;
  tmpvar_9.w = 1.0;
  tmpvar_9.xyz = _WorldSpaceCameraPos;
  highp vec4 tmpvar_10;
  tmpvar_10.w = 0.0;
  tmpvar_10.xyz = tmpvar_2.xyz;
  highp vec4 tmpvar_11;
  tmpvar_11.w = 1.0;
  tmpvar_11.x = unity_4LightPosX0.x;
  tmpvar_11.y = unity_4LightPosY0.x;
  tmpvar_11.z = unity_4LightPosZ0.x;
  highp vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_11 - tmpvar_4).xyz;
  tmpvar_3 = (((1.0/((1.0 + (unity_4LightAtten0.x * dot (tmpvar_12, tmpvar_12))))) * unity_LightColor[0].xyz) * max (0.0, dot (tmpvar_7, normalize (tmpvar_12))));
  highp vec4 tmpvar_13;
  tmpvar_13.w = 1.0;
  tmpvar_13.x = unity_4LightPosX0.y;
  tmpvar_13.y = unity_4LightPosY0.y;
  tmpvar_13.z = unity_4LightPosZ0.y;
  highp vec3 tmpvar_14;
  tmpvar_14 = (tmpvar_13 - tmpvar_4).xyz;
  tmpvar_3 = (tmpvar_3 + (((1.0/((1.0 + (unity_4LightAtten0.y * dot (tmpvar_14, tmpvar_14))))) * unity_LightColor[1].xyz) * max (0.0, dot (tmpvar_7, normalize (tmpvar_14)))));
  highp vec4 tmpvar_15;
  tmpvar_15.w = 1.0;
  tmpvar_15.x = unity_4LightPosX0.z;
  tmpvar_15.y = unity_4LightPosY0.z;
  tmpvar_15.z = unity_4LightPosZ0.z;
  highp vec3 tmpvar_16;
  tmpvar_16 = (tmpvar_15 - tmpvar_4).xyz;
  tmpvar_3 = (tmpvar_3 + (((1.0/((1.0 + (unity_4LightAtten0.z * dot (tmpvar_16, tmpvar_16))))) * unity_LightColor[2].xyz) * max (0.0, dot (tmpvar_7, normalize (tmpvar_16)))));
  highp vec4 tmpvar_17;
  tmpvar_17.w = 1.0;
  tmpvar_17.x = unity_4LightPosX0.w;
  tmpvar_17.y = unity_4LightPosY0.w;
  tmpvar_17.z = unity_4LightPosZ0.w;
  highp vec3 tmpvar_18;
  tmpvar_18 = (tmpvar_17 - tmpvar_4).xyz;
  tmpvar_3 = (tmpvar_3 + (((1.0/((1.0 + (unity_4LightAtten0.w * dot (tmpvar_18, tmpvar_18))))) * unity_LightColor[3].xyz) * max (0.0, dot (tmpvar_7, normalize (tmpvar_18)))));
  highp vec4 o_i0;
  highp vec4 tmpvar_19;
  tmpvar_19 = (tmpvar_8 * 0.5);
  o_i0 = tmpvar_19;
  highp vec2 tmpvar_20;
  tmpvar_20.x = tmpvar_19.x;
  tmpvar_20.y = (tmpvar_19.y * _ProjectionParams.x);
  o_i0.xy = (tmpvar_20 + tmpvar_19.w);
  o_i0.zw = tmpvar_8.zw;
  gl_Position = tmpvar_8;
  xlv_TEXCOORD0 = tmpvar_4;
  xlv_TEXCOORD1 = tmpvar_7;
  xlv_TEXCOORD2 = tmpvar_3;
  xlv_TEXCOORD3 = _glesMultiTexCoord0;
  xlv_TEXCOORD4 = normalize (((_Object2World * _glesVertex) - tmpvar_9).xyz);
  xlv_TEXCOORD5 = (_Object2World * tmpvar_5).xyz;
  xlv_TEXCOORD6 = normalize ((_Object2World * tmpvar_10).xyz);
  xlv_TEXCOORD7 = o_i0;
}



#endif
#ifdef FRAGMENT
#define unity_LightColor0 _glesLightSource[0].diffuse
#define unity_LightColor1 _glesLightSource[1].diffuse
#define unity_LightColor2 _glesLightSource[2].diffuse
#define unity_LightColor3 _glesLightSource[3].diffuse
#define unity_LightPosition0 _glesLightSource[0].position
#define unity_LightPosition1 _glesLightSource[1].position
#define unity_LightPosition2 _glesLightSource[2].position
#define unity_LightPosition3 _glesLightSource[3].position
#define glstate_light0_spotDirection _glesLightSource[0].spotDirection
#define glstate_light1_spotDirection _glesLightSource[1].spotDirection
#define glstate_light2_spotDirection _glesLightSource[2].spotDirection
#define glstate_light3_spotDirection _glesLightSource[3].spotDirection
#define unity_LightAtten0 _glesLightSource[0].atten
#define unity_LightAtten1 _glesLightSource[1].atten
#define unity_LightAtten2 _glesLightSource[2].atten
#define unity_LightAtten3 _glesLightSource[3].atten
#define glstate_lightmodel_ambient _glesLightModel.ambient
#define gl_LightSource _glesLightSource
#define gl_LightSourceParameters _glesLightSourceParameters
struct _glesLightSourceParameters {
  vec4 diffuse;
  vec4 position;
  vec3 spotDirection;
  vec4 atten;
};
uniform _glesLightSourceParameters _glesLightSource[4];
#define gl_LightModel _glesLightModel
#define gl_LightModelParameters _glesLightModelParameters
struct _glesLightModelParameters {
  vec4 ambient;
};
uniform _glesLightModelParameters _glesLightModel;
#define gl_FrontMaterial _glesFrontMaterial
#define gl_BackMaterial _glesFrontMaterial
#define gl_MaterialParameters _glesMaterialParameters
struct _glesMaterialParameters {
  vec4 emission;
  vec4 ambient;
  vec4 diffuse;
  vec4 specular;
  float shininess;
};
uniform _glesMaterialParameters _glesFrontMaterial;

varying highp vec4 xlv_TEXCOORD7;
varying highp vec3 xlv_TEXCOORD4;
varying highp vec4 xlv_TEXCOORD3;
varying highp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;

uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp vec3 _WorldSpaceCameraPos;
uniform sampler2D _ShadowMapTexture;
uniform sampler2D _MainTex;
uniform highp vec4 _LightColor0;
uniform samplerCube _Cube;
void main ()
{
  highp vec4 tmpvar_1;
  lowp vec4 color;
  lowp float SurfAngle;
  highp vec4 reflTex;
  highp vec3 specularReflection;
  highp vec3 lightDirection;
  highp float attenuation;
  highp vec4 textureColor;
  highp vec3 tmpvar_2;
  tmpvar_2 = normalize (xlv_TEXCOORD1);
  highp vec3 tmpvar_3;
  tmpvar_3 = normalize ((_WorldSpaceCameraPos - xlv_TEXCOORD0.xyz));
  highp vec2 tmpvar_4;
  tmpvar_4 = xlv_TEXCOORD3.xy;
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_MainTex, tmpvar_4);
  textureColor = tmpvar_5;
  if ((0.0 == _WorldSpaceLightPos0.w)) {
    attenuation = 1.0;
    lowp vec3 tmpvar_6;
    tmpvar_6 = normalize (_WorldSpaceLightPos0.xyz);
    lightDirection = tmpvar_6;
  } else {
    highp vec3 tmpvar_7;
    tmpvar_7 = (_WorldSpaceLightPos0 - xlv_TEXCOORD0).xyz;
    attenuation = (1.0/(length (tmpvar_7)));
    lightDirection = normalize (tmpvar_7);
  };
  lowp float tmpvar_8;
  tmpvar_8 = texture2DProj (_ShadowMapTexture, xlv_TEXCOORD7).x;
  attenuation = tmpvar_8;
  highp vec3 tmpvar_9;
  tmpvar_9 = ((attenuation * _LightColor0.xyz) * max (0.0, dot (tmpvar_2, lightDirection)));
  highp float tmpvar_10;
  tmpvar_10 = dot (tmpvar_2, lightDirection);
  if ((tmpvar_10 < 0.0)) {
    specularReflection = vec3(0.0, 0.0, 0.0);
  } else {
    specularReflection = ((attenuation * _LightColor0.xyz) * pow (max (0.0, dot (reflect (-(lightDirection), tmpvar_2), tmpvar_3)), 0.06));
  };
  highp vec3 tmpvar_11;
  tmpvar_11 = (specularReflection * 0.1);
  specularReflection = tmpvar_11;
  highp vec3 tmpvar_12;
  tmpvar_12 = reflect (xlv_TEXCOORD4, normalize (xlv_TEXCOORD1));
  lowp vec4 tmpvar_13;
  tmpvar_13 = textureCube (_Cube, tmpvar_12);
  reflTex = tmpvar_13;
  highp float tmpvar_14;
  tmpvar_14 = clamp (abs (dot (tmpvar_12, xlv_TEXCOORD1)), 0.0, 1.0);
  SurfAngle = tmpvar_14;
  lowp float tmpvar_15;
  tmpvar_15 = (pow ((1.0 - SurfAngle), 2.51) * 0.64);
  lowp float tmpvar_16;
  tmpvar_16 = clamp ((0.07 + tmpvar_15), 0.0, 1.0);
  reflTex.xyz = (reflTex.xyz * tmpvar_16);
  highp vec4 tmpvar_17;
  tmpvar_17.w = 1.0;
  tmpvar_17.xyz = ((textureColor.xyz * clamp (((xlv_TEXCOORD2 + gl_LightModel.ambient.xyz) + tmpvar_9), 0.0, 1.0)) + tmpvar_11);
  color = tmpvar_17;
  highp vec4 tmpvar_18;
  tmpvar_18 = (color + reflTex);
  color = tmpvar_18;
  highp vec4 tmpvar_19;
  tmpvar_19 = (color + (tmpvar_15 * reflTex));
  color = tmpvar_19;
  tmpvar_1 = color;
  gl_FragData[0] = tmpvar_1;
}



#endif"
}

}
Program "fp" {
// Fragment combos: 6
//   opengl - ALU: 51 to 53, TEX: 2 to 3
//   d3d9 - ALU: 49 to 52, TEX: 2 to 3
SubProgram "opengl " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" }
Vector 1 [_WorldSpaceCameraPos]
Vector 2 [_WorldSpaceLightPos0]
Vector 3 [_LightColor0]
SetTexture 0 [_MainTex] 2D
SetTexture 1 [_Cube] CUBE
"3.0-!!ARBfp1.0
# 51 ALU, 2 TEX
PARAM c[6] = { state.lightmodel.ambient,
		program.local[1..3],
		{ 0, 1, 2, 0.059999999 },
		{ 0.1, 2.5097656, 0.64013672, 0.070007324 } };
TEMP R0;
TEMP R1;
TEMP R2;
TEMP R3;
ADD R0.xyz, -fragment.texcoord[0], c[2];
DP3 R0.w, R0, R0;
RSQ R0.w, R0.w;
MUL R0.xyz, R0.w, R0;
ADD R3.xyz, -fragment.texcoord[0], c[1];
DP3 R1.w, R3, R3;
RSQ R1.w, R1.w;
ABS R0.w, -c[2];
DP3 R1.x, c[2], c[2];
CMP R0.w, -R0, c[4].x, c[4].y;
RSQ R1.x, R1.x;
ABS R0.w, R0;
MUL R3.xyz, R1.w, R3;
CMP R0.w, -R0, c[4].x, c[4].y;
MUL R1.xyz, R1.x, c[2];
CMP R1.xyz, -R0.w, R0, R1;
DP3 R0.x, fragment.texcoord[1], fragment.texcoord[1];
RSQ R0.x, R0.x;
MUL R0.xyz, R0.x, fragment.texcoord[1];
DP3 R0.w, R0, R1;
MUL R2.xyz, -R0.w, R0;
MAD R1.xyz, -R2, c[4].z, -R1;
SLT R1.w, R0, c[4].x;
DP3 R1.x, R1, R3;
DP3 R2.x, R0, fragment.texcoord[4];
MUL R0.xyz, R0, R2.x;
MAX R1.x, R1, c[4];
POW R1.x, R1.x, c[4].w;
ABS R1.w, R1;
CMP R1.w, -R1, c[4].x, c[4].y;
MUL R1.xyz, R1.x, c[3];
CMP R1.xyz, -R1.w, R1, c[4].x;
MAD R0.xyz, -R0, c[4].z, fragment.texcoord[4];
DP3 R1.w, fragment.texcoord[1], R0;
MUL R3.xyz, R1, c[5].x;
ABS_SAT R1.x, R1.w;
ADD R1.w, -R1.x, c[4].y;
POW R1.w, R1.w, c[5].y;
MUL R2.w, R1, c[5].z;
ADD_SAT R1.w, R2, c[5];
MAX R0.w, R0, c[4].x;
ADD R1.xyz, fragment.texcoord[2], c[0];
MAD_SAT R2.xyz, R0.w, c[3], R1;
TEX R0, R0, texture[1], CUBE;
MUL R0.xyz, R0, R1.w;
TEX R1.xyz, fragment.texcoord[3], texture[0], 2D;
MAD R1.xyz, R1, R2, R3;
MUL R2, R0, R2.w;
MOV R1.w, c[4].y;
ADD R0, R1, R0;
ADD result.color, R0, R2;
END
# 51 instructions, 4 R-regs
"
}

SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" }
Vector 0 [glstate_lightmodel_ambient]
Vector 1 [_WorldSpaceCameraPos]
Vector 2 [_WorldSpaceLightPos0]
Vector 3 [_LightColor0]
SetTexture 0 [_MainTex] 2D
SetTexture 1 [_Cube] CUBE
"ps_3_0
; 49 ALU, 2 TEX
dcl_2d s0
dcl_cube s1
def c4, 0.00000000, 1.00000000, 2.00000000, 0.06000000
def c5, 0.10000000, 2.50976563, 0.64013672, 0.07000732
dcl_texcoord0 v0.xyz
dcl_texcoord1 v1.xyz
dcl_texcoord2 v2.xyz
dcl_texcoord3 v3.xy
dcl_texcoord4 v4.xyz
add r1.xyz, -v0, c2
dp3 r0.y, r1, r1
rsq r0.y, r0.y
add r3.xyz, -v0, c1
dp3 r1.w, r3, r3
rsq r1.w, r1.w
dp3 r0.x, c2, c2
mul r2.xyz, r0.y, r1
rsq r0.x, r0.x
mul r1.xyz, r0.x, c2
abs_pp r0.x, -c2.w
cmp r1.xyz, -r0.x, r1, r2
dp3 r0.x, v1, v1
rsq r0.x, r0.x
mul r0.xyz, r0.x, v1
dp3 r0.w, r0, r1
mul r2.xyz, -r0.w, r0
mad r1.xyz, -r2, c4.z, -r1
mul r3.xyz, r1.w, r3
dp3 r1.x, r1, r3
max r2.x, r1, c4
pow r1, r2.x, c4.w
cmp r1.w, r0, c4.x, c4.y
dp3 r1.y, r0, v4
mul r0.xyz, r0, r1.y
mad r0.xyz, -r0, c4.z, v4
dp3 r2.x, v1, r0
abs_sat r2.x, r2
add_pp r2.w, -r2.x, c4.y
abs_pp r1.w, r1
mul r1.xyz, r1.x, c3
cmp r1.xyz, -r1.w, r1, c4.x
mul r2.xyz, r1, c5.x
pow_pp r1, r2.w, c5.y
mul_pp r2.w, r1.x, c5.z
add_pp_sat r1.w, r2, c5
max r0.w, r0, c4.x
add r3.xyz, v2, c0
mad_sat r3.xyz, r0.w, c3, r3
texld r0, r0, s1
mul r0.xyz, r0, r1.w
texld r1.xyz, v3, s0
mad r1.xyz, r1, r3, r2
mul r2, r0, r2.w
mov r1.w, c4.y
add_pp r0, r1, r0
add_pp oC0, r0, r2
"
}

SubProgram "gles " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" }
"!!GLES"
}

SubProgram "glesdesktop " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" }
"!!GLES"
}

SubProgram "opengl " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" }
Vector 1 [_WorldSpaceCameraPos]
Vector 2 [_WorldSpaceLightPos0]
Vector 3 [_LightColor0]
SetTexture 0 [_MainTex] 2D
SetTexture 1 [_Cube] CUBE
"3.0-!!ARBfp1.0
# 51 ALU, 2 TEX
PARAM c[6] = { state.lightmodel.ambient,
		program.local[1..3],
		{ 0, 1, 2, 0.059999999 },
		{ 0.1, 2.5097656, 0.64013672, 0.070007324 } };
TEMP R0;
TEMP R1;
TEMP R2;
TEMP R3;
ADD R0.xyz, -fragment.texcoord[0], c[2];
DP3 R0.w, R0, R0;
RSQ R0.w, R0.w;
MUL R0.xyz, R0.w, R0;
ADD R3.xyz, -fragment.texcoord[0], c[1];
DP3 R1.w, R3, R3;
RSQ R1.w, R1.w;
ABS R0.w, -c[2];
DP3 R1.x, c[2], c[2];
CMP R0.w, -R0, c[4].x, c[4].y;
RSQ R1.x, R1.x;
ABS R0.w, R0;
MUL R3.xyz, R1.w, R3;
CMP R0.w, -R0, c[4].x, c[4].y;
MUL R1.xyz, R1.x, c[2];
CMP R1.xyz, -R0.w, R0, R1;
DP3 R0.x, fragment.texcoord[1], fragment.texcoord[1];
RSQ R0.x, R0.x;
MUL R0.xyz, R0.x, fragment.texcoord[1];
DP3 R0.w, R0, R1;
MUL R2.xyz, -R0.w, R0;
MAD R1.xyz, -R2, c[4].z, -R1;
SLT R1.w, R0, c[4].x;
DP3 R1.x, R1, R3;
DP3 R2.x, R0, fragment.texcoord[4];
MUL R0.xyz, R0, R2.x;
MAX R1.x, R1, c[4];
POW R1.x, R1.x, c[4].w;
ABS R1.w, R1;
CMP R1.w, -R1, c[4].x, c[4].y;
MUL R1.xyz, R1.x, c[3];
CMP R1.xyz, -R1.w, R1, c[4].x;
MAD R0.xyz, -R0, c[4].z, fragment.texcoord[4];
DP3 R1.w, fragment.texcoord[1], R0;
MUL R3.xyz, R1, c[5].x;
ABS_SAT R1.x, R1.w;
ADD R1.w, -R1.x, c[4].y;
POW R1.w, R1.w, c[5].y;
MUL R2.w, R1, c[5].z;
ADD_SAT R1.w, R2, c[5];
MAX R0.w, R0, c[4].x;
ADD R1.xyz, fragment.texcoord[2], c[0];
MAD_SAT R2.xyz, R0.w, c[3], R1;
TEX R0, R0, texture[1], CUBE;
MUL R0.xyz, R0, R1.w;
TEX R1.xyz, fragment.texcoord[3], texture[0], 2D;
MAD R1.xyz, R1, R2, R3;
MUL R2, R0, R2.w;
MOV R1.w, c[4].y;
ADD R0, R1, R0;
ADD result.color, R0, R2;
END
# 51 instructions, 4 R-regs
"
}

SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" }
Vector 0 [glstate_lightmodel_ambient]
Vector 1 [_WorldSpaceCameraPos]
Vector 2 [_WorldSpaceLightPos0]
Vector 3 [_LightColor0]
SetTexture 0 [_MainTex] 2D
SetTexture 1 [_Cube] CUBE
"ps_3_0
; 49 ALU, 2 TEX
dcl_2d s0
dcl_cube s1
def c4, 0.00000000, 1.00000000, 2.00000000, 0.06000000
def c5, 0.10000000, 2.50976563, 0.64013672, 0.07000732
dcl_texcoord0 v0.xyz
dcl_texcoord1 v1.xyz
dcl_texcoord2 v2.xyz
dcl_texcoord3 v3.xy
dcl_texcoord4 v4.xyz
add r1.xyz, -v0, c2
dp3 r0.y, r1, r1
rsq r0.y, r0.y
add r3.xyz, -v0, c1
dp3 r1.w, r3, r3
rsq r1.w, r1.w
dp3 r0.x, c2, c2
mul r2.xyz, r0.y, r1
rsq r0.x, r0.x
mul r1.xyz, r0.x, c2
abs_pp r0.x, -c2.w
cmp r1.xyz, -r0.x, r1, r2
dp3 r0.x, v1, v1
rsq r0.x, r0.x
mul r0.xyz, r0.x, v1
dp3 r0.w, r0, r1
mul r2.xyz, -r0.w, r0
mad r1.xyz, -r2, c4.z, -r1
mul r3.xyz, r1.w, r3
dp3 r1.x, r1, r3
max r2.x, r1, c4
pow r1, r2.x, c4.w
cmp r1.w, r0, c4.x, c4.y
dp3 r1.y, r0, v4
mul r0.xyz, r0, r1.y
mad r0.xyz, -r0, c4.z, v4
dp3 r2.x, v1, r0
abs_sat r2.x, r2
add_pp r2.w, -r2.x, c4.y
abs_pp r1.w, r1
mul r1.xyz, r1.x, c3
cmp r1.xyz, -r1.w, r1, c4.x
mul r2.xyz, r1, c5.x
pow_pp r1, r2.w, c5.y
mul_pp r2.w, r1.x, c5.z
add_pp_sat r1.w, r2, c5
max r0.w, r0, c4.x
add r3.xyz, v2, c0
mad_sat r3.xyz, r0.w, c3, r3
texld r0, r0, s1
mul r0.xyz, r0, r1.w
texld r1.xyz, v3, s0
mad r1.xyz, r1, r3, r2
mul r2, r0, r2.w
mov r1.w, c4.y
add_pp r0, r1, r0
add_pp oC0, r0, r2
"
}

SubProgram "gles " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" }
"!!GLES"
}

SubProgram "glesdesktop " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" }
"!!GLES"
}

SubProgram "opengl " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "SHADOWS_OFF" }
Vector 1 [_WorldSpaceCameraPos]
Vector 2 [_WorldSpaceLightPos0]
Vector 3 [_LightColor0]
SetTexture 0 [_MainTex] 2D
SetTexture 1 [_Cube] CUBE
"3.0-!!ARBfp1.0
# 51 ALU, 2 TEX
PARAM c[6] = { state.lightmodel.ambient,
		program.local[1..3],
		{ 0, 1, 2, 0.059999999 },
		{ 0.1, 2.5097656, 0.64013672, 0.070007324 } };
TEMP R0;
TEMP R1;
TEMP R2;
TEMP R3;
ADD R0.xyz, -fragment.texcoord[0], c[2];
DP3 R0.w, R0, R0;
RSQ R0.w, R0.w;
MUL R0.xyz, R0.w, R0;
ADD R3.xyz, -fragment.texcoord[0], c[1];
DP3 R1.w, R3, R3;
RSQ R1.w, R1.w;
ABS R0.w, -c[2];
DP3 R1.x, c[2], c[2];
CMP R0.w, -R0, c[4].x, c[4].y;
RSQ R1.x, R1.x;
ABS R0.w, R0;
MUL R3.xyz, R1.w, R3;
CMP R0.w, -R0, c[4].x, c[4].y;
MUL R1.xyz, R1.x, c[2];
CMP R1.xyz, -R0.w, R0, R1;
DP3 R0.x, fragment.texcoord[1], fragment.texcoord[1];
RSQ R0.x, R0.x;
MUL R0.xyz, R0.x, fragment.texcoord[1];
DP3 R0.w, R0, R1;
MUL R2.xyz, -R0.w, R0;
MAD R1.xyz, -R2, c[4].z, -R1;
SLT R1.w, R0, c[4].x;
DP3 R1.x, R1, R3;
DP3 R2.x, R0, fragment.texcoord[4];
MUL R0.xyz, R0, R2.x;
MAX R1.x, R1, c[4];
POW R1.x, R1.x, c[4].w;
ABS R1.w, R1;
CMP R1.w, -R1, c[4].x, c[4].y;
MUL R1.xyz, R1.x, c[3];
CMP R1.xyz, -R1.w, R1, c[4].x;
MAD R0.xyz, -R0, c[4].z, fragment.texcoord[4];
DP3 R1.w, fragment.texcoord[1], R0;
MUL R3.xyz, R1, c[5].x;
ABS_SAT R1.x, R1.w;
ADD R1.w, -R1.x, c[4].y;
POW R1.w, R1.w, c[5].y;
MUL R2.w, R1, c[5].z;
ADD_SAT R1.w, R2, c[5];
MAX R0.w, R0, c[4].x;
ADD R1.xyz, fragment.texcoord[2], c[0];
MAD_SAT R2.xyz, R0.w, c[3], R1;
TEX R0, R0, texture[1], CUBE;
MUL R0.xyz, R0, R1.w;
TEX R1.xyz, fragment.texcoord[3], texture[0], 2D;
MAD R1.xyz, R1, R2, R3;
MUL R2, R0, R2.w;
MOV R1.w, c[4].y;
ADD R0, R1, R0;
ADD result.color, R0, R2;
END
# 51 instructions, 4 R-regs
"
}

SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "SHADOWS_OFF" }
Vector 0 [glstate_lightmodel_ambient]
Vector 1 [_WorldSpaceCameraPos]
Vector 2 [_WorldSpaceLightPos0]
Vector 3 [_LightColor0]
SetTexture 0 [_MainTex] 2D
SetTexture 1 [_Cube] CUBE
"ps_3_0
; 49 ALU, 2 TEX
dcl_2d s0
dcl_cube s1
def c4, 0.00000000, 1.00000000, 2.00000000, 0.06000000
def c5, 0.10000000, 2.50976563, 0.64013672, 0.07000732
dcl_texcoord0 v0.xyz
dcl_texcoord1 v1.xyz
dcl_texcoord2 v2.xyz
dcl_texcoord3 v3.xy
dcl_texcoord4 v4.xyz
add r1.xyz, -v0, c2
dp3 r0.y, r1, r1
rsq r0.y, r0.y
add r3.xyz, -v0, c1
dp3 r1.w, r3, r3
rsq r1.w, r1.w
dp3 r0.x, c2, c2
mul r2.xyz, r0.y, r1
rsq r0.x, r0.x
mul r1.xyz, r0.x, c2
abs_pp r0.x, -c2.w
cmp r1.xyz, -r0.x, r1, r2
dp3 r0.x, v1, v1
rsq r0.x, r0.x
mul r0.xyz, r0.x, v1
dp3 r0.w, r0, r1
mul r2.xyz, -r0.w, r0
mad r1.xyz, -r2, c4.z, -r1
mul r3.xyz, r1.w, r3
dp3 r1.x, r1, r3
max r2.x, r1, c4
pow r1, r2.x, c4.w
cmp r1.w, r0, c4.x, c4.y
dp3 r1.y, r0, v4
mul r0.xyz, r0, r1.y
mad r0.xyz, -r0, c4.z, v4
dp3 r2.x, v1, r0
abs_sat r2.x, r2
add_pp r2.w, -r2.x, c4.y
abs_pp r1.w, r1
mul r1.xyz, r1.x, c3
cmp r1.xyz, -r1.w, r1, c4.x
mul r2.xyz, r1, c5.x
pow_pp r1, r2.w, c5.y
mul_pp r2.w, r1.x, c5.z
add_pp_sat r1.w, r2, c5
max r0.w, r0, c4.x
add r3.xyz, v2, c0
mad_sat r3.xyz, r0.w, c3, r3
texld r0, r0, s1
mul r0.xyz, r0, r1.w
texld r1.xyz, v3, s0
mad r1.xyz, r1, r3, r2
mul r2, r0, r2.w
mov r1.w, c4.y
add_pp r0, r1, r0
add_pp oC0, r0, r2
"
}

SubProgram "gles " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "SHADOWS_OFF" }
"!!GLES"
}

SubProgram "glesdesktop " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "SHADOWS_OFF" }
"!!GLES"
}

SubProgram "opengl " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" }
Vector 1 [_WorldSpaceCameraPos]
Vector 2 [_WorldSpaceLightPos0]
Vector 3 [_LightColor0]
SetTexture 0 [_MainTex] 2D
SetTexture 1 [_ShadowMapTexture] 2D
SetTexture 2 [_Cube] CUBE
"3.0-!!ARBfp1.0
# 53 ALU, 3 TEX
PARAM c[6] = { state.lightmodel.ambient,
		program.local[1..3],
		{ 0, 1, 2, 0.059999999 },
		{ 0.1, 2.5097656, 0.64013672, 0.070007324 } };
TEMP R0;
TEMP R1;
TEMP R2;
TEMP R3;
ADD R0.xyz, -fragment.texcoord[0], c[2];
DP3 R0.w, R0, R0;
RSQ R0.w, R0.w;
MUL R0.xyz, R0.w, R0;
ADD R3.xyz, -fragment.texcoord[0], c[1];
DP3 R1.w, R3, R3;
RSQ R1.w, R1.w;
ABS R0.w, -c[2];
DP3 R1.x, c[2], c[2];
CMP R0.w, -R0, c[4].x, c[4].y;
RSQ R1.x, R1.x;
ABS R0.w, R0;
MUL R3.xyz, R1.w, R3;
CMP R0.w, -R0, c[4].x, c[4].y;
MUL R1.xyz, R1.x, c[2];
CMP R1.xyz, -R0.w, R0, R1;
DP3 R0.x, fragment.texcoord[1], fragment.texcoord[1];
RSQ R0.x, R0.x;
MUL R0.xyz, R0.x, fragment.texcoord[1];
DP3 R0.w, R0, R1;
MUL R2.xyz, -R0.w, R0;
MAD R1.xyz, -R2, c[4].z, -R1;
DP3 R1.x, R1, R3;
MAX R1.y, R1.x, c[4].x;
DP3 R2.w, R0, fragment.texcoord[4];
MUL R0.xyz, R0, R2.w;
POW R1.w, R1.y, c[4].w;
TXP R1.x, fragment.texcoord[7], texture[1], 2D;
MUL R1.xyz, R1.x, c[3];
MUL R2.xyz, R1, R1.w;
SLT R1.w, R0, c[4].x;
ABS R1.w, R1;
CMP R1.w, -R1, c[4].x, c[4].y;
CMP R2.xyz, -R1.w, R2, c[4].x;
MAD R0.xyz, -R0, c[4].z, fragment.texcoord[4];
DP3 R1.w, fragment.texcoord[1], R0;
ABS_SAT R2.w, R1;
MAX R1.w, R0, c[4].x;
ADD R0.w, -R2, c[4].y;
POW R0.w, R0.w, c[5].y;
MUL R2.w, R0, c[5].z;
ADD R3.xyz, fragment.texcoord[2], c[0];
MAD_SAT R3.xyz, R1, R1.w, R3;
TEX R0, R0, texture[2], CUBE;
ADD_SAT R1.w, R2, c[5];
MUL R0.xyz, R0, R1.w;
MUL R2.xyz, R2, c[5].x;
TEX R1.xyz, fragment.texcoord[3], texture[0], 2D;
MAD R1.xyz, R1, R3, R2;
MUL R2, R0, R2.w;
MOV R1.w, c[4].y;
ADD R0, R1, R0;
ADD result.color, R0, R2;
END
# 53 instructions, 4 R-regs
"
}

SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" }
Vector 0 [glstate_lightmodel_ambient]
Vector 1 [_WorldSpaceCameraPos]
Vector 2 [_WorldSpaceLightPos0]
Vector 3 [_LightColor0]
SetTexture 0 [_MainTex] 2D
SetTexture 1 [_ShadowMapTexture] 2D
SetTexture 2 [_Cube] CUBE
"ps_3_0
; 52 ALU, 3 TEX
dcl_2d s0
dcl_2d s1
dcl_cube s2
def c4, 0.00000000, 1.00000000, 2.00000000, 0.06000000
def c5, 0.10000000, 2.50976563, 0.64013672, 0.07000732
dcl_texcoord0 v0.xyz
dcl_texcoord1 v1.xyz
dcl_texcoord2 v2.xyz
dcl_texcoord3 v3.xy
dcl_texcoord4 v4.xyz
dcl_texcoord7 v6
add r1.xyz, -v0, c2
dp3 r0.y, r1, r1
rsq r0.y, r0.y
add r3.xyz, -v0, c1
dp3 r1.w, r3, r3
rsq r1.w, r1.w
dp3 r0.x, c2, c2
mul r2.xyz, r0.y, r1
rsq r0.x, r0.x
mul r1.xyz, r0.x, c2
abs_pp r0.x, -c2.w
cmp r1.xyz, -r0.x, r1, r2
dp3 r0.x, v1, v1
rsq r0.x, r0.x
mul r0.xyz, r0.x, v1
dp3 r0.w, r0, r1
mul r2.xyz, -r0.w, r0
mad r1.xyz, -r2, c4.z, -r1
mul r3.xyz, r1.w, r3
dp3 r1.x, r1, r3
max r2.x, r1, c4
pow r1, r2.x, c4.w
dp3 r1.z, r0, v4
mul r0.xyz, r0, r1.z
mov r1.y, r1.x
texldp r1.x, v6, s1
mul r2.xyz, r1.x, c3
mad r0.xyz, -r0, c4.z, v4
cmp r1.w, r0, c4.x, c4.y
dp3 r2.w, v1, r0
abs_sat r2.w, r2
mul r1.xyz, r2, r1.y
abs_pp r1.w, r1
cmp r1.xyz, -r1.w, r1, c4.x
add_pp r2.w, -r2, c4.y
mul r3.xyz, r1, c5.x
pow_pp r1, r2.w, c5.y
max r2.w, r0, c4.x
add r1.yzw, v2.xxyz, c0.xxyz
mad_sat r2.xyz, r2, r2.w, r1.yzww
mov_pp r0.w, r1.x
mul_pp r2.w, r0, c5.z
texld r1.xyz, v3, s0
texld r0, r0, s2
add_pp_sat r1.w, r2, c5
mul r0.xyz, r0, r1.w
mad r1.xyz, r1, r2, r3
mul r2, r0, r2.w
mov r1.w, c4.y
add_pp r0, r1, r0
add_pp oC0, r0, r2
"
}

SubProgram "gles " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" }
"!!GLES"
}

SubProgram "glesdesktop " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" }
"!!GLES"
}

SubProgram "opengl " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" }
Vector 1 [_WorldSpaceCameraPos]
Vector 2 [_WorldSpaceLightPos0]
Vector 3 [_LightColor0]
SetTexture 0 [_MainTex] 2D
SetTexture 1 [_ShadowMapTexture] 2D
SetTexture 2 [_Cube] CUBE
"3.0-!!ARBfp1.0
# 53 ALU, 3 TEX
PARAM c[6] = { state.lightmodel.ambient,
		program.local[1..3],
		{ 0, 1, 2, 0.059999999 },
		{ 0.1, 2.5097656, 0.64013672, 0.070007324 } };
TEMP R0;
TEMP R1;
TEMP R2;
TEMP R3;
ADD R0.xyz, -fragment.texcoord[0], c[2];
DP3 R0.w, R0, R0;
RSQ R0.w, R0.w;
MUL R0.xyz, R0.w, R0;
ADD R3.xyz, -fragment.texcoord[0], c[1];
DP3 R1.w, R3, R3;
RSQ R1.w, R1.w;
ABS R0.w, -c[2];
DP3 R1.x, c[2], c[2];
CMP R0.w, -R0, c[4].x, c[4].y;
RSQ R1.x, R1.x;
ABS R0.w, R0;
MUL R3.xyz, R1.w, R3;
CMP R0.w, -R0, c[4].x, c[4].y;
MUL R1.xyz, R1.x, c[2];
CMP R1.xyz, -R0.w, R0, R1;
DP3 R0.x, fragment.texcoord[1], fragment.texcoord[1];
RSQ R0.x, R0.x;
MUL R0.xyz, R0.x, fragment.texcoord[1];
DP3 R0.w, R0, R1;
MUL R2.xyz, -R0.w, R0;
MAD R1.xyz, -R2, c[4].z, -R1;
DP3 R1.x, R1, R3;
MAX R1.y, R1.x, c[4].x;
DP3 R2.w, R0, fragment.texcoord[4];
MUL R0.xyz, R0, R2.w;
POW R1.w, R1.y, c[4].w;
TXP R1.x, fragment.texcoord[7], texture[1], 2D;
MUL R1.xyz, R1.x, c[3];
MUL R2.xyz, R1, R1.w;
SLT R1.w, R0, c[4].x;
ABS R1.w, R1;
CMP R1.w, -R1, c[4].x, c[4].y;
CMP R2.xyz, -R1.w, R2, c[4].x;
MAD R0.xyz, -R0, c[4].z, fragment.texcoord[4];
DP3 R1.w, fragment.texcoord[1], R0;
ABS_SAT R2.w, R1;
MAX R1.w, R0, c[4].x;
ADD R0.w, -R2, c[4].y;
POW R0.w, R0.w, c[5].y;
MUL R2.w, R0, c[5].z;
ADD R3.xyz, fragment.texcoord[2], c[0];
MAD_SAT R3.xyz, R1, R1.w, R3;
TEX R0, R0, texture[2], CUBE;
ADD_SAT R1.w, R2, c[5];
MUL R0.xyz, R0, R1.w;
MUL R2.xyz, R2, c[5].x;
TEX R1.xyz, fragment.texcoord[3], texture[0], 2D;
MAD R1.xyz, R1, R3, R2;
MUL R2, R0, R2.w;
MOV R1.w, c[4].y;
ADD R0, R1, R0;
ADD result.color, R0, R2;
END
# 53 instructions, 4 R-regs
"
}

SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" }
Vector 0 [glstate_lightmodel_ambient]
Vector 1 [_WorldSpaceCameraPos]
Vector 2 [_WorldSpaceLightPos0]
Vector 3 [_LightColor0]
SetTexture 0 [_MainTex] 2D
SetTexture 1 [_ShadowMapTexture] 2D
SetTexture 2 [_Cube] CUBE
"ps_3_0
; 52 ALU, 3 TEX
dcl_2d s0
dcl_2d s1
dcl_cube s2
def c4, 0.00000000, 1.00000000, 2.00000000, 0.06000000
def c5, 0.10000000, 2.50976563, 0.64013672, 0.07000732
dcl_texcoord0 v0.xyz
dcl_texcoord1 v1.xyz
dcl_texcoord2 v2.xyz
dcl_texcoord3 v3.xy
dcl_texcoord4 v4.xyz
dcl_texcoord7 v6
add r1.xyz, -v0, c2
dp3 r0.y, r1, r1
rsq r0.y, r0.y
add r3.xyz, -v0, c1
dp3 r1.w, r3, r3
rsq r1.w, r1.w
dp3 r0.x, c2, c2
mul r2.xyz, r0.y, r1
rsq r0.x, r0.x
mul r1.xyz, r0.x, c2
abs_pp r0.x, -c2.w
cmp r1.xyz, -r0.x, r1, r2
dp3 r0.x, v1, v1
rsq r0.x, r0.x
mul r0.xyz, r0.x, v1
dp3 r0.w, r0, r1
mul r2.xyz, -r0.w, r0
mad r1.xyz, -r2, c4.z, -r1
mul r3.xyz, r1.w, r3
dp3 r1.x, r1, r3
max r2.x, r1, c4
pow r1, r2.x, c4.w
dp3 r1.z, r0, v4
mul r0.xyz, r0, r1.z
mov r1.y, r1.x
texldp r1.x, v6, s1
mul r2.xyz, r1.x, c3
mad r0.xyz, -r0, c4.z, v4
cmp r1.w, r0, c4.x, c4.y
dp3 r2.w, v1, r0
abs_sat r2.w, r2
mul r1.xyz, r2, r1.y
abs_pp r1.w, r1
cmp r1.xyz, -r1.w, r1, c4.x
add_pp r2.w, -r2, c4.y
mul r3.xyz, r1, c5.x
pow_pp r1, r2.w, c5.y
max r2.w, r0, c4.x
add r1.yzw, v2.xxyz, c0.xxyz
mad_sat r2.xyz, r2, r2.w, r1.yzww
mov_pp r0.w, r1.x
mul_pp r2.w, r0, c5.z
texld r1.xyz, v3, s0
texld r0, r0, s2
add_pp_sat r1.w, r2, c5
mul r0.xyz, r0, r1.w
mad r1.xyz, r1, r2, r3
mul r2, r0, r2.w
mov r1.w, c4.y
add_pp r0, r1, r0
add_pp oC0, r0, r2
"
}

SubProgram "gles " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" }
"!!GLES"
}

SubProgram "glesdesktop " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" }
"!!GLES"
}

SubProgram "opengl " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "SHADOWS_SCREEN" }
Vector 1 [_WorldSpaceCameraPos]
Vector 2 [_WorldSpaceLightPos0]
Vector 3 [_LightColor0]
SetTexture 0 [_MainTex] 2D
SetTexture 1 [_ShadowMapTexture] 2D
SetTexture 2 [_Cube] CUBE
"3.0-!!ARBfp1.0
# 53 ALU, 3 TEX
PARAM c[6] = { state.lightmodel.ambient,
		program.local[1..3],
		{ 0, 1, 2, 0.059999999 },
		{ 0.1, 2.5097656, 0.64013672, 0.070007324 } };
TEMP R0;
TEMP R1;
TEMP R2;
TEMP R3;
ADD R0.xyz, -fragment.texcoord[0], c[2];
DP3 R0.w, R0, R0;
RSQ R0.w, R0.w;
MUL R0.xyz, R0.w, R0;
ADD R3.xyz, -fragment.texcoord[0], c[1];
DP3 R1.w, R3, R3;
RSQ R1.w, R1.w;
ABS R0.w, -c[2];
DP3 R1.x, c[2], c[2];
CMP R0.w, -R0, c[4].x, c[4].y;
RSQ R1.x, R1.x;
ABS R0.w, R0;
MUL R3.xyz, R1.w, R3;
CMP R0.w, -R0, c[4].x, c[4].y;
MUL R1.xyz, R1.x, c[2];
CMP R1.xyz, -R0.w, R0, R1;
DP3 R0.x, fragment.texcoord[1], fragment.texcoord[1];
RSQ R0.x, R0.x;
MUL R0.xyz, R0.x, fragment.texcoord[1];
DP3 R0.w, R0, R1;
MUL R2.xyz, -R0.w, R0;
MAD R1.xyz, -R2, c[4].z, -R1;
DP3 R1.x, R1, R3;
MAX R1.y, R1.x, c[4].x;
DP3 R2.w, R0, fragment.texcoord[4];
MUL R0.xyz, R0, R2.w;
POW R1.w, R1.y, c[4].w;
TXP R1.x, fragment.texcoord[7], texture[1], 2D;
MUL R1.xyz, R1.x, c[3];
MUL R2.xyz, R1, R1.w;
SLT R1.w, R0, c[4].x;
ABS R1.w, R1;
CMP R1.w, -R1, c[4].x, c[4].y;
CMP R2.xyz, -R1.w, R2, c[4].x;
MAD R0.xyz, -R0, c[4].z, fragment.texcoord[4];
DP3 R1.w, fragment.texcoord[1], R0;
ABS_SAT R2.w, R1;
MAX R1.w, R0, c[4].x;
ADD R0.w, -R2, c[4].y;
POW R0.w, R0.w, c[5].y;
MUL R2.w, R0, c[5].z;
ADD R3.xyz, fragment.texcoord[2], c[0];
MAD_SAT R3.xyz, R1, R1.w, R3;
TEX R0, R0, texture[2], CUBE;
ADD_SAT R1.w, R2, c[5];
MUL R0.xyz, R0, R1.w;
MUL R2.xyz, R2, c[5].x;
TEX R1.xyz, fragment.texcoord[3], texture[0], 2D;
MAD R1.xyz, R1, R3, R2;
MUL R2, R0, R2.w;
MOV R1.w, c[4].y;
ADD R0, R1, R0;
ADD result.color, R0, R2;
END
# 53 instructions, 4 R-regs
"
}

SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "SHADOWS_SCREEN" }
Vector 0 [glstate_lightmodel_ambient]
Vector 1 [_WorldSpaceCameraPos]
Vector 2 [_WorldSpaceLightPos0]
Vector 3 [_LightColor0]
SetTexture 0 [_MainTex] 2D
SetTexture 1 [_ShadowMapTexture] 2D
SetTexture 2 [_Cube] CUBE
"ps_3_0
; 52 ALU, 3 TEX
dcl_2d s0
dcl_2d s1
dcl_cube s2
def c4, 0.00000000, 1.00000000, 2.00000000, 0.06000000
def c5, 0.10000000, 2.50976563, 0.64013672, 0.07000732
dcl_texcoord0 v0.xyz
dcl_texcoord1 v1.xyz
dcl_texcoord2 v2.xyz
dcl_texcoord3 v3.xy
dcl_texcoord4 v4.xyz
dcl_texcoord7 v6
add r1.xyz, -v0, c2
dp3 r0.y, r1, r1
rsq r0.y, r0.y
add r3.xyz, -v0, c1
dp3 r1.w, r3, r3
rsq r1.w, r1.w
dp3 r0.x, c2, c2
mul r2.xyz, r0.y, r1
rsq r0.x, r0.x
mul r1.xyz, r0.x, c2
abs_pp r0.x, -c2.w
cmp r1.xyz, -r0.x, r1, r2
dp3 r0.x, v1, v1
rsq r0.x, r0.x
mul r0.xyz, r0.x, v1
dp3 r0.w, r0, r1
mul r2.xyz, -r0.w, r0
mad r1.xyz, -r2, c4.z, -r1
mul r3.xyz, r1.w, r3
dp3 r1.x, r1, r3
max r2.x, r1, c4
pow r1, r2.x, c4.w
dp3 r1.z, r0, v4
mul r0.xyz, r0, r1.z
mov r1.y, r1.x
texldp r1.x, v6, s1
mul r2.xyz, r1.x, c3
mad r0.xyz, -r0, c4.z, v4
cmp r1.w, r0, c4.x, c4.y
dp3 r2.w, v1, r0
abs_sat r2.w, r2
mul r1.xyz, r2, r1.y
abs_pp r1.w, r1
cmp r1.xyz, -r1.w, r1, c4.x
add_pp r2.w, -r2, c4.y
mul r3.xyz, r1, c5.x
pow_pp r1, r2.w, c5.y
max r2.w, r0, c4.x
add r1.yzw, v2.xxyz, c0.xxyz
mad_sat r2.xyz, r2, r2.w, r1.yzww
mov_pp r0.w, r1.x
mul_pp r2.w, r0, c5.z
texld r1.xyz, v3, s0
texld r0, r0, s2
add_pp_sat r1.w, r2, c5
mul r0.xyz, r0, r1.w
mad r1.xyz, r1, r2, r3
mul r2, r0, r2.w
mov r1.w, c4.y
add_pp r0, r1, r0
add_pp oC0, r0, r2
"
}

SubProgram "gles " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "SHADOWS_SCREEN" }
"!!GLES"
}

SubProgram "glesdesktop " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "SHADOWS_SCREEN" }
"!!GLES"
}

}

#LINE 224

      }
 }
   // The definition of a fallback shader should be commented out 
   // during development:
   Fallback "Specular"
}