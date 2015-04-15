using System;
using System.Xml;
using Microsoft.Xna.Framework;

namespace NgxLib.Serialization
{
    public static class XmlWriterExtensions
    {
        public static void WriteElement(this XmlWriter writer, string name, string value)
        {
            writer.WriteStartElement(name);
            writer.SetAttribute("value", value);
            writer.WriteEndElement();
        }

        public static void WriteElement(this XmlWriter writer, string name, int value)
        {
            writer.WriteStartElement(name);
            writer.SetAttribute("value", value);
            writer.WriteEndElement();
        }

        public static void WriteElement(this XmlWriter writer, string name, float value)
        {
            writer.WriteStartElement(name);
            writer.SetAttribute("value", value);
            writer.WriteEndElement();
        }

        public static void WriteElement(this XmlWriter writer, string name, byte value)
        {
            writer.WriteStartElement(name);
            writer.SetAttribute("value", value);
            writer.WriteEndElement();
        }

        public static void WriteElement(this XmlWriter writer, string name, bool value)
        {
            writer.WriteStartElement(name);
            writer.SetAttribute("value", value);
            writer.WriteEndElement();
        }

        public static void WriteElement(this XmlWriter writer, string name, DateTime value)
        {
            writer.WriteStartElement(name);
            writer.SetAttribute("value", value);
            writer.WriteEndElement();
        }

        public static void WriteElement(this XmlWriter writer, string name, Rectangle value)
        {
            writer.WriteStartElement(name);
            writer.SetAttribute("x", value.X);
            writer.SetAttribute("y", value.Y);
            writer.SetAttribute("w", value.Width);
            writer.SetAttribute("h", value.Height);
            writer.WriteEndElement();
        }

        public static void WriteElement(this XmlWriter writer, string name, NgxRectangle value)
        {
            writer.WriteStartElement(name);
            writer.SetAttribute("x", value.X);
            writer.SetAttribute("y", value.Y);
            writer.SetAttribute("w", value.Width);
            writer.SetAttribute("h", value.Height);
            writer.WriteEndElement();
        }

        public static void WriteElement(this XmlWriter writer, string name, Color value)
        {
            writer.WriteStartElement(name);
            writer.SetAttribute("hex", value.ToHex());
            writer.WriteEndElement();
        }

        public static void WriteElement(this XmlWriter writer, string name, Vector2 value)
        {
            writer.WriteStartElement(name);
            writer.SetAttribute("x", value.X);
            writer.SetAttribute("y", value.Y);
            writer.WriteEndElement();
        }

        public static void SetAttribute(this XmlWriter writer, string name, string value)
        {
            writer.WriteStartAttribute(name);
            writer.WriteValue(value);
            writer.WriteEndAttribute();
        }

        public static void SetAttribute(this XmlWriter writer, string name, bool value)
        {
            writer.WriteStartAttribute(name);
            writer.WriteValue(value);
            writer.WriteEndAttribute();
        }

        public static void SetAttribute(this XmlWriter writer, string name, int value)
        {
            writer.WriteStartAttribute(name);
            writer.WriteValue(value);
            writer.WriteEndAttribute();
        }

        public static void SetAttribute(this XmlWriter writer, string name, float value)
        {
            writer.WriteStartAttribute(name);
            writer.WriteValue(value);
            writer.WriteEndAttribute();
        }

        public static void SetAttribute(this XmlWriter writer, string name, byte value)
        {
            writer.WriteStartAttribute(name);
            writer.WriteValue(value);
            writer.WriteEndAttribute();
        }

        public static void SetAttribute(this XmlWriter writer, string name, DateTime value)
        {
            writer.WriteStartAttribute(name);
            writer.WriteValue(value);
            writer.WriteEndAttribute();
        }
    }
}
