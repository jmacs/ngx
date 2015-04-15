using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;

namespace NgxLib
{
    public delegate void NgxTableEventHandler<T>(NgxTable<T> table, T component) where T : NgxComponent, new();

    /// <summary>
    /// A collection of game components.
    /// </summary>
    /// <typeparam name="T">The component type</typeparam>
    public class NgxTable<T> : INgxTable where T : NgxComponent, new()
    {
        protected NgxDatabase Database { get; set; }
        protected Index<T> Components { get; set; }
        protected ObjectPool<T> Pool { get; set; }
        protected Queue<T> AddQueue { get; set; }
        protected Queue<int> RemoveQueue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this table is modified.
        /// If modifed, the table will commit any adds/deletes made.
        /// </summary>
        /// <value>
        ///   <c>true</c> if modified; otherwise, <c>false</c>.
        /// </value>
        public bool Modified { get; private set; }

        /// <summary>
        /// Gets the message handle for this table's component added event.
        /// </summary>
        /// <value>
        /// The message handle.
        /// </value>
        public int ComponentAdded { get; private set; }

        /// <summary>
        /// Gets the message handle for this table's component removed event.
        /// </summary>
        /// <value>
        /// The message handle.
        /// </value>
        public int ComponentRemoved { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NgxTable{T}"/> class.
        /// </summary>
        /// <param name="database">The database.</param>
        /// <param name="capacity">The capacity.</param>
        public NgxTable(NgxDatabase database, int capacity)
        {
            Database = database;
            Components = new Index<T>(capacity);
            Pool = new ObjectPool<T>(capacity);
            AddQueue = new Queue<T>(capacity);
            RemoveQueue = new Queue<int>(capacity);
            var meta = Ngx.Components.Get<T>();
            ComponentAdded = meta.AddMessageKey;
            ComponentRemoved = meta.RemoveMessageKey;
        }

        /// <summary>
        /// Gets the number of components in this table.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Count
        {
            get { return Components.Count; }
        }

        /// <summary>
        /// Gets the specified entity's component.
        /// </summary>
        /// <value>
        /// The component
        /// </value>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public T this[int entity]
        {
            get { return Get(entity); }
        }

        /// <summary>
        /// Gets the component for the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public T Get(int entity)
        {
            T component;
            Components.TryGetValue(entity, out component);
            return component;
        }

        /// <summary>
        /// Gets the type of the component in this table.
        /// </summary>
        /// <returns></returns>
        public Type GetComponentType()
        {
            return GetType().GetGenericArguments()[0];
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, T>.Enumerator GetEnumerator()
        {
            return Components.GetEnumerator();
        }

        /// <summary>
        /// Execute the specified function on each of the components in the table
        /// </summary>
        /// <typeparam name="TArg">The type of the argument.</typeparam>
        /// <param name="arg">The argument.</param>
        /// <param name="iteratee">The action to execute.</param>
        public void ForEach<TArg>(TArg arg, Action<TArg,T> iteratee)
        {
            foreach (var component in Components)
            {
                iteratee(arg, component.Value);
            }
        }

        /// <summary>
        /// Executes the specified predicate on all components. 
        /// Returns the first component to meet the predicate condition.
        /// </summary>
        /// <typeparam name="TArg">The type of the argument.</typeparam>
        /// <param name="arg">The argument.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        public T Single<TArg>(TArg arg, Func<TArg, T, bool> predicate)
        {
            foreach (var component in Components)
            {
                if (predicate(arg, component.Value))
                {
                    return component.Value;
                }
            }
            return null;
        }

        /// <summary>
        /// Gets the first component in the table. 
        /// This is a non-deterministic operation; may return 
        /// different results each time it is called.
        /// </summary>
        /// <returns></returns>
        public T First()
        {
            foreach (var item in Components) 
                return item.Value;
            return null;
        }

        /// <summary>
        /// Creates a new component for the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Attempt to create component an entity value less than 1</exception>
        public T New (int entity)
        {
            if (entity < 1) throw new Exception("Attempt to create component with id of " + entity);
            
            T component;
            if (Components.TryGetValue(entity, out component))
            {
                return component;
            }

            component = Pool.Get();
            component.Bind(entity);
            AddQueue.Enqueue(component);
            Modified = true;
            Database.Modified = true;
            return component;
        }

        /// <summary>
        /// Removes the specified entity from the table.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Remove(int entity)
        {
            Modified = true;
            Database.Modified = true;

            T component;
            if (Components.TryGetValue(entity, out component))
            {
                RemoveQueue.Enqueue(entity);
            }
        }

        /// <summary>
        /// Commits any add or remove operations performed in the last frame.
        /// </summary>
        public void Commit()
        {
            while (RemoveQueue.Count > 0)
            {
                PrivateRemove( RemoveQueue.Dequeue() );
            }

            while (AddQueue.Count > 0)
            {
                PrivateAdd( AddQueue.Dequeue() );
            }
            Modified = false;
        }

        /// <summary>
        /// Destroys this instance.
        /// </summary>
        public void Destroy()
        {
            foreach (var item in Components)
            {
                item.Value.Unbind();
                item.Value.Destroy();
            }
            Components.Clear();
            AddQueue.Clear();
            Pool.Clear();
            RemoveQueue.Clear();
            Database = null;
        }

        /// <summary>
        /// Clears all components in this instance.
        /// </summary>
        public void Clear()
        {
            Components.Clear();
            AddQueue.Clear();
            RemoveQueue.Clear();
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0}[{1}]", GetType().Name, Components.Count);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Destroy();
        }

        /// <summary>
        /// Determines whether the table contains the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        ///   <c>true</c> if [contains] [the specified entity]; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(int entity)
        {
            return Components.ContainsKey(entity);
        }

        /// <summary>
        /// Enables the specified entity component.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Enable(int entity)
        {
            T component;
            if(Components.TryGetValue(entity, out component))
            {
                component.Enabled = true;
            }
        }

        /// <summary>
        /// Disables the specified entity component.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Disable(int entity)
        {
            T component;
            if (Components.TryGetValue(entity, out component))
            {
                component.Enabled = false;
            }
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        /// Generates an object from its XML representation.
        /// </summary>
        /// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> stream from which the object is deserialized.</param>
        public void ReadXml(XmlReader reader)
        {
            // TODO: read xml
            var component = Pool.Get(); 
            component.ReadXml(reader);
            PrivateAdd(component);
        }

        /// <summary>
        /// Converts an object into its XML representation.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> stream to which the object is serialized.</param>
        public void WriteXml(XmlWriter writer)
        {
            foreach (var item in Components)
            {
                writer.WriteStartElement(GetType().Name);
                item.Value.WriteXml(writer);
                writer.WriteEndElement();
            }
        }

        NgxComponent INgxTable.Get(int entity)
        {
            return Get(entity);
        }

        NgxComponent INgxTable.New(int entity)
        {
            return New(entity);
        }

        protected void PrivateRemove(int entity)
        {
            Ngx.Messenger.Send(ComponentRemoved, entity);

            var component = Components[entity];
            component.Exit();            
            Components.Remove(entity);
            Database.OnComponentRemove(component);
            Pool.Release(component);
            component.Unbind();
        }

        protected void PrivateAdd(T component)
        {
            Ngx.Messenger.Send(ComponentAdded, component.Entity);

            Components.Add(component.Entity, component);
            Database.OnComponentAdd(component);
            component.Enabled = true;
            component.Enter();
        }
    }
}