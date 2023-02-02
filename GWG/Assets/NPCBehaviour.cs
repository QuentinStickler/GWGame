using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCBehaviour : MonoBehaviour, IInteractable
{
    public DialogueTrigger dialogueTrigger;
    public bool lookAt = true;

    public void Interact()
    {
        dialogueTrigger.TriggerDialogue(lookAt);
    }

    public void Glow()
    {
        throw new System.NotImplementedException();
    }

    public void SetDialogueTrigger(DialogueTrigger trigger)
    {
        dialogueTrigger = trigger;
    }
}
