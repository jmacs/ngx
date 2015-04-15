using NgxLib;

namespace Prototype.Components
{
    [ObjectKey(Component.Dispencer)]
    public class Dispencer : NgxComponent
    {
        public int ItemID { get; set; }

        public override void Initialize()
        {
            ItemID = 0;
        }
    }
}
