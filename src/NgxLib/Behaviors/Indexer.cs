using System;

namespace NgxLib.Behaviors
{
    public class Indexer : Composite
    {
        public Func<int> IndexFunction { get; set; }

        public Indexer(Func<int> indexFunction, params Behavior[] children) : base(children)
        {
            IndexFunction = indexFunction;
        }

        public override BahviorStatus Update()
        {
            var index = IndexFunction.Invoke();
            if (index < 0 || index >= Children.Length)
            {
                return BahviorStatus.Failure;
            }
            return Children[index].Update();
        }
    }
}
