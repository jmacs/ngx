namespace NgxLib.Behaviors
{
    /// <summary>
    /// Base class for implementing game AI
    /// </summary>
    public abstract class BehaviorModule : IModule
    {
        protected RootSelector Root { get; set; }

        public int Entity { get; private set; }
        public bool IsInitialized { get; set; }

        public virtual void Initialize(NgxContext context)
        {
        }

        public BahviorStatus Update(int entity)
        {
            Entity = entity;
            return Root.Update();
        }

        public virtual void Destroy()
        {
        }

        public void Dispose()
        {
            Destroy();
            Root = null;
        }
    }
}