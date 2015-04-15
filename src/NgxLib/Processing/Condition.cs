using System;

namespace NgxLib.Processing
{
    public class Condition : Decorator
    {
        protected Func<bool> Function { get; set; }

        public Condition(Func<bool> function, Process child) : base(child)
        {
            Function = function;
        }

        public override ProcessStatus Update()
        {
            if (Function())
            {
                Status = Child.Update();
                return Status;
            }
            return ProcessStatus.Success;
        }
    }
}
