Shader "UI Effects/TileSelect"
{
	Properties
	{
		_Color("Tint", Color) = (1,1,1,1)
		[NoScaleOffset]
		_MainTex("Texture", 2D) = "white"
	}
	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
		}

		Pass
		{
			Blend SrcAlpha One

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				return o;
			}

			sampler2D _MainTex;
			float4 _Color;

			float4 frag(v2f i) : SV_Target
			{
				return float4(_Color.r, _Color.g, _Color.b, tex2D(_MainTex, i.uv).a * _Color.a);
			}
			ENDCG
		}
	}
}
