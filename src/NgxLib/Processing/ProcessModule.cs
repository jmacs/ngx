using System;

namespace NgxLib.Processing
{
    /// <summary>
    /// A base class for implementing game processes. Game processes
    /// are used for executing complex workflows and tasks within
    /// the game. Process are made of up of discrete tasks that are 
    /// sequentially executed over the course of several frames. This
    /// allows workload to be distributed so the the main thread 
    /// is not blocked during long running operations.
    /// </summary>
    public abstract class ProcessModule : Process, IRenewable, IDisposable
    {
        protected NgxRuntime Runtime { get; private set; }
        public float Duration { get; set; }
        public Exception Exception { get; set; }

        protected Process Root { get; set; }

        public virtual void Initialize(NgxRuntime runtime)
        {
            Runtime = runtime;
        }

        public override ProcessStatus Update()
        {
            if (Root == null)
            {
                Status = ProcessStatus.Success;
                return Status;
            }
            Status = Root.Update();
            return Status;
        }

        public void Initialize()
        {
            
        }

        public virtual void Destroy()
        {
        }

        public void Dispose()
        {
            Root = null;
            Destroy();
        }
    }
}