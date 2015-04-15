namespace NgxLib.Collisions
{
    /// <summary>
    /// The masks of two components that can collide and 
    /// the message id to fire when the collision occurs.
    /// </summary>
    public struct CollisionPair
    {
        public readonly Mask Left;
        public readonly Mask Right;
        public readonly int Topic;

        public CollisionPair(Mask left, Mask right, int topic)
        {
            Left = left;
            Right = right;
            Topic = topic;
        }
    }
}