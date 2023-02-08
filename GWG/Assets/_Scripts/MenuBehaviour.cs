using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehaviour : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(1);
        WorldVariables.startOfGame = true;
    }

    public void StartGameAndSkipCutScene()
    {
        SceneManager.LoadScene(1);
        WorldVariables.startOfGame = false;
    }

    public void ToggleMenu()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
    
}
