Shader "Custom/StencilRevealPanel"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Stencil ("Stencil Ref", Int) = 1
        _EmeraldColor ("Emerald Color", Color) = (0, 1, 0, 0.5) // Smaragdgrün, halbtransparent
        _BorderColor ("Border Color", Color) = (0.5, 1, 0.5, 1) // Hellerer Smaragdton
        _PulseSpeed ("Pulse Speed", Range(0.1, 5.0)) = 1.0
        _BorderThickness ("Border Thickness", Range(0.01, 0.3)) = 0.05
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" } // Setzt das Rendering in die Transparenz-Queue
        Blend SrcAlpha OneMinusSrcAlpha   // Aktiviert Alpha-Blending für Transparenz
        ZWrite Off                        // Deaktiviert Tiefen-Write für transparente Objekte

        Pass
        {
            Stencil
            {
                Ref [_Stencil]
                Comp Always
                Pass Replace // Setzt den Stencil-Wert
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            sampler2D _MainTex;
            float4 _EmeraldColor;
            float4 _BorderColor;
            float _PulseSpeed;
            float _BorderThickness;

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                // Smaragdgrüner transparenter Bereich (Hintergrund)
                half4 emeraldColor = _EmeraldColor;

                // Berechne den Abstand zur Kante für den Rand
                float distance = min(i.uv.x, i.uv.y);
                distance = min(distance, 1.0 - i.uv.x);
                distance = min(distance, 1.0 - i.uv.y);

                // Pulsierender Effekt
                float pulse = sin(_Time.y * _PulseSpeed) * 0.5 + 0.5;

                // Pulsierende Randfarbe
                half4 borderColor = _EmeraldColor + (_BorderColor - _EmeraldColor) * pulse * (1.0 - distance / _BorderThickness);

                // Transparenz für den Smaragd-Hintergrund
                emeraldColor.a = 0.5;  // Halbtransparenter Hintergrund

                // Wende den Rand und den Hintergrund an
                if (distance < _BorderThickness)
                {
                    borderColor.a = 1.0;  // Der Rand bleibt vollständig sichtbar
                    return borderColor;    // Pulsierender Rand
                }
                else
                {
                    return emeraldColor;   // Smaragdgrüner transparenter Bereich
                }
            }
            ENDCG
        }
    }
}