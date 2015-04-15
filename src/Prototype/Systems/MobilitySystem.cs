using Microsoft.Xna.Framework.Graphics;
using NgxLib;
using Prototype.Components;

namespace Prototype.Systems
{
    public class MobilitySystem : DynamicSystem<Mobility>
    {
        protected NgxTable<Controller> Controller { get; set; }
        protected NgxTable<RigidBody> RigidBody { get; set; }
        protected NgxTable<Animator> Animator { get; set; }
        protected NgxTable<JumpBoots> JumpBoots { get; set; }
        protected NgxTable<Duckable> Duckable { get; set; }
        protected NgxTable<Sprite> Sprite { get; set; }

        public override void Initialize()
        {
            JumpBoots = Database.Table<JumpBoots>();
            Duckable = Database.Table<Duckable>();
            RigidBody = Database.Table<RigidBody>();
            Controller = Database.Table<Controller>();
            Animator = Database.Table<Animator>();
            Sprite = Database.Table<Sprite>();
        }

        protected override bool  Evaluate(Mobility com)
        {
            var ctrl = Controller[com.Entity];
            var body = RigidBody[com.Entity];

            if (!body.IsGrounded)
            {
                JumpBoots.Enable(com.Entity);
                return false;
            }

            if (ctrl.Is(Ctrl.Jump) && body.IsGrounded)
            {
                JumpBoots.Enable(com.Entity);
                return false;
            }

            if (ctrl.Is(Ctrl.Down))
            {
                Duckable.Enable(com.Entity);
                return false;
            }

            return true;
        }

        protected override void Enter(Mobility com)
        {
            var body = RigidBody[com.Entity];
            body.MaxSpeedX = com.MaxWalkSpeed;
        }

        protected override void Update(Mobility com)
        {
            var body = RigidBody[com.Entity];
            var animator = Animator[com.Entity];
            var ctlr = Controller[com.Entity];
            var sprite = Sprite[com.Entity];

            if (ctlr.Is(Ctrl.Left))
            {
                body.Acceleration.X = -com.WalkSpeed;
                sprite.Effects = SpriteEffects.FlipHorizontally;
            }
            else if (ctlr.Is(Ctrl.Right))
            {
                body.Acceleration.X = com.WalkSpeed;
                sprite.Effects = SpriteEffects.None;
            }
            else
            {
                body.Acceleration.X = 0;                
            }

            if (body.IsMovingLeft || body.IsMovingRight)
            {
                animator.Animation = com.WalkAnimation;
            }
            else
            {
                animator.Animation = com.IdleAnimation;
            }
        }

    }
}
