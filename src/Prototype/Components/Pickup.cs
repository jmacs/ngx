using NgxLib;

namespace Prototype.Components
{
    [ObjectKey(Component.PickUp)]
    public class Pickup : NgxComponent
    {
        public int ItemID { get; set; }

        public override void Initialize()
        {
            ItemID = 0;
        }
    }
}
