Shader "Custom/MillionBullets/BlocksShader" {
	SubShader{
		// アルファを使う
		ZWrite On
		Blend SrcAlpha OneMinusSrcAlpha

		Pass{
		CGPROGRAM

		// シェーダーモデルは5.0を指定
#pragma target 5.0

		// シェーダー関数を設定 
#pragma vertex vert
#pragma geometry geom
#pragma fragment frag

#include "UnityCG.cginc"

		struct BlockElement {
			int X;
			int Y;
			int LifePoint;
		};

	sampler2D _MainTex;
	float4 BoxCenter;
	float BoxAngle;
	float BoxWidth;
	float BoxHeight;
	float DivideX;
	float DivideY;

	StructuredBuffer<BlockElement> BlockElements;

	struct VSOut {
		float4 pos : SV_POSITION;
		float2 tex : TEXCOORD0;
		float4 col : COLOR;
		float id : ELEMENTID;
	};

	// 頂点シェーダ
	VSOut vert(uint id : SV_VertexID)
	{
		VSOut output;
		output.pos = BoxCenter + float4(0, 0, 0, 1);
		output.tex = float2(0, 0);
		output.col = float4(1, 1, 1, 1);
		if (BlockElements[id].LifePoint <= 0) {
			output.col = float4(0, 0, 0, 0);
		}
		output.id = id;
		/*
		output.x = BlockElements[id].X;
		output.y = BlockElements[id].Y;
		*/
		return output;
	}

	// ジオメトリシェーダ
	[maxvertexcount(4)]
	void geom(point VSOut input[1], inout TriangleStream<VSOut> outStream)
	{
		if (input[0].col.a == 0)
		{
			outStream.RestartStrip();
			return;
		}

		VSOut output;

		float4 pos = input[0].pos;
		float4 col = input[0].col;
		float s = sin(BoxAngle);
		float c = cos(BoxAngle);
		int id = (int)(input[0].id+0.1f);
		int basex = BlockElements[id].X;
		int basey = BlockElements[id].Y;

		for (int i = 0; i < 2; i++)
		{
			for (int j = 0; j < 2; j++)
			{

				int x = basex + i;
				int y = basey + j;

				float2 tex = float2(x*DivideX,y*DivideY);
				output.tex = tex;

				// 頂点位置を計算
				float2 localPos = float2((tex.x - 0.5f) * BoxWidth, (tex.y - 0.5f) * BoxHeight);
				float2 rotatePos = float2(localPos.x * c - localPos.y*s, localPos.x*s + localPos.y*c);
				output.pos = pos + float4(rotatePos, 0, 0);
				output.pos = mul(UNITY_MATRIX_VP, output.pos);

				// その他の値も設定
				output.col = col;
				output.id = input[0].id;

				// ストリームに頂点を追加
				outStream.Append(output);
			}
		}

		// トライアングルストリップを終了
		outStream.RestartStrip();
	}

	// ピクセルシェーダー
	fixed4 frag(VSOut i) : COLOR
	{
		// 出力はテクスチャカラーと頂点色
		float4 col = tex2D(_MainTex, i.tex) * i.col;

		// アルファが一定値以下なら中断
		if (col.a < 0.3) discard;

		// 色を返す
		return col;
	}

		ENDCG
	}
	}
}