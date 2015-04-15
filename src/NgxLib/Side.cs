using System;

namespace NgxLib
{
    [Flags]
    public enum Side
    {
        None = 0,
        TopCenter = 1,
        TopLeft = 2,
        LeftCenter = 4,
        BottomLeft = 8,
        BottomCenter = 16,
        BottomRight = 32,
        RightCenter = 64,
        TopRight = 128
    }

    public static class HotspotExtensions
    {
        public static bool Contains(this Side side, Side value)
        {
            return (side & value) == value;
        }
    }
}
