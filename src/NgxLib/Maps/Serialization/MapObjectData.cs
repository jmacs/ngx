using System;
using System.Collections.Generic;

namespace NgxLib.Maps.Serialization
{
    [Serializable]
    public class MapObjectData
    {
        public string Prefab { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int MID { get; set; }
        public int UID { get; set; }
        public IDictionary<string,string> Args { get; set; }
    
        public MapObjectData()
        {
            Args = new Dictionary<string, string>();
        }
    }
}