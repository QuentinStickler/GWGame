using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampsBehaviour : MonoBehaviour
{

    private List<GameObject> lampLight;
    void Start()
    {
        foreach (Transform child in transform)
        {
            GameObject grandChild = child.GetChild(0).gameObject;
            lampLight.Add(grandChild);
            grandChild.SetActive(false);
        }

        GameEvents.OnFoundRightSolutionToGhostGame += () => StartCoroutine(ActivateLights());
    }

    private IEnumerator ActivateLights()
    {
        foreach (var child in lampLight)
        {
            child.SetActive(true);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
