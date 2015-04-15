using NgxLib;

namespace Prototype.Components
{
    [ObjectKey(Component.BlockDecorator)]
    public class Decorator : NgxComponent
    {
        public int DecoratorType { get; set; }
        public int AdjacentX { get; set; }

        public override void Destroy()
        {
            DecoratorType = 0;
        }
    }
}
