using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NgxLib
{
    /// <summary>
    /// The central repository of game components. Each game object 
    /// or "entity" is composed of discrete objects; the raw information
    /// about a specific aspect of an entity. The database contains all
    /// tables for each component type in the game. Components are keyed
    /// on an entity value (an integer). 
    /// </summary>
    public class NgxDatabase : IXmlSerializable
    {
        internal bool Modified { get; set; }
        protected Index<EntityComposition> Compositions { get; set; }
        protected IdentityPool Identity { get; set; }
        protected Dictionary<Type, INgxTable> Tables { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NgxDatabase"/> class.
        /// </summary>
        public NgxDatabase()
        {
            Compositions = new Index<EntityComposition>(10);
            Identity = new IdentityPool();
            Tables = new Dictionary<Type, INgxTable>(10);
        }
        

        /// <summary>
        /// Gets the number of tables in this instance.
        /// </summary>
        /// <value>
        /// The number of tables.
        /// </value>
        public int Count
        {
            get { return Tables.Count; }
        }

        /// <summary>
        /// Commits any adds/removed made since the last commit.
        /// </summary>
        public void Commit()
        {
            if(!Modified) return;

            foreach (var table in Tables)
            {
                if(table.Value.Modified)
                {
                    table.Value.Commit();
                }
            }
            Modified = false;
        }

        /// <summary>
        /// Removes all components with the specified entity key.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Remove(int entity)
        {
            //TODO: prob better way to do this
            foreach (var table in Tables)
            {
                table.Value.Remove(entity);
            }
        }

        /// <summary>
        /// Creates a new entity.
        /// </summary>
        /// <returns>The entity</returns>
        public int CreateEntity()
        {
            return Identity.Next();
        }

        /// <summary>
        /// Destroys this instance.
        /// </summary>
        public void Destroy()
        {
            foreach (var table in Tables)
            {
                table.Value.Dispose();
            }
            Tables.Clear();
            Compositions.Clear();
        }

        /// <summary>
        /// Attaches a new component to the specified entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public T New<T>(int entity) where T : NgxComponent, new()
        {
            return Table<T>().New(entity);
        }

        /// <summary>
        /// Gets the component table of the specified type
        /// </summary>
        /// <typeparam name="T">The table type</typeparam>
        /// <returns>The table</returns>
        public NgxTable<T> Table<T>() where T : NgxComponent, new()
        {
            INgxTable table;
            if (Tables.TryGetValue(typeof (T), out table))
            {
                return table as NgxTable<T>;
            }

            throw new Exception("Table not found: " + typeof (T).FullName);
        }

        /// <summary>
        /// Gets the specified entity component
        /// </summary>
        /// <typeparam name="T">The component type</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public T Component<T>(int entity) where T : NgxComponent, new()
        {
            return Table<T>()[entity];
        }

        /// <summary>
        /// Gets an entity's mask. The mask is a representation 
        /// of the entities component composition.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Mask GetMask(int entity)
        {
            EntityComposition comp;
            if (Compositions.TryGetValue(entity, out comp))
            {
                return comp.Mask;
            }
            return Mask.Null;
        }

        /// <summary>
        /// Determines whether the database contains a component with the specified entity.
        /// </summary>
        /// <typeparam name="T">The component type</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns>
        ///   <c>true</c> if [contains] [the specified entity]; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains<T>(int entity) where T : NgxComponent, new()
        {
            return Table<T>().Contains(entity);
        }

        /// <summary>
        /// Clears all component tables in the database
        /// </summary>
        public void Clear()
        {
            foreach (var table in Tables)
            {
                table.Value.Clear();
            }
        }

        /// <summary>
        /// Registers a table for the specified component type.
        /// </summary>
        /// <typeparam name="T">The component type</typeparam>
        public void Register<T>() where T : NgxComponent, new()
        {
            var table = new NgxTable<T>(this, 10);
            Tables.Add(typeof(T), table);
        }

        /// <summary>
        /// Registers a table for each <see cref="NgxComponent"/> in the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly containing <see cref="NgxComponent"/> types.</param>
        public void Register(Assembly assembly)
        {
            var types = assembly.GetTypes();
            var target = typeof(NgxComponent);
            var tableGenericType = typeof(NgxTable<>);

            for (var i = 0; i < types.Length; i++)
            {
                var type = types[i];
                if (type.IsClass && !type.IsAbstract && target.IsAssignableFrom(type))
                {
                    Type[] typeArgs = { type };
                    var constructed = tableGenericType.MakeGenericType(typeArgs);
                    var table = Activator.CreateInstance(constructed, this, 10) as INgxTable;
                    Tables.Add(type, table);
                }
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Converts an object into its XML representation.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> stream to which the object is serialized.</param>
        public void WriteXml(XmlWriter writer)
        {
            throw new NotImplementedException();
        }

        internal void OnComponentAdd(NgxComponent component)
        {
            EntityComposition comp;
            if (Compositions.TryGetValue(component.Entity, out comp))
            {
                comp.AddComponent(component);
            }
            else
            {
                comp = new EntityComposition(component.Entity);
                comp.AddComponent(component);
                Compositions.Add(comp.Entity, comp);
            }
        }

        internal void OnComponentRemove(NgxComponent component)
        {
            var comp = Compositions[component.Entity];
            comp.RemoveComponent(component);
            if (comp.Components.Count == 0)
            {
                Compositions.Remove(comp.Entity);
                Identity.Release(comp.Entity);
            }
        }
    }
}