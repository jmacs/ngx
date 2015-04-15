using System;

namespace NgxLib
{
    /// <summary>
    /// The base class for all game systems. Game systems implement
    /// a specific feature or behavior in the game. All game logic
    /// is contained in systems.
    /// </summary>
    public class NgxGameSystem : IDisposable
    {
        protected NgxDatabase Database { get; private set; }
        protected NgxContext Context { get; private set; }

        public bool Enabled { get; set; }

        /// <summary>
        /// Binds the system to the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public virtual void BindContext(NgxContext context)
        {
            Context = context;
            Database = context.Database;
        }

        /// <summary>
        /// Perform any set up work here.
        /// </summary>
        public virtual void Initialize()
        {
        }

        /// <summary>
        /// Update the simulation
        /// </summary>
        public virtual void Update()
        {
        }

        /// <summary>
        /// Perform any tear-down work here.
        /// </summary>
        public virtual void Destroy()
        {
        }

        /// <summary>
        /// Performs application-defined tasks associated with 
        /// freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public virtual void Dispose()
        {
            Destroy();
            Database = null;
            Context = null;
        }
    }
}