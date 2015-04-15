using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NgxLib;

namespace Prototype.Systems
{
    public class MapRenderSystem : NgxRenderSystem
    {
        public override void Draw(SpriteBatch batch)
        {            
            if(!Context.MapManager.HasMap) return;

            var map = Context.MapManager.Map;
            map.CellAnimator.Update();

            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    var cell = map.Rows[y].Columns[x];

                    if (!cell.BackMask.IsEmpty)
                    {
                        batch.Draw(
                            map.Tileset.Texture,
                            cell.Area.Location,
                            cell.BackMask.Texels,
                            Color.White, 
                            0, // rotation 
                            Vector2.Zero, 
                            1, // scale
                            SpriteEffects.None, 
                            0); // depth
                    }

                    if (!cell.Tile.IsEmpty)
                    {
                        batch.Draw(
                        map.Tileset.Texture,
                        cell.Area.Location,
                        cell.Tile.Texels,
                        Color.White,
                        0, // rotation
                        Vector2.Zero,
                        1, // scale
                        SpriteEffects.None,
                        0);// depth
                    }
                }
            }
        }
    }
}