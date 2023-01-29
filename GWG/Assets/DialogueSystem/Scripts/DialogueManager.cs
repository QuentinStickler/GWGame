using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class DialogueManager : MonoBehaviour
    {
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI dialogueText;
        public GameObject klausKreis;
        public GameObject shoulderCam;

        public Animator animator;

        private Queue<SpokenWord> sentences = new Queue<SpokenWord>();
        private Quaternion dialogPartnerRot;
        private GameObject currentDialogPartner;
        private GameObject ui;

        private Dialogue currentActiveDialogue;

        // Use this for initialization
        private void Start()
        {
            ui = GameObject.Find("UI");
        }

        public void StartDialogue(Dialogue dialogue, DialogueTrigger trigger)
        {
            Debug.Log("Starting Dialogue");
            ui.SetActive(false);
            animator.SetBool("IsOpen", true);
            if (trigger != null)
            {
                currentDialogPartner = trigger.transform.parent.gameObject;
                dialogPartnerRot = currentDialogPartner.transform.rotation;
                trigger.transform.parent.LookAt(klausKreis.transform);

                var dialogPartnerPos = currentDialogPartner.transform.position;
                dialogPartnerPos.y = klausKreis.transform.position.y;

                klausKreis.transform.LookAt(dialogPartnerPos);
                shoulderCam.SetActive(true);
            }

            if (sentences != null)
                sentences.Clear();
            Cursor.visible = true;

            currentActiveDialogue = dialogue;

            foreach (var dialogueSentence in dialogue.sentences)
            {
                sentences.Enqueue(dialogueSentence);
            }

            DisplayNextSentence();
        }

        public void DisplayNextSentence()
        {
            if (sentences.Count == 0)
            {
                EndDialogue();
                return;
            }

            var spokenWord = sentences.Dequeue();
            var sentence = spokenWord.sentence;
            var npcName = spokenWord.name;
            StopAllCoroutines();
            StartCoroutine(TypeSentence(npcName, sentence));
        }

        IEnumerator TypeSentence(string name, string sentence)
        {
            nameText.text = name;
            dialogueText.text = "";
            foreach (var letter in sentence.ToCharArray())
            {
                dialogueText.text += letter;
                yield return null;
            }
        }

        void EndDialogue()
        {
            Debug.Log("Ending Dialogue");
            animator.SetBool("IsOpen", false);
            Cursor.visible = false;
            GameEvents.OnFinishedDialogue?.Invoke(currentActiveDialogue);
            if (shoulderCam)
                shoulderCam.SetActive(false);
            if (currentDialogPartner)
                currentDialogPartner.transform.rotation = dialogPartnerRot;
            if(WorldVariables.isInCutscene == false)
                ui.SetActive(true);
        }
    }
}