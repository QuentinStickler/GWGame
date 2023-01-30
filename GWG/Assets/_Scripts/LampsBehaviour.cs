using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampsBehaviour : MonoBehaviour
{

    private List<GameObject> lampLight;
    void Start()
    {
        lampLight = new List<GameObject>();
        foreach(GameObject lamp in GameObject.FindGameObjectsWithTag("Lamp"))
        {
            lampLight.Add(lamp);
            lamp.SetActive(false);
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
