using System;
using Microsoft.Xna.Framework;
using NgxLib;

namespace Prototype.Components
{
    [ObjectKey(Component.WarpPipeConnection)]
    public class WarpPipeConnection : NgxComponent
    {
        public int PipeID { get; set; }
        public int Connection { get; set; }
        public int MID { get; set; }
        public bool CanEnter { get; set; }
        public Vector2 Position { get; set; }

    }
}
