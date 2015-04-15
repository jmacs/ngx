using System;
using NgxLib;

namespace Prototype.Components
{
    [ObjectKey(Component.MetaData)]
    public class MetaData : NgxComponent
    {
        public string Prefab { get; set; }
        public DateTime Created { get; set; }

        public override void Initialize()
        {
            Prefab = null;
            Created = DateTime.Now;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", Entity, Prefab);
        }
    }
}
