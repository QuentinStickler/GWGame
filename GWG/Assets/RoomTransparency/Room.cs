using System;
using UnityEngine;

namespace RoomTransparency
{
    public class Room : MonoBehaviour
    {
        public bool initialState = false;
        private bool initialized;
        private bool visible;

        private void Update()
        {
            if (!initialized)
                showRoom(initialState);
            initialized = true;
        }

        public bool isVisible()
        {
            return visible;
        }

        public void showRoom(bool val)
        {
            visible = val;
            recursivelyHide(gameObject, val);
        }

        private void recursivelyHide(GameObject objectToHide, bool val)
        {
            if (objectToHide.TryGetComponent<HideableWall>(out var wall))
            {
                wall.enable(val);
                return;
            }

            if (objectToHide.TryGetComponent<MeshRenderer>(out var renderer))

                renderer.enabled = val;


            foreach (Transform child in objectToHide.transform)
            {
                recursivelyHide(child.gameObject, val);
            }
        }

        private bool hide(GameObject objectToHide, bool val)
        {
            if (objectToHide.TryGetComponent<HideableWall>(out var wall))
            {
                wall.enable(val);
                return true;
            }

            if (objectToHide.TryGetComponent<MeshRenderer>(out var renderer))
            {
                renderer.enabled = val;
                return true;
            }

            return false;
        }
    }
}