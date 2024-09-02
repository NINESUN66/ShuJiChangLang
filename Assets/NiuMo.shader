Shader"Custom/NiuMo" {
  Properties {
    _MainTex ("Main Texture", 2D) = "white" {}
    _Radius ("Radius (范围)", Range(0, 1)) = 0.2
    _Smooth ("Smooth (平滑度)", Range(0, 1)) = 0.5
    _Power ("Power (强度)", Range(0, 1)) = 1
  }
  SubShader {
    CGPROGRAM

    #pragma surface surf Standard fullforwardshadows alpha
    #pragma target 3.0

struct Input
{
    float2 uv_MainTex;
};

sampler2D _MainTex;
half _Radius;
half _Smooth;
half _Power;

half4 GetBlurColor(sampler2D tex, float2 uv, half value, half step, half percent)
{
    value = clamp(value, 0.001, 1);
    step = floor(clamp(step, 2, 100));
    percent = clamp(percent, 0, 1);

    half4 ret = 0;
    half delta = value * 2 / step;
    half count = 0;
    half compareValue = value * value;

    for (half i = -value; i < value; i += delta)
    {
        for (half j = -value; j < value; j += delta)
        {
            if (i * i + j * j > compareValue)
            {
                continue;
            }
            float2 tempUV = uv + float2(i, j);
            fixed4 c = tex2D(tex, tempUV);
            ret += c;
            count += 1;
        }
    }
    return ret / count * percent + tex2D(tex, uv) * (1 - percent);
}

void surf(Input IN, inout SurfaceOutputStandard o)
{
    half4 c = GetBlurColor(_MainTex, IN.uv_MainTex, _Radius, _Smooth * 100, 0.9 + _Power * 0.1);
    o.Emission = c.rgb;
    o.Alpha = c.a;
}

    ENDCG
  }
Fallback"Diffuse"
}