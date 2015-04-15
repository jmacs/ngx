using System;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NgxLib;

namespace Prototype.Components
{
    [ObjectKey(Component.Sprite)]
    public class Sprite : NgxComponent
    {
        public SpriteLayer Layer { get; set; }
        public Vector2 Position;
        public string TilesetName { get; set; }
        public Rectangle Texels { get; set; }
        public Color Color { get; set; }
        public float Alpha { get; set; }
        public float Rotation { get; set; }
        public float Scale { get; set; }
        public SpriteEffects Effects { get; set; }
        public int Depth { get; set; }
        public int TileID { get; set; }

        [XmlIgnore]
        public Texture2D TextureCache { get; set; }

        public override void Initialize()
        {
            Layer = SpriteLayer.Front;
            Color = Color.White;
            Alpha = 1;
            Scale = 1;
            Texels = new Rectangle();
            Rotation = 0;
            Scale = 1;
            Effects = SpriteEffects.None;
            Depth = 0;
            TileID = 0;
        }

        public override void Destroy()
        {
            TextureCache = null;
        }

        public void PositionChanged(Vector2 position)
        {
            Position = position;
        }
    }
}
