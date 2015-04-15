using System.Collections.Generic;

namespace NgxLib
{
    /// <summary>
    /// Represents a collection of values keyed by integers.
    /// </summary>
    /// <typeparam name="T">The value type</typeparam>
    public class Index<T> : NgxDictionary<int, T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Index{T}"/> class.
        /// </summary>
        public Index()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Index{T}"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        public Index(int capacity) : base(capacity)
        {
        }
    }

    /// <summary>
    /// Represents a collection of values keyed by strings.
    /// </summary>
    /// <typeparam name="T">The value type</typeparam>
    public class Hash<T> : NgxDictionary<string, T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Hash{T}"/> class.
        /// </summary>
        public Hash()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Hash{T}"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        public Hash(int capacity) : base(capacity)
        {
        }
    }

    /// <summary>
    /// Represents a collection of keys and values.
    /// </summary>
    /// <typeparam name="TKey">The key type</typeparam>
    /// <typeparam name="TValue">The value type</typeparam>
    public class NgxDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="NgxDictionary{TKey,TValue}"/> class.
        /// </summary>
        public NgxDictionary()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="NgxDictionary{TKey,TValue}"/> class.
        /// </summary>
        /// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Generic.Dictionary`2" /> can contain.</param>
        public NgxDictionary(int capacity) : base(capacity)
        {
        }

        /// <summary>
        /// Gets the value with the specified key if it exists. 
        /// Returns null if an item does not exist with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The value if exists; otherwise null</returns>
        public TValue TryGet(TKey key)
        {
            TValue value;
            TryGetValue(key, out value);
            return value;
        }

        /// <summary>
        /// Puts the item in the collection with the specified key.
        /// Overwrites the existing item if it exists
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>The overwritten value if one existed; otherwise null</returns>
        public TValue Put(TKey key, TValue value)
        {
            TValue item;
            if (TryGetValue(key, out item))
            {
                this[key] = value;
                return item;
            }
            Add(key, value);
            return item;
        }

        /// <summary>
        /// Removed the speficied item from the hash if it exists.
        /// </summary>
        /// <param name="key">The hash key.</param>
        /// <returns>The removed value if one existed; otherwise null</returns>
        public TValue Take(TKey key)
        {
            TValue item;
            if (TryGetValue(key, out item))
            {
                Remove(key);
            }
            return item;
        }
    }
}
