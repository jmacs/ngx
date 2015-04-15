using NgxLib;
using NgxLib.Processing;
using Prototype.Components;

namespace Prototype.Processes
{
    public class TouchMushroom : ProcessModule 
    {
        protected Animator Animator { get; set; }
        protected int Entity { get; set; }

        public TouchMushroom(int entity)
        {
            Entity = entity;

            Root = new Sequence(
                new Task(AddScore),
                new Task(PlaySound),
                new Task(AbortIfNotNullPower),
                new Task(FreezeTime),
                new Task(DoTransitionAnimation),
                new Task(UnfreezeTime),
                new Task(AddComponent)
                );
        }

        public override void Initialize(NgxRuntime runtime)
        {
            base.Initialize(runtime);

            Animator = Runtime.Database.Component<Animator>(Entity);
        }

        protected ProcessStatus AddScore()
        {
            return ProcessStatus.Success;
        }

        protected ProcessStatus AbortIfNotNullPower()
        {
            var db = Runtime.Database;
            if (db.Table<NullPower>().Contains(Entity))
            {
                return ProcessStatus.Success;
            }
            return ProcessStatus.Aborted;
        }

        protected ProcessStatus PlaySound()
        {
            Ngx.Messenger.Send(Msg.Play_Sound, sound: Snd.Powerup);

            return ProcessStatus.Success;
        }

        protected ProcessStatus FreezeTime()
        {
            Time.TimeScale = 0;
            return ProcessStatus.Success;
        }

        protected ProcessStatus DoTransitionAnimation()
        {
            if (Animator.Animation >= 2100)
            {
                Animator.Animation = Animator.Animation - 100;
            }
            else
            {
                Animator.Animation = Animator.Animation + 100;
            }

            if (Duration > 0.5f)
            {
                return ProcessStatus.Success;
            }
            return ProcessStatus.Running;
        }

        protected ProcessStatus UnfreezeTime()
        {
            Time.TimeScale = 1;
            return ProcessStatus.Success;
        }

        protected ProcessStatus AddComponent()
        {
            Runtime.Database.Table<NullPower>().Remove(Entity);
            Runtime.Database.New<SuperPower>(Entity);
            return ProcessStatus.Success;
        }
    }
}
