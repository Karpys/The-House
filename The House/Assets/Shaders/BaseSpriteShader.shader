Shader "Sprites/BaseSpriteShader"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
    }
    
    

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend One OneMinusSrcAlpha


        Pass
        {
            CGPROGRAM
            #pragma vertex SpriteVert
            #pragma fragment SpriteFragment
            #pragma target 2.0
            #pragma multi_compile_instancing
            #include "UnitySprites.cginc"

            fixed4 SpriteFragment(v2f i) : SV_Target
            {
                fixed4 color = SpriteFrag(i);
                return color;
            }
        ENDCG
        }
    }
}