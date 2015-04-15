using System.Collections.Generic;
using Microsoft.Xna.Framework;
using NgxLib.Tilesets;

namespace NgxLib.Maps
{
    public class Map
    {
        public int MID { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public CellAnimator CellAnimator { get; private set; }
        public List<CellRow> Rows { get; private set; }
        public NgxRectangle Area { get; private set; }
        public List<MapObject> Objects { get; private set; }
        public Tileset Tileset { get; set; }
        public Color BackgroundColor { get; set; }

        public Map(int mid, int width, int height)
        {
            MID = mid;
            Width = width;
            Height = height;
            Area = new NgxRectangle(0, 0, width * Cell.PixelWidth, height * Cell.PixelHeight);
            Objects = new List<MapObject>();
            CellAnimator = new CellAnimator(this);

            Rows = new List<CellRow>(Height);
            for (var y = 0; y < Height; y++)
            {
                Rows.Add(new CellRow(Width, y));
            }
        }

        public Cell PositionToCell(Vector2 position)
        {
            return PositionToCell((int)position.X, (int)position.Y);
        }

        public Cell PositionToCell(float x, float y)
        {
            if (x > -1 && x <= Area.Width && y > -1 && y <= Area.Height)
            {
                var row = (int) (y/Cell.PixelWidth);
                var col = (int) (x/Cell.PixelHeight);
                return GetCell(col, row);
            }
            return Cell.Void;
        }

        public Cell GetCell(int x, int y)
        {
            if (x > -1 && x < Width && y > -1 && y < Height)
            {
                return Rows[y].Columns[x];
            }
            return Cell.Void;
        }

        public void SetCell(int x, int y, int tileId)
        {
            var cell = GetCell(x, y);
            if (cell == Cell.Void) return;

            CellAnimator.RemoveCell(cell);

            if (tileId == 0)
            {
                cell.Tile = Tile.Empty;
            }
            else
            {
                cell.Tile = Tileset[tileId];
                CellAnimator.AddCell(cell);    
            }
        }

        public void SetMask(int x, int y, int tileId)
        {
            if (tileId == 0) return;
            var cell = GetCell(x, y);
            if (cell == Cell.Void) return;
            cell.BackMask = Tileset[tileId];
        }

        public bool IsPositionOutOfBounds(float x, float y)
        {
            if (x < 0 || y < 0) return true;
            if (x > Area.Right || y > Area.Bottom) return true;
            return false;
        }

        public Vector2 CellToPosition(int x, int y)
        {
            return GetCell(x, y).Area.Location;
        }

        public override string ToString()
        {
            return string.Format("{0} {{{1}x{2}}}", MID, Width, Height);
        }
    }
}