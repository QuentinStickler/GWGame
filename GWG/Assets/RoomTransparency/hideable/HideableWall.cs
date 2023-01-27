
namespace RoomTransparency
{
    public class HideableWall : Hideable
    {
        protected override bool disabledColliders()
        {
            return true;
        }
    }
}