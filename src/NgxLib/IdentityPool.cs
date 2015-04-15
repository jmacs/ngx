using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NgxLib
{
    /// <summary>
    /// A pool of integers. Used for reusing any integer identifiers
    /// required by intenal meta-systems.
    /// </summary>
    public class IdentityPool : IXmlSerializable
    {
        private readonly Queue<int> _pool = new Queue<int>(1000);
        private int _allocated;
        private int _nextId = 1;

        public int Next()
        {
            if (_pool.Count > 0)
            {
                _allocated++;
                return _pool.Dequeue();
            }
            _allocated++;
            return _nextId++;
        }

        public void Release(int value)
        {
            if(value < 0) return;
            _pool.Enqueue(value);
            _allocated--;
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {        
            _nextId = int.Parse(reader.GetAttribute(0));
            reader.Read();
            if(reader.IsEmptyElement) return;
            var v = reader.ReadElementContentAsString();
            var p = v.Split(',');
            for (int i = 0; i < p.Length-1; i++)
            {
                _pool.Enqueue( int.Parse(p[i]) );
            }
        }

        public void WriteXml(XmlWriter writer)
        {           
            writer.WriteAttributeString("next", _nextId.ToString());

            writer.WriteStartElement("Pool");

            var pool = _pool.ToArray();
            for (int i = 0; i < pool.Length; i++)
            {
                writer.WriteRaw(pool[i] + ",");
            }
            writer.WriteEndElement();

        }
    }
}