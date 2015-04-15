using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using NgxLib;
using NgxLib.Serialization;

namespace TilesetBuilder
{
    public class TestSerializer
    {
        public void Execute()
        {
            var component = new DummyComponent {
                Value1 = "abc", 
                Value2 = 123, 
                Ignore = "xxxxx",
                Vector = new Vector2(3,3),
                MyClock = new Clock(122),
                MyColor = Color.Red,
            };
            component.Bind(1);
            component.Enabled = true;

            var serializer = new XmlSerializer(typeof(DummyComponent));

            using (var stream = Serializer.OpenWriteStream(@"c:\temp\DummyComponent.xml"))
            {
                serializer.Serialize(stream, component);
            }

            using (var stream = Serializer.OpenReadStream(@"c:\temp\DummyComponent.xml"))
            {
                var x = serializer.Deserialize(stream);
            }
            
        }
    }

    public class DummyComponent : NgxComponent
    {
        public const int ComponentId = 1;

        public Color MyColor { get; set; }
        public Clock MyClock { get; set; }
        public string Value1 { get; set; }
        public int Value2 { get; set; }
        public Vector2 Vector { get; set; }
        
        public float X
        {
            get { return Vector.X; }
        }

        [XmlIgnore]
        public object Ignore { get; set; }

    }
}