using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonMInigame : MonoBehaviour
{
    public int rightValue = 16;

    public void Game()
    {
        if (EventSystem.current.currentSelectedGameObject.name.Equals(rightValue.ToString()))
            GameEvents.OnFoundRightSolutionToGhostGame?.Invoke();
    }
}
