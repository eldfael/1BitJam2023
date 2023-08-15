Shader "Hidden/BWDiffuse" {
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _bwBlend ("Black & White blend", Range (0, 1)) = 0
        _fgColor ("Foreground Color", Color) = (1,1,1,1)
        _bgColor ("Background Color", Color) = (0,0,0,1)
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
            uniform float4 _bgColor;
            float4 frag(v2f_img i) : COLOR {
                float4 c = tex2D(_MainTex, i.uv);
                
                float lum = (c.r + c.g + c.b)/3;
                float4 bw = _bgColor;
                if (lum > 0.5)
                {bw = _fgColor;}
                //else
                //{float4 bw = float4( 0, 0, 0, 0 );}
                
                float4 result = c;
                result = lerp(c, bw, _bwBlend);
                return result;
            }
            ENDCG
        }
    }
}