using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	public Dialogue dialogue;

	public void TriggerDialogue ()
	{
		Debug.Log("Triggering Dialogue");
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
	}

}
