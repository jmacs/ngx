using System;
using NgxLib;
using Prototype.Components;
using Prototype.Entities;

namespace Prototype.Commands
{
    public class OnNullPowerComponentAdd : INgxCommand
    {
        public long GetMessageKey()
        {
            return Ngx.Components.Get<NullPower>().AddMessageKey;
        }

        public void Execute(NgxContext context, NgxMessage args)
        {
            var power = context.Database.Component<NullPower>(args.Entity1);

            var body = context.Database.Component<RigidBody>(power.Entity);
            body.Hitbox = new NgxRectangle(0, 0, 16, 16);

            var movement = context.Database.Component<Mobility>(power.Entity);
            movement.IdleAnimation = Mario.NormalIdleAnimation;
            movement.WalkAnimation = Mario.NormalWalkAnimation;

            var jump = context.Database.Component<JumpBoots>(power.Entity);
            jump.JumpAnimation = Mario.NormalJumpAnimation;

            context.Database.Table<Duckable>().Remove(power.Entity);
        }
    }
}
