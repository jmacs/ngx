using NgxLib;

namespace Prototype.Components
{
    [ObjectKey(Component.CoinBag)]
    public class CoinBag : NgxComponent
    {
        public int NumberOfCoins { get; set; }

        public override void Initialize()
        {
            NumberOfCoins = 0;
        }
    }
}
