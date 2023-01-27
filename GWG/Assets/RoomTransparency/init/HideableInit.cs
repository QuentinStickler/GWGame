using UnityEngine;

namespace RoomTransparency
{
    public abstract class HideableInit<T> : MonoBehaviour where T : Hideable
    {
        public void Initialize()
        {
            Destroy(transform.gameObject.GetComponent<WallInit>());
            var originalParent = transform.gameObject.transform.parent;
            
            var newParent = new GameObject();
            newParent.transform.SetParent(originalParent);
            var wall = newParent.AddComponent<T>();
            
            newParent.transform.name = transform.gameObject.transform.name;
            transform.gameObject.transform.parent = newParent.transform;
            transform.gameObject.transform.name = "Opaque Material";

            var opaque = transform.gameObject;
            var transparent = Instantiate(opaque);

            Destroy(opaque.GetComponent<WallInit>());
            Destroy(transparent.GetComponent<WallInit>());
            
            transparent.name = "TransparentMaterial";
            transparent.transform.SetParent(wall.transform);

            foreach (var material in transparent.GetComponent<Renderer>().materials)
            {
                material.SetBlendMode(Extensions.BlendMode.Transparent);
                material.SetFloat("_Surface", 1f);
                Extensions.ChangeAlpha(material, 0.35f);
            }
            
            wall.setup(opaque, transparent);
        }
    }
}