﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Curve/Unlit Textured outlined"
{
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_Outline ("outline strength", Range(0.0,10)) = 0.01
		_OutlineColor ("Outline color", Color) = (0,0,0,0)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass {
			Cull Front

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata {
				float4 vertex : POSITION;
				float4 normal : NORMAL;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
			};

        	//uniform properties adjustable by controller CS script
	        uniform half3 _CurveOrigin; // origin to start curve
	        uniform half _Curvature; // curve power
	        uniform fixed3 _Scale; // flatt scale
	        uniform half _FlatMargin; // flat margin from origin

       		//standard properties
			sampler2D _MainTex;
			float4 _MainTex_ST;
	        fixed _Outline;
	        fixed4 _OutlineColor;
			
			v2f vert (appdata v) {
	        	half4 vpos = v.vertex;
	        	float3 norm = normalize(v.normal);
	        	vpos.xyz += norm * _Outline;
	        	v.vertex = vpos;

				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target {
				return _OutlineColor;
			}
			ENDCG
		}

		Pass {

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata {
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f {
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

        	//uniform properties adjustable by controller CS script
	        uniform half3 _CurveOrigin; // origin to start curve
	        uniform half _Curvature; // curve power
	        uniform fixed3 _Scale; // flatt scale
	        uniform half _FlatMargin; // flat margin from origin

       		//standard properties
			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata v) {
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target {
				fixed4 col = tex2D(_MainTex, i.uv);
				return col;
			}
			ENDCG
		}
	}
}
