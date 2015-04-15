using System;
using System.Reflection;
using Microsoft.Xna.Framework;
using NgxLib.Cameras;
using NgxLib.Processing;

namespace NgxLib
{
    /// <summary>
    /// Provides basic initialization of the graphics device, 
    /// game context, database, and messaging system.
    /// </summary>
    public class NgxRuntime : Game
    {
        public Assembly ExecutingAssembly { get; private set; }
        public GraphicsDeviceManager Graphics { get; private set; }
        public ProcessManager ProcessManager { get; private set; }
        public NgxMessenger Messenger { get; private set; }
        public NgxSession Session { get; private set; }
        public Camera2D Camera { get; private set; }        
        public NgxDatabase Database { get; private set; }
        public NgxContext Context { get; private set; }
        protected INgxApplication Application { get; set; }

        public NgxRuntime(INgxApplication application)
        {
            Graphics = new GraphicsDeviceManager(this);
            ProcessManager = new ProcessManager(this);
            Messenger = Ngx.Messenger;
            Session = new NgxSession();
            Database = new NgxDatabase();
            ExecutingAssembly = application.GetType().Assembly;
            Application = application;
        }

        protected override void Initialize()
        {
            Application.Initialize(this);
            
            base.Initialize();

            Camera = new Camera2D(GraphicsDevice);
            Context = new NullContext();

            Ngx.Components.Register(ExecutingAssembly);
            Ngx.Prefabs.Register(ExecutingAssembly);
            Ngx.Messenger.Register(ExecutingAssembly);
            Ngx.Contexts.Register(ExecutingAssembly);            

            Database.Register(ExecutingAssembly);

            Application.BeginSession(this);
            Context.Initialize(this);
        }

        public void Transition(string name)
        {
            var newContext = Ngx.Contexts.Create(name);
            if (newContext != null)
            {
                if (Context != null)
                {
                    Context.Dispose();
                    GC.Collect();
                }

                Logger.Log("Transitioning to {0}", name);

                Context = newContext;
                Context.Initialize(this);
                return;
            }

            Context = new NullContext();
            Context.Initialize(this);
            Logger.Log("Cannot find context '{0}'", name);
        }

        protected override void Update(GameTime time)
        {
            Time.Update(time, false);
            ProcessManager.Update();
            Context.Update();
            Database.Commit();
            Messenger.Flush(Context);
            ProcessManager.Commit();
            base.Update(time);
        }

        protected override void Draw(GameTime time)
        {          
            Time.Update(time, true);
            Context.Draw();
            base.Draw(time);
        }

        protected override void EndRun()
        {
            Application.EndSession(this);
        }

        protected override void UnloadContent()
        {
            Messenger.Unregister();
            Application.Destroy(this);
            Context.Destroy();
            Debugger.Destroy();
        }
    }
}
