using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCBehaviour : MonoBehaviour, IInteractable
{
    public DialogueTrigger dialogueTrigger;


    public void Interact()
    {
        dialogueTrigger.TriggerDialogue();
    }

    public void Glow()
    {
        throw new System.NotImplementedException();
    }
}
