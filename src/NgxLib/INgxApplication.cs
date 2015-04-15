namespace NgxLib
{
    /// <summary>
    /// Interface for implementing game start-up and shut-down tasks
    /// </summary>
    public interface INgxApplication
    {
        void Initialize(NgxRuntime runtime);
        void BeginSession(NgxRuntime runtime);
        void EndSession(NgxRuntime runtime);
        void Destroy(NgxRuntime runtime);
    }
}