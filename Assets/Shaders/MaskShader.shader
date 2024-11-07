// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/MaskShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _MaskTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _HitPosX("posx",float) = 1.0
        _HitPosY("posy",float) = 1.0
        _HitPosZ("posz",float) = 1.0
        _BrushSize("Brush Size", float) = 0.7 // Varsayılan brush size
        _BrushColor("Brush Color", Color) = (0.87,0.33,0.39,1) // Varsayılan brush color

    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        pass{
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            struct vertin{
                float4 vertex:POSITION;
                float2 uv:TEXCOORD0;
            };
            struct vertout{
                float4 pos:SV_POSITION;
                float2 uv:TEXCOORD0;
                float4 worldpos:TEXCOORD1;
            };

            uniform float _HitPosX;
            uniform float _HitPosY;
            uniform float _HitPosZ;            
            uniform float _BrushSize;
            uniform float4 _BrushColor;

            vertout vert(vertin v) 
            {
                vertout o;
                o.worldpos = mul(unity_ObjectToWorld ,v.vertex);
                o.uv = v.uv;
                v.uv = v.uv*2.0-1.0;
                v.vertex.x = v.uv.x;
                v.vertex.y = -v.uv.y;
                v.vertex.z = 1;
                o.pos = v.vertex;
                return o;
            }
            uniform sampler2D _MaskTex;

            float4 frag(vertout o):COLOR 
            {
                // Mevcut maskTexture'daki rengi alıyoruz
                float4 existingColor = tex2D(_MaskTex, o.uv);

                // Sadece yeni fırça boyutundaki alanı güncelle
                float distanceFromHit = distance(o.worldpos.xyz, float3(_HitPosX, _HitPosY, _HitPosZ));
                if (distanceFromHit < _BrushSize)
                {
                    return _BrushColor; // Yeni renk sadece fırça alanında uygulanır
                }

                // Önceden boyanmış alanları korur
                return existingColor;
            }
            ENDCG
        } 
    }
    FallBack "Diffuse"
}
