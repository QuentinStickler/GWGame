using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMouse : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Cursor.visible = false;
    }

}
