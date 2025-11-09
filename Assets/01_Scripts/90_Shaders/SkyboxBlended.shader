Shader "Skybox/BlendedPanorama"
{
    Properties
    {
        _TexDay("Day Panorama (HDRI)", 2D) = "white" {}
        _TexNight("Night Panorama (HDRI)", 2D) = "black" {}
        _Blend("Blend (0 = Day, 1 = Night)", Range(0,1)) = 0
        _Exposure("Exposure", Float) = 1
        _Rotation("Rotation Y (deg)", Range(0,360)) = 0
    }
    SubShader
    {
        Tags { "Queue"="Background" "IgnoreProjector"="True" "RenderType"="Background" }
        Cull Off
        ZWrite Off
        Lighting Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _TexDay;
            sampler2D _TexNight;
            float _Blend;
            float _Exposure;
            float _Rotation;

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 dir : TEXCOORD0;
            };

            float3 rotateY(float3 v, float deg)
            {
                float rad = deg * UNITY_PI / 180.0;
                float s = sin(rad);
                float c = cos(rad);
                return float3( c*v.x + s*v.z,
                               v.y,
                              -s*v.x + c*v.z );
            }

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.dir = v.vertex.xyz;
                return o;
            }

            float2 dirToUV(float3 dir)
            {
                dir = normalize(dir);
                float u = 0.5 + atan2(dir.z, dir.x) / (2 * UNITY_PI);
                float v = 0.5 - asin(dir.y) / UNITY_PI;
                return float2(u, v);
            }

            fixed4 SamplePanorama(sampler2D tex, float3 dir)
            {
                float2 uv = dirToUV(dir);
                fixed4 col = tex2D(tex, uv) * _Exposure;
                return col;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float3 dir = normalize(i.dir);
                dir = rotateY(dir, _Rotation);

                fixed4 dayCol = SamplePanorama(_TexDay, dir);
                fixed4 nightCol = SamplePanorama(_TexNight, dir);

                fixed4 finalCol = lerp(dayCol, nightCol, _Blend);
                return finalCol;
            }
            ENDCG
        }
    }
    FallBack "RenderFX/Skybox"
}
