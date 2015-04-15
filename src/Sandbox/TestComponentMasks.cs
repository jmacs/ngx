using System;
using NgxLib;

namespace TilesetBuilder
{
    public class MyComponent
    {
        public const int ComponentId = 1;

        public Mask GetComponentMask()
        {
            return new Mask(ComponentId);
        }
    }

    public class TestComponentMasks
    {
        public void Execute()
        {
            var m1 = new Mask(257);
            var m2 = new Mask(16);

            var m3 = m1 + m2;
            Console.WriteLine("{0} + {1} = {2}", m1, m2, m3);

            var m4 = m3 - m2;
            Console.WriteLine("{0} - {1} = {2}", m3, m2, m4);
            Console.WriteLine("{0} == {1} ({2})", m1, m4, m1 == m4);

            var m5 = m1 + m2 + m3;
            Console.WriteLine("{0} + {1} + {2} = {3}", m1,m2,m3,m5);
            Console.WriteLine("{0} == {1} ({2})", m3, m5, m5 == m3);
            
        }
    }
}
