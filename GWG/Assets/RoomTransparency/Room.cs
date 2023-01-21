using System;
using UnityEngine;

namespace RoomTransparency
{
    public class Room : MonoBehaviour
    {
        public event Action<Room> onRoomEnter;
        public bool initialState = false;
        private bool initialized;

        private void Update()
        {
            if (!initialized)
                showRoom(initialState);
            initialized = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.GetComponent<PlayerController>())
                return;
            onRoomEnter?.Invoke(this);
        }

        public void showRoom(bool val)
        {
            Debug.Log("Room: "+val);
            foreach (var child in Extensions.GetAllChildren(gameObject, false))
            {
                if (child.TryGetComponent<HideableWall>(out var wall))
                {
                    wall.enable(val);
                }
                else if (child.TryGetComponent<Renderer>(out var renderer))
                {
                    renderer.enabled = val;
                }
            }
        }
    }
}