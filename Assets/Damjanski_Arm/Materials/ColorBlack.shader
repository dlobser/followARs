Shader "Unlit/ColorBlack"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Cube ("Reflection Map", Cube) = "" {}

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
                float2 uv2 : TEXCOORD1;
                            float3 normal : NORMAL;

            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float2 uv2 : TEXCOORD1;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float3 normalDir : TEXCOORD2;
                float3 viewDir : TEXCOORD3;
                float3 viewNorm : TEXCOORD4;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
                        uniform samplerCUBE _Cube;


            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;//TRANSFORM_TEX(v.uv2, _MainTex);
                o.uv2 = v.uv2;//TRANSFORM_TEX(v.uv2, _MainTex);
                
                 float4x4 modelMatrix = unity_ObjectToWorld;
            float4x4 modelMatrixInverse = unity_WorldToObject; 
            
                o.viewDir = mul(modelMatrix, v.vertex).xyz 
               - _WorldSpaceCameraPos;
            o.normalDir = normalize(
               mul(float4(v.normal, 0.0), modelMatrixInverse).xyz);
               o.viewNorm = COMPUTE_VIEW_NORMAL;
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
 

                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                float l = length(i.uv-.5);
                
                float3 reflectedDir = 
               reflect(i.viewDir, normalize(i.normalDir)*l*.5);
                float4 cube =  texCUBE(_Cube, reflectedDir).rrrr;
                
                return ((1-i.viewNorm.z)*cube)*((1-l)*.75+.25);//+l-.4;
            }
            ENDCG
        }
    }
}
