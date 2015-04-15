using Microsoft.Xna.Framework;

namespace NgxLib.Tilesets
{
    /// <summary>
    /// A single rectangular region of a tileset
    /// </summary>
    public class Tile
    {
        public static readonly Tile Empty = new Tile(0, TileType.Empty, 0, 0, Rectangle.Empty);

        public int Id { get; private set; }
        public TileType Type { get; private set; }
        public int Property { get; private set; }
        public int Animation { get; private set; }
        public Rectangle Texels { get; private set; }

        public Tile(int id, TileType type, int property, int animation, Rectangle rectangle)
        {
            Id = id;
            Property = property;
            Animation = animation;
            Type = type;
            Texels = rectangle;
        }

        public bool IsSolid
        {
            get { return Type == TileType.Solid; }
        }

        public bool IsEmpty
        {
            get { return Type == TileType.Empty; }
        }

        public bool IsMaterial
        {
            get { return Type == TileType.Material; }
        }

        public bool IsSlope
        {
            get { return Type == TileType.Slope; }
        }

        public bool IsOneWay
        {
            get { return Type == TileType.OneWay; }
        }

        public override string ToString()
        {
            return string.Format("Tile {{{0} {1} {2}}} {3}", Id, Type, Property, Texels);
        }
    }
}