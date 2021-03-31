Shader "Unlit/ForestForTrees"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Data1("Data 1", vector) = (1,1,1,1)
        _ColorA("ColorA", color) = (1,1,1,1)
        _ColorB("ColorB", color) = (1,1,1,1)
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
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float4 pos : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Data1;
            float4 _ColorA;
            float4 _ColorB;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.pos = mul(unity_ObjectToWorld, v.vertex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 noise2 = tex2D(_MainTex, i.uv*float2(_Data1.y*.05,.01)+float2(_Time.x*.1,0));
                
                fixed4 wNoise = tex2D(_MainTex, i.pos.xz*.02);
                fixed4 wNoise2 = tex2D(_MainTex, i.pos.xz*.01);
                fixed4 tree = tex2D(_MainTex, i.uv*float2(_Data1.x,10)+float2(noise2.r*2,wNoise2.r*5));
                float chopa = (i.uv*float2(_Data1.x,10)+float2(0,wNoise2.r*5-9)).y;
                float chop = smoothstep(.99,.98,chopa);
                float chopb = smoothstep(.01,0,chopa);
                tree*=chop;
                tree = lerp(tree,1,chopb);
                fixed4 noise = tex2D(_MainTex, i.uv*float2(_Data1.y,_Data1.z));
                fixed4 bristles = tex2D(_MainTex, i.uv*float2(_Data1.w*_Data1.x*.5,_Data1.w+noise.x*.1));
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                clip(smoothstep(0,.2,tree.g*noise.r)-.1);
                return wNoise.r*bristles.b*lerp(_ColorA,_ColorB,chopa+noise2.x)*2 ;
            }
            ENDCG
        }
    }
}
