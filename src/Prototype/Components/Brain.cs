using NgxLib;

namespace Prototype.Components
{
    [ObjectKey(Component.Brain)]
    public class Brain : NgxComponent
    {
        public string BehaviorModule { get; set; }

        public int WalkDirection { get; set; }

        public override void Initialize()
        {
            BehaviorModule = null;
        }
    }
}
