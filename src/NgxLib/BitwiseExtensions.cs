namespace NgxLib
{
    /// <summary>
    /// Helper extensions for working with bitwise operators
    /// </summary>
    public static class BitwiseExtensions
    {
        public static bool Contains(this int self, int value)
        {
            return (self & value) == value;
        }

        public static bool Any(this int self, int ctrl1, int ctrl2)
        {
            return self.Contains(ctrl1) || self.Contains(ctrl2);
        }

        public static bool Any(this int self, int ctrl1, int ctrl2, int ctrl3)
        {
            return self.Contains(ctrl1) || self.Contains(ctrl2) || self.Contains(ctrl3);
        }

        public static bool Any(this int self, int ctrl1, int ctrl2, int ctrl3, int ctrl4)
        {
            return self.Contains(ctrl1) || self.Contains(ctrl2) || self.Contains(ctrl3) || self.Contains(ctrl4);
        }
    }
}
