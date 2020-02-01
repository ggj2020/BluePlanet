Shader "Unlit/SdWavingBar"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Color("Color", color) = (1, 1, 1, 1)
		_Height("Height", Range(0.0, 1.0)) = 1.0
		_Period("Period", Range(0.0, 10.0)) = 1.0
		_Amplitude("Amplitude", Range(0.0, 0.05)) = 0.0
		_WaveSpeed("WaveSpeed", float) = 1.0

		_BubbleNoise("BubbleNoise", 2D) = "white" {}
		_BubbleColor("BubbleColor", color) = (0, 0, 0, 1)
		_BubblePeriod("BubblePeriod", Range(0.0, 1.0)) = 0.0
		_BubbleSpeed("BubbleSpeed", float) = 1.0


		_LightTex("LightTex", 2D) = "white" {}
		_LightColor("LightColor", color) = (1, 1, 1, 1)
		_LightThickness("LightThickness", Range(0.0, 1.0)) = 1.0
		_LightSpeed("LightSpeed", float) = 1.0
	}
    SubShader
    {
		// inside SubShader
		Tags { "Queue" = "Transparent" "RenderType" = "Transparent" "IgnoreProjector" = "True" }

		// inside Pass
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha


        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float2 bubbleNoiseUv : TEXCOORD1;
				float2 lightUv : TEXCOORD2;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
				float2 bubbleNoiseUv : TEXCOORD1;
				float2 lightUv : TEXCOORD2;
				float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			fixed4 _Color;
			float _Height;
			float _Period;
			float _Amplitude;
			float _WaveSpeed;

			sampler2D _BubbleNoise;
			float4 _BubbleNoise_ST;
			fixed4 _BubbleColor;
			float _BubblePeriod;
			float _BubbleSpeed;

			sampler2D _LightTex;
			float4 _LightTex_ST;
			fixed4 _LightColor;
			float _LightThickness;
			float _LightSpeed;


            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.bubbleNoiseUv = TRANSFORM_TEX(v.bubbleNoiseUv, _BubbleNoise);
				o.lightUv = TRANSFORM_TEX(v.lightUv, _LightTex);
                return o;
            }





			fixed gray(fixed3 col) {
				return (col.r + col.g + col.b) * 0.33333;
			}

			fixed gray(fixed4 col) {
				return gray(col.rgb);
			}

			float random(float2 uv)
			{
				return frac(sin(dot(uv, float2(12.9898, 78.233))) * 43758.5453123);
			}



            fixed4 frag (v2f i) : SV_Target
            {
                //fixed4 col = tex2D(_MainTex, i.uv);
				fixed4 col = _Color;

				float waveLine = _Height + cos(i.uv.x * _Period + _Time * _WaveSpeed) * _Amplitude - _Amplitude * 0.5;
				col.a = i.uv.y < waveLine;

				//Bubble
				float2 scrolledBubbleUv;
				scrolledBubbleUv.x = i.bubbleNoiseUv.x + _Time * _BubbleSpeed;
				scrolledBubbleUv.y = i.bubbleNoiseUv.y + sin(_Time * 5.0) * 0.3;

				fixed4 noiseCol = tex2D(_BubbleNoise, scrolledBubbleUv);
				col.rgb = _BubblePeriod < gray(noiseCol) ? _BubbleColor : col.rgb;
				


				//Light
				float2 scrolledLightUv;
				scrolledLightUv.x = i.lightUv.x + _Time * _LightSpeed;
				scrolledLightUv.y = i.lightUv.y + sin(_Time * 3.0) * 0.3;

				fixed4 lightCol = tex2D(_LightTex, scrolledLightUv);
				col.rgb = _LightThickness < gray(lightCol) ? _LightColor.rgb : col.rgb;
				//col.rgb = lightCol.rgb;

                return col;
            }

			

			

            ENDCG
        }
    }
}
