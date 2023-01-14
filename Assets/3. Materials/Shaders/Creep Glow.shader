Shader "Unlit/Creep Glow"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Color("Color", Color) = (1, 1, 1, 1)
    }
        SubShader
    {
        Cull Off
        ZWrite Off
        ZTest Always
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };

            float4 _Color;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.color = v.color;
                return o;
            }

            float rand01(float2 seed) {
                return frac(sin(dot(seed, float2(12.9898, 78.233))) * 43758.5453);
            }

            fixed4 frag(v2f i) : SV_Target
            {

                float x = i.uv.x;
                float y = i.uv.y;
                float dx = x - 0.5;
                float dy = y - 0.5;

                float d = sqrt(dx * dx + dy * dy) * 2;
                d = sqrt(d);
                fixed4 col = i.color;
                col.a = clamp(1 - d , 0, 1);
                if (d > 1) {
                    col.a = 0;
                }
                return col;
            }
            ENDCG
        }
    }
}
