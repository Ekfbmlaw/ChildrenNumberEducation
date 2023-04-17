// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "ShtoporGames/UnlitVertexColor" {
    Properties {
    }
    SubShader {
        Tags { "RenderType" = "Opaque" }
        LOD 100
        Pass {
            Tags { "LightMode" = "ForwardBase" }
            CGPROGRAM
            #include "UnityCG.cginc"
            #pragma vertex vert
            #pragma fragment frag
            #pragma fragmentoption ARB_precision_hint_fastest

            struct v2f {
                float4 pos : SV_POSITION;
                fixed4 color : TEXCOORD0;
            };

            v2f vert (appdata_full v) {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.color = v.color;
                return o;
            }
            	
            fixed4 frag (v2f i) : SV_Target {
                return i.color;
            }
            ENDCG
        }
    }
    Fallback Off
}