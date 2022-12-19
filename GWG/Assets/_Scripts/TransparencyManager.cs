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

        // if ((_thirdParty & (1 << obj.layer)) > 0)
            // MakeTransparent(renderer);

        iTween.FadeTo(obj, iTween.Hash(
            "alpha", targetAlpha,
            "time", fadeOutTime,
            // "onStartTarget", gameObject,
            // "onStart", "MakeTransparent",
            // "onStartParams", renderer,
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
        // var renderer = obj.GetComponent<Renderer>();
        // if(renderer != null && (_thirdParty & (1 << obj.layer)) > 0) 
            // MakeOpaque(renderer);
    }

    private void MakeTransparent(Renderer renderer)
    {
        foreach (var material in renderer.materials)
        {
            // material.SetFloat("_Mode", 3);
            material.SetFloat("_Mode", 2);
            material.SetInt("_ScrBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusDstAlpha);
            material.SetInt("_ZWrite", 0);
            material.DisableKeyword("_ALPHATEST_ON");
            material.EnableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = 3000;
        }
    }
    
    private void MakeOpaque(Renderer renderer)
    {
        foreach (var material in renderer.materials)
        {
            // material.SetFloat("_Mode", 0);
            material.SetInt("_ScrBlend", (int)UnityEngine.Rendering.BlendMode.One);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            material.SetInt("_ZWrite", 1);
            material.DisableKeyword("_ALPHATEST_ON");
            material.DisableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = -1;
        }
    }
}
