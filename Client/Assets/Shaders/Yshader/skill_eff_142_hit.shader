// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.28 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.28;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:33484,y:32801,varname:node_3138,prsc:2|emission-3690-OUT,alpha-6639-OUT;n:type:ShaderForge.SFN_Color,id:7241,x:32363,y:32976,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.5051723,c3:0.125,c4:1;n:type:ShaderForge.SFN_Tex2d,id:3717,x:32379,y:32773,ptovrint:False,ptlb:node_3717,ptin:_node_3717,varname:node_3717,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:b754b31a0fd6fc74e8910c6be2261bed,ntxv:0,isnm:False|UVIN-6772-OUT;n:type:ShaderForge.SFN_Multiply,id:8840,x:32595,y:32739,varname:node_8840,prsc:2|A-3717-RGB,B-7241-RGB;n:type:ShaderForge.SFN_Tex2d,id:9576,x:32506,y:33409,ptovrint:False,ptlb:node_9576,ptin:_node_9576,varname:node_9576,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:019c69eabd7d40f4aa71d30451e5d588,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:8316,x:32506,y:33248,ptovrint:False,ptlb:node_8316,ptin:_node_8316,varname:node_8316,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:0b38cfbf40d32fb44bc9fea02c1f2861,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:5053,x:32773,y:33311,varname:node_5053,prsc:2|A-8316-R,B-9576-R;n:type:ShaderForge.SFN_Multiply,id:5860,x:32820,y:32739,varname:node_5860,prsc:2|A-8840-OUT,B-6036-OUT;n:type:ShaderForge.SFN_Slider,id:6036,x:32548,y:33049,ptovrint:False,ptlb:light,ptin:_light,varname:node_6036,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.937719,max:10;n:type:ShaderForge.SFN_VertexColor,id:5263,x:32846,y:33095,varname:node_5263,prsc:2;n:type:ShaderForge.SFN_Multiply,id:3690,x:33138,y:32938,varname:node_3690,prsc:2|A-5860-OUT,B-5053-OUT,C-5263-RGB;n:type:ShaderForge.SFN_Multiply,id:6639,x:33226,y:33168,varname:node_6639,prsc:2|A-5263-A,B-5053-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9765,x:31091,y:32844,ptovrint:False,ptlb:U,ptin:_U,varname:node_9765,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:9799,x:31091,y:32960,ptovrint:False,ptlb:V,ptin:_V,varname:node_9799,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Time,id:3526,x:31091,y:32671,varname:node_3526,prsc:2;n:type:ShaderForge.SFN_Multiply,id:5889,x:31311,y:32688,varname:node_5889,prsc:2|A-3526-T,B-9765-OUT;n:type:ShaderForge.SFN_Multiply,id:706,x:31330,y:32987,varname:node_706,prsc:2|A-3526-T,B-9799-OUT;n:type:ShaderForge.SFN_TexCoord,id:1489,x:31311,y:32823,varname:node_1489,prsc:2,uv:0;n:type:ShaderForge.SFN_Add,id:2968,x:31524,y:32702,varname:node_2968,prsc:2|A-5889-OUT,B-1489-U;n:type:ShaderForge.SFN_Add,id:2501,x:31524,y:32942,varname:node_2501,prsc:2|A-1489-V,B-706-OUT;n:type:ShaderForge.SFN_Append,id:6772,x:31720,y:32806,varname:node_6772,prsc:2|A-2968-OUT,B-2501-OUT;proporder:7241-3717-6036-9576-8316-9765-9799;pass:END;sub:END;*/

Shader "Y/skill_eff_142_hit" {
    Properties {
        _Color ("Color", Color) = (1,0.5051723,0.125,1)
        _node_3717 ("node_3717", 2D) = "white" {}
        _light ("light", Range(0, 10)) = 0.937719
        _node_9576 ("node_9576", 2D) = "white" {}
        _node_8316 ("node_8316", 2D) = "white" {}
        _U ("U", Float ) = 0
        _V ("V", Float ) = 0
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            //#pragma multi_compile_fwdbase
            //#pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            //#pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float4 _Color;
            uniform sampler2D _node_3717; uniform float4 _node_3717_ST;
            uniform sampler2D _node_9576; uniform float4 _node_9576_ST;
            uniform sampler2D _node_8316; uniform float4 _node_8316_ST;
            uniform float _light;
            uniform float _U;
            uniform float _V;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos(v.vertex );
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
////// Lighting:
////// Emissive:
                float4 node_3526 = _Time + _TimeEditor;
                float2 node_6772 = float2(((node_3526.g*_U)+i.uv0.r),(i.uv0.g+(node_3526.g*_V)));
                float4 _node_3717_var = tex2D(_node_3717,TRANSFORM_TEX(node_6772, _node_3717));
                float4 _node_8316_var = tex2D(_node_8316,TRANSFORM_TEX(i.uv0, _node_8316));
                float4 _node_9576_var = tex2D(_node_9576,TRANSFORM_TEX(i.uv0, _node_9576));
                float node_5053 = (_node_8316_var.r*_node_9576_var.r);
                float3 emissive = (((_node_3717_var.rgb*_Color.rgb)*_light)*node_5053*i.vertexColor.rgb);
                float3 finalColor = emissive;
                return fixed4(finalColor,(i.vertexColor.a*node_5053));
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
