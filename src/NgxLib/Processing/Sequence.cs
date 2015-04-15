namespace NgxLib.Processing
{
    public class Sequence : Composite
    {
        public int Current { get; private set; }

        public Sequence(params Process[] children)
            : base(children)
        {
        }

        public override ProcessStatus Update()
        {
            if (Status != ProcessStatus.New && Status != ProcessStatus.Running)
            {
                return Status;
            }

            if (Current >= Children.Length)
            {
                Status = ProcessStatus.Success;
                return Status;
            }

            var proc = Children[Current];
            var status = proc.Update();

            if (status == ProcessStatus.Running)
            {
                Status = ProcessStatus.Running;
                return Status;
            }

            if (status == ProcessStatus.Success)
            {                
                Current++;
                if (Current >= Children.Length)
                {
                    Status = ProcessStatus.Success;
                    return Status;
                }
                return ProcessStatus.Running;
            }

            Status = status;
            return Status;
        }
    }
}