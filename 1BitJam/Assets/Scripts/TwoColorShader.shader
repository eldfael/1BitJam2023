Shader "Hidden/BWDiffuse" {
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _bwBlend ("Black & White blend", Range (0, 1)) = 0
        _fgColor ("Foreground Color", Color) = (0.75,0.75,0.75,1)
        _mdColor ("Midground Color", Color) = (0.5,0.5,0.5,1)
        _bgColor ("Background Color", Color) = (0.25,0.25,0.25,1)
        _black ("Black Color", Color) = (0,0,0,1)
        _white ("White Color", Color) = (1,1,1,1)
    }
    SubShader {
        Pass {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #include "UnityCG.cginc"
            uniform sampler2D _MainTex;
            uniform float _bwBlend;
            uniform float4 _fgColor;
            uniform float4 _mdColor;
            uniform float4 _bgColor;
            uniform float4 _black;
            uniform float4 _white;
            float4 frag(v2f_img i) : COLOR {
                float4 c = tex2D(_MainTex, i.uv);
                
                float lum = (c.r + c.g + c.b)/3;
                float4 bw = _black;
                if (lum >= 0.2 && lum < 0.4)
                {bw = _bgColor;}
                if (lum >= 0.4 && lum < 0.6)
                {bw = _mdColor;}
                if (lum >= 0.6 && lum < 0.8)
                {bw = _fgColor;}
                if (lum >= 0.8)
                {bw = _white;}


                //else
                //{float4 bw = float4( 0, 0, 0, 0 );}
                
                //float4 result = c;
                //result = lerp(c, bw, _bwBlend);
                return bw;
            }
            ENDCG
        }
    }
}