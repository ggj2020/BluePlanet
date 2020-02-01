Shader "LWUnlit001"
{

	Properties{
		_MainTex("Texture", 2D) = "white" {}
	    _TerrainColor("TerrainColor", Color) = (1, 1, 1, 1)
		_ShadowColor("ShadowColor", Color) = (1, 1, 1, 1)
		_SandStrength("SandStrength", float) = 0.0



		_TerrainRimPower("TerrainRimPower", float) = 0.0
		_TerrainRimStrength("TerrainRimStrength", float) = 0.0
		_TerrainRimColor("TerrainRimColor", Color) = (1, 1, 1, 1)



		_OceanSpecularPower("OceanSpecularPower", float) = 0.0
		_OceanSpecularStrength("OceanSpecularStrength", float) = 0.0
		_OceanSpecularColor("OceanSpecularColor", Color) = (1, 1, 1, 1)



		_GlitterTex("GlitterTex", 2D) = "white"{}
		_GlitterThreshold("GlitterThreshold", float) = 0.0
		_GlitterColor("GlitterColor", Color) = (1, 1, 1, 1)
	}

		SubShader
	{
		Tags {"RenderPipeline" = "LightweightPipeline" "RenderType" = "Opaque" "Queue" = "Geometry"}

	   LOD 100

		Pass
		{
		   HLSLPROGRAM
			#pragma prefer_hlslcc gles
			#pragma exclude_renderers d3d11_9x
			#pragma vertex vert
			#pragma fragment frag

		   //include fog
		   #pragma multi_compile_fog


		   //--------------------------------------
		   // GPU Instancing


		   #pragma multi_compile_instancing

		   #pragma shader_feature _SAMPLE_GI
		   #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
		   #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
		   #pragma multi_compile _ _SHADOWS_SOFT


		   #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

			CBUFFER_START(UnityPerMaterial)
			sampler2D _MainTex;
			float4 _MainTex_ST;
			//TEXTURE2D(_MainTex);
			//SAMPLER(sampler_MainTex);
			CBUFFER_END

			half4 _MainTex_TexelSize;

			struct VertexInput
			{
			   float4 vertex : POSITION;
			   float2 uv : TEXCOORD0;
			   float3 normal : NORMAL;
			   UNITY_VERTEX_INPUT_INSTANCE_ID
			 };

		   struct VertexOutput
			 {
			  float4 vertex : SV_POSITION;
			  float2 uv : TEXCOORD0;
			  float fogCoord : TEXCOORD1;
			  float3 normal : NORMAL;
			  float4 shadowCoord : TEXCOORD2;

			  float3 worldPos : POSITION1;


			  UNITY_VERTEX_INPUT_INSTANCE_ID
			  UNITY_VERTEX_OUTPUT_STEREO
			 };

		 VertexOutput vert(VertexInput v)
		   {
			 VertexOutput o;
			 UNITY_SETUP_INSTANCE_ID(v);
			 UNITY_TRANSFER_INSTANCE_ID(v, o);
			 UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

			 o.vertex = TransformWorldToHClip(TransformObjectToWorld(v.vertex.xyz));
			 o.uv = v.uv;
			 o.normal = normalize(mul(v.normal,(float3x3)UNITY_MATRIX_I_M));
			 o.fogCoord = ComputeFogFactor(o.vertex.z);

			 //#ifdef _MAIN_LIGHT_SHADOWS
			 VertexPositionInputs vertexInput = GetVertexPositionInputs(v.vertex.xyz);
			 o.shadowCoord = GetShadowCoord(vertexInput);
			 //#endif


			 o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;

			return o;
		   }





		 float3 _TerrainColor;
		 float3 _ShadowColor;

		 float3 DiffuseColor(float3 N, float3 L)
		 {
			 N.y *= 0.3;
			 float NdotL = saturate(4 * dot(N, L));

			 float3 color = lerp(_ShadowColor, _TerrainColor, NdotL);
			 return color;
		 }



		 float3 nlerp(float3 n1, float3 n2, float t)
		 {
			 return normalize(lerp(n1, n2, t));
		 }

		 float _SandStrength;

		 float3 SandNormal(float2 uv, float3 N)
		 {
			 // Random vector
			 float3 random = tex2D(_MainTex, uv).rgb;
			 // Random direction
			 // [0,1]->[-1,+1]
			 float3 S = normalize(random * 2 - 1);

			 // Rotates N towards Ns based on _SandStrength
			 float3 Ns = nlerp(N, S, _SandStrength);
			 return Ns;
		 }



		 float _TerrainRimPower;
		 float _TerrainRimStrength;
		 float3 _TerrainRimColor;

		 float _OceanSpecularPower;
		 float _OceanSpecularStrength;
		 float3 _OceanSpecularColor;


		 sampler2D _GlitterTex;
		 float4 _GlitterTex_ST;
		 float _GlitterThreshold;
		 float3 _GlitterColor;

		 float3 GlitterSpecular(float2 uv, float3 N, float3 L, float3 V)
		 {
			 // Random glitter direction
			 float3 G = normalize(tex2D(_GlitterTex, uv).rgb * 2 - 1); // [0,1]->[-1,+1]

			 // Light that reflects on the glitter and hits the eye
			 float3 R = reflect(L, G);
			 float RdotV = max(0, dot(R, V));

			 // Only the strong ones (= small RdotV)
			 if (RdotV > _GlitterThreshold)
				 return 0;

			 return (1 - RdotV) * _GlitterColor;
		 }



		 float3 OceanSpecular(float3 N, float3 L, float3 V)
		 {
			 // Blinn-Phong
			 float3 H = normalize(V + L); // Half direction
			 float NdotH = max(0, dot(N, H));
			 float specular = pow(NdotH, _OceanSpecularPower) * _OceanSpecularStrength;
			 return specular * _OceanSpecularColor;
		 }

		 float3 RimLighting(float3 N, float3 V)
		 {
			 float rim = 1.0 - saturate(dot(N, V));
			 rim = saturate(pow(rim, _TerrainRimPower) * _TerrainRimStrength);
			 rim = max(rim, 0); // Never negative
			 return rim * _TerrainRimColor;
		 }



		half4 frag(VertexOutput i) : SV_Target
		{
			 UNITY_SETUP_INSTANCE_ID(i);
			 UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);

			 float3 viewDir = normalize(_WorldSpaceCameraPos.xyz - i.worldPos.xyz);


			 Light mainLight = GetMainLight(i.shadowCoord);


			 float2 uv = i.uv.xy * (_MainTex_ST.xy + _MainTex_ST.zw);
			 //float4 col = tex2D(_MainTex, uv);
			 float3 normal = i.normal;
			 normal = SandNormal(uv, i.normal);

			 float4 col = float4(DiffuseColor(normal, _MainLightPosition.xyz), 1.0);
			 float3 rimColor = RimLighting(normal, viewDir);
			 float3 oceanColor = OceanSpecular(normal, mainLight.direction, viewDir);
			 float3 glitterColor = GlitterSpecular(uv, normal, mainLight.direction, viewDir);

			 float3 specularColor = saturate(max(rimColor, oceanColor));
			 col = float4(col.rgb + specularColor + glitterColor, 1.0);


			 //float4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);

			 float NdotL = saturate(dot(_MainLightPosition.xyz, normal));
			 half3 ambient = SampleSH(normal);

			 col.rgb *= NdotL * _MainLightColor.rgb * mainLight.shadowAttenuation + ambient;
			 col.rgb = MixFog(col.rgb, i.fogCoord);

			 return col;
		}

		   ENDHLSL
	   }

	   Pass
   {
	   Name "ShadowCaster"

	   Tags{"LightMode" = "ShadowCaster"}

		   Cull Back

		   HLSLPROGRAM

			   // Required to compile gles 2.0 with standard srp library
			   #pragma prefer_hlslcc gles
			   #pragma exclude_renderers d3d11_9x
			   #pragma target 2.0

			   #pragma vertex ShadowPassVertex
			   #pragma fragment ShadowPassFragment

			   #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

			   CBUFFER_START(UnityPerMaterial)
			   CBUFFER_END

			   struct VertexInput
			   {
			   float4 vertex : POSITION;
			   UNITY_VERTEX_INPUT_INSTANCE_ID
			   };

			   struct VertexOutput
			   {
			   float4 vertex : SV_POSITION;

			   UNITY_VERTEX_INPUT_INSTANCE_ID
			   UNITY_VERTEX_OUTPUT_STEREO

			   };

			   VertexOutput ShadowPassVertex(VertexInput v)
			   {
				  VertexOutput o;
				  UNITY_SETUP_INSTANCE_ID(v);
				  UNITY_TRANSFER_INSTANCE_ID(v, o);
				  UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				  o.vertex = TransformWorldToHClip(TransformObjectToWorld(v.vertex.xyz));

				   return o;
			   }

			   half4 ShadowPassFragment(VertexOutput i) : SV_TARGET
			   {
				   UNITY_SETUP_INSTANCE_ID(i);
				   UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);

				   return 0;
			   }

			   ENDHLSL
		   }
   Pass
		   {
			   Name "DepthOnly"
			   Tags{"LightMode" = "DepthOnly"}

			   ZWrite On
			   ColorMask 0

			   Cull Back

			   HLSLPROGRAM

				   // Required to compile gles 2.0 with standard srp library
				   #pragma prefer_hlslcc gles
				   #pragma exclude_renderers d3d11_9x
				   #pragma target 2.0

				   //--------------------------------------
				   // GPU Instancing
				   #pragma multi_compile_instancing

				   #pragma vertex vert
				   #pragma fragment frag

				   #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

				   CBUFFER_START(UnityPerMaterial)
				   CBUFFER_END

				   struct VertexInput
				   {
					   float4 vertex : POSITION;

					   UNITY_VERTEX_INPUT_INSTANCE_ID
				   };

					   struct VertexOutput
					   {
					   float4 vertex : SV_POSITION;

					   UNITY_VERTEX_INPUT_INSTANCE_ID
					   UNITY_VERTEX_OUTPUT_STEREO

					   };

				   VertexOutput vert(VertexInput v)
				   {
					   VertexOutput o
					   UNITY_SETUP_INSTANCE_ID(v);
					   UNITY_TRANSFER_INSTANCE_ID(v, o);
					   UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

					   o.vertex = TransformWorldToHClip(TransformObjectToWorld(v.vertex.xyz));

					   return o;
				   }

				   half4 frag(VertexOutput IN) : SV_TARGET
				   {
					   return 0;
				   }
				   ENDHLSL
			   }

	}
}