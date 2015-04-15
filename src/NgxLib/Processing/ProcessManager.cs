using System;
using System.Collections.Generic;

namespace NgxLib.Processing
{
    /// <summary>
    /// Responsible for updating process modules each frame
    /// </summary>
    public class ProcessManager : IDisposable
    {
        protected IdentityPool IdentityPool = new IdentityPool(); 
        protected NgxRuntime Runtime { get; set; }
        protected Queue<ProcessModule> AddQueue = new Queue<ProcessModule>();
        protected Queue<int> RemoveQueue = new Queue<int>();
        protected Index<ProcessModule> Processes = new Index<ProcessModule>();

        public bool Enabled { get; set; }

        public ProcessManager(NgxRuntime runtime)
        {
            Enabled = true;
            Runtime = runtime;
        }

        public void Start(ProcessModule process)
        {
            AddQueue.Enqueue(process);
        }

        public void Update()
        {
            if (!Enabled || Processes.Count == 0) return;

            foreach (var item in Processes)
            {
                var process = item.Value;
                var status = ProcessStatus.Failure; 

                try
                {
                    status = process.Update();
                    item.Value.Duration += Time.RealTime;
                }
                catch (Exception ex)
                {
                    process.Status = ProcessStatus.Failure;
                    process.Exception = ex;
                }
                
                if (status != ProcessStatus.Running)
                {
                    if (status == ProcessStatus.Failure)
                    {
                        Logger.Log("process failure -> {0}#{1}, {2} seconds", process, item.Key, process.Duration);
                        if (process.Exception != null)
                        {
                            Logger.Log("process exception: {0}", process.Exception.Message);
                            Logger.Log(process.Exception.StackTrace);
                        }
                    }
                    else if (status == ProcessStatus.Aborted)
                    {
                        Logger.Log("process aborted -> {0}#{1}, {2} seconds", process, item.Key, process.Duration);
                    }
                    else if (status == ProcessStatus.Success)
                    {
                        Logger.Log("process success -> {0}#{1}, {2} seconds", process, item.Key, process.Duration);
                    }
                    RemoveQueue.Enqueue(item.Key);
                }
            }
        }

        public void Abort()
        {
            foreach (var process in Processes)
            {
                process.Value.Dispose();
            }
            AddQueue.Clear();
            RemoveQueue.Clear();
            Processes.Clear();
        }

        public void Commit()
        {
            while (RemoveQueue.Count > 0)
            {
                var id = RemoveQueue.Dequeue();
                ProcessModule process;

                if (Processes.TryGetValue(id, out process))
                {
                    process.Dispose();
                    Processes.Remove(id);
                }

                IdentityPool.Release(id);
            }
            while (AddQueue.Count > 0)
            {
                var process = AddQueue.Dequeue();
                process.Initialize(Runtime);
                var id = IdentityPool.Next();
                Processes.Add(id, process);
            }
        }

        public void Dispose()
        {
            Runtime = null;
            Abort();
        }
    }
}
