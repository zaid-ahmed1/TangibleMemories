Shader "Custom/InsideSphereShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            Cull Front // Render only the inside faces of the sphere
            Lighting Off
            ZWrite On
            SetTexture [_MainTex] { combine texture }
        }
    }
}
