using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Outline = cakeslice.Outline;

public class CollectibleBehaviour : MonoBehaviour, IInteractable
{

    float speed = 1.5f;
    private float rotationSpeed = 0.1f;
    float height = 0.5f;
    private Vector3 startingPos;

    public GameObject collectibleText;
    private GameObject collectibleNumber;

    private void Start()
    {
        startingPos = transform.position;
        collectibleNumber = GameObject.Find("CollectiblesFoundNumber");
        collectibleNumber.GetComponent<TextMeshProUGUI>().text = WorldVariables.GetCurrentlyCollectedNumber() + "/5";
    }

    void Update()
    {
        float newY = Mathf.Sin(Time.time * speed) * height;
        transform.position = new Vector3(startingPos.x, startingPos.y + newY, startingPos.z);
        transform.Rotate( new Vector3(0, rotationSpeed, 0) );
    }
    public void Interact()
    {
        StartCoroutine(DestroyText());
    }

    IEnumerator DestroyText()
    {
        collectibleText.SetActive(true);
        GameEvents.OnPickedUpCollectible?.Invoke();
        int currentlyCollectedNumber = WorldVariables.GetCurrentlyCollectedNumber() + 1;
        collectibleNumber.GetComponent<TextMeshProUGUI>().text = currentlyCollectedNumber + "/5";
        WorldVariables.SetCurrentlyCollected(1);
        GetComponent<MeshRenderer>().enabled = false;
        Destroy(GetComponent<Outline>());
        yield return new WaitForSeconds(3);
        collectibleText.SetActive(false);
    }

    public void Glow()
    {
        throw new System.NotImplementedException();
    }
}
