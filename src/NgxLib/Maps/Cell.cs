using System;
using NgxLib.Tilesets;

namespace NgxLib.Maps
{
    public class Cell
    {
        public static readonly Cell Void = new Cell(-1,-1);

        public const int PixelWidth = 16;
        public const int PixelHeight = 16;

        public NgxRectangle Area { get; set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public Tile Tile { get; set; }
        public Tile BackMask { get; set; }

        // associate an entity with this cell
        public int Decorator { get; set; }
        public int DecoratorType { get; set; }

        // For animations
        public float Time { get; set; }
        public int Index { get; set; }

        public bool IsVoid
        {
            get { return X == -1 || Y == -1; }
        }


        public Cell(int x, int y)
        {
            X = x;
            Y = y;
            Area = new NgxRectangle(x * PixelHeight, y * PixelWidth, PixelWidth, PixelHeight);
            Tile = Tile.Empty;
            BackMask = Tile.Empty;
        } 

        public override string ToString()
        {
            return string.Format("{{{0},{1}}} {2}", X, Y, Area);
        }
    }
}