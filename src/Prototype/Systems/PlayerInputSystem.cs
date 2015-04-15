using Microsoft.Xna.Framework.Input;
using NgxLib;
using NgxLib.Input;
using Prototype.Components;
using Prototype.Processes;
using Prototype.User;

namespace Prototype.Systems
{
    public class PlayerInputSystem : TableSystem<Player>
    {
        protected KeyboardState Keyboard { get; set; }
        protected GamePadState Gamepad { get; set; }
        protected PlayerInputBindings Bindings { get; set; }
        protected NgxTable<Controller> Controllers { get; set; }

        public override void Initialize()
        {
            Bindings = new PlayerInputBindings();
            
            Bindings.SetKey(Keys.Space, Ctrl.Jump);
            Bindings.SetKey(Keys.LeftControl, Ctrl.Action1);
            Bindings.SetKey(Keys.Up, Ctrl.Up);
            Bindings.SetKey(Keys.Down, Ctrl.Down);
            Bindings.SetKey(Keys.Right, Ctrl.Right);
            Bindings.SetKey(Keys.Left, Ctrl.Left);
            Bindings.SetKey(Keys.LeftShift, Ctrl.Run);
            Bindings.SetKey(Keys.Escape, Ctrl.Menu);

            // Dual Shock 4
            Bindings.SetBtn(Buttons.X, Ctrl.Action1);//circle
            Bindings.SetBtn(Buttons.Back, Ctrl.Action1);//L2
            Bindings.SetBtn(Buttons.Y, Ctrl.Action2);//triangle
            Bindings.SetBtn(Buttons.Start, Ctrl.Action2);//R2
            Bindings.SetBtn(Buttons.B, Ctrl.Jump);//X
            Bindings.SetBtn(Buttons.A, Ctrl.Run);//square
            Bindings.SetBtn(Buttons.RightStick, Ctrl.Pause);//options
            Bindings.SetBtn(Buttons.LeftStick, Ctrl.Menu);//share
            Bindings.SetBtn(Buttons.DPadUp, Ctrl.Up);
            Bindings.SetBtn(Buttons.DPadDown, Ctrl.Down);
            Bindings.SetBtn(Buttons.DPadLeft, Ctrl.Left);
            Bindings.SetBtn(Buttons.DPadRight, Ctrl.Right);
            Bindings.SetBtn(Buttons.LeftThumbstickUp, Ctrl.Up);
            Bindings.SetBtn(Buttons.LeftThumbstickDown, Ctrl.Down);
            Bindings.SetBtn(Buttons.LeftThumbstickLeft, Ctrl.Left);
            Bindings.SetBtn(Buttons.LeftThumbstickRight, Ctrl.Right);

            Controllers = Database.Table<Controller>();
        }

        protected override void Begin()
        {
            Keyboard = Microsoft.Xna.Framework.Input.Keyboard.GetState();
            Gamepad = Microsoft.Xna.Framework.Input.GamePad.GetState(0);
        }

        private readonly Clock _clock = new Clock();

        protected override void Update(Player player)
        {
            var ctrl = Controllers[player.Entity];

            ctrl.Reset();

            for (int i = 0; i < Bindings.Keys.Count; i++)
            {
                var map = Bindings.Keys[i];
                if (Keyboard.IsKeyDown(map.Key))
                {
                    ctrl.Do(map.Ctrl);
                }
            }

            for (int i = 0; i < Bindings.Btns.Count; i++)
            {
                var map = Bindings.Btns[i];
                if (Gamepad.IsButtonDown(map.Btn))
                {
                    ctrl.Do(map.Ctrl);
                }
            }

            // TODO: design something for these if statements
            if (ctrl.Is(Ctrl.Menu) && _clock.IsZero)
            {
                var profile = Context.Session["profile"] as Profile;
                var proc = new EnterWorld(profile.QuestLog.LastWorld);
                Context.ProcessManager.Start(proc);
                _clock.Add(0.100f);
            }

            if (Keyboard.IsKeyDown(Keys.F1) && _clock.IsZero)
            {
                Ngx.Messenger.Send(Msg.Disable_Sound);
                _clock.Add(0.100f);
            }

            if (Keyboard.IsKeyDown(Keys.F2) && _clock.IsZero)
            {
                var proc = new ExitSuperPower(player.Entity);
                Context.ProcessManager.Start(proc);
                _clock.Add(0.500f);
            }

            if (Keyboard.IsKeyDown(Keys.Tab) && _clock.IsZero)
            {
                Debugger.Enabled = !Debugger.Enabled;
                _clock.Add(0.100f);
            }

            _clock.Minus(Time.Delta);
        }
    }
}
