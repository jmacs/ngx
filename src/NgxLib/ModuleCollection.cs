using System;
using System.Reflection;

namespace NgxLib
{
    public class ModuleCollection<T> : IDisposable 
        where T : class, IModule
    {
        protected Hash<T> Modules { get; set; }
        protected NgxContext Context { get; set; }

        public ModuleCollection(NgxContext context)
        {
            Context = context;
            Modules = new Hash<T>();
        }

        public void Register(Assembly assembly)
        {
            foreach (var instance in TypeActivator.Activate<T>(assembly))
            {
                Modules.Add(instance.GetType().FullName, instance);
            }
        }

        public bool TryGet(string key, out T module)
        {
            if (Modules.TryGetValue(key, out module))
            {
                if (!module.IsInitialized)
                {
                    module.Initialize(Context);
                    module.IsInitialized = true;
                }
                return true;
            }
            return false;
        }

        public void Dispose()
        {
            foreach (var module in Modules)
            {
                module.Value.Dispose();
            }
            Context = null;
            Modules.Clear();
            Modules = null;
        }
    }
}