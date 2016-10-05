// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/Multicolor"
{
	Properties
	{
		_Color1 ("Color 1", Color) = (0,0,0,0) 
		_Color2 ("Color 2", Color) = (0,0,0,0)
		_Color3 ("Color 3", Color) = (0,0,0,0) 
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float4 worldPos : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			float4 _Color1;
			float4 _Color2;
			float4 _Color3;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.worldPos = mul(unity_ObjectToWorld,v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = lerp(_Color1,_Color2,(i.worldPos.y+0.5)/0.5);
				col = lerp(_Color2,_Color3,i.worldPos.y/0.5);
				return col;
			}
			ENDCG
		}
	}
}
