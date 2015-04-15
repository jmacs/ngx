using System;

namespace NgxLib.Processing
{
    public class Task : Process
    {
        protected Func<ProcessStatus> Function { get; set; }

        public Task(Func<ProcessStatus> function)
        {
            Function = function;
        }

        public override ProcessStatus Update()
        {
            Status = Function.Invoke();
            return Status;
        }
    }
}