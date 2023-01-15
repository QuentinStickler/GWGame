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

        public Animator animator;

        private Queue<SpokenWord> sentences;

        // Use this for initialization
        void Start()
        {
            sentences = new Queue<SpokenWord>();
        }

        public void StartDialogue(Dialogue dialogue)
        {
            animator.SetBool("IsOpen", true);

            sentences.Clear();
            Cursor.visible = true;

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
            animator.SetBool("IsOpen", false);
            Cursor.visible = false;
        }
    }
}