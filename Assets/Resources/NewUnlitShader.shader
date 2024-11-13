Shader "Custom/HoleStencilShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "Queue" = "Overlay" }
        Pass
        {
            ColorMask 0
            Stencil
            {
                Ref 1
                Comp always
                Pass replace
            }
        }
    }
}
