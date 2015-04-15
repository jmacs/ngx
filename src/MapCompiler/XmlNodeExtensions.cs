using System.Xml;

namespace MapCompiler
{
    public static class XmlNodeExtensions
    {
        public static string Select(this XmlNode node, string xpath)
        {
            return node.SelectSingleNode(xpath).Value;
        }

        public static int SelectInt(this XmlNode node, string xpath)
        {
            return int.Parse(node.SelectSingleNode(xpath).Value);
        }

        public static int GetAttributeInt(this XmlElement node, string name)
        {
            return int.Parse(node.GetAttribute(name));
        }
    }
}