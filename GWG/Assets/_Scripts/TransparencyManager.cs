using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TransparencyManager : MonoBehaviour
{
    public static TransparencyManager Instance;

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

    public IEnumerator FadeOut(List<GameObject> list, float targetOpacity, float time)
    {
        var timer = time;
        var materials = 
            list
            .Where(e => e.GetComponent<MeshRenderer>() != null)
            .Select(e => e.GetComponent<MeshRenderer>().material)
            .ToList();
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            var progress = timer / time;
            foreach (var material in materials)
                material.ChangeAlpha(1 - (1 - targetOpacity) * (1 - progress));
            
            yield return null;
        }

        if (targetOpacity == 0f)
            foreach (var obj in list)
                obj.SetActive(false);
    }
    
    public IEnumerator FadeIn(List<GameObject> list, float time)
    {
        foreach (var obj in list)
            obj.SetActive(true);
        
        var timer = time;
        var materials = 
            list
                .Where(e => e.GetComponent<MeshRenderer>() != null)
                .Select(e => e.GetComponent<MeshRenderer>().material)
                .ToList();
        
        var alphas = materials.Select(e => e.color.a).ToList();

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            var progress = timer / time;
            for (int i = 0; i < materials.Count; i++)
            {
                var material = materials[i];
                material.ChangeAlpha(alphas[i] + (1 - alphas[i]) * (1 - progress));
            }
            yield return null;
        }
    }
}
