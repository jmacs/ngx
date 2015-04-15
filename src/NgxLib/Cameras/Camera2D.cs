using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NgxLib.Cameras
{
    /// <summary>
    /// Represents the 2D view port
    /// </summary>
    public class Camera2D
    {
        private NgxRectangle _viewport;
        private Matrix _transform;
        private Vector2 _position;

        public GraphicsDevice Graphics { get; private set; }
        public Vector2 ScreenCenter { get; protected set; }
        public float Rotation { get; set; }
        public Vector2 Origin { get; set; }
        public float Scale { get; set; }
        public float MoveSpeed { get; set; }
        public int Follow { get; set; }

        public Matrix Transform
        {
            get { return _transform; }
        }

        public NgxRectangle Viewport
        {
            get { return _viewport; }
        }

        public Vector2 Position
        {
            get { return _position; }
        }

        public Camera2D(GraphicsDevice graphics)
        {
            Graphics = graphics;
        }

        /// <summary>
        /// Called when the GameComponent needs to be initialized. 
        /// </summary>
        public void Initialize()
        {
            _viewport.Width = Graphics.Viewport.Width;
            _viewport.Height = Graphics.Viewport.Height;
            ScreenCenter = _viewport.Center;
            Scale = 1;
            MoveSpeed = 1.25f;
        }

        public void Update()
        {
            _transform = Matrix.Identity *
                        Matrix.CreateTranslation(-_position.X, -_position.Y, 0) *
                        Matrix.CreateRotationZ(Rotation) *
                        Matrix.CreateTranslation(Origin.X, Origin.Y, 0) *
                        Matrix.CreateScale(new Vector3(Scale, Scale, Scale));

            Origin = ScreenCenter / Scale;
        }

        public void Move(float x, float y)
        {
            var cx = _position.X + (x - _position.X) * MoveSpeed;
            var cy = _position.Y + (y - _position.Y) * MoveSpeed;
            SetPosition(cx, cy);
        }

        public void SetPosition(float x, float y)
        {
            _position.X = x;
            _position.Y = y;
            _viewport.X = x - (_viewport.Width * 0.5f) / Scale;
            _viewport.Y = y - (_viewport.Height * 0.5f) / Scale;
        }

        public Vector2 Translate(Vector2 location)
        {
            return Vector2.Transform(location, _transform);
        }
    }
}