using NgxLib;

namespace Prototype.Components
{
    [ObjectKey(Component.StagePortal)]
    public class Portal : NgxComponent
    {
        public int X;
        public int Y;
        public int MID;

        public override void Initialize()
        {
            X = 0;
            Y = 0;
            MID = 0;
        }
    }
}
