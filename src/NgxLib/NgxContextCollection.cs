using System;
using System.Reflection;

namespace NgxLib
{
    public class NgxContextCollection
    {
        protected Hash<Type> _contexts = new Hash<Type>();

        public void Register(Assembly assembly)
        {
            var target = typeof(NgxContext);
            var types = assembly.GetTypes();
            for (int i = 0; i < types.Length; i++)
            {
                var type = types[i];
                if (type.IsClass && !type.IsAbstract && target.IsAssignableFrom(type))
                {
                    _contexts.Add(type.Name, type);
                }
            }
        }

        public NgxContext Create(string name)
        {
            Type contextType;
            if (_contexts.TryGetValue(name, out contextType))
            {                
                return Activator.CreateInstance(contextType) as NgxContext;
            }
            return null;
        }
    }
}