using UnityEngine;
using UnityEngine.Serialization;

namespace RoomTransparency
{
    public class HideableWall : MonoBehaviour
    {
        [FormerlySerializedAs("transparentCopy")]
        public GameObject transparentMaterial;

        public GameObject opaqueOriginal;

        public void enable(bool val)
        {
            if (opaqueOriginal != null)
                opaqueOriginal.SetActive(val);
            if (transparentMaterial != null)
                transparentMaterial.SetActive(val);
        }

        public void makeTransparent(bool value)
        {
            opaqueOriginal.GetComponent<Renderer>().enabled = !value;
            opaqueOriginal.GetComponent<Collider>().enabled = !value;
            transparentMaterial.GetComponent<Renderer>().enabled = value;
            transparentMaterial.GetComponent<Collider>().enabled = value;
        }
    }
}