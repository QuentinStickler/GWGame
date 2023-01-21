using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Video;

public class TransparencyManager : MonoBehaviour
{
    public static TransparencyManager Instance;
    public LayerMask EnvironmentMask;
    public Action<GameObject, float> FadeOutComplete;
    public Action<GameObject, float> FadeInComplete;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void StartFadeOut(GameObject obj, float targetAlpha, float fadeOutTime)
    {
        var renderer = obj.GetComponent<Renderer>();
        if (renderer == null) return;
    
        foreach (var material in renderer.materials)
        {
            material.SetBlendMode(Extensions.BlendMode.Transparent);
            // material.SetBlendMode(Extensions.BlendMode.Fade);
        }
    
        iTween.FadeTo(obj, iTween.Hash(
            "NamedValueColor", "_BaseColor",
            "alpha", targetAlpha,
            "time", fadeOutTime,
            "onCompleteTarget", gameObject,
            "onComplete", "OnFadeOutComplete",
            "onCompleteParams", obj
        ));
    }
    
    private void OnFadeOutComplete(GameObject obj)
    {
        var renderer = obj.GetComponent<Renderer>();
        if (renderer.material.color.a < 0.01f)
            obj.SetActive(false);
    
        Debug.Log("fadeout complete");
    }
    
    public void StartFadeIn(GameObject obj, float fadeInTime)
    {
        var renderer = obj.GetComponent<Renderer>();
        if (renderer == null) return;
        
        if(!obj.activeSelf)
            obj.SetActive(true);
    
        iTween.FadeTo(obj, iTween.Hash(
            "NamedValueColor" ,"_BaseColor",
            "alpha", 1,
            "time", fadeInTime,
            "onCompleteTarget", gameObject,
            "onComplete", "OnFadeInComplete",
            "onCompleteParams", obj
        ));
    }
    
    private void OnFadeInComplete(GameObject obj)
    {
        var renderer = obj.GetComponent<Renderer>();
    
        foreach (var material in renderer.materials)
        {
            material.SetBlendMode(Extensions.BlendMode.Opaque);
        }
        
        Debug.Log("fadein complete");
    }
    
    public void StartFade(GameObject obj, float targetAlpha, float fadeTime, bool isOccluding = false, bool canSetActive = false)
    {
        var renderer = obj.GetComponent<Renderer>();
        if (renderer == null) return;

        foreach (var material in renderer.materials)
        {
            material.SetFloat("_FadeStartTime", Time.time);
            if (targetAlpha > 0.01f)
            {
                if (canSetActive && 1 - targetAlpha < 0.99f)
                    material.SetInt("_IsActive", 1);
            }
            else
                material.SetInt("_IsActive", 0);
            material.SetInt("_IsOccluding", isOccluding? 1 : 0);
            material.SetFloat("_FadeTime", fadeTime);
            material.SetFloat("_FadeSpeed", 1f / fadeTime);
            material.SetFloat("_Alpha", material.GetColor("_BaseColor").a);
            // material.SetFloat("_Alpha", 1);
            material.SetFloat("_TargetAlpha", targetAlpha);
        }
    }
}
