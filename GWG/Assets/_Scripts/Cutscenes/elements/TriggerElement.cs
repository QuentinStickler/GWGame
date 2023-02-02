using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Cutscenes
{
    [CreateAssetMenu(fileName = "CutScene", menuName = "Cutscenes/Elements/TriggerElement", order = 1)]
    [Serializable]
    public class TriggerElement : CutSceneElement
    {
        [SerializeField] public UnityEvent onTriggered;
        [SerializeField] public float delay;
        [SerializeField] public float waitFor;

        protected override void onStartElement(MonoBehaviour monoBehaviour, DialogueManager dialogueManager)
        {
            monoBehaviour.StartCoroutine(Trigger());
        }

        IEnumerator Trigger()
        {
            yield return new WaitForSeconds(delay);
            onTriggered.Invoke();
            yield return new WaitForSeconds(waitFor);
            onEndElement();
        }

        protected override void onEndElement()
        {
            isActive = false;
        }
    }
}
