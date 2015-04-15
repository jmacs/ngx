using Microsoft.Xna.Framework;
using NgxLib;
using Prototype.Components;

namespace Prototype.Systems
{
    public class StompSystem : DynamicSystem<Stompable>
    {
        protected NgxTable<RigidBody> RigidBody { get; set; }
        protected NgxTable<Animator> Animator { get; set; }
        protected NgxTable<Mobility> Mobility { get; set; }

        public override void Initialize()
        {
            RigidBody = Database.Table<RigidBody>();
            Animator = Database.Table<Animator>();
            Mobility = Database.Table<Mobility>();
        }

        protected override void Enter(Stompable com)
        {
            Mobility.Disable(com.Entity);
            RigidBody[com.Entity].Acceleration = Vector2.Zero;
            Animator[com.Entity].Animation = com.StompAnimation;

            Context.Messenger.Send(Msg.Play_Sound, sound: Snd.Stomp);
        }

        protected override void Update(Stompable com)
        {
            if (com.Duration > 0.5f)
            {
                Database.Remove(com.Entity);
            }
        }
    }
}