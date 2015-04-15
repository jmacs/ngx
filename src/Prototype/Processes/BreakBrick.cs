using NgxLib;
using NgxLib.Maps;
using NgxLib.Processing;
using Prototype.Components;
using Prototype.Entities;

namespace Prototype.Processes
{
    public class BreakBrick : ProcessModule
    {
        protected Cell Block { get; set; }

        public BreakBrick(Cell block)
        {
            Block = block;

            Root = new Sequence(
                new Task(DestoryCell),
                new Task(PlaySound),
                new Task(SpawnBrickParticles),
                new Task(AnimateBrickParticles),
                new Task(KillBrickParticles)
                );
        }

        private ProcessStatus DestoryCell()
        {
            Runtime.Context.MapManager.Map.SetCell(Block.X, Block.Y, 0);
            return ProcessStatus.Success;
        }

        private ProcessStatus PlaySound()
        {
            Ngx.Messenger.Send(Msg.Play_Sound, sound: Snd.BreakBrick);
            return ProcessStatus.Success;
        }

        private Sprite tl;
        private Sprite tr;
        private Sprite br;
        private Sprite bl;

        private ProcessStatus SpawnBrickParticles()
        {
            var x = Block.Area.Center.X;
            var y = Block.Area.Center.Y;
            tl = BrickParticle.Create(Runtime.Database, x, y);
            tr = BrickParticle.Create(Runtime.Database, x, y);
            br = BrickParticle.Create(Runtime.Database, x, y);
            bl = BrickParticle.Create(Runtime.Database, x, y);
            return ProcessStatus.Success;
        }

        private float vxt = -20f;
        private float vxb = -20f;
        private float vyt = -105f;
        private float vyb = -75f;
        private const float grav = 20f;
        protected const float speed = 20f;
        protected const float duration = 0.7f;

        private ProcessStatus AnimateBrickParticles()
        {
            vyt += grav * speed * Time.Delta;
            
            tl.Position.Y += vyt * Time.Delta;
            tl.Position.X += vxt * Time.Delta;

            tr.Position.Y += vyt * Time.Delta;
            tr.Position.X += -vxt * Time.Delta;

            vyb += grav * speed * Time.Delta;

            bl.Position.Y += vyb * Time.Delta;
            bl.Position.X += vxb * Time.Delta;

            br.Position.Y += vyb * Time.Delta;
            br.Position.X += -vxb * Time.Delta;

            if (Duration > duration)
            {
                return ProcessStatus.Success;
            }
            return ProcessStatus.Running;
        }

        private ProcessStatus KillBrickParticles()
        {
            Runtime.Database.Remove(tl.Entity);
            Runtime.Database.Remove(tr.Entity);
            Runtime.Database.Remove(bl.Entity);
            Runtime.Database.Remove(br.Entity);
            return ProcessStatus.Success;
        }
        
    }
}
