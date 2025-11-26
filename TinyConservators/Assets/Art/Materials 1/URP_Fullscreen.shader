Shader "Custom/URP_FullscreenBlur"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BlurSize("Blur Size", Float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline"="UniversalPipeline" }
        Pass
        {
Name "Blur"
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

struct Attributes
{
    float4 positionOS : POSITION;
    float2 uv : TEXCOORD0;
};

struct Varyings
{
    float4 positionHCS : SV_POSITION;
    float2 uv : TEXCOORD0;
};

Varyings vert(Attributes IN)
{
    Varyings OUT;
    OUT.positionHCS = TransformObjectToHClip(IN.positionOS);
    OUT.uv = IN.uv;
    return OUT;
}

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
float _BlurSize;

half4 frag(Varyings IN) : SV_Target
{
    float2 uv = IN.uv;
    float2 offset = _BlurSize / float2(_ScreenParams.x, _ScreenParams.y);

    half4 color = 0;
    color += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv + float2(0, 0));
    color += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv + float2(offset.x, 0));
    color += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv + float2(-offset.x, 0));
    color += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv + float2(0, offset.y));
    color += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv + float2(0, -offset.y));

    return color / 5.0;
}
            ENDHLSL
        }
    }
}
