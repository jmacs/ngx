using Microsoft.Xna.Framework;
using NgxLib;

namespace Prototype.Components
{
    [ObjectKey(Component.OverworldNavigator)]
    public class WorldNavigator : NgxComponent
    {
        public Vector2 Position;
        public Vector2 Destination;
        public bool IsNew { get; set; }
        public bool IsAtDestination { get; set; }
        public float MoveSpeed { get; set; }

        public override void Initialize()
        {
            MoveSpeed = 1.5f;
            Position = Vector2.Zero;
            IsNew = true;
            Destination = Vector2.Zero;
            IsAtDestination = false;
        }
    }
}
