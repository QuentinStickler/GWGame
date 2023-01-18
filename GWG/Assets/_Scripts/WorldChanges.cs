using System;
using System.Collections;
using System.Collections.Generic;
using cakeslice;
using UnityEngine;

public class WorldChanges : MonoBehaviour
{
    public GameObject directionalLight;
    public GameObject klausLight;
    public Light ghostLight;
    public GameObject lightBox;
    public Material skybox;
    private void Start()
    {
        GameEvents.OnFoundRightSolutionToGhostGame += SwitchLightMode;
    }

    private void SwitchLightMode()
    {
        lightBox.GetComponent<Outline>().eraseRenderer = true;
        klausLight.SetActive(false);
        directionalLight.SetActive(true);
        ghostLight.gameObject.SetActive(false);
    }
}
