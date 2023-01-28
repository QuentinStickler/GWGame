using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Cutscenes;
using DefaultNamespace;
using UnityEngine;

[CreateAssetMenu(fileName = "CutScene", menuName = "Cutscenes/CutScene", order = 1)]
public class Cutscene : MonoBehaviour
{
    public PlayerController _playerController;
    public DialogueManager dialogueManager;
    public List<ElementsContainer> _cutSceneElements;
    private void Start()
    {
        startCutScene();
        if (!dialogueManager)
            throw new NullReferenceException();
    }

    public void startCutScene()
    {
        Debug.Log("Running Cutscene");
        if (!_playerController)
        {
            Debug.LogWarning("_playerController not set");
            return;
        }
        _playerController.enabled = false;
        StartCoroutine(runCutScene());
    }

    IEnumerator runCutScene()
    {
        foreach (var elementWrapper in _cutSceneElements)
        {
            CutSceneElement cutSceneElement;
            if(!elementWrapper.isDialogueElement && elementWrapper.MovementElement != null)
                cutSceneElement = elementWrapper.MovementElement;
            else
            {
                cutSceneElement = elementWrapper.dialogueElement;
            }
            if(cutSceneElement == null)
                continue;
            Debug.Log("StartElement");
            cutSceneElement.startElement(this, dialogueManager);
            yield return new WaitWhile(() => cutSceneElement.isActive);
        }
        _playerController.enabled = true;
        Debug.Log("Cutscene done");
    }
    
    [Serializable]
    public class ElementsContainer
    {
        public bool isDialogueElement;
        public DialogueElement dialogueElement;
        public MovementElement MovementElement;
    }
}
