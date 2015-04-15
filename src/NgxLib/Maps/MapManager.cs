using System;
using Microsoft.Xna.Framework;
using NgxLib.Maps.Serialization;

namespace NgxLib.Maps
{
    public delegate void MapEventHandler(object sender, Map map);

    public class MapManager
    {
        protected Index<Map> Cache { get; set; }
        protected MapSerializer Serializer { get; set; }
        protected NgxContext Context { get; set; }
        public Color BackgroundColor { get; set; } 

        public Map Map { get; private set; }

        public bool HasMap { get; set; }

        public MapManager(NgxContext context)
        {
            Context = context;
            BackgroundColor = Color.Black;
            Cache = new Index<Map>();
            Serializer = new MapSerializer();
        }

        public void Transition(int mid)
        {
            Map next;

            if (!Cache.TryGetValue(mid, out next))
            {
                next = Load(string.Format("content/maps/{0}.map", mid));
            }

            if (next == null)
            {
                throw new Exception("Map is null! " + mid);
            }

            Map = next;
            Context.Engine.ClearColor = Map.BackgroundColor;
            HasMap = true;
        }

        public Map Load(string path)
        {
            Serializer.Tilesets = Context.Tilesets;
            var map = Serializer.Deserialize(path);
            Cache.Add(map.MID, map);
            return map;
        }

    }
}
