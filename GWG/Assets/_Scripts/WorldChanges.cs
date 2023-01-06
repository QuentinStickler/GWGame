using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldChanges : MonoBehaviour
{
    public Light light;
    public Camera camera;
    public Material skybox;
    private void Start()
    {
        GameEvents.OnFoundRightSolutionToGhostGame += SwitchLightMode;
    }

    private void SwitchLightMode()
    {
        camera.backgroundColor = new Color(23,27,50);
        RenderSettings.skybox = skybox;
        light.type = LightType.Directional;
        light.intensity = 1;
        DynamicGI.UpdateEnvironment();
        camera.backgroundColor = new Color(23,27,50);
    }
}
