using System;
using NgxLib;

namespace Prototype.Components
{
    [ObjectKey(Component.SpawnPoint)]
    public class SpawnPoint : NgxComponent
    {
        public int SpawnID { get; set; }
        public int UID { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public override void Initialize()
        {
            SpawnID = 0;
            UID = 0;
        }
    }
}
