using System;
using UnityEditor;
using UnityEditor.Rendering.BuiltIn.ShaderGraph;
using UnityEngine;

namespace RoomTransparency
{
    public class WallInit : MonoBehaviour
    {
        private void OnEnable()
        {
            Destroy(gameObject.GetComponent<WallInit>());
            var originalParent = transform.parent;
            
            var newParent = new GameObject();
            newParent.transform.SetParent(originalParent);
            var wall = newParent.AddComponent<HideableWall>();
            
            newParent.transform.name = transform.name;
            transform.parent = newParent.transform;
            transform.name = "Opaque Material";

            wall.opaqueOriginal = transform.gameObject;
            Destroy(wall.opaqueOriginal.GetComponent<WallInit>());

            wall.transparentMaterial = Instantiate(wall.opaqueOriginal);
            Destroy(wall.transparentMaterial.GetComponent<WallInit>());
            
            wall.transparentMaterial.name = "TransparentMaterial";
            wall.transparentMaterial.transform.SetParent(wall.transform);

            foreach (var material in wall.transparentMaterial.GetComponent<Renderer>().materials)
            {
                material.SetBlendMode(Extensions.BlendMode.Transparent);
                material.SetFloat("_Surface", 1f);
                Extensions.ChangeAlpha(material, 0.15f);
            }

            wall.transparentMaterial.GetComponent<Renderer>().enabled = false;
        }
    }
}