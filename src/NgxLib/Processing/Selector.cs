using System;

namespace NgxLib.Processing
{
    public class Selector : Composite
    {
        public Func<int> Function { get; private set; }
        public int Current { get; private set; }

        public Selector(Func<int> function, params Process[] children)
            : base(children)
        {
            Function = function;
        }

        public override ProcessStatus Update()
        {
            var index = Function.Invoke();

            if (index >= Children.Length)
            {
                return ProcessStatus.Failure;
            }

            return Children[index].Update();
        }
    }
}