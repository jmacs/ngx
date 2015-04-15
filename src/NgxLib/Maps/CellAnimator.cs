using System.Collections.Generic;
using NgxLib.Tilesets;

namespace NgxLib.Maps
{
    public class CellAnimator
    {
        protected Map Map { get; set; }
        protected Dictionary<Animation, List<Cell>> CellGroup { get; set; }

        public CellAnimator(Map map)
        {
            Map = map;
            CellGroup = new Dictionary<Animation, List<Cell>>();
        }
        
        public void AddCell(Cell cell)
        {
            var animationId = cell.Tile.Animation;
            if (animationId == 0) return;

            var animation = Map.Tileset.GetAnimation(animationId);

            if (!CellGroup.ContainsKey(animation))
            {
                CellGroup.Add(animation, new List<Cell>());
            }
            CellGroup[animation].Add(cell);
        }

        //TODO: fix this so if the cell doesnt have an animation id it still gets removed
        public void RemoveCell(Cell cell)
        {
            var animationId = cell.Tile.Animation;
            if (animationId == 0) return;

            var animation = Map.Tileset.GetAnimation(animationId);

            if (CellGroup.ContainsKey(animation))
            {
                var group = CellGroup[animation];
                group.Remove(cell);
                if (group.Count == 0) CellGroup.Remove(animation);
            }
        }

        public void Update()
        {
            foreach (var group in CellGroup)
            {
                // update the first cell and copy to the rest
                var animation = group.Key;
                var cells = group.Value;
                UpdateCell(animation, cells[0]);

                for (var i = 1; i < cells.Count; i++)
                {
                    cells[i].Index = cells[0].Index;
                    cells[i].Time = cells[0].Time;
                    cells[i].Tile = cells[0].Tile;
                }
            }
        }

        private void UpdateCell(Animation animation, Cell cell)
        {
            if (cell.Time > 0)
            {
                cell.Time -= Time.Delta;
            }
            else
            {
                cell.Index++;

                if (cell.Index >= animation.Frames.Count)
                {
                    cell.Index = 0;
                }

                var frame = animation.Frames[cell.Index];
                cell.Tile = Map.Tileset[frame.TileId];
                cell.Time = frame.FrameTime;
            }
        }
    }
}
