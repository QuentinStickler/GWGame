using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomLoader : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform targetPosition;
     
    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneLoaded;
    }

    private void Awake()
    {
        // DontDestroyOnLoad(gameObject);
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneLoaded;
    }

    private void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0)
            player.position = targetPosition.position;
    }
}
