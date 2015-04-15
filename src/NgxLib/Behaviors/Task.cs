using System;

namespace NgxLib.Behaviors
{
    public class Task : Behavior
    {
        protected Func<BahviorStatus> Function { get; set; }

        public Task(Func<BahviorStatus> function)
        {
            Function = function;
        }

        public override BahviorStatus Update()
        {
            return Function.Invoke();
        }
    }
}
