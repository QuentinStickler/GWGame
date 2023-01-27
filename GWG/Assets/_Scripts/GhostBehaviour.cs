using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBehaviour : MonoBehaviour, IInteractable
{
    float speed = 1f;
    private Vector3 startingPos;
    
    public bool riddleSolved = false;

    public DialogueTrigger dialogueTeller1;
    public DialogueTrigger dialogueTeller2;
    public GameObject boardText;
    public GameObject ghostText;
    public GameObject questText;
    public GameObject questImage;

    private GameObject powerGenerator;

    private void Start()
    {
        GameEvents.OnFoundRightSolutionToGhostGame += UpdateDialogue;
        GameEvents.OnFinishedDialogue += DespawnAndDropLoot;
        startingPos = transform.position;
        powerGenerator = GameObject.Find("PowerGenerator");
    }

    void Update()
    {
        float newY = Mathf.Sin(Time.time * speed) * 0.5f + 1f;
        transform.position = new Vector3(startingPos.x, startingPos.y + newY, startingPos.z);
    }

    public void Interact()
    {
        if (riddleSolved)
        {
            dialogueTeller2.TriggerDialogue();
        }
        else
        {
            dialogueTeller1.TriggerDialogue();
        }
    }

    public void DespawnAndDropLoot()
    {
        Debug.Log(powerGenerator == null);
        powerGenerator.layer = LayerMask.NameToLayer("Interactable");
        if (!riddleSolved) return;
        ghostText.SetActive(true);
        StartCoroutine(DeactivateText());
    }

    IEnumerator DeactivateText()
    {
        yield return new WaitForSeconds(5f);
        ghostText.SetActive(false);
        boardText.SetActive(true);
        questText.SetActive(false);
        questImage.SetActive(false);
        Destroy(gameObject);
    }
    private void UpdateDialogue()
    {
        riddleSolved = true;
    }

    public void Glow()
    {
        throw new System.NotImplementedException();
    }
}
