using System;
using System.Xml.Serialization;

namespace NgxLib.Text
{
    [Serializable]
    public class FontPage
    {
        [XmlAttribute("id")]
        public Int32 ID
        {
            get;
            set;
        }

        [XmlAttribute("file")]
        public String File
        {
            get;
            set;
        }
    }
}