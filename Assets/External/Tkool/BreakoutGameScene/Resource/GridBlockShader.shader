Shader "Custom/BreakoutGameScene/GridBlockShader" {
	Properties
	{
		MainTex("Base (RGB)", 2D) = "white" {}
		TopColor("Top Color", Color) = (1,1,1,0.8)
		LeftColor("Left Color", Color) = (1,1,1,0.4)
		RightColor("Right Color", Color) = (0,0,0,0.3)
		BottomColor("Bottom Color", Color) = (0,0,0,0.6)
		LineWidth("Line Width", float) = 0.015
	}

	SubShader{
		Pass{
			Tags{ "RenderType" = "Opaque" }

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			uniform sampler2D MainTex;
			uniform float4 TopColor;
			uniform float4 LeftColor;
			uniform float4 RightColor;
			uniform float4 BottomColor;
			uniform float LineWidth;

			uniform int ArrayWidth = 1;
			uniform int ArrayHeight = 1;
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
				int y = (int)(v.uv.y * ArrayHeight)*ArrayWidth;

				if (EraseArray[x + y] != 0)
				{
					discard;
				}

				float LineWidthX = LineWidth * ArrayWidth;
				float LineWidthY = LineWidth * ArrayHeight;

				float divx = frac(ArrayWidth * v.uv.x);
				float divy = frac(ArrayHeight * v.uv.y);
				float lx = step(LineWidthX, divx);
				float ly = step(LineWidthY, divy);
				float hx = step(divx, 1 - LineWidthX);
				float hy = step(divy, 1 - LineWidthY);

				float4 color = tex2D(MainTex, v.uv);

				if (lx*ly*hx*hy == 0)
				{
					float4 lineColor;

					if (lx == 0) lineColor = LeftColor;
					if (ly == 0) lineColor = BottomColor;
					if (hx == 0) lineColor = RightColor;
					if (hy == 0) lineColor = TopColor;

					float alpha = lineColor.w;
					lineColor.w = 1;

					color = lerp(color, lineColor, alpha);
				}

				return color;
			}
			ENDCG
		}
	}
}