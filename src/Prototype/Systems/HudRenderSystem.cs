using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NgxLib;
using NgxLib.Text;
using Prototype.Components;

namespace Prototype.Systems
{
    public class HudRenderSystem : NgxRenderSystem
    {
        protected NgxTable<Player> PlayerTable { get; set; }
        protected HudSystemData Hud { get; set; }
        protected Font Font { get; set; }
        
        public override void Initialize()
        {
            var graphics = Context.GraphicsDevice;
            Font = Font.Create(graphics, "Consolas");
            PlayerTable = Database.Table<Player>();
            
            Hud = new HudSystemData();

            Hud.Coins.Y = 10;
            Hud.Coins.X = 500;
            Hud.Coins.Color = Color.Black;
            Hud.Coins.Text = "Coins: 0";

            Hud.Score.Y = 10;
            Hud.Score.X = 700;
            Hud.Score.Color = Color.Black;
            Hud.Score.Text = "Score: 0";
        }

        public override void Draw(SpriteBatch batch)
        {            
            if(PlayerTable == null)return;
            var player = PlayerTable.First();
            if(player == null) return;

            Hud.SetCoins(player.Coins);
            Font.DrawText(batch, Hud.Coins);            

            Hud.SetScore(player.Score);
            Font.DrawText(batch, Hud.Score);            

        }
    }
}
