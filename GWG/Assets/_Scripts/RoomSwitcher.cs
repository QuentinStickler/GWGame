 using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomSwitcher : MonoBehaviour
{
    public int sceneIndex;
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>() == null)
            return;
        SceneManager.LoadScene(sceneIndex);
    }
}
