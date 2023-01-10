using UnityEngine;

public class TransparencyManager : MonoBehaviour
{
    public static TransparencyManager Instance;
    public LayerMask EnvironmentMask;

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
            // material.SetBlendMode(Extensions.BlendMode.Transparent);
            material.SetBlendMode(Extensions.BlendMode.Fade);
        }

        iTween.FadeTo(obj, iTween.Hash(
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
    }

    public void StartFadeIn(GameObject obj, float fadeInTime)
    {
        var renderer = obj.GetComponent<Renderer>();
        if (renderer == null) return;
        
        if(!obj.activeSelf)
            obj.SetActive(true);
        
        iTween.FadeTo(obj, iTween.Hash(
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
    }
}
