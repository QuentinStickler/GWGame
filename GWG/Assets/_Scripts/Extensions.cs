using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
