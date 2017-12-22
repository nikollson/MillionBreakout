Shader "Custom/BreakoutGameScene/GridBlockShader" {
	Properties
	{
		MainTex("Base (RGB)", 2D) = "white" {}
	}

	SubShader{
		Pass{
			Tags{ "RenderType" = "Opaque" }

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			uniform int ArrayWidth;
			uniform int ArrayHeight;
			uniform sampler2D MainTex;
			uniform float EraseArray[1023];

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
				int x = v.uv.x * ArrayWidth;
				int y = (int)(v.uv.y * ArrayHeight)*ArrayHeight;

				if (EraseArray[x + y] != 0)
				{
					discard;
				}

				return tex2D(MainTex, v.uv);
			}
			ENDCG
		}
	}
}