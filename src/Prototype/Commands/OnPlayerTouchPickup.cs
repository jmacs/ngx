using NgxLib;
using Prototype.Components;
using Prototype.Processes;

namespace Prototype.Commands
{
    public class OnPlayerTouchPickup : INgxCommand
    {
        public long GetMessageKey()
        {
            return Msg.On_Player_touch_Pickup;
        }

        public void Execute(NgxContext context, NgxMessage args)
        {
            var pickup = context.Database.Component<Pickup>(args.Entity2);

            if (pickup.ItemID == Item.Mushroom)
            {
                var proc = new TouchMushroom(args.Entity1);
                context.ProcessManager.Start(proc);
            }

            context.Database.Remove(args.Entity2);
        }
    }
}
