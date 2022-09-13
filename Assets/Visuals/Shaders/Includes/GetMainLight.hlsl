// https://blog.unity.com/technology/custom-lighting-in-shader-graph-expanding-your-graphs-in-2019
void GetMainLight_half(float3 WorldPosition, out half3 Direction, out half3 Color, out half DistanceAtten, out half ShadowAtten) {
#if SHADERGRAPH_PREVIEW
    Direction = half3(0.5, 0.5, 0);
    Color = 1;
    DistanceAtten = 1;
    ShadowAtten = 1;
    #else
    #if SHADOWS_SCREEN
    half4 clipPos = TransformWorldToHClip(WorldPosition);
    half4 shadowCoord = ComputeScreenPos(clipPos);
    #else
    half4 shadowCoord = TransformWorldToShadowCoord(WorldPosition);
    #endif
    Light mainLight = GetMainLight(shadowCoord);
    Direction = mainLight.direction;
    Color = mainLight.color;
    DistanceAtten = mainLight.distanceAttenuation;
    ShadowAtten = mainLight.shadowAttenuation;
    #endif
}