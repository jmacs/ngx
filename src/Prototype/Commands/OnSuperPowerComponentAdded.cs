using NgxLib;
using Prototype.Components;
using Prototype.Entities;

namespace Prototype.Commands
{
    public class OnSuperPowerComponentAdded : INgxCommand
    {
        public long GetMessageKey()
        {
            return Ngx.Components.Get<SuperPower>().AddMessageKey;
        }

        public void Execute(NgxContext context, NgxMessage args)
        {
            var power = context.Database.Component<SuperPower>(args.Entity1);

            var body = context.Database.Component<RigidBody>(power.Entity);
            body.Hitbox = new NgxRectangle(0, 0, 16, 24);

            var movement = context.Database.Component<Mobility>(power.Entity);
            movement.IdleAnimation = Mario.SuperIdleAnimation;
            movement.WalkAnimation = Mario.SuperWalkAnimation;

            var jump = context.Database.Component<JumpBoots>(power.Entity);
            jump.JumpAnimation = Mario.SuperJumpAnimation;

            var duck = context.Database.New<Duckable>(power.Entity);
            duck.DuckAnimation = Mario.SuperDuckAnimation;
        }

    }
}
