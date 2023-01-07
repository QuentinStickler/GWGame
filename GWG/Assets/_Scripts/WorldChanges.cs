using System;
using System.Collections;
using System.Collections.Generic;
using cakeslice;
using UnityEngine;

public class WorldChanges : MonoBehaviour
{
    public Light light;
    public Light ghostLight;
    public Camera camera;
    public GameObject lightBox;
    public Material skybox;
    private void Start()
    {
        GameEvents.OnFoundRightSolutionToGhostGame += SwitchLightMode;
    }

    private void SwitchLightMode()
    {
        lightBox.GetComponent<Outline>().eraseRenderer = true;
        lightBox.layer = 8;
        camera.backgroundColor = new Color(23,27,50);
        RenderSettings.skybox = skybox;
        
        light.type = LightType.Directional;
        light.intensity = 1;
        ghostLight.gameObject.SetActive(false);
        
        DynamicGI.UpdateEnvironment();
        camera.backgroundColor = new Color(23,27,50);
    }
}
