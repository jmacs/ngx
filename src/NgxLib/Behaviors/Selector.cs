namespace NgxLib.Behaviors
{
    public class Selector : Composite //OR
    {
        public Selector(params Behavior[] children)
            : base(children)
        {
        }

        public override BahviorStatus Update()
        {
            for (var i = 0; i < Children.Length; i++)
            {
                var status = Children[i].Update();
                switch (status)
                {
                    case BahviorStatus.Failure:
                        continue;
                    case BahviorStatus.Success:
                        return BahviorStatus.Success;
                    case BahviorStatus.Running:
                        return BahviorStatus.Running;
                    default:
                        return BahviorStatus.Failure;
                }
            }
            return BahviorStatus.Failure;
        }
    }
}