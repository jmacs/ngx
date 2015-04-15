using System;
using Microsoft.Xna.Framework;

namespace NgxLib.Collisions
{
    /// <summary>
    /// Represents a collision between two entities
    /// </summary>
    public struct Collision
    {
        public static readonly Collision None = new Collision();

        public readonly Vector2 Depth;

        public Collision(Vector2 depth)
        {
            Depth = depth;
        }

        public bool Colliding
        {
            get { return Depth != Vector2.Zero; }
        }

        public bool YAxis
        {
            get { return Math.Abs(Depth.X) > Math.Abs(Depth.Y); }
        }

        public bool XAxis
        {
            get { return Math.Abs(Depth.X) < Math.Abs(Depth.Y); }
        }

        public bool Top
        {
            get { return YAxis && Depth.Y < 0; }
        }

        public bool Bottom
        {
            get { return YAxis && Depth.Y > 0; }
        }

        public bool Right
        {
            get { return XAxis && Depth.X > 0; }
        }

        public bool Left
        {
            get { return XAxis && Depth.X < 0; }
        }
        
    }
}