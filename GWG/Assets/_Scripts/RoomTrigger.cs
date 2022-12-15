using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            RoomManager.Instance.FadeIn(this);
            RoomManager.Instance.FadeOut(this);
        }
    }
}
