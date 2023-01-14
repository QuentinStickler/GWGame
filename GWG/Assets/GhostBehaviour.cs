using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBehaviour : MonoBehaviour, IInteractable
{
    float speed = 1f;
    private Vector3 startingPos;
    
    bool riddleSolved = false;

    public DialogueTrigger dialogueTeller1;
    public DialogueTrigger dialogueTeller2;
    public GameObject dialogueUi;

    private void Start()
    {
        GameEvents.OnFoundRightSolutionToGhostGame += UpdateDialogue;
        startingPos = transform.position;
    }

    void Update()
    {
        float newY = Mathf.Sin(Time.time * speed) * 0.5f + 1f;
        transform.position = new Vector3(startingPos.x, startingPos.y + newY, startingPos.z);
    }

    public void Interact()
    {
        //Bug: Dialoge werden nicht angezeigt
        //Wann ist Dialog zu Ende? Gibt es da einen Trigger?
        dialogueUi.SetActive(true);
        Cursor.visible = true;
        if(riddleSolved)
            dialogueTeller2.TriggerDialogue();
        else
        {
            dialogueTeller1.TriggerDialogue();
        }
    }

    private void UpdateDialogue()
    {
        riddleSolved = true;
        Debug.Log("Updated Dialog");
    }

    public void Glow()
    {
        throw new System.NotImplementedException();
    }
}
