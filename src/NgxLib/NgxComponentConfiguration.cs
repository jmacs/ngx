using System;
using System.Collections.Generic;
using System.Reflection;

namespace NgxLib
{
    public class NgxComponentConfiguration
    {
        protected Dictionary<Type, NgxComponentMetaData> Types { get; set; }
        protected Dictionary<int, NgxComponentMetaData> Keys { get; set; }
        protected int UniqueKey = -1;

        public NgxComponentConfiguration()
        {
            Types = new Dictionary<Type, NgxComponentMetaData>();
            Keys = new Dictionary<int, NgxComponentMetaData>();
        }

        public NgxComponentMetaData Get(int key)
        {
            NgxComponentMetaData meta;
            if (Keys.TryGetValue(key, out meta)) return meta;
            return null;
        }

        public NgxComponentMetaData Get(Type type)
        {
            NgxComponentMetaData meta;
            if (Types.TryGetValue(type, out meta)) return meta;
            return null;
        }

        public NgxComponentMetaData Get<T>() where T : NgxComponent
        {
            NgxComponentMetaData meta;
            if (Types.TryGetValue(typeof(T), out meta)) return meta;
            return null;
        }

        public void Register(Assembly assembly)
        {
            var types = TypeActivator.Locate(assembly, typeof(NgxComponent));

            foreach (var type in types)
            {
                var objectKey = GetObjectKey(type);

                var meta = new NgxComponentMetaData(
                    objectKey.Value,
                    type,
                    new Mask(objectKey.Value),
                    UniqueKey--,
                    UniqueKey--);

                Types.Add(type, meta);
                Keys.Add(objectKey.Value, meta);
            }
        }

        private static ObjectKeyAttribute GetObjectKey(Type type)
        {
            var attribs = type.GetCustomAttributes(typeof(ObjectKeyAttribute), true);

            foreach (var attrib in attribs)
            {
                if(attrib.GetType() == typeof(ObjectKeyAttribute))
                {
                    return attrib as ObjectKeyAttribute;
                }
            }

            return null;
        }
    }
}