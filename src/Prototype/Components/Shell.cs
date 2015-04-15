using System;
using NgxLib;

namespace Prototype.Components
{
    [ObjectKey(Component.Shell)]
    public class Shell : DynamicComponent
    {
        public bool WakeUp { get; set; }
        public bool TorpedoMode { get; set; }
        public int TorpedoAnimation { get; set; }
        public int ShellAnimation { get; set; }

        public override void Enter()
        {
            Enabled = false;
            WakeUp = false;
            TorpedoMode = false;
        }

    }
}
