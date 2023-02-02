using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomLoader : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform camera;
    [SerializeField] private Transform stairsPosition;
    [SerializeField] private Transform stairsCameraPosition;
    [SerializeField] private Transform parkPosition;
    [SerializeField] private Transform parkCameraPosition;
     
    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneLoaded;
    }

    // private void Awake()
    // {
    //     player = GameObject.Find("KlausKreis").transform;
    //     camera = Camera.main.transform;
    // }

    private void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 1)
        {
            if (SceneTracker.Instance.GetPreviousScene() == 2 || SceneTracker.Instance.GetPreviousScene() == 3)
            {
                player.position = stairsPosition.position;
                camera.position = stairsCameraPosition.position;
                return;
            }
            
            if (SceneTracker.Instance.GetPreviousScene() == 4)
            {
                player.position = parkPosition.position;
                camera.position = parkCameraPosition.position;
            }
        }
    }
}
