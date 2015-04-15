using NgxLib;
using NgxLib.Maps;
using NgxLib.Processing;
using Prototype.Entities;

namespace Prototype.Processes
{
    public class PopPower : PopBlock
    {
        protected int PowerupEntity { get; set; }

        public PopPower(Cell cell) : base(cell, Snd.PowerupAppear)
        {
            Root = new Parallel(
                new Sequence(
                    new Task(BumpBlockUp),
                    new Task(PlaySound),
                    new Task(BumpBlockDown)),
                new Sequence(
                    new Task(SpawnPowerUpItem),
                    new Task(RaisePowerUpItem),
                    new Task(MakeBlockEmpty))
                    );
        }

        protected ProcessStatus SpawnPowerUpItem()
        {
            var args = new PrefabArgs((int)Block.Area.X, (int)Block.Area.Y);
            PowerupEntity = Mushroom.Create(Runtime.Database, args);
            return ProcessStatus.Success;
        }

        protected ProcessStatus RaisePowerUpItem()
        {
            return ProcessStatus.Success;
        }
    }
}
