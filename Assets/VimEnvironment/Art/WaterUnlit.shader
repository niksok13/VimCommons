Shader "Unlit/WaterUnlit"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _Foam ("Foam", Color) = (1,1,1,1)
        _NoiseMap ("Noise", 2D) = "white" {}
        _WaveMap ("Wave", 2D) = "white" {}
    }
    SubShader
    {
        Tags {"Queue"="AlphaTest"}

        Pass
        {
            Cull Back
            CGPROGRAM
// Upgrade NOTE: excluded shader from DX11, OpenGL ES 2.0 because it uses unsized arrays
#pragma exclude_renderers d3d11 gles
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float3 worldPos : TEXCOORD1;
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            int _InvokersCount;
            float4 _InvokersArray[20];
            
            fixed4 _Color;
            fixed4 _Foam;
            sampler2D _NoiseMap;
            sampler2D _WaveMap;


            sampler2D _CameraDepthTexture;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                
                const float time = _Time.g;
                const float2 xz = o.worldPos.zx;

                const float angle = tex2Dlod(_WaveMap, float4(xz * 0.031 + time * 0.007, 0,  0)).x;
                
                const float height = angle;
                const float4 vert = v.vertex;
                o.vertex = UnityObjectToClipPos(vert+ float4(0,height * 0.02,0,0));
                
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float3 world_pos = i.worldPos;
                
                const float time = _Time.g;
                const float2 xz = world_pos.zx;

                const float wind_y = tex2Dlod(_NoiseMap, float4(xz * 0.05 + time * 0.05, 0,  0)).x;
                const float wind_z = tex2Dlod(_NoiseMap, float4(xz * 0.011 + time * 0.07, 0,  0)).x;
                const float wind_x = tex2Dlod(_NoiseMap, float4(xz * 0.007 + time * 0.03, 0,  0)).x;

                const float wind_w = tex2Dlod(_WaveMap, float4(xz * 0.0013 + time * 0.017, 0,  0)).x;
                const float wind_v = tex2Dlod(_WaveMap, float4(xz * 0.0031 + time * 0.029, 0,  0)).x;
                
                float2 screenuv = i.vertex.xy / _ScreenParams.xy;
                float screenDepth = Linear01Depth(tex2D(_CameraDepthTexture, screenuv));
                float diff = screenDepth - Linear01Depth(i.vertex.z);
                float intersect = 0;

                if(diff > 0)
                    intersect = 1 - smoothstep(0, _ProjectionParams.w * 2, diff);

                float glide = wind_v + wind_w;

                float angle = (wind_x + wind_y + wind_z) * (1- glide);
                angle *= angle;
                angle *= angle;
                return lerp(_Color, _Foam, angle + pow(intersect, 4));
            }
            ENDCG
        }
    }
}
