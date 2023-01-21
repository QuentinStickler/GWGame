using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public static class Extensions
{
    public static void ChangeAlpha(this Material material, float alphaValue)
    {
        var color = material.color;
        color.a = alphaValue;
        material.color = color;
    }

    public static List<GameObject> GetAllChildren(this GameObject gameObject, bool includeParent)
    {
        List<GameObject> list = new List<GameObject>();
        
        if (includeParent)
            list.Add(gameObject);
        
        foreach (Transform child in gameObject.transform)
        {
            list.AddRange(GetAllChildren(child.gameObject, true));
        }
            
        return list;
    }

    public enum BlendMode
    {
        Opaque,
        Cutout,
        Fade,
        Transparent
    }
    
    // public static void SetBlendMode(this Material material, BlendMode blendMode)
    // {
    //     switch (blendMode)
    //     {
    //         case BlendMode.Opaque:
    //             material.SetFloat("_Mode", 0);
    //             material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
    //             material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
    //             material.SetInt("_ZWrite", 1);
    //             material.DisableKeyword("_ALPHATEST_ON");
    //             material.DisableKeyword("_ALPHABLEND_ON");
    //             material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
    //             material.renderQueue = -1;
    //             break;
    //         case BlendMode.Cutout:
    //             material.SetFloat("_Mode", 1);
    //             material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
    //             material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
    //             material.SetInt("_ZWrite", 1);
    //             material.EnableKeyword("_ALPHATEST_ON");
    //             material.DisableKeyword("_ALPHABLEND_ON");
    //             material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
    //             material.renderQueue = 2450;
    //             break;
    //         case BlendMode.Fade:
    //             material.SetFloat("_Mode", 2);
    //             material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
    //             material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
    //             material.SetInt("_ZWrite", 0);
    //             material.DisableKeyword("_ALPHATEST_ON");
    //             material.EnableKeyword("_ALPHABLEND_ON");
    //             material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
    //             material.renderQueue = 3000;
    //             break;
    //         case BlendMode.Transparent:
    //             material.SetFloat("_Mode", 3);
    //             material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
    //             material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
    //             material.SetInt("_ZWrite", 0);
    //             material.DisableKeyword("_ALPHATEST_ON");
    //             material.DisableKeyword("_ALPHABLEND_ON");
    //             material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
    //             material.renderQueue = 3000;
    //             break;
    //     }

    public static void SetBlendMode(this Material material, BlendMode blendMode)
    {
        switch (blendMode)
        {
            case BlendMode.Opaque:
                material.SetFloat("_Surface", 0); // set the surface type to Opaque
                // material.SetFloat("_Blend", 0); // disable blending
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetFloat("_ZWrite", 1); // enable Z-writing
                material.DisableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = -1; // use the default render queue
                break;
            case BlendMode.Transparent:
                material.SetFloat("_Surface", 1); // set the surface type to Transparent
                material.SetFloat("_Blend", 0); // enable blending
                // material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetFloat("_ZWrite", 0); // disable Z-writing
                material.DisableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 3000; // set the render queue to 3000
                break;
        }
    }
}
