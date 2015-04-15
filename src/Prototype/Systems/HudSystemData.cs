using NgxLib.Text;

namespace Prototype.Systems
{
    public class HudSystemData
    {
        private int _coins;
        public NgxString Coins { get; set; }

        private int _score;
        public NgxString Score { get; set; }

        public HudSystemData()
        {
            Coins = new NgxString();
            Score = new NgxString();
        }

        public void SetCoins(int value)
        {
            if (_coins == value) return;
            _coins = value;
            Coins.Text = "Coins: " + value;
        }

        public void SetScore(int value)
        {
            if (_score == value) return;
            _score = value;
            Score.Text = "Score: " + value;
        }
    }
}