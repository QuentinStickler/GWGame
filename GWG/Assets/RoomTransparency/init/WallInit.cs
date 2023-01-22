using UnityEngine;

namespace RoomTransparency
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(MeshRenderer))]
    public class WallInit : HideableInit<HideableWall>
    {
        private void OnEnable()
        {
            Initialize();
        }
    }
}