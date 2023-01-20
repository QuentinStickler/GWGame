using System;
using System.Collections;
using System.Collections.Generic;
using cakeslice;
using UnityEngine;

public class InteractwithGame : MonoBehaviour, IInteractable
{
    
    public GameObject miniGame;
    private void Start()
    {
        GameEvents.OnStopInteractingWithMiniGame += DisableGame;
    }

    public void Interact()
    {
        miniGame.SetActive(true);
        GameEvents.OnInteractingWithMiniGame?.Invoke(false);
        Cursor.visible = true;
    }

    private void DisableGame()
    {
        miniGame.SetActive(false);
        Cursor.visible = false;
    }
    public void Glow()
    {
        throw new System.NotImplementedException();
    }
}
