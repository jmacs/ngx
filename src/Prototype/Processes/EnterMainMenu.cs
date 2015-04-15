using NgxLib.Processing;

namespace Prototype.Processes
{
    public class EnterMainMenu : ProcessModule
    {
        public EnterMainMenu()
        {
            Root = new Sequence(
                new Task(TransitionContext)
                );
        }

        private ProcessStatus TransitionContext()
        {
            Runtime.Transition(Ctx.Menu);
            return ProcessStatus.Success;
        }
    }
}
