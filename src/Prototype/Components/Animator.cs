using System.Xml;
using Microsoft.Xna.Framework;
using NgxLib;
using NgxLib.Serialization;

namespace Prototype.Components
{
    [ObjectKey(Component.Animator)]
    public class Animator : NgxComponent
    {
        private int _animation;
        public int Animation
        {
            get { return _animation; }
            set
            {
                if (_animation == value) return;
                _animation = value;
                Changed = true;
                FrameIndex = 0;                
            }
        }
        
        public bool Paused { get; set; }
        public bool Reversed { get; set; }
        public int FrameIndex { get; set; }
        public int FrameCount { get; set; }
        public bool Changed { get; set; }
        public Clock Clock { get; set; }
        public float FrameTime { get; set; }
        public Rectangle Texels { get; set; }

        public Animator()
        {
            Clock = new Clock();
        }

        public override void Initialize()
        {
            Clock.Reset();
            FrameTime = 0;
            Animation = 0;
            Changed = true;
            FrameCount = 0;
            Paused = false;
            Reversed = false;
            FrameIndex = 0;
            Texels = new Rectangle();
        }

        // TODO: move to system
        public bool NextFrame()
        {
            if (Paused) return false;

            if (FrameCount == 1) return false;

            if (!Clock.IsZero)
            {
                Clock.Minus(Time.Delta);
                return false;
            }

            Clock.Add(FrameTime);

            FrameIndex++;

            if (FrameIndex >= FrameCount)
            {
                FrameIndex = 0;
            }

            return true;
        }

        protected override void Serialize(XmlWriter writer)
        {
            writer.WriteElement("Animation", Animation);
            writer.WriteElement("Paused", Paused);
            writer.WriteElement("Reversed", Reversed);
            writer.WriteElement("FrameIndex", FrameIndex);
            writer.WriteElement("Texels", Texels);
            writer.WriteElement("FrameCount", FrameCount);
            writer.WriteElement("Changed", Changed);
            writer.WriteElement("FrameIndex", FrameIndex);
            writer.WriteElement("Clock", Clock.Time);
        }

        protected override void Deserialize(XmlReader reader)
        {
            reader.Read();
            Animation = reader.ReadElementInt();
            Paused = reader.ReadElementBool();
            Reversed = reader.ReadElementBool();
            FrameIndex = reader.ReadElementInt();
            Texels = reader.ReadElementRectangle();
            FrameCount = reader.ReadElementInt();
            Changed = reader.ReadElementBool();
            FrameIndex = reader.ReadElementInt();
            Clock = new Clock(reader.ReadElementFloat());
        }
    }

    
}
