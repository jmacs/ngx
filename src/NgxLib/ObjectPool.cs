using System.Collections.Generic;

namespace NgxLib
{
    /// <summary>
    /// Controls access to a pool of IRenewable objects.
    /// A pool helps minimize object allocations and garbage collection.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObjectPool<T> where T : IRenewable, new()
    {
        private readonly Queue<T> _items = new Queue<T>();
        private int _allocated;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectPool{T}"/> class.
        /// </summary>
        public ObjectPool()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectPool{T}"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        public ObjectPool(int capacity)
        {
            for (int i = 0; i < capacity; i++)
            {
                _items.Enqueue(new T());
            }
        }

        /// <summary>
        /// Get a new instance.
        /// </summary>
        /// <returns></returns>
        public T Get()
        {
            lock(_items)
            {
                if (_items.Count > 0)
                {
                    var obj = _items.Dequeue();
                    obj.Initialize();
                    _allocated++;
                    return obj;
                }
                else
                {
                    var obj = new T();
                    obj.Initialize();
                    _allocated++;
                    return obj;
                }
            }
        }
 
        public void Release(T obj)
        {
            obj.Destroy();
 
            lock (_items)
            {
                _items.Enqueue(obj);
                _allocated--;
            }
        }

        public void Clear()
        {
            _items.Clear();
            _allocated = 0;
        }
    }
}
