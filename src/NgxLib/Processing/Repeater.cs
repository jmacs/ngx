using System;

namespace NgxLib.Processing
{
    public class Repeater : Process
    {
        protected Func<bool> Function { get; set; }
        protected Process Child { get; set; }

        public Repeater(Func<bool> function, Process process)
        {
            Child = process;
            Function = function;
        }

        public override ProcessStatus Update()
        {
            if (Status != ProcessStatus.New && Status != ProcessStatus.Running)
            {
                return Status;
            }

            if (Function.Invoke())
            {
                var status = Child.Update();

                if (status != ProcessStatus.Success && status != ProcessStatus.Running)
                {
                    Status = status;
                    return Status;
                }
            }
            else
            {
                Status = ProcessStatus.Success;
                return Status;
            }

            Status = ProcessStatus.Running;
            return Status;
        }
    }
}
