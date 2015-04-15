using NgxLib;
using Prototype.Components;

namespace Prototype.Commands
{
    public class OnJumpBootsTouchShell : INgxCommand
    {
        public long GetMessageKey()
        {
            return Msg.On_JumpBoots_touch_Shell;
        }

        public void Execute(NgxContext context, NgxMessage args)
        {
            var collision = args.Collision;

            if (!collision.Top) return;

            var playerBody = context.Database.Component<RigidBody>(args.Entity1);
            playerBody.Velocity.Y = -2.5f;

            var shell = context.Database.Component<Shell>(args.Entity2);

            if (shell.Enabled && !shell.TorpedoMode)
            {
                // torpedo
                shell.TorpedoMode = true;
                shell.Duration = 0;
            }
            else if (shell.Enabled && shell.TorpedoMode)
            {
                shell.TorpedoMode = false;
                shell.Duration = 0;
            }
            else
            {
                shell.Duration = 0;
                shell.Enabled = true;
            }
        }
    }
}
