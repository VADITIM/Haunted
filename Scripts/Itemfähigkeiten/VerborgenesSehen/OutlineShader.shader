Shader "Custom/OutlineShaderWithoutPlane"
{
    Properties
    {
        _Color("Main Color", Color) = (1,1,1,1) // Die Hauptfarbe des Objekts
        _OutlineColor("Outline Color", Color) = (0,1,1,1) // Die Farbe des Leuchtrandes
        _Outline("Outline width", Range (.002, 0.04)) = 0.005 // Die Breite des Leuchtrandes
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            // Erster Durchgang für die Kontur (Outline)
            Name "OUTLINE"
            Tags { "LightMode" = "Always" }
            Cull Front

            ZWrite On
            ZTest LEqual
            ColorMask RGB
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : POSITION;
                float4 color : COLOR;
            };

            uniform float _Outline;
            uniform float4 _OutlineColor;

            v2f vert(appdata v)
            {
                // Vergrößere die Position der Vertices entlang der Normalen um die Outline zu erzeugen
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex + v.normal * _Outline);
                o.color = _OutlineColor;
                return o;
            }

            half4 frag(v2f i) : COLOR
            {
                return i.color;
            }
            ENDCG
        }

        Pass
        {
            // Zweiter Durchgang für das eigentliche Objekt (ohne Outline)
            Name "BASE"
            Tags { "LightMode" = "ForwardBase" }
            Cull Back
            ZWrite On
            ColorMask RGB

            CGPROGRAM
            #pragma vertex vert_base
            #pragma fragment frag_base
            #include "UnityCG.cginc"

            struct appdata_base
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f_base
            {
                float4 pos : POSITION;
                float3 normal : TEXCOORD0;
            };

            uniform float4 _Color;

            v2f_base vert_base(appdata_base v)
            {
                v2f_base o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.normal = normalize(v.normal);
                return o;
            }

            half4 frag_base(v2f_base i) : COLOR
            {
                // Das Objekt wird mit seiner normalen Farbe gerendert
                return _Color;
            }
            ENDCG
        }
    }
    Fallback "Diffuse"
}