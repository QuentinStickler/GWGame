using System;
using DefaultNamespace;
using UnityEngine;

namespace _Scripts.Cutscenes
{
    [CreateAssetMenu(fileName = "CutScene", menuName = "Cutscenes/Elements/DialogueElement", order = 1)]
    [Serializable]
    public class DialogueElement : CutSceneElement
    {
        public Dialogue dialogue;
        public GameObject dialogueCamera;

        protected override void onStartElement(MonoBehaviour monoBehaviour, DialogueManager dialogueManager)
        {
            GameEvents.OnFinishedDialogue += onEndDialogue;
            isActive = true;
            dialogueManager.StartDialogue(dialogue, null);
            Debug.Log("Starting Dialogue");
            if (dialogueCamera != null)
                dialogueCamera.SetActive(true);
        }

        protected override void onEndElement()
        {
            if (dialogueCamera != null)
                dialogueCamera.SetActive(false);
            
        }

        private void onEndDialogue(Dialogue dialogue)
        {
            if (!this.dialogue.Equals(dialogue))
                return;
            isActive = false;
            onEndElement();
            GameEvents.OnFinishedDialogue -= onEndDialogue;
            Debug.Log("Dialogue Sequence has ended");
        }
    }
}