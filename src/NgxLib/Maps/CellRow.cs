using System.Collections.Generic;

namespace NgxLib.Maps
{
    public class CellRow
    {
        public int Y { get; private set; }
        public int Width { get; private set; }
        public NgxRectangle Area { get; private set; }
        public List<Cell> Columns { get; private set; }

        public CellRow(int width, int y)
        {
            Y = y;
            Width = width;
            Area = new NgxRectangle(0, y * Cell.PixelHeight, Width * Cell.PixelWidth, Cell.PixelHeight);
            Columns = new List<Cell>(Width);

            for (var x = 0; x < Width; x++)
            {
                Columns.Add(new Cell(x,y));
            }
        }

        public override string ToString()
        {
            return Area.ToString();
        }
    }
}