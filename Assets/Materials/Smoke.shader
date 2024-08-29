// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Smoke"
{
	Properties
	{
		_Float0("Float 0", Range( 0 , 10)) = 4.392158
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard alpha:fade keepalpha addshadow fullforwardshadows exclude_path:deferred 
		struct Input
		{
			float3 worldPos;
		};

		uniform float _Float0;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 color5 = IsGammaSpace() ? float4(0.2641509,0.2599976,0.2599976,0) : float4(0.05672634,0.05497073,0.05497073,0);
			float4 color1 = IsGammaSpace() ? float4(0.490566,0.490566,0.490566,0) : float4(0.2054129,0.2054129,0.2054129,0);
			float3 ase_vertex3Pos = mul( unity_WorldToObject, float4( i.worldPos , 1 ) );
			float4 lerpResult6 = lerp( color5 , color1 , ( ase_vertex3Pos.y * _Float0 ));
			o.Albedo = lerpResult6.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18900
0;564;1283;315;1464.467;48.02421;1.708089;True;True
Node;AmplifyShaderEditor.PosVertexDataNode;4;-653.9949,329.1491;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;8;-603.9949,492.1491;Inherit;False;Property;_Float0;Float 0;0;0;Create;True;0;0;0;False;0;False;4.392158;0.2411764;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;1;-606.5063,-10.11533;Inherit;False;Constant;_Color0;Color 0;0;0;Create;True;0;0;0;False;0;False;0.490566,0.490566,0.490566,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;5;-609.9949,159.1491;Inherit;False;Constant;_Color1;Color 0;0;0;Create;True;0;0;0;False;0;False;0.2641509,0.2599976,0.2599976,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;7;-397.9949,338.1491;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;6;-289.9949,99.14911;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Smoke;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;True;0;False;Transparent;;Transparent;ForwardOnly;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;7;0;4;2
WireConnection;7;1;8;0
WireConnection;6;0;5;0
WireConnection;6;1;1;0
WireConnection;6;2;7;0
WireConnection;0;0;6;0
ASEEND*/
//CHKSM=865A131168C5E04ED2C048E10DE42DAE853DD82F