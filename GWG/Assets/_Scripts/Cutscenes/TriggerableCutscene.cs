using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerableCutscene : Cutscene
{
    [SerializeField] private GameObject _triggerObject;

    private ITriggerable _trigger;

    private void OnEnable()
    {
        _trigger = _triggerObject.GetComponent<ITriggerable>();
        if (_trigger == null)
            throw new MissingComponentException("No ITriggerable found in object Trigger");
        
        _trigger.Triggered += OnStartCutscene;
    }

    private void Start()
    {
        Initialize();
        dialogueUI.Hide(0f);
    }

    private void OnStartCutscene()
    {
        WorldVariables.isInCutscene = true;
        startCutScene();
    }

    protected override IEnumerator runCutScene()
    {
        _trigger.Triggered -= OnStartCutscene;
        StartCoroutine(base.runCutScene());
        yield break;
    }
    
    private void OnDisable()
    {
        _trigger.Triggered -= OnStartCutscene;
    }
}
