using NgxLib;
using Prototype.Components;

namespace Prototype.Commands
{
    public class OnJumpBootsTouchStompable : INgxCommand
    {
        public long GetMessageKey()
        {
            return Msg.On_JumpBoots_touch_Stompable;
        }

        public void Execute(NgxContext context, NgxMessage args)
        {
            var collision = args.Collision;

            if (!collision.Top) return;

            var playerBody = context.Database.Component<RigidBody>(args.Entity1);
            playerBody.Velocity.Y = -2.5f;

            var stompable = context.Database.Component<Stompable>(args.Entity2);
            stompable.Enabled = true;
        }
    }
}
