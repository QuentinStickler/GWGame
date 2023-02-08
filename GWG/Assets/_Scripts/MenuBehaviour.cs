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
        //WorldVariables.startOfGame = true;
        SceneManager.LoadScene(1);
        GameEvents.OnLoadScene?.Invoke(false);
    }

    public void StartGameAndSkipCutScene()
    {
        //WorldVariables.startOfGame = false;
        SceneManager.LoadScene(1);
        GameEvents.OnLoadScene?.Invoke(true);
    }

    public void ToggleMenu()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    
}
