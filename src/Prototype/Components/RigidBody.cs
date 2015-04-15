using System;
using Microsoft.Xna.Framework;
using NgxLib;

namespace Prototype.Components
{
    [ObjectKey(Component.RigidBody)]
    public class RigidBody : NgxComponent
    {
        public bool IsMovingUp
        {
            get { return Velocity.Y < 0; }
        }

        public bool IsMovingDown
        {
            get { return Velocity.Y > 0.1; }
        }

        public bool IsMovingRight
        {
            get { return Velocity.X > 0; }
        }

        public bool IsMovingLeft
        {
            get { return Velocity.X < 0; }
        }

        public bool IsMoving
        {
            get { return Velocity.X != 0 || Velocity.Y != 0; }
        }

        public float Height
        {
            get { return Hitbox.Height; }
        }

        public float Width
        {
            get { return Hitbox.Width; }
        }

        public Side WallSensory { get; set; }

        public Side EntitySensory { get; set; }

        public bool NoClip { get; set; }

        public bool IsGrounded { get; set; }

        public string Prefab { get; set; }

        public float MaxSpeedX { get; set; }

        public NgxRectangle Hitbox;

        public Vector2 Position;

        public Vector2 Velocity;

        public Vector2 Acceleration;

        public override void Initialize()
        {
            MaxSpeedX = 1;
            Velocity = Vector2.Zero;
            Acceleration = Vector2.Zero;
            Position = Vector2.Zero;
            Prefab = null;
            WallSensory = Side.None;
            EntitySensory = Side.None;
            Hitbox = NgxRectangle.Empty;
            NoClip = false;
        }

        public void PositionChanged(Vector2 position)
        {
            Position = position;
            Hitbox.X = position.X;
            Hitbox.Y = position.Y - Height; // origin is bottom left
        }

        public override string ToString()
        {
            return string.Format("{0} p{{{1},{2}}} v{{{3},{4}}} a{{{5},{6}}}",
                Prefab, 
                Position.X, Position.Y, 
                Velocity.X, Velocity.Y,
                Acceleration.X, Acceleration.Y);
        }

        public Vector2 GetIntersectionDepth(NgxRectangle area)
        {
            return Hitbox.GetIntersectionDepth(area);
        }
    }
}
