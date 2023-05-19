Shader "KL/Mobile/Car Paint Unlit" {
   Properties {
   
	  _Color ("Diffuse Material Color (RGB)", Color) = (1,1,1,1) 
	  _MainTex ("Diffuse Texture", 2D) = "white" {} 
	  _Cube("Reflection Map", Cube) = "" {}
	  _Reflection("Reflection Power", Range (0.00, 1)) = 0
	  _FrezPow("Fresnel Power",Range(0,2)) = .25
	  _FrezFalloff("Fresnal Falloff",Range(0,10)) = 4	  
  
   }
SubShader {
   Tags { "QUEUE"="Geometry" "RenderType"="Opaque" " IgnoreProjector"="False"}	  
      Pass {  
      
         Tags { "LightMode" = "ForwardBase" }
 
         Program "vp" {
// Vertex combos: 8
//   opengl - ALU: 16 to 16
//   d3d9 - ALU: 16 to 16
SubProgram "opengl " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 5 [_Object2World]
Matrix 9 [_World2Object]
"!!ARBvp1.0
# 16 ALU
PARAM c[13] = { { 0 },
		state.matrix.mvp,
		program.local[5..12] };
TEMP R0;
MUL R0.xyz, vertex.normal.y, c[10];
MAD R0.xyz, vertex.normal.x, c[9], R0;
MAD R0.xyz, vertex.normal.z, c[11], R0;
ADD R0.xyz, R0, c[0].x;
DP3 R0.w, R0, R0;
RSQ R0.w, R0.w;
MUL result.texcoord[1].xyz, R0.w, R0;
MOV result.texcoord[3], vertex.texcoord[0];
DP4 result.position.w, vertex.position, c[4];
DP4 result.position.z, vertex.position, c[3];
DP4 result.position.y, vertex.position, c[2];
DP4 result.position.x, vertex.position, c[1];
DP4 result.texcoord[0].w, vertex.position, c[8];
DP4 result.texcoord[0].z, vertex.position, c[7];
DP4 result.texcoord[0].y, vertex.position, c[6];
DP4 result.texcoord[0].x, vertex.position, c[5];
END
# 16 instructions, 1 R-regs
"
}

SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
Matrix 4 [_Object2World]
Matrix 8 [_World2Object]
"vs_2_0
; 16 ALU
def c12, 0.00000000, 0, 0, 0
dcl_position0 v0
dcl_normal0 v1
dcl_texcoord0 v2
mul r0.xyz, v1.y, c9
mad r0.xyz, v1.x, c8, r0
mad r0.xyz, v1.z, c10, r0
add r0.xyz, r0, c12.x
dp3 r0.w, r0, r0
rsq r0.w, r0.w
mul oT1.xyz, r0.w, r0
mov oT3, v2
dp4 oPos.w, v0, c3
dp4 oPos.z, v0, c2
dp4 oPos.y, v0, c1
dp4 oPos.x, v0, c0
dp4 oT0.w, v0, c7
dp4 oT0.z, v0, c6
dp4 oT0.y, v0, c5
dp4 oT0.x, v0, c4
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

varying mediump vec4 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;

uniform highp mat4 _World2Object;
uniform highp mat4 _Object2World;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  lowp vec3 tmpvar_1;
  lowp vec4 tmpvar_2;
  tmpvar_2.w = 0.0;
  tmpvar_2.xyz = normalize (_glesNormal);
  highp vec3 tmpvar_3;
  tmpvar_3 = normalize ((tmpvar_2 * _World2Object).xyz);
  tmpvar_1 = tmpvar_3;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = (_Object2World * _glesVertex);
  xlv_TEXCOORD1 = tmpvar_1;
  xlv_TEXCOORD3 = _glesMultiTexCoord0;
}



#endif
#ifdef FRAGMENT

varying mediump vec4 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform lowp float _Reflection;
uniform highp vec3 _WorldSpaceCameraPos;
uniform sampler2D _MainTex;
uniform lowp float _FrezPow;
uniform mediump float _FrezFalloff;
uniform samplerCube _Cube;
void main ()
{
  lowp float frez;
  lowp vec4 reflTex;
  lowp vec3 viewDirection;
  highp vec3 tmpvar_1;
  tmpvar_1 = normalize ((_WorldSpaceCameraPos - xlv_TEXCOORD0.xyz));
  viewDirection = tmpvar_1;
  mediump vec2 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD3.xy;
  lowp vec3 tmpvar_3;
  tmpvar_3 = reflect (-(viewDirection), normalize (xlv_TEXCOORD1));
  lowp vec4 tmpvar_4;
  tmpvar_4 = textureCube (_Cube, tmpvar_3);
  reflTex = tmpvar_4;
  lowp float tmpvar_5;
  tmpvar_5 = clamp (abs (dot (tmpvar_3, xlv_TEXCOORD1)), 0.0, 1.0);
  mediump float tmpvar_6;
  tmpvar_6 = pow ((1.0 - tmpvar_5), _FrezFalloff);
  frez = tmpvar_6;
  lowp float tmpvar_7;
  tmpvar_7 = (frez * _FrezPow);
  frez = tmpvar_7;
  reflTex.xyz = (tmpvar_4.xyz * clamp ((_Reflection + tmpvar_7), 0.0, 1.0));
  lowp vec4 tmpvar_8;
  tmpvar_8.w = 1.0;
  tmpvar_8.xyz = ((texture2D (_MainTex, tmpvar_2).xyz + reflTex.xyz) + (tmpvar_7 * reflTex).xyz);
  gl_FragData[0] = tmpvar_8;
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

varying mediump vec4 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;

uniform highp mat4 _World2Object;
uniform highp mat4 _Object2World;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  lowp vec3 tmpvar_1;
  lowp vec4 tmpvar_2;
  tmpvar_2.w = 0.0;
  tmpvar_2.xyz = normalize (_glesNormal);
  highp vec3 tmpvar_3;
  tmpvar_3 = normalize ((tmpvar_2 * _World2Object).xyz);
  tmpvar_1 = tmpvar_3;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = (_Object2World * _glesVertex);
  xlv_TEXCOORD1 = tmpvar_1;
  xlv_TEXCOORD3 = _glesMultiTexCoord0;
}



#endif
#ifdef FRAGMENT

varying mediump vec4 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform lowp float _Reflection;
uniform highp vec3 _WorldSpaceCameraPos;
uniform sampler2D _MainTex;
uniform lowp float _FrezPow;
uniform mediump float _FrezFalloff;
uniform samplerCube _Cube;
void main ()
{
  lowp float frez;
  lowp vec4 reflTex;
  lowp vec3 viewDirection;
  highp vec3 tmpvar_1;
  tmpvar_1 = normalize ((_WorldSpaceCameraPos - xlv_TEXCOORD0.xyz));
  viewDirection = tmpvar_1;
  mediump vec2 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD3.xy;
  lowp vec3 tmpvar_3;
  tmpvar_3 = reflect (-(viewDirection), normalize (xlv_TEXCOORD1));
  lowp vec4 tmpvar_4;
  tmpvar_4 = textureCube (_Cube, tmpvar_3);
  reflTex = tmpvar_4;
  lowp float tmpvar_5;
  tmpvar_5 = clamp (abs (dot (tmpvar_3, xlv_TEXCOORD1)), 0.0, 1.0);
  mediump float tmpvar_6;
  tmpvar_6 = pow ((1.0 - tmpvar_5), _FrezFalloff);
  frez = tmpvar_6;
  lowp float tmpvar_7;
  tmpvar_7 = (frez * _FrezPow);
  frez = tmpvar_7;
  reflTex.xyz = (tmpvar_4.xyz * clamp ((_Reflection + tmpvar_7), 0.0, 1.0));
  lowp vec4 tmpvar_8;
  tmpvar_8.w = 1.0;
  tmpvar_8.xyz = ((texture2D (_MainTex, tmpvar_2).xyz + reflTex.xyz) + (tmpvar_7 * reflTex).xyz);
  gl_FragData[0] = tmpvar_8;
}



#endif"
}

SubProgram "flash " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
Matrix 4 [_Object2World]
Matrix 8 [_World2Object]
"agal_vs
c12 0.0 0.0 0.0 0.0
[bc]
adaaaaaaaaaaahacabaaaaffaaaaaaaaajaaaaoeabaaaaaa mul r0.xyz, a1.y, c9
adaaaaaaabaaahacabaaaaaaaaaaaaaaaiaaaaoeabaaaaaa mul r1.xyz, a1.x, c8
abaaaaaaaaaaahacabaaaakeacaaaaaaaaaaaakeacaaaaaa add r0.xyz, r1.xyzz, r0.xyzz
adaaaaaaabaaahacabaaaakkaaaaaaaaakaaaaoeabaaaaaa mul r1.xyz, a1.z, c10
abaaaaaaaaaaahacabaaaakeacaaaaaaaaaaaakeacaaaaaa add r0.xyz, r1.xyzz, r0.xyzz
abaaaaaaaaaaahacaaaaaakeacaaaaaaamaaaaaaabaaaaaa add r0.xyz, r0.xyzz, c12.x
bcaaaaaaaaaaaiacaaaaaakeacaaaaaaaaaaaakeacaaaaaa dp3 r0.w, r0.xyzz, r0.xyzz
akaaaaaaaaaaaiacaaaaaappacaaaaaaaaaaaaaaaaaaaaaa rsq r0.w, r0.w
adaaaaaaabaaahaeaaaaaappacaaaaaaaaaaaakeacaaaaaa mul v1.xyz, r0.w, r0.xyzz
aaaaaaaaadaaapaeadaaaaoeaaaaaaaaaaaaaaaaaaaaaaaa mov v3, a3
bdaaaaaaaaaaaiadaaaaaaoeaaaaaaaaadaaaaoeabaaaaaa dp4 o0.w, a0, c3
bdaaaaaaaaaaaeadaaaaaaoeaaaaaaaaacaaaaoeabaaaaaa dp4 o0.z, a0, c2
bdaaaaaaaaaaacadaaaaaaoeaaaaaaaaabaaaaoeabaaaaaa dp4 o0.y, a0, c1
bdaaaaaaaaaaabadaaaaaaoeaaaaaaaaaaaaaaoeabaaaaaa dp4 o0.x, a0, c0
bdaaaaaaaaaaaiaeaaaaaaoeaaaaaaaaahaaaaoeabaaaaaa dp4 v0.w, a0, c7
bdaaaaaaaaaaaeaeaaaaaaoeaaaaaaaaagaaaaoeabaaaaaa dp4 v0.z, a0, c6
bdaaaaaaaaaaacaeaaaaaaoeaaaaaaaaafaaaaoeabaaaaaa dp4 v0.y, a0, c5
bdaaaaaaaaaaabaeaaaaaaoeaaaaaaaaaeaaaaoeabaaaaaa dp4 v0.x, a0, c4
aaaaaaaaabaaaiaeaaaaaaoeabaaaaaaaaaaaaaaaaaaaaaa mov v1.w, c0
"
}

SubProgram "opengl " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 5 [_Object2World]
Matrix 9 [_World2Object]
"!!ARBvp1.0
# 16 ALU
PARAM c[13] = { { 0 },
		state.matrix.mvp,
		program.local[5..12] };
TEMP R0;
MUL R0.xyz, vertex.normal.y, c[10];
MAD R0.xyz, vertex.normal.x, c[9], R0;
MAD R0.xyz, vertex.normal.z, c[11], R0;
ADD R0.xyz, R0, c[0].x;
DP3 R0.w, R0, R0;
RSQ R0.w, R0.w;
MUL result.texcoord[1].xyz, R0.w, R0;
MOV result.texcoord[3], vertex.texcoord[0];
DP4 result.position.w, vertex.position, c[4];
DP4 result.position.z, vertex.position, c[3];
DP4 result.position.y, vertex.position, c[2];
DP4 result.position.x, vertex.position, c[1];
DP4 result.texcoord[0].w, vertex.position, c[8];
DP4 result.texcoord[0].z, vertex.position, c[7];
DP4 result.texcoord[0].y, vertex.position, c[6];
DP4 result.texcoord[0].x, vertex.position, c[5];
END
# 16 instructions, 1 R-regs
"
}

SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
Matrix 4 [_Object2World]
Matrix 8 [_World2Object]
"vs_2_0
; 16 ALU
def c12, 0.00000000, 0, 0, 0
dcl_position0 v0
dcl_normal0 v1
dcl_texcoord0 v2
mul r0.xyz, v1.y, c9
mad r0.xyz, v1.x, c8, r0
mad r0.xyz, v1.z, c10, r0
add r0.xyz, r0, c12.x
dp3 r0.w, r0, r0
rsq r0.w, r0.w
mul oT1.xyz, r0.w, r0
mov oT3, v2
dp4 oPos.w, v0, c3
dp4 oPos.z, v0, c2
dp4 oPos.y, v0, c1
dp4 oPos.x, v0, c0
dp4 oT0.w, v0, c7
dp4 oT0.z, v0, c6
dp4 oT0.y, v0, c5
dp4 oT0.x, v0, c4
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

varying mediump vec4 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;

uniform highp mat4 _World2Object;
uniform highp mat4 _Object2World;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  lowp vec3 tmpvar_1;
  lowp vec4 tmpvar_2;
  tmpvar_2.w = 0.0;
  tmpvar_2.xyz = normalize (_glesNormal);
  highp vec3 tmpvar_3;
  tmpvar_3 = normalize ((tmpvar_2 * _World2Object).xyz);
  tmpvar_1 = tmpvar_3;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = (_Object2World * _glesVertex);
  xlv_TEXCOORD1 = tmpvar_1;
  xlv_TEXCOORD3 = _glesMultiTexCoord0;
}



#endif
#ifdef FRAGMENT

varying mediump vec4 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform lowp float _Reflection;
uniform highp vec3 _WorldSpaceCameraPos;
uniform sampler2D _MainTex;
uniform lowp float _FrezPow;
uniform mediump float _FrezFalloff;
uniform samplerCube _Cube;
void main ()
{
  lowp float frez;
  lowp vec4 reflTex;
  lowp vec3 viewDirection;
  highp vec3 tmpvar_1;
  tmpvar_1 = normalize ((_WorldSpaceCameraPos - xlv_TEXCOORD0.xyz));
  viewDirection = tmpvar_1;
  mediump vec2 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD3.xy;
  lowp vec3 tmpvar_3;
  tmpvar_3 = reflect (-(viewDirection), normalize (xlv_TEXCOORD1));
  lowp vec4 tmpvar_4;
  tmpvar_4 = textureCube (_Cube, tmpvar_3);
  reflTex = tmpvar_4;
  lowp float tmpvar_5;
  tmpvar_5 = clamp (abs (dot (tmpvar_3, xlv_TEXCOORD1)), 0.0, 1.0);
  mediump float tmpvar_6;
  tmpvar_6 = pow ((1.0 - tmpvar_5), _FrezFalloff);
  frez = tmpvar_6;
  lowp float tmpvar_7;
  tmpvar_7 = (frez * _FrezPow);
  frez = tmpvar_7;
  reflTex.xyz = (tmpvar_4.xyz * clamp ((_Reflection + tmpvar_7), 0.0, 1.0));
  lowp vec4 tmpvar_8;
  tmpvar_8.w = 1.0;
  tmpvar_8.xyz = ((texture2D (_MainTex, tmpvar_2).xyz + reflTex.xyz) + (tmpvar_7 * reflTex).xyz);
  gl_FragData[0] = tmpvar_8;
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

varying mediump vec4 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;

uniform highp mat4 _World2Object;
uniform highp mat4 _Object2World;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  lowp vec3 tmpvar_1;
  lowp vec4 tmpvar_2;
  tmpvar_2.w = 0.0;
  tmpvar_2.xyz = normalize (_glesNormal);
  highp vec3 tmpvar_3;
  tmpvar_3 = normalize ((tmpvar_2 * _World2Object).xyz);
  tmpvar_1 = tmpvar_3;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = (_Object2World * _glesVertex);
  xlv_TEXCOORD1 = tmpvar_1;
  xlv_TEXCOORD3 = _glesMultiTexCoord0;
}



#endif
#ifdef FRAGMENT

varying mediump vec4 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform lowp float _Reflection;
uniform highp vec3 _WorldSpaceCameraPos;
uniform sampler2D _MainTex;
uniform lowp float _FrezPow;
uniform mediump float _FrezFalloff;
uniform samplerCube _Cube;
void main ()
{
  lowp float frez;
  lowp vec4 reflTex;
  lowp vec3 viewDirection;
  highp vec3 tmpvar_1;
  tmpvar_1 = normalize ((_WorldSpaceCameraPos - xlv_TEXCOORD0.xyz));
  viewDirection = tmpvar_1;
  mediump vec2 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD3.xy;
  lowp vec3 tmpvar_3;
  tmpvar_3 = reflect (-(viewDirection), normalize (xlv_TEXCOORD1));
  lowp vec4 tmpvar_4;
  tmpvar_4 = textureCube (_Cube, tmpvar_3);
  reflTex = tmpvar_4;
  lowp float tmpvar_5;
  tmpvar_5 = clamp (abs (dot (tmpvar_3, xlv_TEXCOORD1)), 0.0, 1.0);
  mediump float tmpvar_6;
  tmpvar_6 = pow ((1.0 - tmpvar_5), _FrezFalloff);
  frez = tmpvar_6;
  lowp float tmpvar_7;
  tmpvar_7 = (frez * _FrezPow);
  frez = tmpvar_7;
  reflTex.xyz = (tmpvar_4.xyz * clamp ((_Reflection + tmpvar_7), 0.0, 1.0));
  lowp vec4 tmpvar_8;
  tmpvar_8.w = 1.0;
  tmpvar_8.xyz = ((texture2D (_MainTex, tmpvar_2).xyz + reflTex.xyz) + (tmpvar_7 * reflTex).xyz);
  gl_FragData[0] = tmpvar_8;
}



#endif"
}

SubProgram "flash " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
Matrix 4 [_Object2World]
Matrix 8 [_World2Object]
"agal_vs
c12 0.0 0.0 0.0 0.0
[bc]
adaaaaaaaaaaahacabaaaaffaaaaaaaaajaaaaoeabaaaaaa mul r0.xyz, a1.y, c9
adaaaaaaabaaahacabaaaaaaaaaaaaaaaiaaaaoeabaaaaaa mul r1.xyz, a1.x, c8
abaaaaaaaaaaahacabaaaakeacaaaaaaaaaaaakeacaaaaaa add r0.xyz, r1.xyzz, r0.xyzz
adaaaaaaabaaahacabaaaakkaaaaaaaaakaaaaoeabaaaaaa mul r1.xyz, a1.z, c10
abaaaaaaaaaaahacabaaaakeacaaaaaaaaaaaakeacaaaaaa add r0.xyz, r1.xyzz, r0.xyzz
abaaaaaaaaaaahacaaaaaakeacaaaaaaamaaaaaaabaaaaaa add r0.xyz, r0.xyzz, c12.x
bcaaaaaaaaaaaiacaaaaaakeacaaaaaaaaaaaakeacaaaaaa dp3 r0.w, r0.xyzz, r0.xyzz
akaaaaaaaaaaaiacaaaaaappacaaaaaaaaaaaaaaaaaaaaaa rsq r0.w, r0.w
adaaaaaaabaaahaeaaaaaappacaaaaaaaaaaaakeacaaaaaa mul v1.xyz, r0.w, r0.xyzz
aaaaaaaaadaaapaeadaaaaoeaaaaaaaaaaaaaaaaaaaaaaaa mov v3, a3
bdaaaaaaaaaaaiadaaaaaaoeaaaaaaaaadaaaaoeabaaaaaa dp4 o0.w, a0, c3
bdaaaaaaaaaaaeadaaaaaaoeaaaaaaaaacaaaaoeabaaaaaa dp4 o0.z, a0, c2
bdaaaaaaaaaaacadaaaaaaoeaaaaaaaaabaaaaoeabaaaaaa dp4 o0.y, a0, c1
bdaaaaaaaaaaabadaaaaaaoeaaaaaaaaaaaaaaoeabaaaaaa dp4 o0.x, a0, c0
bdaaaaaaaaaaaiaeaaaaaaoeaaaaaaaaahaaaaoeabaaaaaa dp4 v0.w, a0, c7
bdaaaaaaaaaaaeaeaaaaaaoeaaaaaaaaagaaaaoeabaaaaaa dp4 v0.z, a0, c6
bdaaaaaaaaaaacaeaaaaaaoeaaaaaaaaafaaaaoeabaaaaaa dp4 v0.y, a0, c5
bdaaaaaaaaaaabaeaaaaaaoeaaaaaaaaaeaaaaoeabaaaaaa dp4 v0.x, a0, c4
aaaaaaaaabaaaiaeaaaaaaoeabaaaaaaaaaaaaaaaaaaaaaa mov v1.w, c0
"
}

SubProgram "opengl " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "SHADOWS_OFF" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 5 [_Object2World]
Matrix 9 [_World2Object]
"!!ARBvp1.0
# 16 ALU
PARAM c[13] = { { 0 },
		state.matrix.mvp,
		program.local[5..12] };
TEMP R0;
MUL R0.xyz, vertex.normal.y, c[10];
MAD R0.xyz, vertex.normal.x, c[9], R0;
MAD R0.xyz, vertex.normal.z, c[11], R0;
ADD R0.xyz, R0, c[0].x;
DP3 R0.w, R0, R0;
RSQ R0.w, R0.w;
MUL result.texcoord[1].xyz, R0.w, R0;
MOV result.texcoord[3], vertex.texcoord[0];
DP4 result.position.w, vertex.position, c[4];
DP4 result.position.z, vertex.position, c[3];
DP4 result.position.y, vertex.position, c[2];
DP4 result.position.x, vertex.position, c[1];
DP4 result.texcoord[0].w, vertex.position, c[8];
DP4 result.texcoord[0].z, vertex.position, c[7];
DP4 result.texcoord[0].y, vertex.position, c[6];
DP4 result.texcoord[0].x, vertex.position, c[5];
END
# 16 instructions, 1 R-regs
"
}

SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "SHADOWS_OFF" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
Matrix 4 [_Object2World]
Matrix 8 [_World2Object]
"vs_2_0
; 16 ALU
def c12, 0.00000000, 0, 0, 0
dcl_position0 v0
dcl_normal0 v1
dcl_texcoord0 v2
mul r0.xyz, v1.y, c9
mad r0.xyz, v1.x, c8, r0
mad r0.xyz, v1.z, c10, r0
add r0.xyz, r0, c12.x
dp3 r0.w, r0, r0
rsq r0.w, r0.w
mul oT1.xyz, r0.w, r0
mov oT3, v2
dp4 oPos.w, v0, c3
dp4 oPos.z, v0, c2
dp4 oPos.y, v0, c1
dp4 oPos.x, v0, c0
dp4 oT0.w, v0, c7
dp4 oT0.z, v0, c6
dp4 oT0.y, v0, c5
dp4 oT0.x, v0, c4
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

varying mediump vec4 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;

uniform highp mat4 _World2Object;
uniform highp mat4 _Object2World;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  lowp vec3 tmpvar_1;
  lowp vec4 tmpvar_2;
  tmpvar_2.w = 0.0;
  tmpvar_2.xyz = normalize (_glesNormal);
  highp vec3 tmpvar_3;
  tmpvar_3 = normalize ((tmpvar_2 * _World2Object).xyz);
  tmpvar_1 = tmpvar_3;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = (_Object2World * _glesVertex);
  xlv_TEXCOORD1 = tmpvar_1;
  xlv_TEXCOORD3 = _glesMultiTexCoord0;
}



#endif
#ifdef FRAGMENT

varying mediump vec4 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform lowp float _Reflection;
uniform highp vec3 _WorldSpaceCameraPos;
uniform sampler2D _MainTex;
uniform lowp float _FrezPow;
uniform mediump float _FrezFalloff;
uniform samplerCube _Cube;
void main ()
{
  lowp float frez;
  lowp vec4 reflTex;
  lowp vec3 viewDirection;
  highp vec3 tmpvar_1;
  tmpvar_1 = normalize ((_WorldSpaceCameraPos - xlv_TEXCOORD0.xyz));
  viewDirection = tmpvar_1;
  mediump vec2 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD3.xy;
  lowp vec3 tmpvar_3;
  tmpvar_3 = reflect (-(viewDirection), normalize (xlv_TEXCOORD1));
  lowp vec4 tmpvar_4;
  tmpvar_4 = textureCube (_Cube, tmpvar_3);
  reflTex = tmpvar_4;
  lowp float tmpvar_5;
  tmpvar_5 = clamp (abs (dot (tmpvar_3, xlv_TEXCOORD1)), 0.0, 1.0);
  mediump float tmpvar_6;
  tmpvar_6 = pow ((1.0 - tmpvar_5), _FrezFalloff);
  frez = tmpvar_6;
  lowp float tmpvar_7;
  tmpvar_7 = (frez * _FrezPow);
  frez = tmpvar_7;
  reflTex.xyz = (tmpvar_4.xyz * clamp ((_Reflection + tmpvar_7), 0.0, 1.0));
  lowp vec4 tmpvar_8;
  tmpvar_8.w = 1.0;
  tmpvar_8.xyz = ((texture2D (_MainTex, tmpvar_2).xyz + reflTex.xyz) + (tmpvar_7 * reflTex).xyz);
  gl_FragData[0] = tmpvar_8;
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

varying mediump vec4 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;

uniform highp mat4 _World2Object;
uniform highp mat4 _Object2World;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  lowp vec3 tmpvar_1;
  lowp vec4 tmpvar_2;
  tmpvar_2.w = 0.0;
  tmpvar_2.xyz = normalize (_glesNormal);
  highp vec3 tmpvar_3;
  tmpvar_3 = normalize ((tmpvar_2 * _World2Object).xyz);
  tmpvar_1 = tmpvar_3;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = (_Object2World * _glesVertex);
  xlv_TEXCOORD1 = tmpvar_1;
  xlv_TEXCOORD3 = _glesMultiTexCoord0;
}



#endif
#ifdef FRAGMENT

varying mediump vec4 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform lowp float _Reflection;
uniform highp vec3 _WorldSpaceCameraPos;
uniform sampler2D _MainTex;
uniform lowp float _FrezPow;
uniform mediump float _FrezFalloff;
uniform samplerCube _Cube;
void main ()
{
  lowp float frez;
  lowp vec4 reflTex;
  lowp vec3 viewDirection;
  highp vec3 tmpvar_1;
  tmpvar_1 = normalize ((_WorldSpaceCameraPos - xlv_TEXCOORD0.xyz));
  viewDirection = tmpvar_1;
  mediump vec2 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD3.xy;
  lowp vec3 tmpvar_3;
  tmpvar_3 = reflect (-(viewDirection), normalize (xlv_TEXCOORD1));
  lowp vec4 tmpvar_4;
  tmpvar_4 = textureCube (_Cube, tmpvar_3);
  reflTex = tmpvar_4;
  lowp float tmpvar_5;
  tmpvar_5 = clamp (abs (dot (tmpvar_3, xlv_TEXCOORD1)), 0.0, 1.0);
  mediump float tmpvar_6;
  tmpvar_6 = pow ((1.0 - tmpvar_5), _FrezFalloff);
  frez = tmpvar_6;
  lowp float tmpvar_7;
  tmpvar_7 = (frez * _FrezPow);
  frez = tmpvar_7;
  reflTex.xyz = (tmpvar_4.xyz * clamp ((_Reflection + tmpvar_7), 0.0, 1.0));
  lowp vec4 tmpvar_8;
  tmpvar_8.w = 1.0;
  tmpvar_8.xyz = ((texture2D (_MainTex, tmpvar_2).xyz + reflTex.xyz) + (tmpvar_7 * reflTex).xyz);
  gl_FragData[0] = tmpvar_8;
}



#endif"
}

SubProgram "flash " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "SHADOWS_OFF" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
Matrix 4 [_Object2World]
Matrix 8 [_World2Object]
"agal_vs
c12 0.0 0.0 0.0 0.0
[bc]
adaaaaaaaaaaahacabaaaaffaaaaaaaaajaaaaoeabaaaaaa mul r0.xyz, a1.y, c9
adaaaaaaabaaahacabaaaaaaaaaaaaaaaiaaaaoeabaaaaaa mul r1.xyz, a1.x, c8
abaaaaaaaaaaahacabaaaakeacaaaaaaaaaaaakeacaaaaaa add r0.xyz, r1.xyzz, r0.xyzz
adaaaaaaabaaahacabaaaakkaaaaaaaaakaaaaoeabaaaaaa mul r1.xyz, a1.z, c10
abaaaaaaaaaaahacabaaaakeacaaaaaaaaaaaakeacaaaaaa add r0.xyz, r1.xyzz, r0.xyzz
abaaaaaaaaaaahacaaaaaakeacaaaaaaamaaaaaaabaaaaaa add r0.xyz, r0.xyzz, c12.x
bcaaaaaaaaaaaiacaaaaaakeacaaaaaaaaaaaakeacaaaaaa dp3 r0.w, r0.xyzz, r0.xyzz
akaaaaaaaaaaaiacaaaaaappacaaaaaaaaaaaaaaaaaaaaaa rsq r0.w, r0.w
adaaaaaaabaaahaeaaaaaappacaaaaaaaaaaaakeacaaaaaa mul v1.xyz, r0.w, r0.xyzz
aaaaaaaaadaaapaeadaaaaoeaaaaaaaaaaaaaaaaaaaaaaaa mov v3, a3
bdaaaaaaaaaaaiadaaaaaaoeaaaaaaaaadaaaaoeabaaaaaa dp4 o0.w, a0, c3
bdaaaaaaaaaaaeadaaaaaaoeaaaaaaaaacaaaaoeabaaaaaa dp4 o0.z, a0, c2
bdaaaaaaaaaaacadaaaaaaoeaaaaaaaaabaaaaoeabaaaaaa dp4 o0.y, a0, c1
bdaaaaaaaaaaabadaaaaaaoeaaaaaaaaaaaaaaoeabaaaaaa dp4 o0.x, a0, c0
bdaaaaaaaaaaaiaeaaaaaaoeaaaaaaaaahaaaaoeabaaaaaa dp4 v0.w, a0, c7
bdaaaaaaaaaaaeaeaaaaaaoeaaaaaaaaagaaaaoeabaaaaaa dp4 v0.z, a0, c6
bdaaaaaaaaaaacaeaaaaaaoeaaaaaaaaafaaaaoeabaaaaaa dp4 v0.y, a0, c5
bdaaaaaaaaaaabaeaaaaaaoeaaaaaaaaaeaaaaoeabaaaaaa dp4 v0.x, a0, c4
aaaaaaaaabaaaiaeaaaaaaoeabaaaaaaaaaaaaaaaaaaaaaa mov v1.w, c0
"
}

SubProgram "opengl " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 5 [_Object2World]
Matrix 9 [_World2Object]
"!!ARBvp1.0
# 16 ALU
PARAM c[13] = { { 0 },
		state.matrix.mvp,
		program.local[5..12] };
TEMP R0;
MUL R0.xyz, vertex.normal.y, c[10];
MAD R0.xyz, vertex.normal.x, c[9], R0;
MAD R0.xyz, vertex.normal.z, c[11], R0;
ADD R0.xyz, R0, c[0].x;
DP3 R0.w, R0, R0;
RSQ R0.w, R0.w;
MUL result.texcoord[1].xyz, R0.w, R0;
MOV result.texcoord[3], vertex.texcoord[0];
DP4 result.position.w, vertex.position, c[4];
DP4 result.position.z, vertex.position, c[3];
DP4 result.position.y, vertex.position, c[2];
DP4 result.position.x, vertex.position, c[1];
DP4 result.texcoord[0].w, vertex.position, c[8];
DP4 result.texcoord[0].z, vertex.position, c[7];
DP4 result.texcoord[0].y, vertex.position, c[6];
DP4 result.texcoord[0].x, vertex.position, c[5];
END
# 16 instructions, 1 R-regs
"
}

SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
Matrix 4 [_Object2World]
Matrix 8 [_World2Object]
"vs_2_0
; 16 ALU
def c12, 0.00000000, 0, 0, 0
dcl_position0 v0
dcl_normal0 v1
dcl_texcoord0 v2
mul r0.xyz, v1.y, c9
mad r0.xyz, v1.x, c8, r0
mad r0.xyz, v1.z, c10, r0
add r0.xyz, r0, c12.x
dp3 r0.w, r0, r0
rsq r0.w, r0.w
mul oT1.xyz, r0.w, r0
mov oT3, v2
dp4 oPos.w, v0, c3
dp4 oPos.z, v0, c2
dp4 oPos.y, v0, c1
dp4 oPos.x, v0, c0
dp4 oT0.w, v0, c7
dp4 oT0.z, v0, c6
dp4 oT0.y, v0, c5
dp4 oT0.x, v0, c4
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

varying mediump vec4 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;

uniform highp mat4 _World2Object;
uniform highp mat4 _Object2World;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  lowp vec3 tmpvar_1;
  lowp vec4 tmpvar_2;
  tmpvar_2.w = 0.0;
  tmpvar_2.xyz = normalize (_glesNormal);
  highp vec3 tmpvar_3;
  tmpvar_3 = normalize ((tmpvar_2 * _World2Object).xyz);
  tmpvar_1 = tmpvar_3;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = (_Object2World * _glesVertex);
  xlv_TEXCOORD1 = tmpvar_1;
  xlv_TEXCOORD3 = _glesMultiTexCoord0;
}



#endif
#ifdef FRAGMENT

varying mediump vec4 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform lowp float _Reflection;
uniform highp vec3 _WorldSpaceCameraPos;
uniform sampler2D _MainTex;
uniform lowp float _FrezPow;
uniform mediump float _FrezFalloff;
uniform samplerCube _Cube;
void main ()
{
  lowp float frez;
  lowp vec4 reflTex;
  lowp vec3 viewDirection;
  highp vec3 tmpvar_1;
  tmpvar_1 = normalize ((_WorldSpaceCameraPos - xlv_TEXCOORD0.xyz));
  viewDirection = tmpvar_1;
  mediump vec2 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD3.xy;
  lowp vec3 tmpvar_3;
  tmpvar_3 = reflect (-(viewDirection), normalize (xlv_TEXCOORD1));
  lowp vec4 tmpvar_4;
  tmpvar_4 = textureCube (_Cube, tmpvar_3);
  reflTex = tmpvar_4;
  lowp float tmpvar_5;
  tmpvar_5 = clamp (abs (dot (tmpvar_3, xlv_TEXCOORD1)), 0.0, 1.0);
  mediump float tmpvar_6;
  tmpvar_6 = pow ((1.0 - tmpvar_5), _FrezFalloff);
  frez = tmpvar_6;
  lowp float tmpvar_7;
  tmpvar_7 = (frez * _FrezPow);
  frez = tmpvar_7;
  reflTex.xyz = (tmpvar_4.xyz * clamp ((_Reflection + tmpvar_7), 0.0, 1.0));
  lowp vec4 tmpvar_8;
  tmpvar_8.w = 1.0;
  tmpvar_8.xyz = ((texture2D (_MainTex, tmpvar_2).xyz + reflTex.xyz) + (tmpvar_7 * reflTex).xyz);
  gl_FragData[0] = tmpvar_8;
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

varying mediump vec4 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;

uniform highp mat4 _World2Object;
uniform highp mat4 _Object2World;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  lowp vec3 tmpvar_1;
  lowp vec4 tmpvar_2;
  tmpvar_2.w = 0.0;
  tmpvar_2.xyz = normalize (_glesNormal);
  highp vec3 tmpvar_3;
  tmpvar_3 = normalize ((tmpvar_2 * _World2Object).xyz);
  tmpvar_1 = tmpvar_3;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = (_Object2World * _glesVertex);
  xlv_TEXCOORD1 = tmpvar_1;
  xlv_TEXCOORD3 = _glesMultiTexCoord0;
}



#endif
#ifdef FRAGMENT

varying mediump vec4 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform lowp float _Reflection;
uniform highp vec3 _WorldSpaceCameraPos;
uniform sampler2D _MainTex;
uniform lowp float _FrezPow;
uniform mediump float _FrezFalloff;
uniform samplerCube _Cube;
void main ()
{
  lowp float frez;
  lowp vec4 reflTex;
  lowp vec3 viewDirection;
  highp vec3 tmpvar_1;
  tmpvar_1 = normalize ((_WorldSpaceCameraPos - xlv_TEXCOORD0.xyz));
  viewDirection = tmpvar_1;
  mediump vec2 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD3.xy;
  lowp vec3 tmpvar_3;
  tmpvar_3 = reflect (-(viewDirection), normalize (xlv_TEXCOORD1));
  lowp vec4 tmpvar_4;
  tmpvar_4 = textureCube (_Cube, tmpvar_3);
  reflTex = tmpvar_4;
  lowp float tmpvar_5;
  tmpvar_5 = clamp (abs (dot (tmpvar_3, xlv_TEXCOORD1)), 0.0, 1.0);
  mediump float tmpvar_6;
  tmpvar_6 = pow ((1.0 - tmpvar_5), _FrezFalloff);
  frez = tmpvar_6;
  lowp float tmpvar_7;
  tmpvar_7 = (frez * _FrezPow);
  frez = tmpvar_7;
  reflTex.xyz = (tmpvar_4.xyz * clamp ((_Reflection + tmpvar_7), 0.0, 1.0));
  lowp vec4 tmpvar_8;
  tmpvar_8.w = 1.0;
  tmpvar_8.xyz = ((texture2D (_MainTex, tmpvar_2).xyz + reflTex.xyz) + (tmpvar_7 * reflTex).xyz);
  gl_FragData[0] = tmpvar_8;
}



#endif"
}

SubProgram "flash " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
Matrix 4 [_Object2World]
Matrix 8 [_World2Object]
"agal_vs
c12 0.0 0.0 0.0 0.0
[bc]
adaaaaaaaaaaahacabaaaaffaaaaaaaaajaaaaoeabaaaaaa mul r0.xyz, a1.y, c9
adaaaaaaabaaahacabaaaaaaaaaaaaaaaiaaaaoeabaaaaaa mul r1.xyz, a1.x, c8
abaaaaaaaaaaahacabaaaakeacaaaaaaaaaaaakeacaaaaaa add r0.xyz, r1.xyzz, r0.xyzz
adaaaaaaabaaahacabaaaakkaaaaaaaaakaaaaoeabaaaaaa mul r1.xyz, a1.z, c10
abaaaaaaaaaaahacabaaaakeacaaaaaaaaaaaakeacaaaaaa add r0.xyz, r1.xyzz, r0.xyzz
abaaaaaaaaaaahacaaaaaakeacaaaaaaamaaaaaaabaaaaaa add r0.xyz, r0.xyzz, c12.x
bcaaaaaaaaaaaiacaaaaaakeacaaaaaaaaaaaakeacaaaaaa dp3 r0.w, r0.xyzz, r0.xyzz
akaaaaaaaaaaaiacaaaaaappacaaaaaaaaaaaaaaaaaaaaaa rsq r0.w, r0.w
adaaaaaaabaaahaeaaaaaappacaaaaaaaaaaaakeacaaaaaa mul v1.xyz, r0.w, r0.xyzz
aaaaaaaaadaaapaeadaaaaoeaaaaaaaaaaaaaaaaaaaaaaaa mov v3, a3
bdaaaaaaaaaaaiadaaaaaaoeaaaaaaaaadaaaaoeabaaaaaa dp4 o0.w, a0, c3
bdaaaaaaaaaaaeadaaaaaaoeaaaaaaaaacaaaaoeabaaaaaa dp4 o0.z, a0, c2
bdaaaaaaaaaaacadaaaaaaoeaaaaaaaaabaaaaoeabaaaaaa dp4 o0.y, a0, c1
bdaaaaaaaaaaabadaaaaaaoeaaaaaaaaaaaaaaoeabaaaaaa dp4 o0.x, a0, c0
bdaaaaaaaaaaaiaeaaaaaaoeaaaaaaaaahaaaaoeabaaaaaa dp4 v0.w, a0, c7
bdaaaaaaaaaaaeaeaaaaaaoeaaaaaaaaagaaaaoeabaaaaaa dp4 v0.z, a0, c6
bdaaaaaaaaaaacaeaaaaaaoeaaaaaaaaafaaaaoeabaaaaaa dp4 v0.y, a0, c5
bdaaaaaaaaaaabaeaaaaaaoeaaaaaaaaaeaaaaoeabaaaaaa dp4 v0.x, a0, c4
aaaaaaaaabaaaiaeaaaaaaoeabaaaaaaaaaaaaaaaaaaaaaa mov v1.w, c0
"
}

SubProgram "opengl " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 5 [_Object2World]
Matrix 9 [_World2Object]
"!!ARBvp1.0
# 16 ALU
PARAM c[13] = { { 0 },
		state.matrix.mvp,
		program.local[5..12] };
TEMP R0;
MUL R0.xyz, vertex.normal.y, c[10];
MAD R0.xyz, vertex.normal.x, c[9], R0;
MAD R0.xyz, vertex.normal.z, c[11], R0;
ADD R0.xyz, R0, c[0].x;
DP3 R0.w, R0, R0;
RSQ R0.w, R0.w;
MUL result.texcoord[1].xyz, R0.w, R0;
MOV result.texcoord[3], vertex.texcoord[0];
DP4 result.position.w, vertex.position, c[4];
DP4 result.position.z, vertex.position, c[3];
DP4 result.position.y, vertex.position, c[2];
DP4 result.position.x, vertex.position, c[1];
DP4 result.texcoord[0].w, vertex.position, c[8];
DP4 result.texcoord[0].z, vertex.position, c[7];
DP4 result.texcoord[0].y, vertex.position, c[6];
DP4 result.texcoord[0].x, vertex.position, c[5];
END
# 16 instructions, 1 R-regs
"
}

SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
Matrix 4 [_Object2World]
Matrix 8 [_World2Object]
"vs_2_0
; 16 ALU
def c12, 0.00000000, 0, 0, 0
dcl_position0 v0
dcl_normal0 v1
dcl_texcoord0 v2
mul r0.xyz, v1.y, c9
mad r0.xyz, v1.x, c8, r0
mad r0.xyz, v1.z, c10, r0
add r0.xyz, r0, c12.x
dp3 r0.w, r0, r0
rsq r0.w, r0.w
mul oT1.xyz, r0.w, r0
mov oT3, v2
dp4 oPos.w, v0, c3
dp4 oPos.z, v0, c2
dp4 oPos.y, v0, c1
dp4 oPos.x, v0, c0
dp4 oT0.w, v0, c7
dp4 oT0.z, v0, c6
dp4 oT0.y, v0, c5
dp4 oT0.x, v0, c4
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

varying mediump vec4 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;

uniform highp mat4 _World2Object;
uniform highp mat4 _Object2World;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  lowp vec3 tmpvar_1;
  lowp vec4 tmpvar_2;
  tmpvar_2.w = 0.0;
  tmpvar_2.xyz = normalize (_glesNormal);
  highp vec3 tmpvar_3;
  tmpvar_3 = normalize ((tmpvar_2 * _World2Object).xyz);
  tmpvar_1 = tmpvar_3;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = (_Object2World * _glesVertex);
  xlv_TEXCOORD1 = tmpvar_1;
  xlv_TEXCOORD3 = _glesMultiTexCoord0;
}



#endif
#ifdef FRAGMENT

varying mediump vec4 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform lowp float _Reflection;
uniform highp vec3 _WorldSpaceCameraPos;
uniform sampler2D _MainTex;
uniform lowp float _FrezPow;
uniform mediump float _FrezFalloff;
uniform samplerCube _Cube;
void main ()
{
  lowp float frez;
  lowp vec4 reflTex;
  lowp vec3 viewDirection;
  highp vec3 tmpvar_1;
  tmpvar_1 = normalize ((_WorldSpaceCameraPos - xlv_TEXCOORD0.xyz));
  viewDirection = tmpvar_1;
  mediump vec2 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD3.xy;
  lowp vec3 tmpvar_3;
  tmpvar_3 = reflect (-(viewDirection), normalize (xlv_TEXCOORD1));
  lowp vec4 tmpvar_4;
  tmpvar_4 = textureCube (_Cube, tmpvar_3);
  reflTex = tmpvar_4;
  lowp float tmpvar_5;
  tmpvar_5 = clamp (abs (dot (tmpvar_3, xlv_TEXCOORD1)), 0.0, 1.0);
  mediump float tmpvar_6;
  tmpvar_6 = pow ((1.0 - tmpvar_5), _FrezFalloff);
  frez = tmpvar_6;
  lowp float tmpvar_7;
  tmpvar_7 = (frez * _FrezPow);
  frez = tmpvar_7;
  reflTex.xyz = (tmpvar_4.xyz * clamp ((_Reflection + tmpvar_7), 0.0, 1.0));
  lowp vec4 tmpvar_8;
  tmpvar_8.w = 1.0;
  tmpvar_8.xyz = ((texture2D (_MainTex, tmpvar_2).xyz + reflTex.xyz) + (tmpvar_7 * reflTex).xyz);
  gl_FragData[0] = tmpvar_8;
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

varying mediump vec4 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;

uniform highp mat4 _World2Object;
uniform highp mat4 _Object2World;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  lowp vec3 tmpvar_1;
  lowp vec4 tmpvar_2;
  tmpvar_2.w = 0.0;
  tmpvar_2.xyz = normalize (_glesNormal);
  highp vec3 tmpvar_3;
  tmpvar_3 = normalize ((tmpvar_2 * _World2Object).xyz);
  tmpvar_1 = tmpvar_3;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = (_Object2World * _glesVertex);
  xlv_TEXCOORD1 = tmpvar_1;
  xlv_TEXCOORD3 = _glesMultiTexCoord0;
}



#endif
#ifdef FRAGMENT

varying mediump vec4 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform lowp float _Reflection;
uniform highp vec3 _WorldSpaceCameraPos;
uniform sampler2D _MainTex;
uniform lowp float _FrezPow;
uniform mediump float _FrezFalloff;
uniform samplerCube _Cube;
void main ()
{
  lowp float frez;
  lowp vec4 reflTex;
  lowp vec3 viewDirection;
  highp vec3 tmpvar_1;
  tmpvar_1 = normalize ((_WorldSpaceCameraPos - xlv_TEXCOORD0.xyz));
  viewDirection = tmpvar_1;
  mediump vec2 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD3.xy;
  lowp vec3 tmpvar_3;
  tmpvar_3 = reflect (-(viewDirection), normalize (xlv_TEXCOORD1));
  lowp vec4 tmpvar_4;
  tmpvar_4 = textureCube (_Cube, tmpvar_3);
  reflTex = tmpvar_4;
  lowp float tmpvar_5;
  tmpvar_5 = clamp (abs (dot (tmpvar_3, xlv_TEXCOORD1)), 0.0, 1.0);
  mediump float tmpvar_6;
  tmpvar_6 = pow ((1.0 - tmpvar_5), _FrezFalloff);
  frez = tmpvar_6;
  lowp float tmpvar_7;
  tmpvar_7 = (frez * _FrezPow);
  frez = tmpvar_7;
  reflTex.xyz = (tmpvar_4.xyz * clamp ((_Reflection + tmpvar_7), 0.0, 1.0));
  lowp vec4 tmpvar_8;
  tmpvar_8.w = 1.0;
  tmpvar_8.xyz = ((texture2D (_MainTex, tmpvar_2).xyz + reflTex.xyz) + (tmpvar_7 * reflTex).xyz);
  gl_FragData[0] = tmpvar_8;
}



#endif"
}

SubProgram "flash " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
Matrix 4 [_Object2World]
Matrix 8 [_World2Object]
"agal_vs
c12 0.0 0.0 0.0 0.0
[bc]
adaaaaaaaaaaahacabaaaaffaaaaaaaaajaaaaoeabaaaaaa mul r0.xyz, a1.y, c9
adaaaaaaabaaahacabaaaaaaaaaaaaaaaiaaaaoeabaaaaaa mul r1.xyz, a1.x, c8
abaaaaaaaaaaahacabaaaakeacaaaaaaaaaaaakeacaaaaaa add r0.xyz, r1.xyzz, r0.xyzz
adaaaaaaabaaahacabaaaakkaaaaaaaaakaaaaoeabaaaaaa mul r1.xyz, a1.z, c10
abaaaaaaaaaaahacabaaaakeacaaaaaaaaaaaakeacaaaaaa add r0.xyz, r1.xyzz, r0.xyzz
abaaaaaaaaaaahacaaaaaakeacaaaaaaamaaaaaaabaaaaaa add r0.xyz, r0.xyzz, c12.x
bcaaaaaaaaaaaiacaaaaaakeacaaaaaaaaaaaakeacaaaaaa dp3 r0.w, r0.xyzz, r0.xyzz
akaaaaaaaaaaaiacaaaaaappacaaaaaaaaaaaaaaaaaaaaaa rsq r0.w, r0.w
adaaaaaaabaaahaeaaaaaappacaaaaaaaaaaaakeacaaaaaa mul v1.xyz, r0.w, r0.xyzz
aaaaaaaaadaaapaeadaaaaoeaaaaaaaaaaaaaaaaaaaaaaaa mov v3, a3
bdaaaaaaaaaaaiadaaaaaaoeaaaaaaaaadaaaaoeabaaaaaa dp4 o0.w, a0, c3
bdaaaaaaaaaaaeadaaaaaaoeaaaaaaaaacaaaaoeabaaaaaa dp4 o0.z, a0, c2
bdaaaaaaaaaaacadaaaaaaoeaaaaaaaaabaaaaoeabaaaaaa dp4 o0.y, a0, c1
bdaaaaaaaaaaabadaaaaaaoeaaaaaaaaaaaaaaoeabaaaaaa dp4 o0.x, a0, c0
bdaaaaaaaaaaaiaeaaaaaaoeaaaaaaaaahaaaaoeabaaaaaa dp4 v0.w, a0, c7
bdaaaaaaaaaaaeaeaaaaaaoeaaaaaaaaagaaaaoeabaaaaaa dp4 v0.z, a0, c6
bdaaaaaaaaaaacaeaaaaaaoeaaaaaaaaafaaaaoeabaaaaaa dp4 v0.y, a0, c5
bdaaaaaaaaaaabaeaaaaaaoeaaaaaaaaaeaaaaoeabaaaaaa dp4 v0.x, a0, c4
aaaaaaaaabaaaiaeaaaaaaoeabaaaaaaaaaaaaaaaaaaaaaa mov v1.w, c0
"
}

SubProgram "opengl " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "SHADOWS_SCREEN" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 5 [_Object2World]
Matrix 9 [_World2Object]
"!!ARBvp1.0
# 16 ALU
PARAM c[13] = { { 0 },
		state.matrix.mvp,
		program.local[5..12] };
TEMP R0;
MUL R0.xyz, vertex.normal.y, c[10];
MAD R0.xyz, vertex.normal.x, c[9], R0;
MAD R0.xyz, vertex.normal.z, c[11], R0;
ADD R0.xyz, R0, c[0].x;
DP3 R0.w, R0, R0;
RSQ R0.w, R0.w;
MUL result.texcoord[1].xyz, R0.w, R0;
MOV result.texcoord[3], vertex.texcoord[0];
DP4 result.position.w, vertex.position, c[4];
DP4 result.position.z, vertex.position, c[3];
DP4 result.position.y, vertex.position, c[2];
DP4 result.position.x, vertex.position, c[1];
DP4 result.texcoord[0].w, vertex.position, c[8];
DP4 result.texcoord[0].z, vertex.position, c[7];
DP4 result.texcoord[0].y, vertex.position, c[6];
DP4 result.texcoord[0].x, vertex.position, c[5];
END
# 16 instructions, 1 R-regs
"
}

SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "SHADOWS_SCREEN" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
Matrix 4 [_Object2World]
Matrix 8 [_World2Object]
"vs_2_0
; 16 ALU
def c12, 0.00000000, 0, 0, 0
dcl_position0 v0
dcl_normal0 v1
dcl_texcoord0 v2
mul r0.xyz, v1.y, c9
mad r0.xyz, v1.x, c8, r0
mad r0.xyz, v1.z, c10, r0
add r0.xyz, r0, c12.x
dp3 r0.w, r0, r0
rsq r0.w, r0.w
mul oT1.xyz, r0.w, r0
mov oT3, v2
dp4 oPos.w, v0, c3
dp4 oPos.z, v0, c2
dp4 oPos.y, v0, c1
dp4 oPos.x, v0, c0
dp4 oT0.w, v0, c7
dp4 oT0.z, v0, c6
dp4 oT0.y, v0, c5
dp4 oT0.x, v0, c4
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

varying mediump vec4 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;

uniform highp mat4 _World2Object;
uniform highp mat4 _Object2World;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  lowp vec3 tmpvar_1;
  lowp vec4 tmpvar_2;
  tmpvar_2.w = 0.0;
  tmpvar_2.xyz = normalize (_glesNormal);
  highp vec3 tmpvar_3;
  tmpvar_3 = normalize ((tmpvar_2 * _World2Object).xyz);
  tmpvar_1 = tmpvar_3;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = (_Object2World * _glesVertex);
  xlv_TEXCOORD1 = tmpvar_1;
  xlv_TEXCOORD3 = _glesMultiTexCoord0;
}



#endif
#ifdef FRAGMENT

varying mediump vec4 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform lowp float _Reflection;
uniform highp vec3 _WorldSpaceCameraPos;
uniform sampler2D _MainTex;
uniform lowp float _FrezPow;
uniform mediump float _FrezFalloff;
uniform samplerCube _Cube;
void main ()
{
  lowp float frez;
  lowp vec4 reflTex;
  lowp vec3 viewDirection;
  highp vec3 tmpvar_1;
  tmpvar_1 = normalize ((_WorldSpaceCameraPos - xlv_TEXCOORD0.xyz));
  viewDirection = tmpvar_1;
  mediump vec2 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD3.xy;
  lowp vec3 tmpvar_3;
  tmpvar_3 = reflect (-(viewDirection), normalize (xlv_TEXCOORD1));
  lowp vec4 tmpvar_4;
  tmpvar_4 = textureCube (_Cube, tmpvar_3);
  reflTex = tmpvar_4;
  lowp float tmpvar_5;
  tmpvar_5 = clamp (abs (dot (tmpvar_3, xlv_TEXCOORD1)), 0.0, 1.0);
  mediump float tmpvar_6;
  tmpvar_6 = pow ((1.0 - tmpvar_5), _FrezFalloff);
  frez = tmpvar_6;
  lowp float tmpvar_7;
  tmpvar_7 = (frez * _FrezPow);
  frez = tmpvar_7;
  reflTex.xyz = (tmpvar_4.xyz * clamp ((_Reflection + tmpvar_7), 0.0, 1.0));
  lowp vec4 tmpvar_8;
  tmpvar_8.w = 1.0;
  tmpvar_8.xyz = ((texture2D (_MainTex, tmpvar_2).xyz + reflTex.xyz) + (tmpvar_7 * reflTex).xyz);
  gl_FragData[0] = tmpvar_8;
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

varying mediump vec4 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;

uniform highp mat4 _World2Object;
uniform highp mat4 _Object2World;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  lowp vec3 tmpvar_1;
  lowp vec4 tmpvar_2;
  tmpvar_2.w = 0.0;
  tmpvar_2.xyz = normalize (_glesNormal);
  highp vec3 tmpvar_3;
  tmpvar_3 = normalize ((tmpvar_2 * _World2Object).xyz);
  tmpvar_1 = tmpvar_3;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = (_Object2World * _glesVertex);
  xlv_TEXCOORD1 = tmpvar_1;
  xlv_TEXCOORD3 = _glesMultiTexCoord0;
}



#endif
#ifdef FRAGMENT

varying mediump vec4 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform lowp float _Reflection;
uniform highp vec3 _WorldSpaceCameraPos;
uniform sampler2D _MainTex;
uniform lowp float _FrezPow;
uniform mediump float _FrezFalloff;
uniform samplerCube _Cube;
void main ()
{
  lowp float frez;
  lowp vec4 reflTex;
  lowp vec3 viewDirection;
  highp vec3 tmpvar_1;
  tmpvar_1 = normalize ((_WorldSpaceCameraPos - xlv_TEXCOORD0.xyz));
  viewDirection = tmpvar_1;
  mediump vec2 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD3.xy;
  lowp vec3 tmpvar_3;
  tmpvar_3 = reflect (-(viewDirection), normalize (xlv_TEXCOORD1));
  lowp vec4 tmpvar_4;
  tmpvar_4 = textureCube (_Cube, tmpvar_3);
  reflTex = tmpvar_4;
  lowp float tmpvar_5;
  tmpvar_5 = clamp (abs (dot (tmpvar_3, xlv_TEXCOORD1)), 0.0, 1.0);
  mediump float tmpvar_6;
  tmpvar_6 = pow ((1.0 - tmpvar_5), _FrezFalloff);
  frez = tmpvar_6;
  lowp float tmpvar_7;
  tmpvar_7 = (frez * _FrezPow);
  frez = tmpvar_7;
  reflTex.xyz = (tmpvar_4.xyz * clamp ((_Reflection + tmpvar_7), 0.0, 1.0));
  lowp vec4 tmpvar_8;
  tmpvar_8.w = 1.0;
  tmpvar_8.xyz = ((texture2D (_MainTex, tmpvar_2).xyz + reflTex.xyz) + (tmpvar_7 * reflTex).xyz);
  gl_FragData[0] = tmpvar_8;
}



#endif"
}

SubProgram "flash " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "SHADOWS_SCREEN" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
Matrix 4 [_Object2World]
Matrix 8 [_World2Object]
"agal_vs
c12 0.0 0.0 0.0 0.0
[bc]
adaaaaaaaaaaahacabaaaaffaaaaaaaaajaaaaoeabaaaaaa mul r0.xyz, a1.y, c9
adaaaaaaabaaahacabaaaaaaaaaaaaaaaiaaaaoeabaaaaaa mul r1.xyz, a1.x, c8
abaaaaaaaaaaahacabaaaakeacaaaaaaaaaaaakeacaaaaaa add r0.xyz, r1.xyzz, r0.xyzz
adaaaaaaabaaahacabaaaakkaaaaaaaaakaaaaoeabaaaaaa mul r1.xyz, a1.z, c10
abaaaaaaaaaaahacabaaaakeacaaaaaaaaaaaakeacaaaaaa add r0.xyz, r1.xyzz, r0.xyzz
abaaaaaaaaaaahacaaaaaakeacaaaaaaamaaaaaaabaaaaaa add r0.xyz, r0.xyzz, c12.x
bcaaaaaaaaaaaiacaaaaaakeacaaaaaaaaaaaakeacaaaaaa dp3 r0.w, r0.xyzz, r0.xyzz
akaaaaaaaaaaaiacaaaaaappacaaaaaaaaaaaaaaaaaaaaaa rsq r0.w, r0.w
adaaaaaaabaaahaeaaaaaappacaaaaaaaaaaaakeacaaaaaa mul v1.xyz, r0.w, r0.xyzz
aaaaaaaaadaaapaeadaaaaoeaaaaaaaaaaaaaaaaaaaaaaaa mov v3, a3
bdaaaaaaaaaaaiadaaaaaaoeaaaaaaaaadaaaaoeabaaaaaa dp4 o0.w, a0, c3
bdaaaaaaaaaaaeadaaaaaaoeaaaaaaaaacaaaaoeabaaaaaa dp4 o0.z, a0, c2
bdaaaaaaaaaaacadaaaaaaoeaaaaaaaaabaaaaoeabaaaaaa dp4 o0.y, a0, c1
bdaaaaaaaaaaabadaaaaaaoeaaaaaaaaaaaaaaoeabaaaaaa dp4 o0.x, a0, c0
bdaaaaaaaaaaaiaeaaaaaaoeaaaaaaaaahaaaaoeabaaaaaa dp4 v0.w, a0, c7
bdaaaaaaaaaaaeaeaaaaaaoeaaaaaaaaagaaaaoeabaaaaaa dp4 v0.z, a0, c6
bdaaaaaaaaaaacaeaaaaaaoeaaaaaaaaafaaaaoeabaaaaaa dp4 v0.y, a0, c5
bdaaaaaaaaaaabaeaaaaaaoeaaaaaaaaaeaaaaoeabaaaaaa dp4 v0.x, a0, c4
aaaaaaaaabaaaiaeaaaaaaoeabaaaaaaaaaaaaaaaaaaaaaa mov v1.w, c0
"
}

SubProgram "opengl " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" "VERTEXLIGHT_ON" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 5 [_Object2World]
Matrix 9 [_World2Object]
"!!ARBvp1.0
# 16 ALU
PARAM c[13] = { { 0 },
		state.matrix.mvp,
		program.local[5..12] };
TEMP R0;
MUL R0.xyz, vertex.normal.y, c[10];
MAD R0.xyz, vertex.normal.x, c[9], R0;
MAD R0.xyz, vertex.normal.z, c[11], R0;
ADD R0.xyz, R0, c[0].x;
DP3 R0.w, R0, R0;
RSQ R0.w, R0.w;
MUL result.texcoord[1].xyz, R0.w, R0;
MOV result.texcoord[3], vertex.texcoord[0];
DP4 result.position.w, vertex.position, c[4];
DP4 result.position.z, vertex.position, c[3];
DP4 result.position.y, vertex.position, c[2];
DP4 result.position.x, vertex.position, c[1];
DP4 result.texcoord[0].w, vertex.position, c[8];
DP4 result.texcoord[0].z, vertex.position, c[7];
DP4 result.texcoord[0].y, vertex.position, c[6];
DP4 result.texcoord[0].x, vertex.position, c[5];
END
# 16 instructions, 1 R-regs
"
}

SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" "VERTEXLIGHT_ON" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
Matrix 4 [_Object2World]
Matrix 8 [_World2Object]
"vs_2_0
; 16 ALU
def c12, 0.00000000, 0, 0, 0
dcl_position0 v0
dcl_normal0 v1
dcl_texcoord0 v2
mul r0.xyz, v1.y, c9
mad r0.xyz, v1.x, c8, r0
mad r0.xyz, v1.z, c10, r0
add r0.xyz, r0, c12.x
dp3 r0.w, r0, r0
rsq r0.w, r0.w
mul oT1.xyz, r0.w, r0
mov oT3, v2
dp4 oPos.w, v0, c3
dp4 oPos.z, v0, c2
dp4 oPos.y, v0, c1
dp4 oPos.x, v0, c0
dp4 oT0.w, v0, c7
dp4 oT0.z, v0, c6
dp4 oT0.y, v0, c5
dp4 oT0.x, v0, c4
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

varying mediump vec4 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;

uniform highp mat4 _World2Object;
uniform highp mat4 _Object2World;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  lowp vec3 tmpvar_1;
  lowp vec4 tmpvar_2;
  tmpvar_2.w = 0.0;
  tmpvar_2.xyz = normalize (_glesNormal);
  highp vec3 tmpvar_3;
  tmpvar_3 = normalize ((tmpvar_2 * _World2Object).xyz);
  tmpvar_1 = tmpvar_3;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = (_Object2World * _glesVertex);
  xlv_TEXCOORD1 = tmpvar_1;
  xlv_TEXCOORD3 = _glesMultiTexCoord0;
}



#endif
#ifdef FRAGMENT

varying mediump vec4 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform lowp float _Reflection;
uniform highp vec3 _WorldSpaceCameraPos;
uniform sampler2D _MainTex;
uniform lowp float _FrezPow;
uniform mediump float _FrezFalloff;
uniform samplerCube _Cube;
void main ()
{
  lowp float frez;
  lowp vec4 reflTex;
  lowp vec3 viewDirection;
  highp vec3 tmpvar_1;
  tmpvar_1 = normalize ((_WorldSpaceCameraPos - xlv_TEXCOORD0.xyz));
  viewDirection = tmpvar_1;
  mediump vec2 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD3.xy;
  lowp vec3 tmpvar_3;
  tmpvar_3 = reflect (-(viewDirection), normalize (xlv_TEXCOORD1));
  lowp vec4 tmpvar_4;
  tmpvar_4 = textureCube (_Cube, tmpvar_3);
  reflTex = tmpvar_4;
  lowp float tmpvar_5;
  tmpvar_5 = clamp (abs (dot (tmpvar_3, xlv_TEXCOORD1)), 0.0, 1.0);
  mediump float tmpvar_6;
  tmpvar_6 = pow ((1.0 - tmpvar_5), _FrezFalloff);
  frez = tmpvar_6;
  lowp float tmpvar_7;
  tmpvar_7 = (frez * _FrezPow);
  frez = tmpvar_7;
  reflTex.xyz = (tmpvar_4.xyz * clamp ((_Reflection + tmpvar_7), 0.0, 1.0));
  lowp vec4 tmpvar_8;
  tmpvar_8.w = 1.0;
  tmpvar_8.xyz = ((texture2D (_MainTex, tmpvar_2).xyz + reflTex.xyz) + (tmpvar_7 * reflTex).xyz);
  gl_FragData[0] = tmpvar_8;
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

varying mediump vec4 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;

uniform highp mat4 _World2Object;
uniform highp mat4 _Object2World;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  lowp vec3 tmpvar_1;
  lowp vec4 tmpvar_2;
  tmpvar_2.w = 0.0;
  tmpvar_2.xyz = normalize (_glesNormal);
  highp vec3 tmpvar_3;
  tmpvar_3 = normalize ((tmpvar_2 * _World2Object).xyz);
  tmpvar_1 = tmpvar_3;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = (_Object2World * _glesVertex);
  xlv_TEXCOORD1 = tmpvar_1;
  xlv_TEXCOORD3 = _glesMultiTexCoord0;
}



#endif
#ifdef FRAGMENT

varying mediump vec4 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform lowp float _Reflection;
uniform highp vec3 _WorldSpaceCameraPos;
uniform sampler2D _MainTex;
uniform lowp float _FrezPow;
uniform mediump float _FrezFalloff;
uniform samplerCube _Cube;
void main ()
{
  lowp float frez;
  lowp vec4 reflTex;
  lowp vec3 viewDirection;
  highp vec3 tmpvar_1;
  tmpvar_1 = normalize ((_WorldSpaceCameraPos - xlv_TEXCOORD0.xyz));
  viewDirection = tmpvar_1;
  mediump vec2 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD3.xy;
  lowp vec3 tmpvar_3;
  tmpvar_3 = reflect (-(viewDirection), normalize (xlv_TEXCOORD1));
  lowp vec4 tmpvar_4;
  tmpvar_4 = textureCube (_Cube, tmpvar_3);
  reflTex = tmpvar_4;
  lowp float tmpvar_5;
  tmpvar_5 = clamp (abs (dot (tmpvar_3, xlv_TEXCOORD1)), 0.0, 1.0);
  mediump float tmpvar_6;
  tmpvar_6 = pow ((1.0 - tmpvar_5), _FrezFalloff);
  frez = tmpvar_6;
  lowp float tmpvar_7;
  tmpvar_7 = (frez * _FrezPow);
  frez = tmpvar_7;
  reflTex.xyz = (tmpvar_4.xyz * clamp ((_Reflection + tmpvar_7), 0.0, 1.0));
  lowp vec4 tmpvar_8;
  tmpvar_8.w = 1.0;
  tmpvar_8.xyz = ((texture2D (_MainTex, tmpvar_2).xyz + reflTex.xyz) + (tmpvar_7 * reflTex).xyz);
  gl_FragData[0] = tmpvar_8;
}



#endif"
}

SubProgram "flash " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" "VERTEXLIGHT_ON" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
Matrix 4 [_Object2World]
Matrix 8 [_World2Object]
"agal_vs
c12 0.0 0.0 0.0 0.0
[bc]
adaaaaaaaaaaahacabaaaaffaaaaaaaaajaaaaoeabaaaaaa mul r0.xyz, a1.y, c9
adaaaaaaabaaahacabaaaaaaaaaaaaaaaiaaaaoeabaaaaaa mul r1.xyz, a1.x, c8
abaaaaaaaaaaahacabaaaakeacaaaaaaaaaaaakeacaaaaaa add r0.xyz, r1.xyzz, r0.xyzz
adaaaaaaabaaahacabaaaakkaaaaaaaaakaaaaoeabaaaaaa mul r1.xyz, a1.z, c10
abaaaaaaaaaaahacabaaaakeacaaaaaaaaaaaakeacaaaaaa add r0.xyz, r1.xyzz, r0.xyzz
abaaaaaaaaaaahacaaaaaakeacaaaaaaamaaaaaaabaaaaaa add r0.xyz, r0.xyzz, c12.x
bcaaaaaaaaaaaiacaaaaaakeacaaaaaaaaaaaakeacaaaaaa dp3 r0.w, r0.xyzz, r0.xyzz
akaaaaaaaaaaaiacaaaaaappacaaaaaaaaaaaaaaaaaaaaaa rsq r0.w, r0.w
adaaaaaaabaaahaeaaaaaappacaaaaaaaaaaaakeacaaaaaa mul v1.xyz, r0.w, r0.xyzz
aaaaaaaaadaaapaeadaaaaoeaaaaaaaaaaaaaaaaaaaaaaaa mov v3, a3
bdaaaaaaaaaaaiadaaaaaaoeaaaaaaaaadaaaaoeabaaaaaa dp4 o0.w, a0, c3
bdaaaaaaaaaaaeadaaaaaaoeaaaaaaaaacaaaaoeabaaaaaa dp4 o0.z, a0, c2
bdaaaaaaaaaaacadaaaaaaoeaaaaaaaaabaaaaoeabaaaaaa dp4 o0.y, a0, c1
bdaaaaaaaaaaabadaaaaaaoeaaaaaaaaaaaaaaoeabaaaaaa dp4 o0.x, a0, c0
bdaaaaaaaaaaaiaeaaaaaaoeaaaaaaaaahaaaaoeabaaaaaa dp4 v0.w, a0, c7
bdaaaaaaaaaaaeaeaaaaaaoeaaaaaaaaagaaaaoeabaaaaaa dp4 v0.z, a0, c6
bdaaaaaaaaaaacaeaaaaaaoeaaaaaaaaafaaaaoeabaaaaaa dp4 v0.y, a0, c5
bdaaaaaaaaaaabaeaaaaaaoeaaaaaaaaaeaaaaoeabaaaaaa dp4 v0.x, a0, c4
aaaaaaaaabaaaiaeaaaaaaoeabaaaaaaaaaaaaaaaaaaaaaa mov v1.w, c0
"
}

SubProgram "opengl " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" "VERTEXLIGHT_ON" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 5 [_Object2World]
Matrix 9 [_World2Object]
"!!ARBvp1.0
# 16 ALU
PARAM c[13] = { { 0 },
		state.matrix.mvp,
		program.local[5..12] };
TEMP R0;
MUL R0.xyz, vertex.normal.y, c[10];
MAD R0.xyz, vertex.normal.x, c[9], R0;
MAD R0.xyz, vertex.normal.z, c[11], R0;
ADD R0.xyz, R0, c[0].x;
DP3 R0.w, R0, R0;
RSQ R0.w, R0.w;
MUL result.texcoord[1].xyz, R0.w, R0;
MOV result.texcoord[3], vertex.texcoord[0];
DP4 result.position.w, vertex.position, c[4];
DP4 result.position.z, vertex.position, c[3];
DP4 result.position.y, vertex.position, c[2];
DP4 result.position.x, vertex.position, c[1];
DP4 result.texcoord[0].w, vertex.position, c[8];
DP4 result.texcoord[0].z, vertex.position, c[7];
DP4 result.texcoord[0].y, vertex.position, c[6];
DP4 result.texcoord[0].x, vertex.position, c[5];
END
# 16 instructions, 1 R-regs
"
}

SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" "VERTEXLIGHT_ON" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
Matrix 4 [_Object2World]
Matrix 8 [_World2Object]
"vs_2_0
; 16 ALU
def c12, 0.00000000, 0, 0, 0
dcl_position0 v0
dcl_normal0 v1
dcl_texcoord0 v2
mul r0.xyz, v1.y, c9
mad r0.xyz, v1.x, c8, r0
mad r0.xyz, v1.z, c10, r0
add r0.xyz, r0, c12.x
dp3 r0.w, r0, r0
rsq r0.w, r0.w
mul oT1.xyz, r0.w, r0
mov oT3, v2
dp4 oPos.w, v0, c3
dp4 oPos.z, v0, c2
dp4 oPos.y, v0, c1
dp4 oPos.x, v0, c0
dp4 oT0.w, v0, c7
dp4 oT0.z, v0, c6
dp4 oT0.y, v0, c5
dp4 oT0.x, v0, c4
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

varying mediump vec4 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;

uniform highp mat4 _World2Object;
uniform highp mat4 _Object2World;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  lowp vec3 tmpvar_1;
  lowp vec4 tmpvar_2;
  tmpvar_2.w = 0.0;
  tmpvar_2.xyz = normalize (_glesNormal);
  highp vec3 tmpvar_3;
  tmpvar_3 = normalize ((tmpvar_2 * _World2Object).xyz);
  tmpvar_1 = tmpvar_3;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = (_Object2World * _glesVertex);
  xlv_TEXCOORD1 = tmpvar_1;
  xlv_TEXCOORD3 = _glesMultiTexCoord0;
}



#endif
#ifdef FRAGMENT

varying mediump vec4 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform lowp float _Reflection;
uniform highp vec3 _WorldSpaceCameraPos;
uniform sampler2D _MainTex;
uniform lowp float _FrezPow;
uniform mediump float _FrezFalloff;
uniform samplerCube _Cube;
void main ()
{
  lowp float frez;
  lowp vec4 reflTex;
  lowp vec3 viewDirection;
  highp vec3 tmpvar_1;
  tmpvar_1 = normalize ((_WorldSpaceCameraPos - xlv_TEXCOORD0.xyz));
  viewDirection = tmpvar_1;
  mediump vec2 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD3.xy;
  lowp vec3 tmpvar_3;
  tmpvar_3 = reflect (-(viewDirection), normalize (xlv_TEXCOORD1));
  lowp vec4 tmpvar_4;
  tmpvar_4 = textureCube (_Cube, tmpvar_3);
  reflTex = tmpvar_4;
  lowp float tmpvar_5;
  tmpvar_5 = clamp (abs (dot (tmpvar_3, xlv_TEXCOORD1)), 0.0, 1.0);
  mediump float tmpvar_6;
  tmpvar_6 = pow ((1.0 - tmpvar_5), _FrezFalloff);
  frez = tmpvar_6;
  lowp float tmpvar_7;
  tmpvar_7 = (frez * _FrezPow);
  frez = tmpvar_7;
  reflTex.xyz = (tmpvar_4.xyz * clamp ((_Reflection + tmpvar_7), 0.0, 1.0));
  lowp vec4 tmpvar_8;
  tmpvar_8.w = 1.0;
  tmpvar_8.xyz = ((texture2D (_MainTex, tmpvar_2).xyz + reflTex.xyz) + (tmpvar_7 * reflTex).xyz);
  gl_FragData[0] = tmpvar_8;
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

varying mediump vec4 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;

uniform highp mat4 _World2Object;
uniform highp mat4 _Object2World;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  lowp vec3 tmpvar_1;
  lowp vec4 tmpvar_2;
  tmpvar_2.w = 0.0;
  tmpvar_2.xyz = normalize (_glesNormal);
  highp vec3 tmpvar_3;
  tmpvar_3 = normalize ((tmpvar_2 * _World2Object).xyz);
  tmpvar_1 = tmpvar_3;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = (_Object2World * _glesVertex);
  xlv_TEXCOORD1 = tmpvar_1;
  xlv_TEXCOORD3 = _glesMultiTexCoord0;
}



#endif
#ifdef FRAGMENT

varying mediump vec4 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform lowp float _Reflection;
uniform highp vec3 _WorldSpaceCameraPos;
uniform sampler2D _MainTex;
uniform lowp float _FrezPow;
uniform mediump float _FrezFalloff;
uniform samplerCube _Cube;
void main ()
{
  lowp float frez;
  lowp vec4 reflTex;
  lowp vec3 viewDirection;
  highp vec3 tmpvar_1;
  tmpvar_1 = normalize ((_WorldSpaceCameraPos - xlv_TEXCOORD0.xyz));
  viewDirection = tmpvar_1;
  mediump vec2 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD3.xy;
  lowp vec3 tmpvar_3;
  tmpvar_3 = reflect (-(viewDirection), normalize (xlv_TEXCOORD1));
  lowp vec4 tmpvar_4;
  tmpvar_4 = textureCube (_Cube, tmpvar_3);
  reflTex = tmpvar_4;
  lowp float tmpvar_5;
  tmpvar_5 = clamp (abs (dot (tmpvar_3, xlv_TEXCOORD1)), 0.0, 1.0);
  mediump float tmpvar_6;
  tmpvar_6 = pow ((1.0 - tmpvar_5), _FrezFalloff);
  frez = tmpvar_6;
  lowp float tmpvar_7;
  tmpvar_7 = (frez * _FrezPow);
  frez = tmpvar_7;
  reflTex.xyz = (tmpvar_4.xyz * clamp ((_Reflection + tmpvar_7), 0.0, 1.0));
  lowp vec4 tmpvar_8;
  tmpvar_8.w = 1.0;
  tmpvar_8.xyz = ((texture2D (_MainTex, tmpvar_2).xyz + reflTex.xyz) + (tmpvar_7 * reflTex).xyz);
  gl_FragData[0] = tmpvar_8;
}



#endif"
}

SubProgram "flash " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" "VERTEXLIGHT_ON" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
Matrix 4 [_Object2World]
Matrix 8 [_World2Object]
"agal_vs
c12 0.0 0.0 0.0 0.0
[bc]
adaaaaaaaaaaahacabaaaaffaaaaaaaaajaaaaoeabaaaaaa mul r0.xyz, a1.y, c9
adaaaaaaabaaahacabaaaaaaaaaaaaaaaiaaaaoeabaaaaaa mul r1.xyz, a1.x, c8
abaaaaaaaaaaahacabaaaakeacaaaaaaaaaaaakeacaaaaaa add r0.xyz, r1.xyzz, r0.xyzz
adaaaaaaabaaahacabaaaakkaaaaaaaaakaaaaoeabaaaaaa mul r1.xyz, a1.z, c10
abaaaaaaaaaaahacabaaaakeacaaaaaaaaaaaakeacaaaaaa add r0.xyz, r1.xyzz, r0.xyzz
abaaaaaaaaaaahacaaaaaakeacaaaaaaamaaaaaaabaaaaaa add r0.xyz, r0.xyzz, c12.x
bcaaaaaaaaaaaiacaaaaaakeacaaaaaaaaaaaakeacaaaaaa dp3 r0.w, r0.xyzz, r0.xyzz
akaaaaaaaaaaaiacaaaaaappacaaaaaaaaaaaaaaaaaaaaaa rsq r0.w, r0.w
adaaaaaaabaaahaeaaaaaappacaaaaaaaaaaaakeacaaaaaa mul v1.xyz, r0.w, r0.xyzz
aaaaaaaaadaaapaeadaaaaoeaaaaaaaaaaaaaaaaaaaaaaaa mov v3, a3
bdaaaaaaaaaaaiadaaaaaaoeaaaaaaaaadaaaaoeabaaaaaa dp4 o0.w, a0, c3
bdaaaaaaaaaaaeadaaaaaaoeaaaaaaaaacaaaaoeabaaaaaa dp4 o0.z, a0, c2
bdaaaaaaaaaaacadaaaaaaoeaaaaaaaaabaaaaoeabaaaaaa dp4 o0.y, a0, c1
bdaaaaaaaaaaabadaaaaaaoeaaaaaaaaaaaaaaoeabaaaaaa dp4 o0.x, a0, c0
bdaaaaaaaaaaaiaeaaaaaaoeaaaaaaaaahaaaaoeabaaaaaa dp4 v0.w, a0, c7
bdaaaaaaaaaaaeaeaaaaaaoeaaaaaaaaagaaaaoeabaaaaaa dp4 v0.z, a0, c6
bdaaaaaaaaaaacaeaaaaaaoeaaaaaaaaafaaaaoeabaaaaaa dp4 v0.y, a0, c5
bdaaaaaaaaaaabaeaaaaaaoeaaaaaaaaaeaaaaoeabaaaaaa dp4 v0.x, a0, c4
aaaaaaaaabaaaiaeaaaaaaoeabaaaaaaaaaaaaaaaaaaaaaa mov v1.w, c0
"
}

}
Program "fp" {
// Fragment combos: 6
//   opengl - ALU: 22 to 22, TEX: 2 to 2
//   d3d9 - ALU: 24 to 24, TEX: 2 to 2
SubProgram "opengl " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" }
Vector 0 [_WorldSpaceCameraPos]
Float 1 [_Reflection]
Float 2 [_FrezPow]
Float 3 [_FrezFalloff]
SetTexture 0 [_MainTex] 2D
SetTexture 1 [_Cube] CUBE
"!!ARBfp1.0
OPTION ARB_precision_hint_fastest;
# 22 ALU, 2 TEX
PARAM c[5] = { program.local[0..3],
		{ 2, 1 } };
TEMP R0;
TEMP R1;
TEMP R2;
ADD R0.xyz, -fragment.texcoord[0], c[0];
DP3 R1.x, R0, R0;
RSQ R1.x, R1.x;
DP3 R0.w, fragment.texcoord[1], fragment.texcoord[1];
MUL R0.xyz, R1.x, R0;
RSQ R0.w, R0.w;
MUL R1.xyz, R0.w, fragment.texcoord[1];
DP3 R0.w, R1, -R0;
MUL R1.xyz, R1, R0.w;
MAD R2.xyz, -R1, c[4].x, -R0;
DP3 R0.w, R2, fragment.texcoord[1];
ABS_SAT R0.w, R0;
ADD R0.w, -R0, c[4].y;
POW R0.w, R0.w, c[3].x;
MUL R0.w, R0, c[2].x;
ADD_SAT R1.w, R0, c[1].x;
MOV result.color.w, c[4].y;
TEX R0.xyz, R2, texture[1], CUBE;
TEX R1.xyz, fragment.texcoord[3], texture[0], 2D;
MUL R0.xyz, R0, R1.w;
ADD R1.xyz, R0, R1;
MAD result.color.xyz, R0.w, R0, R1;
END
# 22 instructions, 3 R-regs
"
}

SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" }
Vector 0 [_WorldSpaceCameraPos]
Float 1 [_Reflection]
Float 2 [_FrezPow]
Float 3 [_FrezFalloff]
SetTexture 0 [_MainTex] 2D
SetTexture 1 [_Cube] CUBE
"ps_2_0
; 24 ALU, 2 TEX
dcl_2d s0
dcl_cube s1
def c4, 2.00000000, 1.00000000, 0, 0
dcl t0.xyz
dcl t1.xyz
dcl t3.xy
add r2.xyz, -t0, c0
dp3 r1.x, r2, r2
dp3_pp r0.x, t1, t1
rsq r1.x, r1.x
mul r1.xyz, r1.x, r2
rsq_pp r0.x, r0.x
mul_pp r2.xyz, r0.x, t1
dp3_pp r0.x, r2, -r1
mul_pp r0.xyz, r2, r0.x
mad_pp r0.xyz, -r0, c4.x, -r1
mov_pp r0.w, c4.y
texld r2, r0, s1
texld r1, t3, s0
dp3_pp r0.x, r0, t1
abs_pp_sat r0.x, r0
add_pp r0.x, -r0, c4.y
pow_pp r3.x, r0.x, c3.x
mov_pp r0.x, r3.x
mul_pp r0.x, r0, c2
add_pp_sat r3.x, r0, c1
mul_pp r2.xyz, r2, r3.x
add_pp r1.xyz, r2, r1
mad_pp r0.xyz, r0.x, r2, r1
mov_pp oC0, r0
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

SubProgram "flash " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" }
Vector 0 [_WorldSpaceCameraPos]
Float 1 [_Reflection]
Float 2 [_FrezPow]
Float 3 [_FrezFalloff]
SetTexture 0 [_MainTex] 2D
SetTexture 1 [_Cube] CUBE
"agal_ps
c4 2.0 1.0 0.0 0.0
[bc]
bfaaaaaaacaaahacaaaaaaoeaeaaaaaaaaaaaaaaaaaaaaaa neg r2.xyz, v0
abaaaaaaacaaahacacaaaakeacaaaaaaaaaaaaoeabaaaaaa add r2.xyz, r2.xyzz, c0
bcaaaaaaabaaabacacaaaakeacaaaaaaacaaaakeacaaaaaa dp3 r1.x, r2.xyzz, r2.xyzz
bcaaaaaaaaaaabacabaaaaoeaeaaaaaaabaaaaoeaeaaaaaa dp3 r0.x, v1, v1
akaaaaaaabaaabacabaaaaaaacaaaaaaaaaaaaaaaaaaaaaa rsq r1.x, r1.x
adaaaaaaabaaahacabaaaaaaacaaaaaaacaaaakeacaaaaaa mul r1.xyz, r1.x, r2.xyzz
akaaaaaaaaaaabacaaaaaaaaacaaaaaaaaaaaaaaaaaaaaaa rsq r0.x, r0.x
adaaaaaaacaaahacaaaaaaaaacaaaaaaabaaaaoeaeaaaaaa mul r2.xyz, r0.x, v1
bfaaaaaaadaaahacabaaaakeacaaaaaaaaaaaaaaaaaaaaaa neg r3.xyz, r1.xyzz
bcaaaaaaaaaaabacacaaaakeacaaaaaaadaaaakeacaaaaaa dp3 r0.x, r2.xyzz, r3.xyzz
adaaaaaaaaaaahacacaaaakeacaaaaaaaaaaaaaaacaaaaaa mul r0.xyz, r2.xyzz, r0.x
bfaaaaaaaeaaahacaaaaaakeacaaaaaaaaaaaaaaaaaaaaaa neg r4.xyz, r0.xyzz
adaaaaaaaeaaahacaeaaaakeacaaaaaaaeaaaaaaabaaaaaa mul r4.xyz, r4.xyzz, c4.x
acaaaaaaaaaaahacaeaaaakeacaaaaaaabaaaakeacaaaaaa sub r0.xyz, r4.xyzz, r1.xyzz
aaaaaaaaaaaaaiacaeaaaaffabaaaaaaaaaaaaaaaaaaaaaa mov r0.w, c4.y
ciaaaaaaacaaapacaaaaaageacaaaaaaabaaaaaaafbababb tex r2, r0.xyzy, s1 <cube wrap linear point>
ciaaaaaaabaaapacadaaaaoeaeaaaaaaaaaaaaaaafaababb tex r1, v3, s0 <2d wrap linear point>
bcaaaaaaaaaaabacaaaaaakeacaaaaaaabaaaaoeaeaaaaaa dp3 r0.x, r0.xyzz, v1
beaaaaaaaaaaabacaaaaaaaaacaaaaaaaaaaaaaaaaaaaaaa abs r0.x, r0.x
bgaaaaaaaaaaabacaaaaaaaaacaaaaaaaaaaaaaaaaaaaaaa sat r0.x, r0.x
bfaaaaaaaaaaabacaaaaaaaaacaaaaaaaaaaaaaaaaaaaaaa neg r0.x, r0.x
abaaaaaaaaaaabacaaaaaaaaacaaaaaaaeaaaaffabaaaaaa add r0.x, r0.x, c4.y
alaaaaaaadaaapacaaaaaaaaacaaaaaaadaaaaaaabaaaaaa pow r3, r0.x, c3.x
aaaaaaaaaaaaabacadaaaaaaacaaaaaaaaaaaaaaaaaaaaaa mov r0.x, r3.x
adaaaaaaaaaaabacaaaaaaaaacaaaaaaacaaaaoeabaaaaaa mul r0.x, r0.x, c2
abaaaaaaadaaabacaaaaaaaaacaaaaaaabaaaaoeabaaaaaa add r3.x, r0.x, c1
bgaaaaaaadaaabacadaaaaaaacaaaaaaaaaaaaaaaaaaaaaa sat r3.x, r3.x
adaaaaaaacaaahacacaaaakeacaaaaaaadaaaaaaacaaaaaa mul r2.xyz, r2.xyzz, r3.x
abaaaaaaabaaahacacaaaakeacaaaaaaabaaaakeacaaaaaa add r1.xyz, r2.xyzz, r1.xyzz
adaaaaaaaaaaahacaaaaaaaaacaaaaaaacaaaakeacaaaaaa mul r0.xyz, r0.x, r2.xyzz
abaaaaaaaaaaahacaaaaaakeacaaaaaaabaaaakeacaaaaaa add r0.xyz, r0.xyzz, r1.xyzz
aaaaaaaaaaaaapadaaaaaaoeacaaaaaaaaaaaaaaaaaaaaaa mov o0, r0
"
}

SubProgram "opengl " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" }
Vector 0 [_WorldSpaceCameraPos]
Float 1 [_Reflection]
Float 2 [_FrezPow]
Float 3 [_FrezFalloff]
SetTexture 0 [_MainTex] 2D
SetTexture 1 [_Cube] CUBE
"!!ARBfp1.0
OPTION ARB_precision_hint_fastest;
# 22 ALU, 2 TEX
PARAM c[5] = { program.local[0..3],
		{ 2, 1 } };
TEMP R0;
TEMP R1;
TEMP R2;
ADD R0.xyz, -fragment.texcoord[0], c[0];
DP3 R1.x, R0, R0;
RSQ R1.x, R1.x;
DP3 R0.w, fragment.texcoord[1], fragment.texcoord[1];
MUL R0.xyz, R1.x, R0;
RSQ R0.w, R0.w;
MUL R1.xyz, R0.w, fragment.texcoord[1];
DP3 R0.w, R1, -R0;
MUL R1.xyz, R1, R0.w;
MAD R2.xyz, -R1, c[4].x, -R0;
DP3 R0.w, R2, fragment.texcoord[1];
ABS_SAT R0.w, R0;
ADD R0.w, -R0, c[4].y;
POW R0.w, R0.w, c[3].x;
MUL R0.w, R0, c[2].x;
ADD_SAT R1.w, R0, c[1].x;
MOV result.color.w, c[4].y;
TEX R0.xyz, R2, texture[1], CUBE;
TEX R1.xyz, fragment.texcoord[3], texture[0], 2D;
MUL R0.xyz, R0, R1.w;
ADD R1.xyz, R0, R1;
MAD result.color.xyz, R0.w, R0, R1;
END
# 22 instructions, 3 R-regs
"
}

SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" }
Vector 0 [_WorldSpaceCameraPos]
Float 1 [_Reflection]
Float 2 [_FrezPow]
Float 3 [_FrezFalloff]
SetTexture 0 [_MainTex] 2D
SetTexture 1 [_Cube] CUBE
"ps_2_0
; 24 ALU, 2 TEX
dcl_2d s0
dcl_cube s1
def c4, 2.00000000, 1.00000000, 0, 0
dcl t0.xyz
dcl t1.xyz
dcl t3.xy
add r2.xyz, -t0, c0
dp3 r1.x, r2, r2
dp3_pp r0.x, t1, t1
rsq r1.x, r1.x
mul r1.xyz, r1.x, r2
rsq_pp r0.x, r0.x
mul_pp r2.xyz, r0.x, t1
dp3_pp r0.x, r2, -r1
mul_pp r0.xyz, r2, r0.x
mad_pp r0.xyz, -r0, c4.x, -r1
mov_pp r0.w, c4.y
texld r2, r0, s1
texld r1, t3, s0
dp3_pp r0.x, r0, t1
abs_pp_sat r0.x, r0
add_pp r0.x, -r0, c4.y
pow_pp r3.x, r0.x, c3.x
mov_pp r0.x, r3.x
mul_pp r0.x, r0, c2
add_pp_sat r3.x, r0, c1
mul_pp r2.xyz, r2, r3.x
add_pp r1.xyz, r2, r1
mad_pp r0.xyz, r0.x, r2, r1
mov_pp oC0, r0
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

SubProgram "flash " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" }
Vector 0 [_WorldSpaceCameraPos]
Float 1 [_Reflection]
Float 2 [_FrezPow]
Float 3 [_FrezFalloff]
SetTexture 0 [_MainTex] 2D
SetTexture 1 [_Cube] CUBE
"agal_ps
c4 2.0 1.0 0.0 0.0
[bc]
bfaaaaaaacaaahacaaaaaaoeaeaaaaaaaaaaaaaaaaaaaaaa neg r2.xyz, v0
abaaaaaaacaaahacacaaaakeacaaaaaaaaaaaaoeabaaaaaa add r2.xyz, r2.xyzz, c0
bcaaaaaaabaaabacacaaaakeacaaaaaaacaaaakeacaaaaaa dp3 r1.x, r2.xyzz, r2.xyzz
bcaaaaaaaaaaabacabaaaaoeaeaaaaaaabaaaaoeaeaaaaaa dp3 r0.x, v1, v1
akaaaaaaabaaabacabaaaaaaacaaaaaaaaaaaaaaaaaaaaaa rsq r1.x, r1.x
adaaaaaaabaaahacabaaaaaaacaaaaaaacaaaakeacaaaaaa mul r1.xyz, r1.x, r2.xyzz
akaaaaaaaaaaabacaaaaaaaaacaaaaaaaaaaaaaaaaaaaaaa rsq r0.x, r0.x
adaaaaaaacaaahacaaaaaaaaacaaaaaaabaaaaoeaeaaaaaa mul r2.xyz, r0.x, v1
bfaaaaaaadaaahacabaaaakeacaaaaaaaaaaaaaaaaaaaaaa neg r3.xyz, r1.xyzz
bcaaaaaaaaaaabacacaaaakeacaaaaaaadaaaakeacaaaaaa dp3 r0.x, r2.xyzz, r3.xyzz
adaaaaaaaaaaahacacaaaakeacaaaaaaaaaaaaaaacaaaaaa mul r0.xyz, r2.xyzz, r0.x
bfaaaaaaaeaaahacaaaaaakeacaaaaaaaaaaaaaaaaaaaaaa neg r4.xyz, r0.xyzz
adaaaaaaaeaaahacaeaaaakeacaaaaaaaeaaaaaaabaaaaaa mul r4.xyz, r4.xyzz, c4.x
acaaaaaaaaaaahacaeaaaakeacaaaaaaabaaaakeacaaaaaa sub r0.xyz, r4.xyzz, r1.xyzz
aaaaaaaaaaaaaiacaeaaaaffabaaaaaaaaaaaaaaaaaaaaaa mov r0.w, c4.y
ciaaaaaaacaaapacaaaaaageacaaaaaaabaaaaaaafbababb tex r2, r0.xyzy, s1 <cube wrap linear point>
ciaaaaaaabaaapacadaaaaoeaeaaaaaaaaaaaaaaafaababb tex r1, v3, s0 <2d wrap linear point>
bcaaaaaaaaaaabacaaaaaakeacaaaaaaabaaaaoeaeaaaaaa dp3 r0.x, r0.xyzz, v1
beaaaaaaaaaaabacaaaaaaaaacaaaaaaaaaaaaaaaaaaaaaa abs r0.x, r0.x
bgaaaaaaaaaaabacaaaaaaaaacaaaaaaaaaaaaaaaaaaaaaa sat r0.x, r0.x
bfaaaaaaaaaaabacaaaaaaaaacaaaaaaaaaaaaaaaaaaaaaa neg r0.x, r0.x
abaaaaaaaaaaabacaaaaaaaaacaaaaaaaeaaaaffabaaaaaa add r0.x, r0.x, c4.y
alaaaaaaadaaapacaaaaaaaaacaaaaaaadaaaaaaabaaaaaa pow r3, r0.x, c3.x
aaaaaaaaaaaaabacadaaaaaaacaaaaaaaaaaaaaaaaaaaaaa mov r0.x, r3.x
adaaaaaaaaaaabacaaaaaaaaacaaaaaaacaaaaoeabaaaaaa mul r0.x, r0.x, c2
abaaaaaaadaaabacaaaaaaaaacaaaaaaabaaaaoeabaaaaaa add r3.x, r0.x, c1
bgaaaaaaadaaabacadaaaaaaacaaaaaaaaaaaaaaaaaaaaaa sat r3.x, r3.x
adaaaaaaacaaahacacaaaakeacaaaaaaadaaaaaaacaaaaaa mul r2.xyz, r2.xyzz, r3.x
abaaaaaaabaaahacacaaaakeacaaaaaaabaaaakeacaaaaaa add r1.xyz, r2.xyzz, r1.xyzz
adaaaaaaaaaaahacaaaaaaaaacaaaaaaacaaaakeacaaaaaa mul r0.xyz, r0.x, r2.xyzz
abaaaaaaaaaaahacaaaaaakeacaaaaaaabaaaakeacaaaaaa add r0.xyz, r0.xyzz, r1.xyzz
aaaaaaaaaaaaapadaaaaaaoeacaaaaaaaaaaaaaaaaaaaaaa mov o0, r0
"
}

SubProgram "opengl " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "SHADOWS_OFF" }
Vector 0 [_WorldSpaceCameraPos]
Float 1 [_Reflection]
Float 2 [_FrezPow]
Float 3 [_FrezFalloff]
SetTexture 0 [_MainTex] 2D
SetTexture 1 [_Cube] CUBE
"!!ARBfp1.0
OPTION ARB_precision_hint_fastest;
# 22 ALU, 2 TEX
PARAM c[5] = { program.local[0..3],
		{ 2, 1 } };
TEMP R0;
TEMP R1;
TEMP R2;
ADD R0.xyz, -fragment.texcoord[0], c[0];
DP3 R1.x, R0, R0;
RSQ R1.x, R1.x;
DP3 R0.w, fragment.texcoord[1], fragment.texcoord[1];
MUL R0.xyz, R1.x, R0;
RSQ R0.w, R0.w;
MUL R1.xyz, R0.w, fragment.texcoord[1];
DP3 R0.w, R1, -R0;
MUL R1.xyz, R1, R0.w;
MAD R2.xyz, -R1, c[4].x, -R0;
DP3 R0.w, R2, fragment.texcoord[1];
ABS_SAT R0.w, R0;
ADD R0.w, -R0, c[4].y;
POW R0.w, R0.w, c[3].x;
MUL R0.w, R0, c[2].x;
ADD_SAT R1.w, R0, c[1].x;
MOV result.color.w, c[4].y;
TEX R0.xyz, R2, texture[1], CUBE;
TEX R1.xyz, fragment.texcoord[3], texture[0], 2D;
MUL R0.xyz, R0, R1.w;
ADD R1.xyz, R0, R1;
MAD result.color.xyz, R0.w, R0, R1;
END
# 22 instructions, 3 R-regs
"
}

SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "SHADOWS_OFF" }
Vector 0 [_WorldSpaceCameraPos]
Float 1 [_Reflection]
Float 2 [_FrezPow]
Float 3 [_FrezFalloff]
SetTexture 0 [_MainTex] 2D
SetTexture 1 [_Cube] CUBE
"ps_2_0
; 24 ALU, 2 TEX
dcl_2d s0
dcl_cube s1
def c4, 2.00000000, 1.00000000, 0, 0
dcl t0.xyz
dcl t1.xyz
dcl t3.xy
add r2.xyz, -t0, c0
dp3 r1.x, r2, r2
dp3_pp r0.x, t1, t1
rsq r1.x, r1.x
mul r1.xyz, r1.x, r2
rsq_pp r0.x, r0.x
mul_pp r2.xyz, r0.x, t1
dp3_pp r0.x, r2, -r1
mul_pp r0.xyz, r2, r0.x
mad_pp r0.xyz, -r0, c4.x, -r1
mov_pp r0.w, c4.y
texld r2, r0, s1
texld r1, t3, s0
dp3_pp r0.x, r0, t1
abs_pp_sat r0.x, r0
add_pp r0.x, -r0, c4.y
pow_pp r3.x, r0.x, c3.x
mov_pp r0.x, r3.x
mul_pp r0.x, r0, c2
add_pp_sat r3.x, r0, c1
mul_pp r2.xyz, r2, r3.x
add_pp r1.xyz, r2, r1
mad_pp r0.xyz, r0.x, r2, r1
mov_pp oC0, r0
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

SubProgram "flash " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "SHADOWS_OFF" }
Vector 0 [_WorldSpaceCameraPos]
Float 1 [_Reflection]
Float 2 [_FrezPow]
Float 3 [_FrezFalloff]
SetTexture 0 [_MainTex] 2D
SetTexture 1 [_Cube] CUBE
"agal_ps
c4 2.0 1.0 0.0 0.0
[bc]
bfaaaaaaacaaahacaaaaaaoeaeaaaaaaaaaaaaaaaaaaaaaa neg r2.xyz, v0
abaaaaaaacaaahacacaaaakeacaaaaaaaaaaaaoeabaaaaaa add r2.xyz, r2.xyzz, c0
bcaaaaaaabaaabacacaaaakeacaaaaaaacaaaakeacaaaaaa dp3 r1.x, r2.xyzz, r2.xyzz
bcaaaaaaaaaaabacabaaaaoeaeaaaaaaabaaaaoeaeaaaaaa dp3 r0.x, v1, v1
akaaaaaaabaaabacabaaaaaaacaaaaaaaaaaaaaaaaaaaaaa rsq r1.x, r1.x
adaaaaaaabaaahacabaaaaaaacaaaaaaacaaaakeacaaaaaa mul r1.xyz, r1.x, r2.xyzz
akaaaaaaaaaaabacaaaaaaaaacaaaaaaaaaaaaaaaaaaaaaa rsq r0.x, r0.x
adaaaaaaacaaahacaaaaaaaaacaaaaaaabaaaaoeaeaaaaaa mul r2.xyz, r0.x, v1
bfaaaaaaadaaahacabaaaakeacaaaaaaaaaaaaaaaaaaaaaa neg r3.xyz, r1.xyzz
bcaaaaaaaaaaabacacaaaakeacaaaaaaadaaaakeacaaaaaa dp3 r0.x, r2.xyzz, r3.xyzz
adaaaaaaaaaaahacacaaaakeacaaaaaaaaaaaaaaacaaaaaa mul r0.xyz, r2.xyzz, r0.x
bfaaaaaaaeaaahacaaaaaakeacaaaaaaaaaaaaaaaaaaaaaa neg r4.xyz, r0.xyzz
adaaaaaaaeaaahacaeaaaakeacaaaaaaaeaaaaaaabaaaaaa mul r4.xyz, r4.xyzz, c4.x
acaaaaaaaaaaahacaeaaaakeacaaaaaaabaaaakeacaaaaaa sub r0.xyz, r4.xyzz, r1.xyzz
aaaaaaaaaaaaaiacaeaaaaffabaaaaaaaaaaaaaaaaaaaaaa mov r0.w, c4.y
ciaaaaaaacaaapacaaaaaageacaaaaaaabaaaaaaafbababb tex r2, r0.xyzy, s1 <cube wrap linear point>
ciaaaaaaabaaapacadaaaaoeaeaaaaaaaaaaaaaaafaababb tex r1, v3, s0 <2d wrap linear point>
bcaaaaaaaaaaabacaaaaaakeacaaaaaaabaaaaoeaeaaaaaa dp3 r0.x, r0.xyzz, v1
beaaaaaaaaaaabacaaaaaaaaacaaaaaaaaaaaaaaaaaaaaaa abs r0.x, r0.x
bgaaaaaaaaaaabacaaaaaaaaacaaaaaaaaaaaaaaaaaaaaaa sat r0.x, r0.x
bfaaaaaaaaaaabacaaaaaaaaacaaaaaaaaaaaaaaaaaaaaaa neg r0.x, r0.x
abaaaaaaaaaaabacaaaaaaaaacaaaaaaaeaaaaffabaaaaaa add r0.x, r0.x, c4.y
alaaaaaaadaaapacaaaaaaaaacaaaaaaadaaaaaaabaaaaaa pow r3, r0.x, c3.x
aaaaaaaaaaaaabacadaaaaaaacaaaaaaaaaaaaaaaaaaaaaa mov r0.x, r3.x
adaaaaaaaaaaabacaaaaaaaaacaaaaaaacaaaaoeabaaaaaa mul r0.x, r0.x, c2
abaaaaaaadaaabacaaaaaaaaacaaaaaaabaaaaoeabaaaaaa add r3.x, r0.x, c1
bgaaaaaaadaaabacadaaaaaaacaaaaaaaaaaaaaaaaaaaaaa sat r3.x, r3.x
adaaaaaaacaaahacacaaaakeacaaaaaaadaaaaaaacaaaaaa mul r2.xyz, r2.xyzz, r3.x
abaaaaaaabaaahacacaaaakeacaaaaaaabaaaakeacaaaaaa add r1.xyz, r2.xyzz, r1.xyzz
adaaaaaaaaaaahacaaaaaaaaacaaaaaaacaaaakeacaaaaaa mul r0.xyz, r0.x, r2.xyzz
abaaaaaaaaaaahacaaaaaakeacaaaaaaabaaaakeacaaaaaa add r0.xyz, r0.xyzz, r1.xyzz
aaaaaaaaaaaaapadaaaaaaoeacaaaaaaaaaaaaaaaaaaaaaa mov o0, r0
"
}

SubProgram "opengl " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" }
Vector 0 [_WorldSpaceCameraPos]
Float 1 [_Reflection]
Float 2 [_FrezPow]
Float 3 [_FrezFalloff]
SetTexture 0 [_MainTex] 2D
SetTexture 1 [_Cube] CUBE
"!!ARBfp1.0
OPTION ARB_precision_hint_fastest;
# 22 ALU, 2 TEX
PARAM c[5] = { program.local[0..3],
		{ 2, 1 } };
TEMP R0;
TEMP R1;
TEMP R2;
ADD R0.xyz, -fragment.texcoord[0], c[0];
DP3 R1.x, R0, R0;
RSQ R1.x, R1.x;
DP3 R0.w, fragment.texcoord[1], fragment.texcoord[1];
MUL R0.xyz, R1.x, R0;
RSQ R0.w, R0.w;
MUL R1.xyz, R0.w, fragment.texcoord[1];
DP3 R0.w, R1, -R0;
MUL R1.xyz, R1, R0.w;
MAD R2.xyz, -R1, c[4].x, -R0;
DP3 R0.w, R2, fragment.texcoord[1];
ABS_SAT R0.w, R0;
ADD R0.w, -R0, c[4].y;
POW R0.w, R0.w, c[3].x;
MUL R0.w, R0, c[2].x;
ADD_SAT R1.w, R0, c[1].x;
MOV result.color.w, c[4].y;
TEX R0.xyz, R2, texture[1], CUBE;
TEX R1.xyz, fragment.texcoord[3], texture[0], 2D;
MUL R0.xyz, R0, R1.w;
ADD R1.xyz, R0, R1;
MAD result.color.xyz, R0.w, R0, R1;
END
# 22 instructions, 3 R-regs
"
}

SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" }
Vector 0 [_WorldSpaceCameraPos]
Float 1 [_Reflection]
Float 2 [_FrezPow]
Float 3 [_FrezFalloff]
SetTexture 0 [_MainTex] 2D
SetTexture 1 [_Cube] CUBE
"ps_2_0
; 24 ALU, 2 TEX
dcl_2d s0
dcl_cube s1
def c4, 2.00000000, 1.00000000, 0, 0
dcl t0.xyz
dcl t1.xyz
dcl t3.xy
add r2.xyz, -t0, c0
dp3 r1.x, r2, r2
dp3_pp r0.x, t1, t1
rsq r1.x, r1.x
mul r1.xyz, r1.x, r2
rsq_pp r0.x, r0.x
mul_pp r2.xyz, r0.x, t1
dp3_pp r0.x, r2, -r1
mul_pp r0.xyz, r2, r0.x
mad_pp r0.xyz, -r0, c4.x, -r1
mov_pp r0.w, c4.y
texld r2, r0, s1
texld r1, t3, s0
dp3_pp r0.x, r0, t1
abs_pp_sat r0.x, r0
add_pp r0.x, -r0, c4.y
pow_pp r3.x, r0.x, c3.x
mov_pp r0.x, r3.x
mul_pp r0.x, r0, c2
add_pp_sat r3.x, r0, c1
mul_pp r2.xyz, r2, r3.x
add_pp r1.xyz, r2, r1
mad_pp r0.xyz, r0.x, r2, r1
mov_pp oC0, r0
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

SubProgram "flash " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" }
Vector 0 [_WorldSpaceCameraPos]
Float 1 [_Reflection]
Float 2 [_FrezPow]
Float 3 [_FrezFalloff]
SetTexture 0 [_MainTex] 2D
SetTexture 1 [_Cube] CUBE
"agal_ps
c4 2.0 1.0 0.0 0.0
[bc]
bfaaaaaaacaaahacaaaaaaoeaeaaaaaaaaaaaaaaaaaaaaaa neg r2.xyz, v0
abaaaaaaacaaahacacaaaakeacaaaaaaaaaaaaoeabaaaaaa add r2.xyz, r2.xyzz, c0
bcaaaaaaabaaabacacaaaakeacaaaaaaacaaaakeacaaaaaa dp3 r1.x, r2.xyzz, r2.xyzz
bcaaaaaaaaaaabacabaaaaoeaeaaaaaaabaaaaoeaeaaaaaa dp3 r0.x, v1, v1
akaaaaaaabaaabacabaaaaaaacaaaaaaaaaaaaaaaaaaaaaa rsq r1.x, r1.x
adaaaaaaabaaahacabaaaaaaacaaaaaaacaaaakeacaaaaaa mul r1.xyz, r1.x, r2.xyzz
akaaaaaaaaaaabacaaaaaaaaacaaaaaaaaaaaaaaaaaaaaaa rsq r0.x, r0.x
adaaaaaaacaaahacaaaaaaaaacaaaaaaabaaaaoeaeaaaaaa mul r2.xyz, r0.x, v1
bfaaaaaaadaaahacabaaaakeacaaaaaaaaaaaaaaaaaaaaaa neg r3.xyz, r1.xyzz
bcaaaaaaaaaaabacacaaaakeacaaaaaaadaaaakeacaaaaaa dp3 r0.x, r2.xyzz, r3.xyzz
adaaaaaaaaaaahacacaaaakeacaaaaaaaaaaaaaaacaaaaaa mul r0.xyz, r2.xyzz, r0.x
bfaaaaaaaeaaahacaaaaaakeacaaaaaaaaaaaaaaaaaaaaaa neg r4.xyz, r0.xyzz
adaaaaaaaeaaahacaeaaaakeacaaaaaaaeaaaaaaabaaaaaa mul r4.xyz, r4.xyzz, c4.x
acaaaaaaaaaaahacaeaaaakeacaaaaaaabaaaakeacaaaaaa sub r0.xyz, r4.xyzz, r1.xyzz
aaaaaaaaaaaaaiacaeaaaaffabaaaaaaaaaaaaaaaaaaaaaa mov r0.w, c4.y
ciaaaaaaacaaapacaaaaaageacaaaaaaabaaaaaaafbababb tex r2, r0.xyzy, s1 <cube wrap linear point>
ciaaaaaaabaaapacadaaaaoeaeaaaaaaaaaaaaaaafaababb tex r1, v3, s0 <2d wrap linear point>
bcaaaaaaaaaaabacaaaaaakeacaaaaaaabaaaaoeaeaaaaaa dp3 r0.x, r0.xyzz, v1
beaaaaaaaaaaabacaaaaaaaaacaaaaaaaaaaaaaaaaaaaaaa abs r0.x, r0.x
bgaaaaaaaaaaabacaaaaaaaaacaaaaaaaaaaaaaaaaaaaaaa sat r0.x, r0.x
bfaaaaaaaaaaabacaaaaaaaaacaaaaaaaaaaaaaaaaaaaaaa neg r0.x, r0.x
abaaaaaaaaaaabacaaaaaaaaacaaaaaaaeaaaaffabaaaaaa add r0.x, r0.x, c4.y
alaaaaaaadaaapacaaaaaaaaacaaaaaaadaaaaaaabaaaaaa pow r3, r0.x, c3.x
aaaaaaaaaaaaabacadaaaaaaacaaaaaaaaaaaaaaaaaaaaaa mov r0.x, r3.x
adaaaaaaaaaaabacaaaaaaaaacaaaaaaacaaaaoeabaaaaaa mul r0.x, r0.x, c2
abaaaaaaadaaabacaaaaaaaaacaaaaaaabaaaaoeabaaaaaa add r3.x, r0.x, c1
bgaaaaaaadaaabacadaaaaaaacaaaaaaaaaaaaaaaaaaaaaa sat r3.x, r3.x
adaaaaaaacaaahacacaaaakeacaaaaaaadaaaaaaacaaaaaa mul r2.xyz, r2.xyzz, r3.x
abaaaaaaabaaahacacaaaakeacaaaaaaabaaaakeacaaaaaa add r1.xyz, r2.xyzz, r1.xyzz
adaaaaaaaaaaahacaaaaaaaaacaaaaaaacaaaakeacaaaaaa mul r0.xyz, r0.x, r2.xyzz
abaaaaaaaaaaahacaaaaaakeacaaaaaaabaaaakeacaaaaaa add r0.xyz, r0.xyzz, r1.xyzz
aaaaaaaaaaaaapadaaaaaaoeacaaaaaaaaaaaaaaaaaaaaaa mov o0, r0
"
}

SubProgram "opengl " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" }
Vector 0 [_WorldSpaceCameraPos]
Float 1 [_Reflection]
Float 2 [_FrezPow]
Float 3 [_FrezFalloff]
SetTexture 0 [_MainTex] 2D
SetTexture 1 [_Cube] CUBE
"!!ARBfp1.0
OPTION ARB_precision_hint_fastest;
# 22 ALU, 2 TEX
PARAM c[5] = { program.local[0..3],
		{ 2, 1 } };
TEMP R0;
TEMP R1;
TEMP R2;
ADD R0.xyz, -fragment.texcoord[0], c[0];
DP3 R1.x, R0, R0;
RSQ R1.x, R1.x;
DP3 R0.w, fragment.texcoord[1], fragment.texcoord[1];
MUL R0.xyz, R1.x, R0;
RSQ R0.w, R0.w;
MUL R1.xyz, R0.w, fragment.texcoord[1];
DP3 R0.w, R1, -R0;
MUL R1.xyz, R1, R0.w;
MAD R2.xyz, -R1, c[4].x, -R0;
DP3 R0.w, R2, fragment.texcoord[1];
ABS_SAT R0.w, R0;
ADD R0.w, -R0, c[4].y;
POW R0.w, R0.w, c[3].x;
MUL R0.w, R0, c[2].x;
ADD_SAT R1.w, R0, c[1].x;
MOV result.color.w, c[4].y;
TEX R0.xyz, R2, texture[1], CUBE;
TEX R1.xyz, fragment.texcoord[3], texture[0], 2D;
MUL R0.xyz, R0, R1.w;
ADD R1.xyz, R0, R1;
MAD result.color.xyz, R0.w, R0, R1;
END
# 22 instructions, 3 R-regs
"
}

SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" }
Vector 0 [_WorldSpaceCameraPos]
Float 1 [_Reflection]
Float 2 [_FrezPow]
Float 3 [_FrezFalloff]
SetTexture 0 [_MainTex] 2D
SetTexture 1 [_Cube] CUBE
"ps_2_0
; 24 ALU, 2 TEX
dcl_2d s0
dcl_cube s1
def c4, 2.00000000, 1.00000000, 0, 0
dcl t0.xyz
dcl t1.xyz
dcl t3.xy
add r2.xyz, -t0, c0
dp3 r1.x, r2, r2
dp3_pp r0.x, t1, t1
rsq r1.x, r1.x
mul r1.xyz, r1.x, r2
rsq_pp r0.x, r0.x
mul_pp r2.xyz, r0.x, t1
dp3_pp r0.x, r2, -r1
mul_pp r0.xyz, r2, r0.x
mad_pp r0.xyz, -r0, c4.x, -r1
mov_pp r0.w, c4.y
texld r2, r0, s1
texld r1, t3, s0
dp3_pp r0.x, r0, t1
abs_pp_sat r0.x, r0
add_pp r0.x, -r0, c4.y
pow_pp r3.x, r0.x, c3.x
mov_pp r0.x, r3.x
mul_pp r0.x, r0, c2
add_pp_sat r3.x, r0, c1
mul_pp r2.xyz, r2, r3.x
add_pp r1.xyz, r2, r1
mad_pp r0.xyz, r0.x, r2, r1
mov_pp oC0, r0
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

SubProgram "flash " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" }
Vector 0 [_WorldSpaceCameraPos]
Float 1 [_Reflection]
Float 2 [_FrezPow]
Float 3 [_FrezFalloff]
SetTexture 0 [_MainTex] 2D
SetTexture 1 [_Cube] CUBE
"agal_ps
c4 2.0 1.0 0.0 0.0
[bc]
bfaaaaaaacaaahacaaaaaaoeaeaaaaaaaaaaaaaaaaaaaaaa neg r2.xyz, v0
abaaaaaaacaaahacacaaaakeacaaaaaaaaaaaaoeabaaaaaa add r2.xyz, r2.xyzz, c0
bcaaaaaaabaaabacacaaaakeacaaaaaaacaaaakeacaaaaaa dp3 r1.x, r2.xyzz, r2.xyzz
bcaaaaaaaaaaabacabaaaaoeaeaaaaaaabaaaaoeaeaaaaaa dp3 r0.x, v1, v1
akaaaaaaabaaabacabaaaaaaacaaaaaaaaaaaaaaaaaaaaaa rsq r1.x, r1.x
adaaaaaaabaaahacabaaaaaaacaaaaaaacaaaakeacaaaaaa mul r1.xyz, r1.x, r2.xyzz
akaaaaaaaaaaabacaaaaaaaaacaaaaaaaaaaaaaaaaaaaaaa rsq r0.x, r0.x
adaaaaaaacaaahacaaaaaaaaacaaaaaaabaaaaoeaeaaaaaa mul r2.xyz, r0.x, v1
bfaaaaaaadaaahacabaaaakeacaaaaaaaaaaaaaaaaaaaaaa neg r3.xyz, r1.xyzz
bcaaaaaaaaaaabacacaaaakeacaaaaaaadaaaakeacaaaaaa dp3 r0.x, r2.xyzz, r3.xyzz
adaaaaaaaaaaahacacaaaakeacaaaaaaaaaaaaaaacaaaaaa mul r0.xyz, r2.xyzz, r0.x
bfaaaaaaaeaaahacaaaaaakeacaaaaaaaaaaaaaaaaaaaaaa neg r4.xyz, r0.xyzz
adaaaaaaaeaaahacaeaaaakeacaaaaaaaeaaaaaaabaaaaaa mul r4.xyz, r4.xyzz, c4.x
acaaaaaaaaaaahacaeaaaakeacaaaaaaabaaaakeacaaaaaa sub r0.xyz, r4.xyzz, r1.xyzz
aaaaaaaaaaaaaiacaeaaaaffabaaaaaaaaaaaaaaaaaaaaaa mov r0.w, c4.y
ciaaaaaaacaaapacaaaaaageacaaaaaaabaaaaaaafbababb tex r2, r0.xyzy, s1 <cube wrap linear point>
ciaaaaaaabaaapacadaaaaoeaeaaaaaaaaaaaaaaafaababb tex r1, v3, s0 <2d wrap linear point>
bcaaaaaaaaaaabacaaaaaakeacaaaaaaabaaaaoeaeaaaaaa dp3 r0.x, r0.xyzz, v1
beaaaaaaaaaaabacaaaaaaaaacaaaaaaaaaaaaaaaaaaaaaa abs r0.x, r0.x
bgaaaaaaaaaaabacaaaaaaaaacaaaaaaaaaaaaaaaaaaaaaa sat r0.x, r0.x
bfaaaaaaaaaaabacaaaaaaaaacaaaaaaaaaaaaaaaaaaaaaa neg r0.x, r0.x
abaaaaaaaaaaabacaaaaaaaaacaaaaaaaeaaaaffabaaaaaa add r0.x, r0.x, c4.y
alaaaaaaadaaapacaaaaaaaaacaaaaaaadaaaaaaabaaaaaa pow r3, r0.x, c3.x
aaaaaaaaaaaaabacadaaaaaaacaaaaaaaaaaaaaaaaaaaaaa mov r0.x, r3.x
adaaaaaaaaaaabacaaaaaaaaacaaaaaaacaaaaoeabaaaaaa mul r0.x, r0.x, c2
abaaaaaaadaaabacaaaaaaaaacaaaaaaabaaaaoeabaaaaaa add r3.x, r0.x, c1
bgaaaaaaadaaabacadaaaaaaacaaaaaaaaaaaaaaaaaaaaaa sat r3.x, r3.x
adaaaaaaacaaahacacaaaakeacaaaaaaadaaaaaaacaaaaaa mul r2.xyz, r2.xyzz, r3.x
abaaaaaaabaaahacacaaaakeacaaaaaaabaaaakeacaaaaaa add r1.xyz, r2.xyzz, r1.xyzz
adaaaaaaaaaaahacaaaaaaaaacaaaaaaacaaaakeacaaaaaa mul r0.xyz, r0.x, r2.xyzz
abaaaaaaaaaaahacaaaaaakeacaaaaaaabaaaakeacaaaaaa add r0.xyz, r0.xyzz, r1.xyzz
aaaaaaaaaaaaapadaaaaaaoeacaaaaaaaaaaaaaaaaaaaaaa mov o0, r0
"
}

SubProgram "opengl " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "SHADOWS_SCREEN" }
Vector 0 [_WorldSpaceCameraPos]
Float 1 [_Reflection]
Float 2 [_FrezPow]
Float 3 [_FrezFalloff]
SetTexture 0 [_MainTex] 2D
SetTexture 1 [_Cube] CUBE
"!!ARBfp1.0
OPTION ARB_precision_hint_fastest;
# 22 ALU, 2 TEX
PARAM c[5] = { program.local[0..3],
		{ 2, 1 } };
TEMP R0;
TEMP R1;
TEMP R2;
ADD R0.xyz, -fragment.texcoord[0], c[0];
DP3 R1.x, R0, R0;
RSQ R1.x, R1.x;
DP3 R0.w, fragment.texcoord[1], fragment.texcoord[1];
MUL R0.xyz, R1.x, R0;
RSQ R0.w, R0.w;
MUL R1.xyz, R0.w, fragment.texcoord[1];
DP3 R0.w, R1, -R0;
MUL R1.xyz, R1, R0.w;
MAD R2.xyz, -R1, c[4].x, -R0;
DP3 R0.w, R2, fragment.texcoord[1];
ABS_SAT R0.w, R0;
ADD R0.w, -R0, c[4].y;
POW R0.w, R0.w, c[3].x;
MUL R0.w, R0, c[2].x;
ADD_SAT R1.w, R0, c[1].x;
MOV result.color.w, c[4].y;
TEX R0.xyz, R2, texture[1], CUBE;
TEX R1.xyz, fragment.texcoord[3], texture[0], 2D;
MUL R0.xyz, R0, R1.w;
ADD R1.xyz, R0, R1;
MAD result.color.xyz, R0.w, R0, R1;
END
# 22 instructions, 3 R-regs
"
}

SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "SHADOWS_SCREEN" }
Vector 0 [_WorldSpaceCameraPos]
Float 1 [_Reflection]
Float 2 [_FrezPow]
Float 3 [_FrezFalloff]
SetTexture 0 [_MainTex] 2D
SetTexture 1 [_Cube] CUBE
"ps_2_0
; 24 ALU, 2 TEX
dcl_2d s0
dcl_cube s1
def c4, 2.00000000, 1.00000000, 0, 0
dcl t0.xyz
dcl t1.xyz
dcl t3.xy
add r2.xyz, -t0, c0
dp3 r1.x, r2, r2
dp3_pp r0.x, t1, t1
rsq r1.x, r1.x
mul r1.xyz, r1.x, r2
rsq_pp r0.x, r0.x
mul_pp r2.xyz, r0.x, t1
dp3_pp r0.x, r2, -r1
mul_pp r0.xyz, r2, r0.x
mad_pp r0.xyz, -r0, c4.x, -r1
mov_pp r0.w, c4.y
texld r2, r0, s1
texld r1, t3, s0
dp3_pp r0.x, r0, t1
abs_pp_sat r0.x, r0
add_pp r0.x, -r0, c4.y
pow_pp r3.x, r0.x, c3.x
mov_pp r0.x, r3.x
mul_pp r0.x, r0, c2
add_pp_sat r3.x, r0, c1
mul_pp r2.xyz, r2, r3.x
add_pp r1.xyz, r2, r1
mad_pp r0.xyz, r0.x, r2, r1
mov_pp oC0, r0
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

SubProgram "flash " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "SHADOWS_SCREEN" }
Vector 0 [_WorldSpaceCameraPos]
Float 1 [_Reflection]
Float 2 [_FrezPow]
Float 3 [_FrezFalloff]
SetTexture 0 [_MainTex] 2D
SetTexture 1 [_Cube] CUBE
"agal_ps
c4 2.0 1.0 0.0 0.0
[bc]
bfaaaaaaacaaahacaaaaaaoeaeaaaaaaaaaaaaaaaaaaaaaa neg r2.xyz, v0
abaaaaaaacaaahacacaaaakeacaaaaaaaaaaaaoeabaaaaaa add r2.xyz, r2.xyzz, c0
bcaaaaaaabaaabacacaaaakeacaaaaaaacaaaakeacaaaaaa dp3 r1.x, r2.xyzz, r2.xyzz
bcaaaaaaaaaaabacabaaaaoeaeaaaaaaabaaaaoeaeaaaaaa dp3 r0.x, v1, v1
akaaaaaaabaaabacabaaaaaaacaaaaaaaaaaaaaaaaaaaaaa rsq r1.x, r1.x
adaaaaaaabaaahacabaaaaaaacaaaaaaacaaaakeacaaaaaa mul r1.xyz, r1.x, r2.xyzz
akaaaaaaaaaaabacaaaaaaaaacaaaaaaaaaaaaaaaaaaaaaa rsq r0.x, r0.x
adaaaaaaacaaahacaaaaaaaaacaaaaaaabaaaaoeaeaaaaaa mul r2.xyz, r0.x, v1
bfaaaaaaadaaahacabaaaakeacaaaaaaaaaaaaaaaaaaaaaa neg r3.xyz, r1.xyzz
bcaaaaaaaaaaabacacaaaakeacaaaaaaadaaaakeacaaaaaa dp3 r0.x, r2.xyzz, r3.xyzz
adaaaaaaaaaaahacacaaaakeacaaaaaaaaaaaaaaacaaaaaa mul r0.xyz, r2.xyzz, r0.x
bfaaaaaaaeaaahacaaaaaakeacaaaaaaaaaaaaaaaaaaaaaa neg r4.xyz, r0.xyzz
adaaaaaaaeaaahacaeaaaakeacaaaaaaaeaaaaaaabaaaaaa mul r4.xyz, r4.xyzz, c4.x
acaaaaaaaaaaahacaeaaaakeacaaaaaaabaaaakeacaaaaaa sub r0.xyz, r4.xyzz, r1.xyzz
aaaaaaaaaaaaaiacaeaaaaffabaaaaaaaaaaaaaaaaaaaaaa mov r0.w, c4.y
ciaaaaaaacaaapacaaaaaageacaaaaaaabaaaaaaafbababb tex r2, r0.xyzy, s1 <cube wrap linear point>
ciaaaaaaabaaapacadaaaaoeaeaaaaaaaaaaaaaaafaababb tex r1, v3, s0 <2d wrap linear point>
bcaaaaaaaaaaabacaaaaaakeacaaaaaaabaaaaoeaeaaaaaa dp3 r0.x, r0.xyzz, v1
beaaaaaaaaaaabacaaaaaaaaacaaaaaaaaaaaaaaaaaaaaaa abs r0.x, r0.x
bgaaaaaaaaaaabacaaaaaaaaacaaaaaaaaaaaaaaaaaaaaaa sat r0.x, r0.x
bfaaaaaaaaaaabacaaaaaaaaacaaaaaaaaaaaaaaaaaaaaaa neg r0.x, r0.x
abaaaaaaaaaaabacaaaaaaaaacaaaaaaaeaaaaffabaaaaaa add r0.x, r0.x, c4.y
alaaaaaaadaaapacaaaaaaaaacaaaaaaadaaaaaaabaaaaaa pow r3, r0.x, c3.x
aaaaaaaaaaaaabacadaaaaaaacaaaaaaaaaaaaaaaaaaaaaa mov r0.x, r3.x
adaaaaaaaaaaabacaaaaaaaaacaaaaaaacaaaaoeabaaaaaa mul r0.x, r0.x, c2
abaaaaaaadaaabacaaaaaaaaacaaaaaaabaaaaoeabaaaaaa add r3.x, r0.x, c1
bgaaaaaaadaaabacadaaaaaaacaaaaaaaaaaaaaaaaaaaaaa sat r3.x, r3.x
adaaaaaaacaaahacacaaaakeacaaaaaaadaaaaaaacaaaaaa mul r2.xyz, r2.xyzz, r3.x
abaaaaaaabaaahacacaaaakeacaaaaaaabaaaakeacaaaaaa add r1.xyz, r2.xyzz, r1.xyzz
adaaaaaaaaaaahacaaaaaaaaacaaaaaaacaaaakeacaaaaaa mul r0.xyz, r0.x, r2.xyzz
abaaaaaaaaaaahacaaaaaakeacaaaaaaabaaaakeacaaaaaa add r0.xyz, r0.xyzz, r1.xyzz
aaaaaaaaaaaaapadaaaaaaoeacaaaaaaaaaaaaaaaaaaaaaa mov o0, r0
"
}

}

#LINE 90

      }
 }

}