using NgxLib;

namespace Prototype.Components
{
    [ObjectKey(Component.JumpBoots)]
    public class JumpBoots : DynamicComponent
    {
        public float JumpTimer { get; set; }
        public float JumpImpulse { get; set; }
        public float JumpResponseTime { get; set; }
        public float JumpDrift { get; set; }
        public float AirDrag { get; set; }
        public int JumpAnimation { get; set; }

        public override void Initialize()
        {
            Duration = 0;
            JumpDrift = 0;
            JumpImpulse = 0;
            JumpResponseTime = 0;
            JumpDrift = 0;
            AirDrag = 0;
            JumpAnimation = 0;
        }
    }
}