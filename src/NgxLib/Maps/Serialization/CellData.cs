using System;

namespace NgxLib.Maps.Serialization
{
    [Serializable]
    public class CellData
    {
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}