using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NgxLib;
using NgxLib.Gui;
using Prototype.Systems;

namespace Prototype.Contexts
{
    public class MenuContext : NgxContext
    {
        protected override void Load()
        {
            var tex = new Texture2D(Runtime.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            tex.SetData(new[] { Color.White });

            var pan1 = new Panel();
            pan1.ControlLayout = ControlLayout.Vertical;
            pan1.Color = Color.Gray;
            pan1.Margin = new Vector2(10, 10);
            pan1.Padding = new Vector2(10, 10);
            pan1.Width = 600;
            pan1.Height = 300;
            pan1.Texture = tex;
            Controls.Add(pan1);

            var pan2 = new Panel();
            pan2.WatchMouse = true;
            pan2.Color = Color.Green;
            pan2.Width = 100;
            pan2.Height = 100;
            pan2.Alpha = 0.8f; 
            pan2.Texture = tex;
            pan1.Controls.Add(pan2);

            var pan3 = new Panel();
            pan3.WatchMouse = true;
            pan3.Color = Color.Blue;
            pan3.Width = 100;
            pan3.Height = 100;
            pan3.Alpha = 0.9f; 
            pan3.Texture = tex;
            pan1.Controls.Add(pan3);

            var pan4 = new Panel();
            pan4.WatchMouse = true;
            pan4.Color = Color.Red;
            pan4.Width = 100;
            pan4.Height = 100;
            pan4.Alpha = 0.7f; 
            pan4.Texture = tex;
            pan1.Controls.Add(pan4);

            // Render Systems
            //Engine.Add(new GuiRenderSystem(NgxRenderLayer.Screen0));
        }
    }
}
