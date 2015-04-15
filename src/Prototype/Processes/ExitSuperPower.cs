using NgxLib;
using NgxLib.Processing;
using Prototype.Components;
using Prototype.Entities;

namespace Prototype.Processes
{
    public class ExitSuperPower : ProcessModule
    {
        protected Animator Animator { get; set; }
        protected int Entity { get; set; }

        public ExitSuperPower(int entity)
        {
            Entity = entity;

            Root = new Sequence(
                new Task(PlaySound),
                new Task(FreezeTime),
                new Task(DoTransitionAnimation),
                new Task(UnfreezeTime),
                new Task(RemoveComponent)
                );
        }

        public override void Initialize(NgxRuntime runtime)
        {
            base.Initialize(runtime);
            Animator = Runtime.Database.Component<Animator>(Entity);
        }

        protected ProcessStatus PlaySound()
        {
            Ngx.Messenger.Send(Msg.Play_Sound, sound: Snd.WarpPipe);
            return ProcessStatus.Success;
        }

        protected ProcessStatus FreezeTime()
        {
            Time.TimeScale = 0;
            return ProcessStatus.Success;
        }

        protected ProcessStatus DoTransitionAnimation()
        {
            if (Duration < 0.5f)
            {
                if (Animator.Animation == Mario.NormalIdleAnimation)
                {
                    Animator.Animation = Mario.SuperIdleAnimation;
                }
                else
                {
                    Animator.Animation = Mario.NormalIdleAnimation;
                }
                return ProcessStatus.Running;
            }
            return ProcessStatus.Success;
        }

        protected ProcessStatus UnfreezeTime()
        {
            Time.TimeScale = 1;
            return ProcessStatus.Success;
        }

        protected ProcessStatus RemoveComponent()
        {
            Runtime.Database.Table<SuperPower>().Remove(Entity);
            Runtime.Database.New<NullPower>(Entity);
            return ProcessStatus.Success;
        }
    }
}
