namespace NgxLib
{
    public static class Logger
    {
        public static void Log(string message, params object[] args)
        {
            System.Diagnostics.Debug.WriteLine(message, args);
        }
    }
}
