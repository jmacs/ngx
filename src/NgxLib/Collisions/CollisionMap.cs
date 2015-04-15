using System.Collections.Generic;

namespace NgxLib.Collisions
{
    /// <summary>
    /// Entities don't collide, thier components do. This
    /// class maps the types of component collisions that 
    /// are possible in the game and the message that should 
    /// be fired when a collision between the two components happen.
    /// </summary>
    public static class CollisionMap
    {
        public static List<CollisionPair> CollisionPairs { get; private set; }

        static CollisionMap()
        {
            CollisionPairs = new List<CollisionPair>();
        }

        public static void Register<T1,T2>(int topic) 
            where T1 : NgxComponent, new() where T2 : NgxComponent, new()
        {
            var m1 = Ngx.Components.Get<T1>().ComponentMask;
            var m2 = Ngx.Components.Get<T2>().ComponentMask;
            CollisionPairs.Add(new CollisionPair(m1, m2, topic));
        }
    }
}
