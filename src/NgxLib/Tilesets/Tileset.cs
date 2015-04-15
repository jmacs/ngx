using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NgxLib.Serialization;

namespace NgxLib.Tilesets
{
    /// <summary>
    /// A collection of rectangular drawing tiles
    /// </summary>
    public class Tileset : IXmlSerializable, IDisposable
    {
        private Tile[] _tiles;
        private Index<Animation> _animations;

        public int Size { get; private set; }
        public string TexturePath { get; private set; }
        public Texture2D Texture { get; private set; }

        protected Tileset()
        {
            _animations = new Index<Animation>();
            _tiles = new Tile[0];
        }

        public Tile this[int id]
        {
            get
            {
                if (id > Size) return null;
                return _tiles[id];
            }
        }

        public Animation GetAnimation(int id)
        {
            Animation animation;
            _animations.TryGetValue(id, out animation);
            return animation;
        }

        public void LoadTexture(GraphicsDevice device)
        {
            if (Texture != null) return;
            using (var stream = Serializer.OpenReadStream(TexturePath))
            {
                Texture = Texture2D.FromStream(device, stream);
            }
        }

        public override string ToString()
        {
            return string.Format("Tileset, {0}", TexturePath);
        }

        public void Dispose()
        {
            if (Texture == null) return;
            Texture.Dispose();
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            var xr = new XmlReaderWrapper(reader);
            Animation animation = null;

            do
            {
                if (xr.IsElement("Tileset"))
                {
                    TexturePath = xr.GetAttribute("texture");
                    Size = xr.GetAttributeInt("size");
                    _tiles = new Tile[Size];
                }
                else if (xr.IsElement("Tile"))
                {
                    var id = xr.GetAttributeInt("id");
                    var type = xr.GetAttributeInt("type");
                    var prop = xr.GetAttributeInt("prop");
                    var anim = xr.GetAttributeInt("anim");
                    var x = xr.GetAttributeInt("x");
                    var y = xr.GetAttributeInt("y");
                    var w = xr.GetAttributeInt("w");
                    var h = xr.GetAttributeInt("h");
                    var texels = new Rectangle(x, y, w, h);
                    var tile = new Tile(id, (TileType)type, prop, anim, texels);
                    _tiles[tile.Id] = tile;
                }
                else if (xr.IsElement("Animation"))
                {
                    var set = xr.GetAttribute("set");
                    var name = xr.GetAttribute("name");
                    var id = xr.GetAttributeInt("id");
                    animation = new Animation(id, set, name);
                    _animations.Add(id, animation);
                }
                else if (xr.IsElement("Frame"))
                {
                    var tile = int.Parse(xr.GetAttribute("tile"));
                    var time = xr.GetAttributeFloat("time");
                    var frame = new AnimationFrame(tile, time);
                    animation.Frames.Add(frame);
                }

            } while (xr.Read());
        }

        public void WriteXml(XmlWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
