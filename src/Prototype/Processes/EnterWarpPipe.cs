using Microsoft.Xna.Framework;
using NgxLib;
using NgxLib.Maps;
using NgxLib.Processing;
using Prototype.Components;
using Prototype.Entities;

namespace Prototype.Processes
{
    public class EnterWarpPipe : ProcessModule
    {
        protected NgxDatabase Database { get; set; }
        protected Spatial EntitySpatial { get; set; }
        protected MapManager MapManager { get; set; }
        protected Vector2 EnterPipePosition { get; set; }
        protected Vector2 ExitPipePosition { get; set; }
        protected int PlayerEntity { get; set; }
        protected int PipeEntity { get; set; }
        protected int EnterPipeID { get; set; }
        protected int ExitPipeID { get; set; }
        protected int ExitMID { get; set; }
        protected bool IsNewMap { get; set; }

        public EnterWarpPipe(int playerEntity, int pipeEntity)
        {
            PlayerEntity = playerEntity;
            PipeEntity = pipeEntity;
            
            Root = new Sequence(
                        new Task(DisableControl),
                        new Task(InitProc),
                        new Task(ToggleSpriteLayer),
                        new Task(ToggleWarpAnimation),
                        new Task(Delay),
                        new Task(PlaySound),
                        new Task(MoveEntityDown),
                        new Task(LoadDestinationMap),
                        new Task(SpawnEntities),
                        new Task(FindExitPipe),
                        new Task(WarpToDestination),
                        new Task(Delay),
                        new Task(PlaySound),
                        new Task(MoveEntityUp),
                        new Task(ToggleSpriteLayer),
                        new Task(EnableControl) 
                    );
        }

        public override void Initialize(NgxRuntime runtime)
        {
            base.Initialize(runtime);
            Database = runtime.Database;
            MapManager = runtime.Context.MapManager;
            EntitySpatial = Database.Component<Spatial>(PlayerEntity);
        }

        private ProcessStatus InitProc()
        {
            var enter = Database.Component<WarpPipeConnection>(PipeEntity);
            EnterPipePosition = enter.Position;
            EnterPipeID = enter.PipeID;
            ExitPipeID = enter.Connection;
            ExitMID = enter.MID;
            return ProcessStatus.Success;
        }

        private ProcessStatus DisableControl()
        {
            Database.Component<Controller>(PlayerEntity).Reset();
            Database.Table<Player>().Disable(PlayerEntity);
            Database.Table<Mobility>().Disable(PlayerEntity);
            Database.Table<RigidBody>().Disable(PlayerEntity);
            return ProcessStatus.Success;
        }

        private ProcessStatus EnableControl()
        {
            Database.Table<Mobility>().Enable(PlayerEntity);
            Database.Table<Player>().Enable(PlayerEntity);
            Database.Table<RigidBody>().Enable(PlayerEntity);
            return ProcessStatus.Success;
        }

        private ProcessStatus ToggleSpriteLayer()
        {
            var sprite = Database.Component<Sprite>(PlayerEntity);
            if (sprite.Layer == SpriteLayer.Back)
            {
                sprite.Layer = SpriteLayer.Front;
            }
            else
            {
                sprite.Layer = SpriteLayer.Back;
            }
            return ProcessStatus.Success;
        }

        private ProcessStatus ToggleWarpAnimation()
        {
            var anim = Database.Component<Animator>(PlayerEntity);
            anim.Animation = Mario.NormalHeadshotAnimation;
            return ProcessStatus.Success;
        }

        private ProcessStatus Delay()
        {
            if (Duration > 0.3f)
            {
                return ProcessStatus.Success;
            }
            return ProcessStatus.Running;
        }

        private ProcessStatus MoveEntityDown()
        {
            var dest = EnterPipePosition + new Vector2(0, 16);

            if (EntitySpatial.Move(ref dest, 0.5f))
            {
                return ProcessStatus.Success;
            }
            return ProcessStatus.Running;
        }

        private ProcessStatus LoadDestinationMap()
        {
            var map = MapManager.Map;

            if (map.MID == ExitMID)
            {
                return ProcessStatus.Success;
            }

            var currentMID = map.MID;
            MapManager.Transition(ExitMID);
            Database.Table<MapLocale>().ForEach(currentMID, RemovePrevMapEntities);
            IsNewMap = true;
            return ProcessStatus.Success;
        }

        private void RemovePrevMapEntities(int mid, MapLocale locale)
        {
            if (locale.MID == mid)
            {
                Database.Remove(locale.Entity);
            }
        }

        protected ProcessStatus SpawnEntities()
        {
            if (!IsNewMap) return ProcessStatus.Success;

            var db = Database;
            var map = MapManager.Map;

            for (int i = 0; i < map.Objects.Count; i++)
            {
                var m = map.Objects[i];
                Ngx.Prefabs.Create(db, m.Prefab, m.Args);
            }

            return ProcessStatus.Success;
        }


        private ProcessStatus FindExitPipe()
        {
            var exit = Database.Table<WarpPipeConnection>()
                .Single(ExitPipeID, FindPipe);

            if (exit == null)
            {
                Logger.Log("cannot find exit pipe, {0}->{1}", ExitPipeID, ExitPipeID);
                return ProcessStatus.Failure;
            }

            ExitPipePosition = exit.Position;
            return ProcessStatus.Success;
        }

        private bool FindPipe(int pipeId, WarpPipeConnection pipe)
        {
            return pipe.PipeID == pipeId;
        }

        private ProcessStatus WarpToDestination()
        {
            var player = Database.Component<Spatial>(PlayerEntity);
            player.Position = ExitPipePosition;
            return ProcessStatus.Success;
        }

        private ProcessStatus MoveEntityUp()
        {
            var dest = ExitPipePosition - new Vector2(0, 16);

            if (EntitySpatial.Move(ref dest, 0.5f))
            {
                return ProcessStatus.Success;
            }
            return ProcessStatus.Running;
        }

        private ProcessStatus PlaySound()
        {
            Ngx.Messenger.Send(Msg.Play_Sound, sound: Snd.WarpPipe);

            return ProcessStatus.Success;
        }
    }
}
