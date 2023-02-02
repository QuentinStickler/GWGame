using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	public Dialogue dialogue;

	public void TriggerDialogue(bool lookAt)
	{
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue, this, lookAt);
	}

}
