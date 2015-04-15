namespace NgxLib
{
    /// <summary>
    /// A collection of user-defined session variables 
    /// that are persistent during a user's game session.
    /// </summary>
    public class NgxSession : Hash<object>
    {
        /// <summary>
        /// Gets the value with the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns>The value as T</returns>
        public T Get<T>(string key) where T : class
        {
            object o;
            if (TryGetValue(key, out o))
            {
                return o as T;
            }
            return null;
        }
    }
}
