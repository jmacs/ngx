using NgxLib;
using Prototype.Components;

namespace Prototype.Systems
{
    public class DuckSystem : DynamicSystem<Duckable>
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

        protected override bool Evaluate(Duckable com)
        {
            var ctrl = Controller[com.Entity];

            if (ctrl.Is(Ctrl.Down))
            {
                return true;
            }
            
            return false;
        }

        protected override void Enter(Duckable com)
        {
            Mobility.Disable(com.Entity);
            Animator[com.Entity].Animation = com.DuckAnimation;
        }

        protected override void Update(Duckable com)
        {
        }

        protected override void Exit(Duckable com)
        {
            Mobility.Enable(com.Entity);
        }
    }
}
