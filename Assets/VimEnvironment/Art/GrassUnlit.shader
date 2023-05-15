Shader "Unlit/GrassUnlit"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
        _NoiseMap ("Noise", 2D) = "white" {}
    }
    SubShader
    {
        Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout"}

        Pass
        {
            Cull Off
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
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            int _InvokersCount;
            float4 _InvokersArray[20];
            
            fixed4 _Color;
            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _NoiseMap;

            v2f vert (appdata v)
            {
                v2f o;

                const float4 vert = v.vertex;
                float4 world_pos = mul(unity_ObjectToWorld, vert);

                const float time = _Time.g;
                const float2 xz = world_pos.xz;

                const float wind_x = tex2Dlod(_NoiseMap, float4(xz + time * 0.005, 0,  0)).x * 0.5f;
                const float wind_y = tex2Dlod(_NoiseMap, float4(xz * 0.05 + time * 0.07, 0,  0)).x * 0.25f;
                const float wind_z = tex2Dlod(_NoiseMap, float4(xz * 0.03 + time * 0.11, 0,  0)).x * 0.25f;

                const float angle = wind_x + wind_y + wind_z;

                float4 offset = -float4(angle - 0.3f, - angle * angle, angle - 0.3f, 0); 
                for (int i = 0; i < _InvokersCount; i++)
                {
                    const float4 world_invoker = mul(unity_ObjectToWorld, _InvokersArray[i]);
                    float4 bend_dir = world_invoker - world_pos;
                    bend_dir.y = 0;
                
                    const float invoker_distance = 1.9 - clamp(0, 1.9, length(bend_dir));
                    const float bend_force = invoker_distance * invoker_distance;
                    const float4 bend_factor = normalize(bend_dir) * bend_force;
                    
                    offset -= bend_factor;  
                }
                

                o.vertex = UnityObjectToClipPos(vert + vert.y * offset);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                clip(col.a - 0.5);
                return col * _Color;
            }
            ENDCG
        }
    }
}
