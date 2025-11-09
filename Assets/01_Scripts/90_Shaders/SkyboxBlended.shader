Shader "Skybox/BlendedCubemap"
{
    Properties
    {
        _TexDay("Day Cubemap", CUBE) = "" {}
        _TexNight("Night Cubemap", CUBE) = "" {}
        _Blend("Blend", Range(0,1)) = 0
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

            samplerCUBE _TexDay;
            samplerCUBE _TexNight;
            float _Blend;
            float _Exposure;
            float _Rotation; // degrees

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 dir : TEXCOORD0;
            };

            // rotate direction around Y axis by degrees
            float3 rotateY(float3 v, float deg)
            {
                float rad = deg * UNITY_PI / 180.0;
                float s = sin(rad);
                float c = cos(rad);
                return float3( c * v.x + s * v.z,
                               v.y,
                              -s * v.x + c * v.z );
            }

            v2f vert(appdata v)
            {
                v2f o;
                // Expand to clip space as a fullscreen cube (preserve sign) 
                // Use vertex position as direction (skybox cube mesh should be centered at origin)
                o.pos = UnityObjectToClipPos(v.vertex);
                // direction in object space points from origin to vertex
                o.dir = v.vertex.xyz;
                return o;
            }

            fixed4 SampleCubemap(samplerCUBE cube, float3 dir)
            {
                // sample and apply exposure (linear space)
                float4 col = texCUBE(cube, dir);
                // ensure linear space before exposure multiply if necessary
                return col * _Exposure;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // normalize direction
                float3 dir = normalize(i.dir);
                // rotate around Y if requested
                dir = rotateY(dir, _Rotation);

                float4 dayCol = SampleCubemap(_TexDay, dir);
                float4 nightCol = SampleCubemap(_TexNight, dir);

                float4 finalCol = lerp(dayCol, nightCol, _Blend);

                // If project using gamma space, convert linear->gamma for final output
                #ifdef UNITY_COLORSPACE_GAMMA
                    finalCol.rgb = finalCol.rgb;
                #else
                    finalCol.rgb = finalCol.rgb;
                #endif

                return finalCol;
            }
            ENDCG
        }
    }
    FallBack "RenderFX/Skybox"
}
