using NgxLib;
using NgxLib.Behaviors;
using NgxLib.Maps;
using Prototype.Components;

namespace Prototype.Behaviors
{
    public class MobBehavior : BehaviorModule
    {
        private const int Left = 0;
        private const int Right = 1;

        public MobBehavior()
        {
            Root = new RootSelector(

                new Indexer(GetWalkDirection,
                    new Task(WalkToLeft),
                    new Task(WalkToRight) )                
                );
        }

        protected NgxTable<Controller> CtrlTable { get; set; }
        protected NgxTable<RigidBody> BodyTable { get; set; }
        protected NgxTable<Brain> BrainTable { get; set; }
        protected MapManager MapManager { get; set; }

        public override void Initialize(NgxContext context)
        {
            var database = context.Database;
            BodyTable = database.Table<RigidBody>();
            MapManager = context.MapManager;
            CtrlTable = database.Table<Controller>();
            BrainTable = database.Table<Brain>();
        }
      
        private int GetWalkDirection()
        {
            var brain = BrainTable[Entity];
            var body = BodyTable[Entity];

            if (body.Prefab == "Mushroom")
            {
            }

            if (body.WallSensory.Contains(Side.RightCenter))
            {
                brain.WalkDirection = Left;
            }
            else if (body.WallSensory.Contains(Side.LeftCenter))
            {
                brain.WalkDirection = Right;
            }

            return brain.WalkDirection;
        }

        private BahviorStatus WalkToLeft()
        {
            CtrlTable[Entity].Do(Ctrl.Left);
            return BahviorStatus.Success;
        }

        private BahviorStatus WalkToRight()
        {
            CtrlTable[Entity].Do(Ctrl.Right);
            return BahviorStatus.Success;
        }

    }
}
