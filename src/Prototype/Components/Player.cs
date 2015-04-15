using NgxLib;

namespace Prototype.Components
{
    [ObjectKey(Component.Player)]
    public class Player : NgxComponent
    {
        public int PlayerNumber { get; set; }
        public int Lives { get; set; }
        public int Coins { get; set; }
        public int Score { get; set; }
    }
}
