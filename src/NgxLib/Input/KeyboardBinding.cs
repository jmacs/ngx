using System;
using Microsoft.Xna.Framework.Input;

namespace NgxLib.Input
{
    public struct KeyboardBinding : IEquatable<KeyboardBinding>
    {
        public readonly Keys Key;
        public readonly int Ctrl;

        public KeyboardBinding(Keys key, int ctrl)
        {
            Key = key;
            Ctrl = ctrl;
        }

        public override int GetHashCode()
        {
            var hash = 23;
            hash = hash * 31 + (int) Key;
            hash = hash * 31 + Ctrl;
            return hash;
        }

        public bool Equals(KeyboardBinding other)
        {
            return Key == other.Key && Ctrl == other.Ctrl;
        }

        public override bool Equals(object obj)
        {
            if (obj is KeyboardBinding)
            {
                return Equals((KeyboardBinding)obj);
            }
            return false;
        }
    }
}