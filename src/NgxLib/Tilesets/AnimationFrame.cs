namespace NgxLib.Tilesets
{
    /// <summary>
    /// A single animation frame
    /// </summary>
    public class AnimationFrame
    {
        public int TileId { get; private set; }

        public float FrameTime { get; private set; }

        public AnimationFrame(int tileId, float frameTime)
        {
            TileId = tileId;
            FrameTime = frameTime;
        }
    }
}
