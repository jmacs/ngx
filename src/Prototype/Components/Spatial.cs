using System;
using Microsoft.Xna.Framework;
using NgxLib;

namespace Prototype.Components
{
    [ObjectKey(Component.Spatial)]
    public class Spatial : NgxComponent
    {
        public bool IsDirty { get; set; }

        private Vector2 _position;

        public Vector2 Position
        {
            get
            {
                return _position;
            }
            set
            {
                IsDirty = true;
                _position = value;
            }
        }

        public float X
        {
            get { return _position.X; }
            set
            {
                IsDirty = true;
                _position.X = value;
            }
        }

        public float Y
        {
            get { return _position.Y; }
            set
            {
                IsDirty = true;
                _position.Y = value;
            }
        }

        public override void Initialize()
        {
            Position = Vector2.Zero;
            IsDirty = true;
        }

        public override string ToString()
        {
            return Position.ToString();
        }

        public bool Move(ref Vector2 destination, float speed)
        {
            IsDirty = true;
            _position = _position.Move(destination, speed);
            return _position == destination;
        }
    }
}
