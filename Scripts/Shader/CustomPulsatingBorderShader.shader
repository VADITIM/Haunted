Shader "Custom/UnlitPulsatingBorderShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {} // Das Bild, das auf dem Panel angezeigt wird
        _BorderColor ("Border Color", Color) = (1,1,1,1) // Farbe des pulsierenden Rands
        _PulseSpeed ("Pulse Speed", Range(0.1, 5.0)) = 1.0 // Geschwindigkeit des Pulsierens
        _BorderThickness ("Border Thickness", Range(0.0, 0.3)) = 0.05 // Dicke des Rands
        _ImageColor ("Image Color", Color) = (1, 1, 1, 1) // Farbe und Transparenz des Source Images
    }
    SubShader
    {
        Tags { "Queue"="Overlay" "IgnoreProjector"="True" "RenderType"="Transparent" }
        LOD 100

        // Blending für Transparenz aktivieren
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            sampler2D _MainTex; // Die Textur des Panels
            fixed4 _BorderColor; // Farbe des Rands
            float _PulseSpeed; // Geschwindigkeit des Pulsierens
            float _BorderThickness; // Dicke des Rands
            fixed4 _ImageColor; // Farbe und Transparenz des Source Images

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
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
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Lese die Textur des Panels und wende die _ImageColor an, inklusive Transparenz
                fixed4 c = tex2D(_MainTex, i.uv) * _ImageColor;

                // Berechnung der Ränder und Pulsieren
                float distance = min(i.uv.x, i.uv.y);
                distance = min(distance, 1.0 - i.uv.x);
                distance = min(distance, 1.0 - i.uv.y);

                // Pulsing-Effekt basierend auf der Zeit
                float pulse = sin(_Time.y * _PulseSpeed) * 0.5 + 0.5;
                fixed4 borderColor = _BorderColor * pulse;

                // Überprüfe, ob der Pixel innerhalb der Randdicke liegt
                if (distance < _BorderThickness)
                {
                    return borderColor;
                }
                else
                {
                    return c; // Gibt die ursprüngliche Textur zurück, aber mit der modifizierten Farbe und Transparenz
                }
            }
            ENDCG
        }
    }
    FallBack "Unlit/Transparent"
}