using System;
using NgxLib;

namespace Prototype
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var game = new PrototypeGame();
            using (var runtime = new NgxRuntime(game))
            {
                runtime.Run();
            }
        }
    }
#endif
}
