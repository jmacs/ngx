namespace NgxLib.Processing
{
    public class Parallel : Composite
    {
        protected ProcessStatus[] ReturnStatus { get; set; }

        public Parallel(params Process[] children)
            : base(children)
        {
            ReturnStatus = new ProcessStatus[children.Length];
            for (var i = 0; i < ReturnStatus.Length; i++)
            {
                ReturnStatus[i] = ProcessStatus.New;
            }
        }

        public override ProcessStatus Update()
        {
            if (Status != ProcessStatus.New && Status != ProcessStatus.Running)
            {
                return Status;
            }

            var complete = true;

            for (var i = 0; i < ReturnStatus.Length; i++)
            {
                var returnStatus = ReturnStatus[i];

                if (returnStatus == ProcessStatus.New || ReturnStatus[i] == ProcessStatus.Running)
                {
                    
                    ReturnStatus[i] = Children[i].Update();

                    switch (ReturnStatus[i])
                    {
                        case ProcessStatus.Running:
                            complete = false;
                            break;
                        case ProcessStatus.Aborted:
                            Status = ProcessStatus.Aborted;
                            return Status;
                        case ProcessStatus.Failure:
                            Status = ProcessStatus.Failure;
                            return Status;
                    }
                }
            }

            return complete ? ProcessStatus.Success : ProcessStatus.Running;
        }
    }
}
