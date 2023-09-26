Shader "Custom/OnHitWhiteShader"
{

    Properties{
        _MainTex("MainTex (RGB)", 2D) = "white" {}
        _FlashColor("FlashColor" , Color) = (1,1,1,1)
        _ColorRange("ColorRange" , Range(0.0, 1.0)) = 0
    }

    SubShader{
        Tags { "RenderType" = "Opaque" }
        LOD 200
        Cull off

        CGPROGRAM
        
        #pragma surface surf Lambert

        // ���Զ����Լ��Ĺ���ģ��  
        //#pragma surface surf CustomDiffuse 
        /*
            ������SurfaceOutput ����������������룻lightDir ���߷���atten ��˥��ϵ��
        */
        //half4 LightingCustomDiffuse(SurfaceOutput s, half3 lightDir, half atten) {
        //    half difLight = max(0, dot(s.Normal, lightDir));    //���������ǰ�������  
        //    difLight = difLight * 0.5 + 0.5;    //�͹�������Ч������Χ�� 0-1 �� 0.5-1  
        //    half4 col;
        //    col.rgb = s.Albedo * _LightColor0.rgb * (difLight * atten * 1.5); //�����������ɫ������ɫ�������ȡ�˥���ȼ��㵱ǰ�����ɫ
        //    col.a = s.Alpha;
        //    return col;
        //}

        sampler2D _MainTex;
        half4 _FlashColor;
        float _ColorRange;

        struct Input {
            float2 uv_MainTex;
            float3 viewDir;
        };

        void surf(Input IN, inout SurfaceOutput o)
        {
            half4 c = tex2D(_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb;
            o.Alpha = c.a;
            IN.viewDir = normalize(IN.viewDir);
            float NdotV = dot(o.Normal, IN.viewDir);
            if (NdotV < _ColorRange)
                //o.Emission = lerp(0, _FlashColor, NdotV);
                o.Emission = _FlashColor.rgb * lerp(0, 1, (_ColorRange - NdotV) / (1 - _ColorRange));
        }



        ENDCG
    }
    FallBack "Diffuse"
}
