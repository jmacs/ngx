namespace NgxLib.Behaviors
{
    public class Inverter : Decorator
    {
        public Inverter(Behavior child) : base(child)
        {
        }

        public override BahviorStatus Update()
        {
            var status = Child.Update();
            switch (status)
            {
                case BahviorStatus.Failure:
                    return BahviorStatus.Success;
                case BahviorStatus.Success:
                    return BahviorStatus.Failure;
                case BahviorStatus.Running:
                    return BahviorStatus.Running;
            }
            return BahviorStatus.Failure;
        }
    }
}
