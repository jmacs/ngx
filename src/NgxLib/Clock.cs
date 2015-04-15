using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NgxLib
{
    public class Clock : IXmlSerializable
    {
        public const float MaxTime = float.MaxValue;

        private float _time;
        private float _max;
        private bool _enabled;

        public float Time
        {
            get { return _time; }
        }

        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        public bool IsZero
        {
            get { return _time <= 0; }
        }

        public bool IsMax
        {
            get { return _time >= MaxTime; }
        }

        public Clock() : this(0)
        {
        }

        public Clock(float time)
            : this(time, MaxTime, true)
        {
        }

        public Clock(float time, float max, bool enabled)
        {
            _max = max;
            _enabled = enabled;
            _time = time;
        }

        public bool Minus(float t)
        {
            if(!_enabled) return false;
            _time -= t;
            if (_time < 0) Reset();
            return IsZero;
        }

        public bool Add(float t)
        {
            if(!_enabled) return false;
            _time += t;
            if (_time > _max) _time = _max;
            return IsMax;
        }

        public void Reset()
        {
            _time = 0;
        }

        public override string ToString()
        {
            return _time.ToString();
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            _time = float.Parse(reader.GetAttribute("time"));
            _max = float.Parse(reader.GetAttribute("max"));
            _enabled = bool.Parse(reader.GetAttribute("enabled"));
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("time", _time.ToString());
            writer.WriteAttributeString("max", _max.ToString());
            writer.WriteAttributeString("enabled", _enabled.ToString());
        }
    }
}
