using NgxLib;

namespace Prototype.Components
{
    [ObjectKey(Component.MapLocale)]
    public class MapLocale : NgxComponent
    {
        public int MID { get; set; }

        public override void Initialize()
        {
            MID = 0;
        }
    }
}
