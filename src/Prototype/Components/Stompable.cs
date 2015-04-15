using System;
using NgxLib;

namespace Prototype.Components
{
    [ObjectKey(Component.Stompable)]
    public class Stompable : DynamicComponent
    {
        public override void Enter()
        {
            Enabled = false;
        }

        public int StompAnimation { get; set; }
    }
}
