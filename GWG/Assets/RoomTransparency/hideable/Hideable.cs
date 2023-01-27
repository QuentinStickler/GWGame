using System;
using UnityEngine;

namespace RoomTransparency
{
    public abstract class Hideable : MonoBehaviour
    {
        public GameObject transparentMaterial;

        public GameObject opaqueOriginal;

        private bool enabled;
        
        public void setup(GameObject opaque, GameObject transparent)
        {
            opaqueOriginal = opaque;
            transparentMaterial = transparent;

            transparent.GetComponent<MeshRenderer>().enabled = false;
        }

        public void enable(bool val)
        {
            if (opaqueOriginal)
            {
                opaqueOriginal.GetComponent<MeshRenderer>().enabled = val;
                if(disabledColliders())
                    opaqueOriginal.GetComponent<Collider>().enabled = val;
            }
            if (transparentMaterial)
            {
                transparentMaterial.GetComponent<MeshRenderer>().enabled = val;
                if(disabledColliders())
                    transparentMaterial.GetComponent<Collider>().enabled = val;
            }
            enabled = val;
            if (val)
                makeTransparent(false);
        }

        protected abstract bool disabledColliders();

        public void makeTransparent(bool value)
        {
            if (!enabled)
                return;
            if (!opaqueOriginal)
                throw new Exception("opaqueOriginal of " + gameObject.name + " in room " +
                                    gameObject.transform.parent.name + " is null");
            if (!transparentMaterial)
                throw new Exception("transparentMaterial of " + gameObject.name + " in room " +
                                    gameObject.transform.parent.name + " is null");
            opaqueOriginal.GetComponent<MeshRenderer>().enabled = !value;
            //opaqueOriginal.GetComponent<Collider>().enabled = !value;
            transparentMaterial.GetComponent<MeshRenderer>().enabled = value;
            //transparentMaterial.GetComponent<Collider>().enabled = value;
        }
    }
}