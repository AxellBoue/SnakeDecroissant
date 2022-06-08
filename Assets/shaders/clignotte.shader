Shader "Sprites/clignotte"
{
	Properties{
		_Color("Tint", Color) = (0, 0, 0, 1)
		_MainTex("Texture", 2D) = "white" {}
		_ClignotteRatio("Clignotte", float) = 0
		_ClignotteColor("ClignotteColor", Color) = (0, 0, 0, 1)
		_ClignotteSpeed("ClignotteSpeed", float) = 100
	}

		SubShader{
			Tags{
				"RenderType" = "Transparent"
				"Queue" = "Transparent"
			}

			Blend SrcAlpha OneMinusSrcAlpha

			ZWrite off
			Cull off

			Pass{

				CGPROGRAM

				#include "UnityCG.cginc"

				#pragma vertex vert
				#pragma fragment frag


				sampler2D _MainTex;
				float4 _MainTex_ST;

				fixed4 _Color;
				
				fixed4 _ClignotteColor;
				float _ClignotteRatio;
				float _ClignotteSpeed;

				struct appdata {
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
					fixed4 color : COLOR;
				};

				struct v2f {
					float4 position : SV_POSITION;
					float2 uv : TEXCOORD0;
					fixed4 color : COLOR;
				};

				v2f vert(appdata v) {
					v2f o;
					o.position = UnityObjectToClipPos(v.vertex);
					o.uv = TRANSFORM_TEX(v.uv, _MainTex);
					o.color = v.color;
					return o;
				}

				fixed4 frag(v2f i) : SV_TARGET{
					fixed4 col = tex2D(_MainTex, i.uv);
					//col *= _Color;
					col *= i.color;
					col.rgb = lerp(col.rgb, _ClignotteColor.rgb, ( cos( _Time.x * _ClignotteSpeed ) + 1 ) / 2 *_ClignotteRatio); 
					return col;
				}

				ENDCG
			}
	}
}
