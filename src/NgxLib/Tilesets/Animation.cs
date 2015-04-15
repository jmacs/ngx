using System.Collections.Generic;

namespace NgxLib.Tilesets
{
    /// <summary>
    /// Contains animation frame data
    /// </summary>
    public class Animation
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Set { get; private set; }
        public string FullName { get; private set; }
        public List<AnimationFrame> Frames { get; private set; }

        public Animation(int id, string set, string name)
        {
            Id = id;
            Name = name;
            Set = set;
            Frames = new List<AnimationFrame>();
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}|{2}", Id, Set, Name);
        }
    }
}