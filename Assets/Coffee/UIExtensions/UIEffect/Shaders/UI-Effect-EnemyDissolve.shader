Shader "UI/Hidden/UI-Effect-EnemyDissolve"
{
	Properties
	{
		[PerRendererData] _MainTex ("Main Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		
		_StencilComp ("Stencil Comparison", Float) = 8
		_Stencil ("Stencil ID", Float) = 0
		_StencilOp ("Stencil Operation", Float) = 0
		_StencilWriteMask ("Stencil Write Mask", Float) = 255
		_StencilReadMask ("Stencil Read Mask", Float) = 255

		_ColorMask ("Color Mask", Float) = 15

		[Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0

		[Header(Transition)]
		_NoiseTex ("Transition Texture (A)", 2D) = "white" {}
		_ParamTex ("Parameter Texture", 2D) = "white" {}
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}
		
		Stencil
		{
			Ref [_Stencil]
			Comp [_StencilComp]
			Pass [_StencilOp] 
			ReadMask [_StencilReadMask]
			WriteMask [_StencilWriteMask]
		}

		Cull Off
		Lighting Off
		ZWrite Off
		ZTest [unity_GUIZTestMode]
		Blend SrcAlpha OneMinusSrcAlpha
		ColorMask [_ColorMask]

		Pass
		{
			Name "Default"

		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 2.0
			
			#define REVERSE 1
			#define ADD 1
			#pragma multi_compile __ UNITY_UI_ALPHACLIP

			#pragma shader_feature __ FADE CUTOFF DISSOLVE
			#include "UnityCG.cginc"
			#include "UnityUI.cginc"

			#define UI_TRANSITION 1
			#include "UI-Effect.cginc"
			#include "UI-Effect-Sprite.cginc"

		
			fixed4 MyApplyTransitionEffect(half4 color, half3 transParam)
			{
				fixed4 param = tex2D(_ParamTex, float2(0.25, transParam.z));
				float alpha = tex2D(_NoiseTex, transParam.xy).a;

				#if REVERSE
				fixed effectFactor = 1 - param.x;
				#else
				fixed effectFactor = param.x;
				#endif

				#if FADE
				color.a *= saturate(alpha + (1 - effectFactor * 2));
				#elif CUTOFF
				color.a *= step(0.001, color.a * alpha - effectFactor);
				#elif DISSOLVE
				fixed width = param.y/4;
				fixed softness = param.z;
				fixed3 dissolveColor = tex2D(_ParamTex, float2(0.75, transParam.z)).rgb;
				float factor = alpha - effectFactor * ( 1 + width ) + width;
				fixed edgeLerp = step(factor, color.a) * saturate((width - factor)*16/ softness);
				// color = ApplyColorEffect(color, fixed4(dissolveColor, edgeLerp));
				color = ApplyColorEffect(color, fixed4(dissolveColor, 1));
				color.a *= saturate((factor)*32/ softness);
				#endif

				return color;
			}
		
			fixed4 frag(v2f IN) : SV_Target
			{
				half4 color = (tex2D(_MainTex, IN.texcoord) + _TextureSampleAdd);
				color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
				
				color = MyApplyTransitionEffect(color, IN.eParam) * IN.color;

				#if UNITY_UI_ALPHACLIP
				clip (color.a - 0.001);
				#endif

				return color;
			}
		ENDCG
		}
	}
}
