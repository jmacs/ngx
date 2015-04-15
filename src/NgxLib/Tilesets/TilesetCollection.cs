using System;
using Microsoft.Xna.Framework.Graphics;
using NgxLib.Serialization;

namespace NgxLib.Tilesets
{
    /// <summary>
    /// A collection of tilesets
    /// </summary>
    public class TilesetCollection : IDisposable
    {
        private readonly Hash<Tileset> _items = new Hash<Tileset>();

        public Tileset this[string name]
        {
            get { return _items[name]; }
        }

        public void Clear()
        {
            _items.Clear();
        }

        public Tileset Get(string name)
        {
            return _items[name];
        }

        public void LoadTextures(GraphicsDevice device)
        {
            foreach (var item in _items)
            {
                item.Value.LoadTexture(device);
            }
        }

        public void Dispose()
        {
            foreach (var item in _items)
            {
                item.Value.Dispose();
            }
        }

        public Tileset Load(string path)
        {
            if(_items.ContainsKey(path)) return _items[path];
            var tileset = Serializer.Deserialize<Tileset>(path);
            _items.Add(path, tileset);
            return tileset;
        }
    }
}
