using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NgxLib.Gui
{
    public class Panel : Control
    {
        public Texture2D Texture { get; set; }
        public Color Color { get; set; }
        public float Alpha { get; set; }

        public Panel()
        {
            Alpha = 1.0f;
        }

        public override void Render(SpriteBatch batch)
        {
            if (Texture != null)
            {
                batch.Draw(Texture, Margin + Position, Surface, Color * Alpha);
            }
            Controls.Render(batch);
        }

        private Color OldColor;

        protected override void OnMouseEnter()
        {
            OldColor = Color;
            Color = Color.Purple;
        }

        protected override void OnMouseLeave()
        {
            Color = OldColor;
        }

        protected override void OnMouseDown(MouseState mouse)
        {
            Color = Color.White;
        }
    }
}
