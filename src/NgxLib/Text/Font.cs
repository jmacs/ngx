using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NgxLib.Serialization;

namespace NgxLib.Text
{
    public class Font : IDisposable
    {
        public string Name { get; private set; }
        public Texture2D Texture { get; private set; }
        public CharacterMap CharacterMap { get; private set; }

        public static Font Create(GraphicsDevice graphics, string fontName)
        {
            var fontPath = string.Format("content/fonts/{0}.fnt", fontName);
            var fontFile = Serializer.Deserialize<FontFile>(fontPath);

            var textPath = string.Format("content/fonts/{0}", fontFile.Pages[0].File);

            Texture2D texture;
            using (var stream = Serializer.OpenReadStream(textPath))
            {
                texture = Texture2D.FromStream(graphics, stream);
            }

            var map = new CharacterMap(fontFile);

            return new Font(fontName, map, texture);
        }

        public Font(string name, CharacterMap characterMap, Texture2D texture)
        {
            Name = name;
            Texture = texture;
            CharacterMap = characterMap;
        }

        public void DrawText(SpriteBatch spriteBatch, NgxString s)
        {
            DrawText(spriteBatch, s.X, s.Y, s.Text, s.Color);
        }

        public void DrawText(SpriteBatch spriteBatch, int x, int y, string text, Color color)
        {
            int dx = x;
            int dy = y;
            for (var i = 0; i < text.Length; i++)
            {
                char c = text[i];
                FontChar fc;
                if (CharacterMap.TryGetValue(c, out fc))
                {
                    var sourceRectangle = new Rectangle(fc.X, fc.Y, fc.Width, fc.Height);
                    var position = new Vector2(dx + fc.XOffset, dy + fc.YOffset);

                    spriteBatch.Draw(
                        Texture,
                        position,
                        sourceRectangle,
                        color,
                        0, // rotation 
                        Vector2.Zero,
                        1, // scale
                        SpriteEffects.None,
                        0); // depth

                    dx += fc.XAdvance;
                }
            }
        }

        public void Dispose()
        {
            Texture.Dispose();
            CharacterMap.Clear();
            CharacterMap = null;
            Texture = null;
            Name = null;
        }
    }
}
