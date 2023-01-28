using System;
using DefaultNamespace;
using UnityEngine;

namespace _Scripts.Cutscenes
{
    public abstract class CutSceneElement
    {
        public bool isActive;

        public void startElement(MonoBehaviour monoBehaviour, DialogueManager dialogueManager)
        {
            isActive = true;
            Debug.Log("Running onStartElement on " + GetType().Name);
            onStartElement(monoBehaviour, dialogueManager);
        }

        protected abstract void onStartElement(MonoBehaviour monoBehaviour, DialogueManager dialogueManager);
        protected abstract void onEndElement();
    }
}