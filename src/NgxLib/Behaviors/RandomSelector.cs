using System;

namespace NgxLib.Behaviors
{
    public class RandomSelector : Composite
    {
        private static readonly Random Random = new Random(DateTime.Now.Millisecond);

        public override BahviorStatus Update()
        {
            var index = Random.Next(0, Children.Length - 1);
            var status = Children[index].Update();
            switch (status)
            {
                case BahviorStatus.Failure:
                    return BahviorStatus.Failure;
                case BahviorStatus.Success:
                    return BahviorStatus.Success;
                case BahviorStatus.Running:
                    return BahviorStatus.Running;
                default:
                    return BahviorStatus.Failure;
            }
        }
    }
}
