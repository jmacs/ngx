using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NgxLib.Text;

namespace NgxLib
{
    public static class Debugger
    {
        public static bool Enabled { get; set; }
        public static Hash<Surface> WorldSurfaces { get; private set; }
        public static Hash<Surface> ScreenSurfaces { get; private set; }
        public static Hash<NgxString> Strings { get; set; }
        public static Font Font { get; private set; }
       
        private static Texture2D Texture { get; set; }

        static Debugger()
        {
            Strings = new Hash<NgxString>();
            WorldSurfaces = new Hash<Surface>();
            ScreenSurfaces = new Hash<Surface>();
        }

        public static void Initialize(GraphicsDevice graphics, string fontName)
        {
            Font = Font.Create(graphics, fontName);
            Texture = new Texture2D(graphics, 1, 1, false, SurfaceFormat.Color);
            Texture.SetData(new[] { Color.White });
        }

        public static void Unload()
        {
            Strings.Clear();
            WorldSurfaces.Clear();
            ScreenSurfaces.Clear();
        }

        public static void Destroy()
        {
            Texture.Dispose();
            Font.Dispose();
        }

        public static void DrawWorld(SpriteBatch spriteBatch)
        {
            if(!Enabled) return;

            foreach (var item in WorldSurfaces)
            {
                var surface = item.Value;
                var rect = new Rectangle(
                    (int)surface.X, (int)(surface.Y - surface.Height), 
                    (int)surface.Width, (int)surface.Height);
                spriteBatch.Draw(
                    Texture, 
                    rect, 
                    surface.Color * surface.Alpha); 
            }
        }

        public static void DrawScreen(SpriteBatch spriteBatch)
        {
            if (!Enabled) return;

            foreach (var item in ScreenSurfaces)
            {
                var surface = item.Value;
                var rect = new Rectangle(
                    (int)surface.X, (int)surface.Y,
                    (int)surface.Width, (int)surface.Height);
                spriteBatch.Draw(
                    Texture,
                    rect,
                    surface.Color * surface.Alpha);
            }
        }

        public static void DrawText(SpriteBatch spriteBatch)
        {
            if (!Enabled) return;

            foreach (var item in Strings)
            {
                Font.DrawText(spriteBatch, item.Value);                
            }
        }

        public static Surface CreateWorldSurface(string name, int width, int height, Color color, float alpha)
        {
            var surface = new Surface(width, height, color, alpha);
            WorldSurfaces.Add(name, surface);
            return surface;
        }

        public static NgxString CreateString(string name, int x, int y, string text, Color color)
        {
            var s = new NgxString(x, y, text, color);
            Strings.Add(name, s);
            return s;
        }

        public static Surface CreateScreenSurface(string name, int width, int height, Color color, float alpha)
        {
            var surface = new Surface(width, height, color, alpha);
            ScreenSurfaces.Add(name, surface);
            return surface;
        }
    }
}
