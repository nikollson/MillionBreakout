Shader "Custom/BreakoutGameScene/GridBlockShader" {
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "white" {}
	}

	SubShader{
		Pass{
			Tags{ "RenderType" = "Opaque" }

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			uniform int _ArrayWidth;
			uniform int _ArrayHeight;
			uniform sampler2D _MainTex;
			uniform float _EraseArray[1023];

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
				int x = v.uv.x * _ArrayWidth;
				int y = (int)(v.uv.y * _ArrayHeight)*_ArrayHeight;

				if (_EraseArray[x + y] != 0)
				{
					discard;
				}

				return tex2D(_MainTex, v.uv);
			}
			ENDCG
		}
	}
}