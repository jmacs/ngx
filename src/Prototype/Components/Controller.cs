using NgxLib;

namespace Prototype.Components
{
    [ObjectKey(Component.Controller)]
    public class Controller : NgxComponent
    {
        public int Values = Ctrl.None;

        public override void Initialize()
        {
            Reset();
        }

        public bool Any(int ctrl1, int ctrl2)
        {
            return Values.Any(ctrl1, ctrl2);
        }

        public bool Any(int ctrl1, int ctrl2, int ctrl3)
        {
            return Values.Any(ctrl1, ctrl2, ctrl3);
        }

        public bool Any(int ctrl1, int ctrl2, int ctrl3, int ctrl4)
        {
            return Values.Any(ctrl1, ctrl2, ctrl3, ctrl4);
        }

        public void Reset()
        {
            Values = Ctrl.None;
        }

        public bool Is(int ctrl)
        {
            return Values.Contains(ctrl);
        }

        public void Do(int ctrl)
        {
            Values |= ctrl;
        }

        public override string ToString()
        {
            return Values.ToString();
        }
    }
}