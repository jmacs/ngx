using NgxLib;
using Prototype.Components;

namespace Prototype.Systems
{
    public class JumpSystem : DynamicSystem<JumpBoots>
    {
        protected NgxTable<Controller> Controller { get; set; }
        protected NgxTable<RigidBody> RigidBody { get; set; }
        protected NgxTable<Animator> Animator { get; set; }
        protected NgxTable<Mobility> Mobility { get; set; }

        public override void Initialize()
        {
            RigidBody = Database.Table<RigidBody>();
            Controller = Database.Table<Controller>();
            Animator = Database.Table<Animator>();
            Mobility = Database.Table<Mobility>();
        }

        protected override bool Evaluate(JumpBoots com)
        {
            var body = RigidBody[com.Entity];

            if (com.Duration > 0.5f && body.IsGrounded)
            {
                body.Acceleration.Y = 0;
                Mobility.Enable(com.Entity);
                return false;
            }

            return true;
        }

        protected override void Enter(JumpBoots com)
        {
            Animator[com.Entity].Animation = com.JumpAnimation;
        }

        protected override void Update(JumpBoots com)
        {
            var body = RigidBody[com.Entity];
            var ctlr = Controller[com.Entity];

            // impluse
            if (body.IsGrounded && ctlr.Is(Ctrl.Jump) && com.JumpTimer <= 0)
            {
                body.Acceleration.Y = -com.JumpImpulse;
                com.JumpTimer = com.JumpResponseTime;

                Ngx.Messenger.Send(Msg.Play_Sound, sound: Snd.Jump);

            }

            // Count down
            if (com.JumpTimer > 0)
            {
                com.JumpTimer -= Time.Delta;
            }

            // cut jump
            if (body.Acceleration.Y < 0 && !ctlr.Is(Ctrl.Jump))
            {
                body.Velocity.Y = body.Velocity.Y * com.JumpDrift * Time.Delta;
                body.Acceleration.Y = 0;
            }

            // max jump
            if (body.Acceleration.Y < 0 && com.JumpTimer < 0)
            {
                body.Velocity.Y = body.Velocity.Y * com.JumpDrift * Time.Delta;
                body.Acceleration.Y = 0;
            }

            HandleDrift(com);
        }

        protected void HandleDrift(JumpBoots com)
        {
            var body = RigidBody[com.Entity];
            var ctlr = Controller[com.Entity];
            
            // verticle movement & drag
            if (!ctlr.Any(Ctrl.Left, Ctrl.Right))
            {
                body.Acceleration.X = 0;
            }
            else if (ctlr.Is(Ctrl.Left))
            {
                body.Acceleration.X = -com.AirDrag;
            }
            else if (ctlr.Is(Ctrl.Right))
            {
                body.Acceleration.X = com.AirDrag;
            }
        }
    }
}
