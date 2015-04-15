using NgxLib;
using NgxLib.Processing;
using Prototype.Components;
using Prototype.Entities;
using Prototype.User;

namespace Prototype.Processes
{
    public class EnterWorld : ProcessModule
    {
        protected Profile Profile { get; set; }
        protected int MID { get; set; }
        protected int PlayerEntity { get; set; }

        public EnterWorld(int mid)
        {
            MID = mid;
            Root = new Sequence(
                    new Task(InitProc),
                    new Task(TransitionContext),
                    new Task(TransitionMap),
                    new Task(SpawnEntities),
                    new Task(SpawnPlayer),
                    new Task(InitPlayerStatus)
                );
        }

        private ProcessStatus InitProc()
        {
            Profile = Runtime.Session["profile"] as Profile;
            return ProcessStatus.Success;
        }

        private ProcessStatus TransitionContext()
        {
            Runtime.Database.Clear();
            Runtime.Transition(Ctx.World);
            return ProcessStatus.Success;
        }

        private ProcessStatus TransitionMap()
        {
            Runtime.Context.MapManager.Transition(MID);
            return ProcessStatus.Success;
        }

        private ProcessStatus SpawnEntities()
        {
            var db = Runtime.Database;
            var map = Runtime.Context.MapManager.Map;

            for (int i = 0; i < map.Objects.Count; i++)
            {
                var m = map.Objects[i];
                Ngx.Prefabs.Create(db, m.Prefab, m.Args);
            }

            return ProcessStatus.Success;
        }

        private ProcessStatus SpawnPlayer()
        {
            var db = Runtime.Database;

            if (Profile.QuestLog.LastStage == 0)
            {
                var spawn = db.Table<SpawnPoint>().Single(Spwn.PlayerStart, FindSpawn);
                var args = new PrefabArgs(spawn.X, spawn.Y);
                PlayerEntity = WorldMario.Create(db, args);
            }
            else
            {
                var laststand = Profile.QuestLog.LastStage;
                var portal = db.Table<Portal>().Single(laststand, FindStage);
                var args = new PrefabArgs(portal.X, portal.Y);
                PlayerEntity = WorldMario.Create(db, args);
            }
            
            return ProcessStatus.Success;
        }

        private bool FindStage(int mid, Portal portal)
        {
            return portal.MID == mid;
        }

        private bool FindSpawn(int type, SpawnPoint spawn)
        {
            return spawn.SpawnID == type;
        }

        private ProcessStatus InitPlayerStatus()
        {
            Profile.QuestLog.LastWorld = MID;
            //TODO: give player, abilities, coins, lives, etc
            //information is in session?)
            return ProcessStatus.Success;
        }
    }
}
