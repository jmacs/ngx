using System;

namespace NgxLib
{
    public class NgxComponentMetaData
    {
        public int ComponentId { get; private set; }
        public Type ComponentType { get; private set; }
        public Mask ComponentMask { get; private set; }
        public int AddMessageKey { get; private set; }
        public int RemoveMessageKey { get; private set; }

        public NgxComponentMetaData(int id, Type type, Mask mask, int addKey, int removeKey)
        {
            ComponentId = id;
            ComponentType = type;
            ComponentMask = mask;
            AddMessageKey = addKey;
            RemoveMessageKey = removeKey;
        }

        public override string ToString()
        {
            return string.Format("{0}, ID={1}", ComponentType, ComponentId);
        }
    }
}