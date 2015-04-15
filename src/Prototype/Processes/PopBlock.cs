using Microsoft.Xna.Framework;
using NgxLib;
using NgxLib.Maps;
using NgxLib.Processing;

namespace Prototype.Processes
{
    public abstract class PopBlock : ProcessModule
    {
        // the height to pop the block
        private const float BumpSpeed = 100;
        private const float BumpHeight = 8;

        protected int Sound { get; set; }
        protected Map Map { get; set; }
        protected Cell Block { get; set; }
        protected NgxRectangle OriginalArea;

        //TODO: redesign where/how process tree gets build so other processes can inherit without allocating 2 trees
        protected PopBlock(Cell block, int sound)
        {
            Block = block;
            Sound = sound;
            OriginalArea = block.Area;
        }

        public override void Initialize(NgxRuntime runtime)
        {
            base.Initialize(runtime);

            Map = runtime.Context.MapManager.Map;
        }

        protected ProcessStatus PlaySound()
        {
            Ngx.Messenger.Send(Msg.Play_Sound, sound: Sound);

            return ProcessStatus.Success;
        }

        // bump block
        protected ProcessStatus BumpBlockUp()
        {
            var target = new Vector2(OriginalArea.X, OriginalArea.Y - BumpHeight);
            var p = Block.Area.Location.Move(target, BumpSpeed * Time.Delta);
            if (p == target) return ProcessStatus.Success;
            Block.Area = new NgxRectangle(p.X, p.Y, Block.Area.Width, Block.Area.Height);
            return ProcessStatus.Running;
        }

        protected ProcessStatus BumpBlockDown()
        {
            var target = new Vector2(OriginalArea.X, OriginalArea.Y);
            var p = Block.Area.Location.Move(target, BumpSpeed * Time.Delta);
            if (p == target)
            {
                Block.Area = OriginalArea;
                return ProcessStatus.Success;
            }
            Block.Area = new NgxRectangle(p.X, p.Y, Block.Area.Width, Block.Area.Height);
            return ProcessStatus.Running;
        }

        protected ProcessStatus MakeBlockEmpty()
        {
            Map.SetCell(Block.X, Block.Y, WorldTile.EmptyBlock);
            Runtime.Database.Remove(Block.Decorator);
            Block.Decorator = 0;
            Block.DecoratorType = 0;
            return ProcessStatus.Success;
        }
    }
}