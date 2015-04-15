using Microsoft.Xna.Framework;

namespace NgxLib
{
    /// <summary>
    /// Represents a renderable rectangle with position and color.
    /// </summary>
    public class Surface
    {
        private Vector2 _position;

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public float X
        {
            get { return _position.X; }
            set { _position.X = value; }
        }

        public float Y
        {
            get { return _position.Y; }
            set { _position.Y = value; }
        }

        public float Height;
        public float Width;
        public Color Color;
        public float Alpha;

        public Surface()
        {
            Alpha = 0.5f;
            Color = Color.Red;
            Width = 16;
            Height = 16;
        }

        public Surface(int width, int height, Color color, float alpha = 0.5f)
        {
            Alpha = alpha;
            Color = color;
            Width = width;
            Height = height;
        }

    }
}