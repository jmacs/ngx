using NgxLib;

namespace Prototype.Components
{
    [ObjectKey(Component.Mobility)]
    public class Mobility : DynamicComponent
    {
        public int WalkAnimation { get; set; }
        public int IdleAnimation { get; set; }
        public float WalkSpeed { get; set; }
        public float MaxWalkSpeed { get; set; }

        public override void Initialize()
        {
            Duration = 0;
            WalkSpeed = 0;
            MaxWalkSpeed = 0;
        }
    }
}
