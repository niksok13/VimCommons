Shader "Custom/GrassSurface"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _NoiseMap ("Noise", 2D) = "white" {}
    }
    SubShader
    {
        Cull Off
        Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout"}
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows vertex:vert

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0


        struct Input
        {
            float2 uv_MainTex;
        };

        fixed4 _Color;
        sampler2D _MainTex;
        half _Glossiness;
        half _Metallic;
            
        int _InvokersCount;
        float4 _InvokersArray[20];
        sampler2D _NoiseMap;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        
        void vert (inout appdata_full v)
        {

            float4 worldPos = mul(unity_ObjectToWorld, v.vertex);
            
            float4 windX = tex2Dlod(_NoiseMap, float4(worldPos.xz * 0.023 + _Time.g * 0.11, 0,  0));
            float4 windZ = tex2Dlod(_NoiseMap, float4(worldPos.xz * 0.029 + _Time.g * 0.05, 0,  0));
            float4 windFactor = float4(windX.r *windX.r * 0.3, 0, 0, windZ.r * windZ.r * 0.3);

            float4 offset = float4(0,0,0,0);
            for (int i = 0; i < _InvokersCount; i++)
            {
                float4 worldInvoker = _InvokersArray[i];
                float4 bendDir = worldInvoker - worldPos;
                bendDir.y = 0;

                float invokerDistance = clamp(0,2,length(bendDir))-2;
                float bendForce = invokerDistance * invokerDistance * invokerDistance;
                float4 bendFactor = normalize(bendDir)*bendForce;
                
                offset += v.vertex.y * (windFactor + bendFactor);  
            }
            v.vertex = v.vertex - offset;
            UNITY_TRANSFER_FOG(v,v.vertex);
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
              clip(c.a - 0.5);
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
