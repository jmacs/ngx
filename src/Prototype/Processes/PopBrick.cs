using NgxLib.Maps;
using NgxLib.Processing;

namespace Prototype.Processes
{
    public class PopBrick : PopBlock
    {        
        public PopBrick(Cell block) : base(block, Snd.Bump)
        {
            Root = new Parallel(
                        new Sequence(
                            new Task(BumpBlockUp),
                            new Task(BumpBlockDown)),
                        new Task(PlaySound));
        }      
    }
}
