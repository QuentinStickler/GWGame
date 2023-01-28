using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CinematicBars : MonoBehaviour
{

    private RectTransform topBar, bottomBar;
    private float changeSizeAmount;
    private void Awake()
    {
        GameObject gameObject = new GameObject("topBar", typeof(Image));
        gameObject.transform.SetParent(transform,false);
        gameObject.GetComponent<Image>().color = Color.black;
        topBar = gameObject.GetComponent<RectTransform>();
        topBar.anchorMin = new Vector2(0, 1);
        topBar.anchorMax = new Vector2(1, 1);
        topBar.sizeDelta = new Vector2(0, 300);
        
        GameObject gameObjectBottom = new GameObject("bottomBar", typeof(Image));
        gameObjectBottom.transform.SetParent(transform,false);
        gameObjectBottom.GetComponent<Image>().color = Color.black;
        bottomBar = gameObjectBottom.GetComponent<RectTransform>();
        bottomBar.anchorMin = new Vector2(0, 0);
        bottomBar.anchorMax = new Vector2(1, 0);
        bottomBar.sizeDelta = new Vector2(0, 300);
    }

}
