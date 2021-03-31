Shader "Unlit/Arm"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Data("Data",vector) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 pos : COLOR;
                float4 n : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float4 pos : COLOR;
                float4 n : NORMAL;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Data;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.pos = mul(unity_ObjectToWorld,v.vertex);
                o.n =  UnityObjectToWorldNormal(v.n).xyzx;
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                float fade = saturate((((i.pos.y+_Data.x)*_Data.y)))-.01;
                clip(fade);
                return lerp(col * lerp(.5,1.5,saturate(i.n.y+.25)) * float4(1,1,1,1),float4(0,0,0,1) ,(1-fade));
            }
            ENDCG
        }
    }
}
