using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace NgxLib.Serialization
{
    public static class Serializer
    {
        public readonly static XmlSerializerNamespaces XmlNameSpace = new XmlSerializerNamespaces(new[] {
                new XmlQualifiedName(string.Empty, "urn:null")
            });

        private readonly static string Root = GetRoot();

        private static string GetRoot()
        {
            return new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName;
        }

        public static Stream OpenReadStream(string path)
        {
            return new FileStream(Path.Combine(Root, path), FileMode.Open, FileAccess.Read);
        }

        public static Stream OpenWriteStream(string path)
        {
            return new FileStream(Path.Combine(Root, path), FileMode.Create, FileAccess.Write);
        }

        public static T Deserialize<T>(string path) where T : class
        {
            using (var stream = OpenReadStream(path))
            {
                var serializer = new XmlSerializer(typeof(T));
                return serializer.Deserialize(stream) as T;
            }
        }

        public static void Serialize<T>(string path, T obj)
        {
            using (var stream = OpenWriteStream(path))
            {
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(stream, obj, XmlNameSpace);
            }
        }
    }
}
