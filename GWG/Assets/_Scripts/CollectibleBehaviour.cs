using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Outline = cakeslice.Outline;

public class CollectibleBehaviour : MonoBehaviour, IInteractable
{

    float speed = 1.5f;
    private float rotationSpeed = 0.1f;
    float height = 0.3f;
    private Vector3 startingPos;

    public GameObject collectibleText;

    private void Start()
    {
        startingPos = transform.position;
    }

    void Update()
    {
        float newY = Mathf.Sin(Time.time * speed);
        transform.position = new Vector3(startingPos.x, startingPos.y + newY, startingPos.z) * height;
        transform.Rotate( new Vector3(0, rotationSpeed, 0) );
    }
    public void Interact()
    {
        StartCoroutine(DestroyText());
    }

    IEnumerator DestroyText()
    {
        collectibleText.SetActive(true);
        GetComponent<MeshRenderer>().enabled = false;
        Destroy(GetComponent<Outline>());
        yield return new WaitForSeconds(3);
        collectibleText.SetActive(false);
        Destroy(gameObject);
    }
    public void Glow()
    {
        throw new System.NotImplementedException();
    }
}
