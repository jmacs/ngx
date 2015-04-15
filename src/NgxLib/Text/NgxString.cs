using Microsoft.Xna.Framework;

namespace NgxLib.Text
{
    public class NgxString
    {
        public int X;
        public int Y;
        public string Text;
        public Color Color;

        public NgxString()
        {
        }

        public NgxString(int x, int y, string text)
        {
            X = x;
            Y = y;
            Text = text;
        }

        public NgxString(int x, int y, string text, Color color)
        {
            X = x;
            Y = y;
            Text = text;
            Color = color;
        }

        public override string ToString()
        {
            return string.Format("{{{0},{1}}} '{2}'", X, Y, Text);
        }

        public void Set(string text, params object[] args)
        {
            Text = string.Format(text, args);
        }
    }
}