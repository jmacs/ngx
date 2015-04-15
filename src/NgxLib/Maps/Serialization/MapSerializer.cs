using NgxLib.Serialization;
using NgxLib.Tilesets;

namespace NgxLib.Maps.Serialization
{
    public class MapSerializer
    {
        public TilesetCollection Tilesets { get; set; }

        public Map Deserialize(string path)
        {
            MapData md = null;
            using (var stream = Serializer.OpenReadStream(path))
            {
                md = MapData.Deserialize(stream);
            }

            var map = new Map(md.MID, md.Width, md.Height);

            //TODo: get the tileset out of the map
            var tilesetPath = string.Format("content/tilesets/{0}.xml", md.Tileset);
            map.Tileset = Tilesets[tilesetPath];

            var background = ColorExtensions.ToColor(md.BackgroundColor);
            map.BackgroundColor = background;

            for (int i = 0; i < md.Terrain.Count; i++)
            {
                var cell = md.Terrain[i];
                map.SetCell(cell.X, cell.Y, cell.Id);
            }

            for (int i = 0; i < md.BackMask.Count; i++)
            {
                var cell = md.BackMask[i];
                map.SetMask(cell.X, cell.Y, cell.Id);
            }

            for (int i = 0; i < md.Objects.Count; i++)
            {
                var item = md.Objects[i];
                var args = new PrefabArgs(item.X, item.Y, item.UID, item.MID, item.Args);
                var obj = new MapObject(item.Prefab, args);
                map.Objects.Add(obj);
            }

            return map;
        }

    }
}
