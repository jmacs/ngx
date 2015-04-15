using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace NgxLib.Input
{
    public class PlayerInputBindings
    {
        public List<KeyboardBinding> Keys { get; private set; }
        public List<GamepadBinding> Btns { get; private set; }

        public PlayerInputBindings()
        {
            Keys = new List<KeyboardBinding>(10);
            Btns = new List<GamepadBinding>(10);
        }

        public void SetKey(Keys k, int c)
        {
            Keys.Add(new KeyboardBinding(k,c));
        }

        public void SetBtn(Buttons b, int c)
        {
            Btns.Add(new GamepadBinding(b, c));
        }
    }
}