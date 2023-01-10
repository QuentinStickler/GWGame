using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonMInigame : MonoBehaviour
{
    public int rightValue = 16;

    public void Game()
    {
        GameObject currentButton = EventSystem.current.currentSelectedGameObject;
        if (EventSystem.current.currentSelectedGameObject.name.Equals(rightValue.ToString()))
        {
            GameEvents.OnFoundRightSolutionToGhostGame?.Invoke();
            currentButton.GetComponent<Image>().color = Color.green;
        }
    }
}
