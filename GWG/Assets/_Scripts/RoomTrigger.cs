using System;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    public event Action<RoomTrigger> RoomEntered;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() == null)
            return;

        RoomEntered?.Invoke(this);
    }
}
