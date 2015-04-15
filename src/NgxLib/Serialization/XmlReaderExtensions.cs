using System;
using System.Xml;
using Microsoft.Xna.Framework;

namespace NgxLib.Serialization
{
    public static class XmlReaderExtensions
    {
        public static string ReadElement(this XmlReader reader)
        {
            var value = reader.GetAttribute("value");
            reader.Read();
            return value;
        }

        public static bool ReadElementBool(this XmlReader reader)
        {
            var value = reader.GetAttributeBool("value");
            reader.Read();
            return value;
        }

        public static int ReadElementInt(this XmlReader reader)
        {
            var value = reader.GetAttributeInt("value");
            reader.Read();
            return value;
        }

        public static float ReadElementFloat(this XmlReader reader)
        {
            var value = reader.GetAttributeFloat("value");
            reader.Read();
            return value;
        }

        public static Vector2 ReadElementVector2(this XmlReader reader)
        {
            var x = reader.GetAttributeFloat("x");
            var y = reader.GetAttributeFloat("y");
            reader.Read();
            return new Vector2(x,y);
        }

        public static NgxRectangle ReadElementNgxRectangle(this XmlReader reader)
        {
            var x = reader.GetAttributeFloat("x");
            var y = reader.GetAttributeFloat("y");
            var w = reader.GetAttributeFloat("w");
            var h = reader.GetAttributeFloat("h");
            reader.Read();
            return new NgxRectangle(x, y, w, h);
        }

        public static Rectangle ReadElementRectangle(this XmlReader reader)
        {
            var x = reader.GetAttributeInt("x");
            var y = reader.GetAttributeInt("y");
            var w = reader.GetAttributeInt("w");
            var h = reader.GetAttributeInt("h");
            reader.Read();
            return new Rectangle(x, y, w, h);
        }

        public static Color ReadElementColor(this XmlReader reader)
        {
            var value = reader.GetAttribute("hex");
            reader.Read();
            return ColorExtensions.ToColor(value);
        }

        public static DateTime ReadElementDateTime(this XmlReader reader)
        {
            var value = reader.GetAttributeDateTime("value");
            reader.Read();
            return value;
        }

        public static bool GetAttributeBool(this XmlReader reader, string name)
        {
            bool value;
            bool.TryParse(reader.GetAttribute(name), out value);
            return value;
        }

        public static int GetAttributeInt(this XmlReader reader, string name)
        {
            int value;
            int.TryParse(reader.GetAttribute(name), out value);
            return value;
        }

        public static float GetAttributeFloat(this XmlReader reader, string name)
        {
            float value;
            float.TryParse(reader.GetAttribute(name), out value);
            return value;
        }

        public static byte GetAttributeByte(this XmlReader reader, string name)
        {
            byte value;
            byte.TryParse(reader.GetAttribute(name), out value);
            return value;
        }

        public static DateTime GetAttributeDateTime(this XmlReader reader, string name)
        {
            DateTime value;
            DateTime.TryParse(reader.GetAttribute(name), out value);
            return value;
        }
    }
}
