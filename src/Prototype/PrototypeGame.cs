using NgxLib;
using NgxLib.Serialization;
using Prototype.Processes;
using Prototype.User;

namespace Prototype
{
    public class PrototypeGame : INgxApplication
    {
        public void Initialize(NgxRuntime runtime)
        {
            Logger.Log("initializing application");

            var config = Serializer.Deserialize<Config>("content/config.xml");
            runtime.Session["config"] = config;

            runtime.Window.Title = "Prototype";
            runtime.Content.RootDirectory = "Content";
            runtime.Graphics.PreferredBackBufferWidth = config.ScreenWidth;
            runtime.Graphics.PreferredBackBufferHeight = config.ScreenHeight;
            runtime.Graphics.IsFullScreen = config.IsFullScreen;
            runtime.IsMouseVisible = true;
        }

        public void BeginSession(NgxRuntime runtime)
        {
            Logger.Log("starting session");

            Debugger.Enabled = true;
            Debugger.Initialize(runtime.GraphicsDevice, "Consolas");

            // kick off the startup process for the game
            var proc = new ContinueGame("bomb.com");  
            runtime.ProcessManager.Start(proc);
        }

        public void EndSession(NgxRuntime runtime)
        {
            Logger.Log("ending session");
        }

        public void Destroy(NgxRuntime runtime)
        {
            Logger.Log("application shutdown");
        }
    }
}
