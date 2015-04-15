using System;

namespace NgxLib
{
    //TODO: either extend on this concept or remove it
    public interface IModule : IDisposable
    {
        bool IsInitialized { get; set; }
        void Initialize(NgxContext database);
    }
}