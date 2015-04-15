using Microsoft.Xna.Framework;
using NgxLib;
using Prototype.Components;

namespace Prototype.Systems
{
    public class ShellSystem : DynamicSystem<Shell>
    {
        protected NgxTable<RigidBody> RigidBody { get; set; }
        protected NgxTable<Animator> Animator { get; set; }
        protected NgxTable<Mobility> Mobility { get; set; }
        protected NgxTable<Interactor> Interactor { get; set; }

        public override void Initialize()
        {
            RigidBody = Database.Table<RigidBody>();
            Animator = Database.Table<Animator>();
            Interactor = Database.Table<Interactor>();
            Mobility = Database.Table<Mobility>();
        }

        protected override void Enter(Shell com)
        {
            var body = RigidBody[com.Entity];
            var anim = Animator[com.Entity];

            if (com.TorpedoMode)
            {
                anim.Animation = com.TorpedoAnimation;
                anim.Paused = false;
                body.MaxSpeedX = 3;
                body.Acceleration.X = 6;
                Interactor.Disable(com.Entity);

                Ngx.Messenger.Send(Msg.Play_Sound, sound: Snd.Kick);
            }
            else
            {
                Mobility.Disable(com.Entity);
                Interactor.Enable(com.Entity);
                body.Acceleration = Vector2.Zero;
                anim.Animation = com.ShellAnimation;
                anim.Paused = true;

                Ngx.Messenger.Send(Msg.Play_Sound, sound: Snd.Stomp);

            }
        }

        protected override bool Evaluate(Shell com)
        {
            if (com.WakeUp)
            {
                com.WakeUp = false;
                Mobility.Enable(com.Entity);
                return false;
            }

            return true;
        }

        protected override void Update(Shell com)
        {
            if (com.TorpedoMode)
            {
                var body = RigidBody[com.Entity];

                if (body.WallSensory.Contains(Side.LeftCenter))
                {
                    body.Acceleration.X = 3;
                    body.Velocity.X = -body.Velocity.X;

                    Ngx.Messenger.Send(Msg.Play_Sound, sound: Snd.Bump);

                }
                else if (body.WallSensory.Contains(Side.RightCenter))
                {
                    Ngx.Messenger.Send(Msg.Play_Sound, sound: Snd.Bump);

                    body.Acceleration.X = -3;
                    body.Velocity.X = -body.Velocity.X;
                }
            }
            else
            {
                if (com.Duration > 3.0f)
                {
                    Animator[com.Entity].Paused = false;
                }
                com.WakeUp = com.Duration > 6.0f;
            }
            
        }
    }
}