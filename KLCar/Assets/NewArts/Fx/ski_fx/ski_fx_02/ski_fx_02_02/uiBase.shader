Shader "Custom/uiBase" {
 
     Properties {
       _Alpha("Alpha",Range(0,1))=1
           _MainTex ("Base (RGB)", 2D) = "white" { }
      }
     SubShader {
      Tags { "Queue" = "Transparent" }
         Pass {
               
          Blend SrcAlpha OneMinusSrcAlpha
 
             Material {
                 Diffuse [_Color]
             }
           
                    Lighting On

             SetTexture [_MainTex]{
         
                constantColor (1,1,1,[_Alpha])
                combine constant *texture             
             }
         }
     }
 }
