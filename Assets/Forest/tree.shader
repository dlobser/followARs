Shader "Unlit/tree"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
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
            #include "noise.cginc"

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

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                float r = 5;
                fixed4 col = tex2D(_MainTex, i.uv);
                float sn = snoise(i.uv*100*r);
                float sn2 = snoise(float2(i.uv.x*3*r,0));
                
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                float s = min(1,max(-1,(sin(i.uv.x*6.28*10*r))*1000));
                float fu = frac((i.uv.x)*20*r*s+ (1-s) );
                float t = smoothstep(.3,.4,fu*pow((1-(i.uv.y-sn2*.2)),.5)+(sn*.2-.2));
                clip(t-.01);
                return float4(1,1,1,1);
            }
            ENDCG
        }
    }
}
