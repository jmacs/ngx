using System.Xml.Serialization;

namespace TilesetBuilder
{
    public class Tile
    {
        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlAttribute("x")]        
        public int X { get; set; }
        
        [XmlAttribute("y")]        
        public int Y { get; set; }

        [XmlAttribute("w")]        
        public int Width { get; set; }

        [XmlAttribute("h")]        
        public int Height { get; set; }

        [XmlAttribute("type")]        
        public int Type { get; set; }
    }
}