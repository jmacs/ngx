using Microsoft.Xna.Framework;
using NgxLib;
using NgxLib.Maps;
using NgxLib.Processing;
using Prototype.Components;
using Prototype.Entities;

namespace Prototype.Processes
{
    public class PopCoin : PopBlock
    {
        // the height to pop the brick
        private const float CoinPopHeight = 48;
        private const float CoinPopSpeed = 250;

        protected int CoinPopEntity { get; set; }
        protected NgxTable<CoinBag> CoinPurseTable { get; set; }
        protected NgxTable<Player> Player { get; set; }        
        protected NgxDatabase Database { get; set; }
        protected NgxTable<Spatial> SpatialTable { get; set; }

        public PopCoin(Cell block) : base(block, Snd.Coin)
        {
            Root = new Parallel(
                new Sequence(
                    new Task(BumpBlockUp),
                    new Task(BumpBlockDown) ),
                new Sequence(
                    new Task(SpawnCoinPop),
                    new Task(PlaySound),
                    new Task(AddCoinToPlayer),
                    new Condition(CoinsRemain, 
                        new Task(MakeBlockEmpty)),
                    new Task(DispenceCoin) )
                );
        }

        public override void Initialize(NgxRuntime runtime)
        {
            base.Initialize(runtime);
            Database = runtime.Database;
            CoinPurseTable = Database.Table<CoinBag>();
            SpatialTable = Database.Table<Spatial>();
            Player = Database.Table<Player>();
        }

        // animate coins
        protected ProcessStatus SpawnCoinPop()
        {
            var args = new PrefabArgs((int)OriginalArea.X, (int)OriginalArea.Y);
            CoinPopEntity = CoinPop.Create(Database, args); 
            return ProcessStatus.Success;
        }

        protected ProcessStatus DispenceCoin()
        {
            var spatial = SpatialTable[CoinPopEntity];
            var target = new Vector2(OriginalArea.X, OriginalArea.Y - CoinPopHeight);
            var p = spatial.Position.Move(target, CoinPopSpeed * Time.Delta);
            if (p == target)
            {
                Database.Remove(CoinPopEntity);
                return ProcessStatus.Success;
            }
            spatial.Position = p;
            return ProcessStatus.Running;
        }

        // add coin to player
        protected ProcessStatus AddCoinToPlayer()
        {
            var coinPurse = CoinPurseTable[Block.Decorator];
            coinPurse.NumberOfCoins--;
            Player.First().Coins++;
            return ProcessStatus.Success;
        }

        protected bool CoinsRemain()
        {
            var coinPurse = CoinPurseTable[Block.Decorator];
            return coinPurse.NumberOfCoins < 1;
        }
    }
}
