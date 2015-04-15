using System;
using Microsoft.Xna.Framework;
using NgxLib;

namespace Prototype.Components
{
    [ObjectKey(Component.Projectile)]
    public class Projectile : NgxComponent
    {
        public bool IsMoving;

        public Vector2 Direction { get; set; }
    }
}
