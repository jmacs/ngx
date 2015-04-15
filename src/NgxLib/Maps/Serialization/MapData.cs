using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace NgxLib.Maps.Serialization
{
    [Serializable]
    public class MapData
    {
        public int MID { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Tileset { get; set; }
        public string BackgroundColor { get; set; }

        public List<CellData> Terrain { get; set; }
        public List<CellData> BackMask { get; set; }
        public List<MapObjectData> Objects { get; set; }

        public MapData()
        {
            Terrain = new List<CellData>();
            BackMask = new List<CellData>();
            Objects = new List<MapObjectData>();
        }

        public void Serialize(Stream stream)
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);
        }

        public static MapData Deserialize(Stream stream)
        {
            var formatter = new BinaryFormatter();
            return formatter.Deserialize(stream) as MapData;
        }
    }
}
