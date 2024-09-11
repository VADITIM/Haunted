Shader "UI/BlurGreenTint"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _BlurSize ("Blur Size", Range(0.0, 10.0)) = 1.0
        _TintColor ("Tint Color", Color) = (0, 1, 0, 0.5) // Green tint with transparency
    }
    SubShader
    {
        Tags { "Queue" = "Overlay" }
        Blend SrcAlpha OneMinusSrcAlpha // Enable alpha blending
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _BlurSize;
            float4 _TintColor; // Tint color with alpha

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float4 color = float4(0, 0, 0, 0);
                float2 offset = float2(_BlurSize, _BlurSize);

                // Sample the texture multiple times in different directions
                for (int x = -4; x <= 4; x++)
                {
                    for (int y = -4; y <= 4; y++)
                    {
                        float2 sampleOffset = float2(x, y) * offset;
                        color += tex2D(_MainTex, i.uv + sampleOffset);
                    }
                }

                // Average the samples
                color /= 81.0;

                // Apply green tint and transparency
                float4 tintedColor = lerp(color, _TintColor, _TintColor.a);
                return tintedColor;
            }
            ENDCG
        }
    }
}