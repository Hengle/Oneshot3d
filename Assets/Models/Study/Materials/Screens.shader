// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Screens"
{
	Properties
	{
		_TExture("TExture", 2D) = "white" {}
		_NoiseFac("Noise Fac", 2D) = "white" {}
		_Light("Light", Color) = (1,0.4764151,0.8854613,0)
		_dark("dark", Color) = (0.1976027,0,0.2358491,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _TExture;
		uniform float4 _TExture_ST;
		uniform sampler2D _NoiseFac;
		uniform float4 _NoiseFac_ST;
		uniform float4 _Light;
		uniform float4 _dark;


		float3 mod3D289( float3 x ) { return x - floor( x / 289.0 ) * 289.0; }

		float4 mod3D289( float4 x ) { return x - floor( x / 289.0 ) * 289.0; }

		float4 permute( float4 x ) { return mod3D289( ( x * 34.0 + 1.0 ) * x ); }

		float4 taylorInvSqrt( float4 r ) { return 1.79284291400159 - r * 0.85373472095314; }

		float snoise( float3 v )
		{
			const float2 C = float2( 1.0 / 6.0, 1.0 / 3.0 );
			float3 i = floor( v + dot( v, C.yyy ) );
			float3 x0 = v - i + dot( i, C.xxx );
			float3 g = step( x0.yzx, x0.xyz );
			float3 l = 1.0 - g;
			float3 i1 = min( g.xyz, l.zxy );
			float3 i2 = max( g.xyz, l.zxy );
			float3 x1 = x0 - i1 + C.xxx;
			float3 x2 = x0 - i2 + C.yyy;
			float3 x3 = x0 - 0.5;
			i = mod3D289( i);
			float4 p = permute( permute( permute( i.z + float4( 0.0, i1.z, i2.z, 1.0 ) ) + i.y + float4( 0.0, i1.y, i2.y, 1.0 ) ) + i.x + float4( 0.0, i1.x, i2.x, 1.0 ) );
			float4 j = p - 49.0 * floor( p / 49.0 );  // mod(p,7*7)
			float4 x_ = floor( j / 7.0 );
			float4 y_ = floor( j - 7.0 * x_ );  // mod(j,N)
			float4 x = ( x_ * 2.0 + 0.5 ) / 7.0 - 1.0;
			float4 y = ( y_ * 2.0 + 0.5 ) / 7.0 - 1.0;
			float4 h = 1.0 - abs( x ) - abs( y );
			float4 b0 = float4( x.xy, y.xy );
			float4 b1 = float4( x.zw, y.zw );
			float4 s0 = floor( b0 ) * 2.0 + 1.0;
			float4 s1 = floor( b1 ) * 2.0 + 1.0;
			float4 sh = -step( h, 0.0 );
			float4 a0 = b0.xzyw + s0.xzyw * sh.xxyy;
			float4 a1 = b1.xzyw + s1.xzyw * sh.zzww;
			float3 g0 = float3( a0.xy, h.x );
			float3 g1 = float3( a0.zw, h.y );
			float3 g2 = float3( a1.xy, h.z );
			float3 g3 = float3( a1.zw, h.w );
			float4 norm = taylorInvSqrt( float4( dot( g0, g0 ), dot( g1, g1 ), dot( g2, g2 ), dot( g3, g3 ) ) );
			g0 *= norm.x;
			g1 *= norm.y;
			g2 *= norm.z;
			g3 *= norm.w;
			float4 m = max( 0.6 - float4( dot( x0, x0 ), dot( x1, x1 ), dot( x2, x2 ), dot( x3, x3 ) ), 0.0 );
			m = m* m;
			m = m* m;
			float4 px = float4( dot( x0, g0 ), dot( x1, g1 ), dot( x2, g2 ), dot( x3, g3 ) );
			return 42.0 * dot( m, px);
		}


		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_TExture = i.uv_texcoord * _TExture_ST.xy + _TExture_ST.zw;
			float2 uv_NoiseFac = i.uv_texcoord * _NoiseFac_ST.xy + _NoiseFac_ST.zw;
			float4 tex2DNode18 = tex2D( _NoiseFac, uv_NoiseFac );
			float2 break31_g1 = float2( 0,20000.43 );
			float2 temp_output_15_0_g1 = float2( 20,20 );
			float2 break26_g1 = ( i.uv_texcoord * temp_output_15_0_g1 );
			float2 appendResult27_g1 = (float2(( ( 0.0 * step( 1.0 , ( break26_g1.y % 2.0 ) ) ) + break26_g1.x ) , break26_g1.y));
			float dotResult4_g3 = dot( floor( appendResult27_g1 ) , float2( 12.9898,78.233 ) );
			float lerpResult10_g3 = lerp( break31_g1.x , break31_g1.y , frac( ( sin( dotResult4_g3 ) * 43758.55 ) ));
			float2 break12_g1 = temp_output_15_0_g1;
			float temp_output_21_0_g1 = sign( ( break12_g1.y - break12_g1.x ) );
			float temp_output_14_0_g1 = 1.0;
			float2 appendResult10_g2 = (float2(( ( ( 1.0 / break12_g1.y ) * max( temp_output_21_0_g1 , 0.0 ) ) + temp_output_14_0_g1 ) , ( temp_output_14_0_g1 + ( ( -1.0 / break12_g1.x ) * min( temp_output_21_0_g1 , 0.0 ) ) )));
			float2 temp_output_11_0_g2 = ( abs( (frac( appendResult27_g1 )*2.0 + -1.0) ) - appendResult10_g2 );
			float2 break16_g2 = ( 1.0 - ( temp_output_11_0_g2 / fwidth( temp_output_11_0_g2 ) ) );
			float temp_output_2_0_g1 = saturate( min( break16_g2.x , break16_g2.y ) );
			float3 temp_cast_0 = (( lerpResult10_g3 * temp_output_2_0_g1 )).xxx;
			float simplePerlin3D3 = snoise( temp_cast_0*_Time.x );
			simplePerlin3D3 = simplePerlin3D3*0.5 + 0.5;
			float clampResult13 = clamp( simplePerlin3D3 , 0.0 , 1.0 );
			float4 lerpResult14 = lerp( _Light , _dark , clampResult13);
			float4 temp_output_22_0 = ( tex2DNode18.r * lerpResult14 );
			o.Albedo = ( ( tex2D( _TExture, uv_TExture ) * round( ( 1.0 - tex2DNode18.r ) ) ) + temp_output_22_0 ).rgb;
			o.Emission = temp_output_22_0.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18400
-41;334;1920;646;2346.185;-66.5296;1.3;True;True
Node;AmplifyShaderEditor.Vector2Node;32;-1330.694,326.9544;Inherit;False;Constant;_Tiling;Tiling;4;0;Create;True;0;0;False;0;False;20,20;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.FunctionNode;9;-1136.98,328.1143;Inherit;True;Bricks Pattern;-1;;1;7d219d3a79fd53a48987a86fa91d6bac;0;4;15;FLOAT2;20,20;False;14;FLOAT;1;False;16;FLOAT;0;False;17;FLOAT2;0,20000.43;False;2;FLOAT;0;FLOAT;3
Node;AmplifyShaderEditor.TimeNode;11;-1046.079,590.514;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TexturePropertyNode;2;-1437.627,41.79359;Inherit;True;Property;_NoiseFac;Noise Fac;1;0;Create;True;0;0;False;0;False;52aa73ccb0f06bf4295c71a0d61e7f3d;None;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.SamplerNode;18;-1173.042,31.26047;Inherit;True;Property;_TextureSample0;Texture Sample 0;4;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.NoiseGeneratorNode;3;-739.02,383.75;Inherit;True;Simplex3D;True;False;2;0;FLOAT3;0,0,0;False;1;FLOAT;-231;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;13;-483.715,369.0814;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;15;-460.4479,99.02705;Inherit;False;Property;_Light;Light;2;0;Create;True;0;0;False;0;False;1,0.4764151,0.8854613,0;1,0.4764151,0.8854613,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TexturePropertyNode;1;-1378.806,-303.9162;Inherit;True;Property;_TExture;TExture;0;0;Create;True;0;0;False;0;False;78168680835c74c48b88ed49116ade37;None;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.OneMinusNode;29;-814.7236,-41.46387;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;16;-630.9476,197.8266;Inherit;False;Property;_dark;dark;3;0;Create;True;0;0;False;0;False;0.1976027,0,0.2358491,0;0.1976027,0,0.2358491,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;19;-1108.444,-313.7056;Inherit;True;Property;_TextureSample1;Texture Sample 1;4;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;14;-178.2719,270.6037;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RoundOpNode;28;-626.7236,-102.4639;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;22;-29.81897,-2.056488;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;-517.6783,-308.4478;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;31;-25.72363,-245.4639;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;594.5,-94.40001;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Screens;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;True;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;9;15;32;0
WireConnection;18;0;2;0
WireConnection;3;0;9;3
WireConnection;3;1;11;1
WireConnection;13;0;3;0
WireConnection;29;0;18;1
WireConnection;19;0;1;0
WireConnection;14;0;15;0
WireConnection;14;1;16;0
WireConnection;14;2;13;0
WireConnection;28;0;29;0
WireConnection;22;0;18;1
WireConnection;22;1;14;0
WireConnection;6;0;19;0
WireConnection;6;1;28;0
WireConnection;31;0;6;0
WireConnection;31;1;22;0
WireConnection;0;0;31;0
WireConnection;0;2;22;0
ASEEND*/
//CHKSM=E0AB805FC164EBBF53687D6245124AF8662DDDE3