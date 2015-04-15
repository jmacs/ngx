using System;
using System.Collections.Generic;
using System.Reflection;

namespace NgxLib
{
    /// <summary>
    /// Helper class for locating and activating system types.
    /// </summary>
    public static class TypeActivator
    {
        public static IEnumerable<T> Activate<T>(Assembly assembly) where T : class
        {
            foreach (var type in Locate(assembly, typeof(T)))
            {
                yield return Activator.CreateInstance(type) as T;
            }
        }

        public static IEnumerable<Type> Locate(Assembly assembly, Type target)
        {
            var types = assembly.GetTypes();
            for (var i = 0; i < types.Length; i++)
            {
                var type = types[i];
                if (type.IsClass && !type.IsAbstract && target.IsAssignableFrom(type))
                {
                    yield return type;
                }
            }
        }
    }
}