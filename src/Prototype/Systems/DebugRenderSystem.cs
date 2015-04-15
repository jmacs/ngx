using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NgxLib;

namespace Prototype.Systems
{
    public class DebugRenderSystem : NgxRenderSystem
    {
        protected Surface DebugFrameRate { get; set; }
        protected Surface DebugFrameRateBk { get; set; }

        public override void Initialize()
        {
            DebugFrameRateBk = Debugger.CreateScreenSurface("DebugFrameRateBk", 60 * 8, 8, Color.Black, 0.3f);
            DebugFrameRate = Debugger.CreateScreenSurface("DebugFrameRate", 0, 8, Color.Green, 0.3f);
        }

        public override void Update()
        {            
            if (Debugger.Enabled)
            {
                var color = Color.Green;
                if (Time.IsSlow) { color = Color.Red; }
                else if (Time.FrameRate >= 50 && Time.FrameRate < 59) { color = Color.Purple; }
                else if (Time.FrameRate < 50) { color = Color.Purple; }
                DebugFrameRate.Color = color;
                DebugFrameRate.Width = Time.FrameRate * 8;
            }
        }        

        public override void Draw(SpriteBatch batch)
        {
            Debugger.DrawScreen(batch);
            Debugger.DrawText(batch);              
        }

        public override void Destroy()
        {
            Debugger.ScreenSurfaces.Remove("DebugFrameRateBk");
            Debugger.ScreenSurfaces.Remove("DebugFrameRate");
            Debugger.Unload();
        }
    }
}
