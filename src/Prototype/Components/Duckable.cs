using System;
using NgxLib;

namespace Prototype.Components
{
    [ObjectKey(Component.Ducker)]
    public class Duckable : DynamicComponent
    {
        public int DuckAnimation { get; set; }
    }
}
