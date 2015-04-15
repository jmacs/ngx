using Microsoft.Xna.Framework;
using NgxLib.Collisions;

namespace NgxLib
{
    public class NgxMessage : IRenewable
    {
        public long MessageKey { get; set; }
        public Vector2 Vector1 { get; set; }
        public Vector2 Vector2 { get; set; }
        public Vector2 Vector3 { get; set; }
        public Collision Collision { get; set; } 
        public int Entity1 { get; set; }
        public int Entity2 { get; set; }
        public int Entity3 { get; set; }
        public int Sound { get; set; }

        public void Initialize()
        {
            MessageKey = 0;
            Vector1 = Vector2.Zero;
            Vector2 = Vector2.Zero;
            Vector2 = Vector2.Zero;
            Entity1 = 0;
            Entity2 = 0;
            Entity3 = 0;
            Sound = 0;
        }

        public void Destroy()
        {
        }
    }
}