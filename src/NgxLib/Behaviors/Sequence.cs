namespace NgxLib.Behaviors
{
    /// <summary>
    /// 
    /// </summary>
    public class Sequence : Composite
    {
        public Sequence(params Behavior[] children)
            : base(children)
        {
        }

        public override BahviorStatus Update()
        {
            var anyRunning = false;
            for (var i = 0; i < Children.Length; i++)
            {
                var status = Children[i].Update();
                switch (status)
                {
                    case BahviorStatus.Failure:
                        return BahviorStatus.Failure;
                    case BahviorStatus.Success:
                        continue;
                    case BahviorStatus.Running:
                        anyRunning = true;
                        continue;
                    default:
                        return BahviorStatus.Failure;
                }
            }
            //if none running, return success, otherwise return running
            return !anyRunning ? BahviorStatus.Success : BahviorStatus.Running;
        }
    }
}
