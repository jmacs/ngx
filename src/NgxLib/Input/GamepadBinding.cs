using System;
using Microsoft.Xna.Framework.Input;

namespace NgxLib.Input
{
    public struct GamepadBinding : IEquatable<GamepadBinding>
    {
        public readonly Buttons Btn;
        public readonly int Ctrl;

        public GamepadBinding(Buttons btn, int ctrl)
        {
            Btn = btn;
            Ctrl = ctrl;
        }

        public override int GetHashCode()
        {
            var hash = 23;
            hash = hash * 31 + (int)Btn;
            hash = hash * 31 + Ctrl;
            return hash;
        }

        public bool Equals(GamepadBinding other)
        {
            return Btn == other.Btn && Ctrl == other.Ctrl;
        }

        public override bool Equals(object obj)
        {
            if (obj is GamepadBinding)
            {
                return Equals((GamepadBinding)obj);
            }
            return false;
        }
    }
}