Shader "Custom/Fresnel" {
	Properties{
		_InnerColor("Inner Color", Color) = (0,0,0,0)
		_Color("Rim Color", Color) = (0,0,0,0)
		_RimPower("Rim Power", Range(0.5,20)) = 3.0


	}
		SubShader{
		Tags{ "RenderType" = "Opaque" }
		CGPROGRAM
#pragma surface surf Lambert  
	struct Input {
		float2 uv_MainTex;
		float3 worldRefl;
		float3 viewDir;
	};
	fixed4 _InnerColor;
	fixed4 _Color;
	float _RimPower;

	void surf(Input IN, inout SurfaceOutput o) {
		o.Albedo = _InnerColor;
		half rim = saturate(dot(normalize(IN.viewDir), o.Normal));
		o.Emission = _Color.rgb * (1-pow(rim,_RimPower));
	}
	ENDCG
	}
		Fallback "Diffuse"
}