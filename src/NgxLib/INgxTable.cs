using System;
using System.Xml.Serialization;

namespace NgxLib
{
    /// <summary>
    /// A collection of game components
    /// </summary>
    public interface INgxTable :  IXmlSerializable, IDisposable
    {
        bool Modified { get; }
        Type GetComponentType();
        NgxComponent Get(int entity);
        NgxComponent New(int entity);
        void Remove(int entity);
        void Commit();
        void Destroy();
        void Clear();
    }
}