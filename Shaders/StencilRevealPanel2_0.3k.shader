Shader "Custom/StencilEmeraldRevealPanelWithMasks"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Mask1 ("Detail Mask", 2D) = "white" {} // Erste Maske für Form- oder Detailkontrolle
        _Mask2 ("Reflection Mask", 2D) = "white" {} // Zweite Maske für Glanz oder Reflexionseffekte
        _Stencil ("Stencil Ref", Int) = 1
        _EmeraldColor ("Emerald Color", Color) = (0, 1, 0, 0.5) // Smaragdgrün, halbtransparent
        _BorderColor ("Border Color", Color) = (0.5, 1, 0.5, 1) // Hellerer Smaragdton
        _PulseSpeed ("Pulse Speed", Range(0.1, 5.0)) = 1.0
        _BorderThickness ("Border Thickness", Range(0.01, 0.3)) = 0.05
        _Glossiness ("Glossiness", Range(0, 1)) = 0.8
        _Metallic ("Metallic", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha   
        ZWrite Off                        

        Pass
        {
            Stencil
            {
                Ref [_Stencil]
                Comp Always
                Pass Replace
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            sampler2D _Mask1; // Erste Maske (verwende verschiedene Farbkanäle)
            sampler2D _Mask2; // Zweite Maske
            float4 _EmeraldColor;
            float4 _BorderColor;
            float _PulseSpeed;
            float _BorderThickness;
            half _Glossiness;
            half _Metallic;

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
                // Hauptfarbe des Smaragds
                half4 emeraldColor = _EmeraldColor;

                // Lesen der Maskenwerte aus verschiedenen Farbkanälen
                float mask1Value = tex2D(_Mask1, i.uv).r; // Maske 1 für Details (Rotkanal)
                float mask2Value = tex2D(_Mask2, i.uv).b; // Maske 2 für Reflexionseffekte (Grünkanal)

                // Abstand zur Kante für den Rand berechnen
                float distance = min(i.uv.x, i.uv.y);
                distance = min(distance, 1.0 - i.uv.x);
                distance = min(distance, 1.0 - i.uv.y);

                // Pulsierender Effekt für den Rand
                float pulse = sin(_Time.y * _PulseSpeed) * 0.5 + 0.5;

                // Anpassung der Randfarbe basierend auf den Masken
                half4 borderColor = _EmeraldColor + (_BorderColor - _EmeraldColor) * pulse * mask1Value; // Randfarbe mit Puls-Effekt und Maske 1

                // Glanz an den Rändern verstärken basierend auf Maske 2
                borderColor.rgb += _BorderColor.rgb * mask2Value * _Glossiness;

                // Transparenz für den Smaragd-Hintergrund
                emeraldColor.a = 0.5;  // Halbtransparenter Hintergrund

                // Kombinierte Darstellung von Rand und Hintergrund
                if (distance < _BorderThickness)
                {
                    borderColor.a = 1.0;  // Der Rand bleibt sichtbar
                    return borderColor;   // Pulsierender Rand mit Masken-Effekten
                }
                else
                {
                    return emeraldColor;  // Smaragdgrüner transparenter Bereich
                }
            }
            ENDCG
        }
    }
    FallBack "Transparent/Cutout/VertexLit"
}