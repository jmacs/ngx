namespace NgxLib
{
    /// <summary>
    /// An interface to allow an object to be pooled in an Object Pool.
    /// </summary>
    public interface IRenewable
    {
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Destroys this instance.
        /// </summary>
        void Destroy();
    }
}