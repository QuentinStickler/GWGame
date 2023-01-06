using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractwithGame : MonoBehaviour, IInteractable
{
    private void Start()
    {
        GameEvents.OnStopInteractingWithMiniGame += DisableGame;
    }

    public GameObject miniGame;
    public void Interact()
    {
        miniGame.SetActive(true);
        GameEvents.OnInteractingWithMiniGame?.Invoke(false);
    }

    private void DisableGame()
    {
        miniGame.SetActive(false);
    }
    public void Glow()
    {
        throw new System.NotImplementedException();
    }
}
