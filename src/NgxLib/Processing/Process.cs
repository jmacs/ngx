namespace NgxLib.Processing
{
    public abstract class Process
    {
        public ProcessStatus Status { get; set; }

        public virtual ProcessStatus Update()
        {
            return Status;
        }
    }
}
