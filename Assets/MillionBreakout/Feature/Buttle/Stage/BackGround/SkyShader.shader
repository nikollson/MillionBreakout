Shader "Custom/MillionBreakout/SkyShader"{
	Properties
	{
		TopColor("Top Color", Color) = (0.1,0.1,0.4,1)
		BottomColor("Bottom Color", Color) = (0.4,0.4,0.6,1)
		TopBottomCurve("Top Bottom Curve", Range(0.01,2)) = 1
	}

	SubShader{
		Pass{
			Tags{ "RenderType" = "Opaque" }

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			uniform float4 TopColor;
			uniform float4 BottomColor;
			uniform float TopBottomCurve;

			struct appdata {
				float4 vertex   : POSITION;
				float4 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 pos : SV_POSITION;
				float2 uv  : TEXCOORD0;
			};

			v2f vert(appdata v) {
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.texcoord.xy;
				return o;
			}

			fixed4 frag(v2f v) : SV_Target{
				return lerp(BottomColor, TopColor, pow(v.uv.y, TopBottomCurve));
			}
			ENDCG
		}
	}
}