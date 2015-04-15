using System;

namespace NgxLib.Behaviors
{
    public class Conditional : Behavior
    {
        protected Func<bool> Function { get; set; }

        public Conditional(Func<bool> function)
        {
            Function = function;
        }

        public override BahviorStatus Update()
        {
            var result =  Function();
            if (result) return BahviorStatus.Success;
            return BahviorStatus.Failure;
        }
    }
}