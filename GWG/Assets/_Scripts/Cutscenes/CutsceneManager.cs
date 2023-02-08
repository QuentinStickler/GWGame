using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager Instance;

    private Dictionary<string, bool> _cutsceneSeen = new Dictionary<string, bool>();
    
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        GameEvents.OnLoadScene += SetCutsceneSeen;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

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

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var cutscenes = FindObjectsOfType<Cutscene>();

        foreach (var cutscene in cutscenes)
        {
            if (_cutsceneSeen.ContainsKey(cutscene.name))
            {
                cutscene.enabled = false;
                cutscene.gameObject.SetActive(false);
                Debug.Log("Cutscene has already been seen, so it's been disabled: " + cutscene.name);
                continue;
            }
            
            _cutsceneSeen.Add(cutscene.name, false);
            Debug.Log("Cutscene added to Manager: " + cutscene.name);
        }

        Debug.Log("Seen Cutscenes: ");
        foreach (var cutscene in _cutsceneSeen)
        {
            Debug.Log(cutscene.Key);
        }
    }
    
    public void SetCutsceneSeen(bool seen)
    {
        if (seen == false)
        {
            _cutsceneSeen.Remove("Starting Cutscene");
        }
    }

    public void SetCutsceneSeen(string name, bool seen)
    {
        if (_cutsceneSeen.ContainsKey(name))
            _cutsceneSeen[name] = seen;
    }
}
