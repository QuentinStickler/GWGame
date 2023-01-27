using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomLoader : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform stairsPosition;
    [SerializeField] private Transform parkPosition;
     
    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneLoaded;
    }

    private void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 1)
        {
            if (SceneTracker.Instance.GetPreviousScene() == 2 || SceneTracker.Instance.GetPreviousScene() == 3)
            {
                player.position = stairsPosition.position;
                return;
            }
            
            if (SceneTracker.Instance.GetPreviousScene() == 4)
            {
                player.position = parkPosition.position;
            }
        }
    }
}
