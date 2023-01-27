
namespace RoomTransparency
{
    public class HideableFloor : Hideable
    {
        protected override bool disabledColliders()
        {
            return false;
        }
    }
}