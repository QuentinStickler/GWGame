using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Cutscenes;
using DefaultNamespace;
using UnityEngine;

[CreateAssetMenu(fileName = "CutScene", menuName = "Cutscenes/CutScene", order = 1)]
public class Cutscene : MonoBehaviour
{
    public string name;
    public PlayerController _playerController;
    public DialogueManager dialogueManager;
    public List<ElementsContainer> _cutSceneElements;
    protected CinematicBars dialogueUI;
    protected GameObject ui;
    private void Start()
    {
        Initialize();
        WorldVariables.isInCutscene = true;
        startCutScene();
    }

    protected void Initialize()
    {
        dialogueUI = GameObject.Find("Dialogue - UI").GetComponent<CinematicBars>();
        ui = GameObject.Find("UI");
        if (!dialogueManager)
            throw new NullReferenceException();
    }

    public void startCutScene()
    {
        //if (!WorldVariables.startOfGame)
            //return;
        ui.SetActive(false);
        Debug.Log(WorldVariables.startOfGame);
        Debug.Log("Running Cutscene");
        dialogueUI.Show(200,.3f);
        if (!_playerController)
        {
            Debug.LogWarning("_playerController not set");
            return;
        }
        _playerController.enabled = false;
        StartCoroutine(runCutScene());
    }

    protected virtual IEnumerator runCutScene()
    {
        foreach (var elementWrapper in _cutSceneElements)
        {
            CutSceneElement cutSceneElement;
            if (!elementWrapper.isDialogueElement)
            {
                if (!elementWrapper.isTriggerElement && elementWrapper.movementElement != null)
                    cutSceneElement = elementWrapper.movementElement;
                else
                {
                    cutSceneElement = elementWrapper.triggerElement;
                }
            }
            else
            {
                cutSceneElement = elementWrapper.dialogueElement;
            }
            if(cutSceneElement == null)
                continue;
            cutSceneElement.startElement(this, dialogueManager);
            yield return new WaitWhile(() => cutSceneElement.isActive);
        }
        _playerController.enabled = true;
        dialogueUI.Hide(.3f);
        WorldVariables.isInCutscene = false;
        WorldVariables.startOfGame = false;
        CutsceneManager.Instance.SetCutsceneSeen(this.name, true);
        ui.SetActive(true);
        Debug.Log("Cutscene done");
    }
    
    [Serializable]
    public class ElementsContainer
    {
        public bool isDialogueElement;
        public bool isTriggerElement;
        public DialogueElement dialogueElement;
        public MovementElement movementElement;
        public TriggerElement triggerElement;
    }
}
