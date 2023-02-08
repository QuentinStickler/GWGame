using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    private MenuBehaviour menuUI;
    // Start is called before the first frame update
    void Start()
    {
        menuUI = GameObject.Find("Menu").GetComponent<MenuBehaviour>();
        Debug.Log(menuUI.gameObject.name);
        menuUI.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            menuUI.ToggleMenu();
        }
    }
}
