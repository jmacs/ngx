using System;

namespace NgxLib
{
    public class ObjectKeyAttribute : Attribute
    {
        public int Value { get; set; }

        public ObjectKeyAttribute(int key)
        {
            Value = key;
        }
    }
}
