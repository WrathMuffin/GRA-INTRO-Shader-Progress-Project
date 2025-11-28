Shader "StencilHole"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}  // Main texture
        _MainColor("Main Color", Color) = (0,0,0,0)

        [KeywordEnum(TextureOn, TextureOff)] _TextureMode("Texture Mode", Float) = 0
    }

    SubShader
    {
        Tags { "RenderPipeline" = "UniversalRenderPipeline" "Queue" = "Geometry" }

        // Stencil operations
        Stencil
        {
            Ref 1  // Reference value to check against
            Comp NotEqual  // Only render where the stencil buffer is NOT equal to the reference
        }

        // Standard pass
        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #pragma multi_compile _TEXTUREMODE_TEXTUREON _TEXTUREMODE_TEXTUREOFF

            // Include URP core functionality
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;  // Object space position
                float2 uv : TEXCOORD0;         // UV coordinates for texture sampling
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;  // Clip-space position
                float2 uv : TEXCOORD0;             // UV coordinates passed to fragment shader
            };

            CBUFFER_START(UnityPerMaterial)
                float4 _MainColor;   // Base color property
            CBUFFER_END

            // Texture sampler
            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            // Vertex shader
            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);  // Transform position to clip space
                OUT.uv = IN.uv;  // Pass UV coordinates
                return OUT;
            }

            // Fragment shader
            half4 frag(Varyings IN) : SV_Target
            {
                // Sample texture using UV coordinates
                half3 final_color = _MainColor.rgb;

                #ifdef _TEXTUREMODE_TEXTUREON
                    half4 texColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv);
                    final_color = _MainColor.rgb * texColor;
                #endif
                return half4(final_color, _MainColor.a);
            }

            ENDHLSL
        }
    }
}