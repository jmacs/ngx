using System;
using System.IO;
using System.Xml;

namespace NgxLib.Serialization
{
    //TODO: get rid of this class
    public class XmlReaderWrapper : IDisposable
    {
        private Stream stream;
        private XmlReader reader;

        public XmlReaderWrapper(string path)
        {
            stream = Serializer.OpenReadStream(path);
            reader = new XmlTextReader(stream);
            //textReader.WhitespaceHandling = WhitespaceHandling.None;
        }

        public XmlReaderWrapper(XmlReader xmlReader)
        {
            reader = xmlReader;
        }

        public bool Read()
        {
            return reader.Read();
        }

        public bool IsElement(string name)
        {
            return reader.Name.Equals(name)
                   && (reader.NodeType == XmlNodeType.Element);
        }

        public bool IsElement()
        {
            return reader.NodeType == XmlNodeType.Element;
        }

        public string GetAttribute(string name)
        {
            return reader.GetAttribute(name);
        }

        public int GetAttributeInt(string name)
        {
            var value = reader.GetAttribute(name);          
            return value == null ? 0 : int.Parse(value);
        }

        public byte GetAttributeByte(string name)
        {
            var value = reader.GetAttribute(name);
            return value == null ? (byte)0 : byte.Parse(value);
        }

        public float GetAttributeFloat(string name)
        {
            var value = reader.GetAttribute(name);
            return value == null ? 0 : float.Parse(value);
        }

        public bool GetAttributeBool(string name)
        {
            var value = reader.GetAttribute(name);
            return value == null ? false : bool.Parse(value);
        }

        public void Close()
        {
            if (stream != null) stream.Dispose();
            stream = null;
            reader = null;
        }

        public void Dispose()
        {
            Close();
        }
    }
}
