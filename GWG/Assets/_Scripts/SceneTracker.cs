using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTracker : MonoBehaviour
{
    private int _currentScene = 1;
    private int _previousScene;
    public static SceneTracker Instance;
    
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
        _previousScene = _currentScene;
        _currentScene = SceneManager.GetActiveScene().buildIndex;

        Debug.Log("Current Scene: " + _currentScene);
        Debug.Log("Previous Scene: " + _previousScene);
        if (_currentScene == 2)
        {
            WorldVariables.startOfGame = true;
        }
    }

    public int GetCurrentScene()
    {
        return _currentScene;
    }
    
    public int GetPreviousScene()
    {
        return _previousScene;
    }
}
