using NgxLib;

namespace Prototype.Components
{
    [ObjectKey(Component.Interactor)]
    public class Interactor : NgxComponent
    {
        public bool Kickable { get; set; }
        public bool Carryable { get; set; }
    }
}
