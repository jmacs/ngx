using System;
using System.Reflection;

namespace NgxLib
{
    /// <summary>
    /// A catalog of "Prefab" objects used for entity prefabrication.
    /// </summary>
    public class NgxPrefabCollection
    {        
        private readonly Hash<IPrefab> _prefabs = new Hash<IPrefab>();

        /// <summary>
        /// Registers all IPrefab implementations in the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        public void Register(Assembly assembly)
        {
            var prefabType = typeof (IPrefab);
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (prefabType.IsAssignableFrom(type)
                    && type.IsClass && !type.IsAbstract)
                {
                    var instantce = Activator.CreateInstance(type) as IPrefab;
                    _prefabs.Add(type.Name, instantce);
                }
            }
        }

        /// <summary>
        /// Finds the prefab with the speficied name and 
        /// executes the prefab in the speficied database.
        /// </summary>
        public int Create<T>(NgxDatabase db, int x, int y)
        {
            var name = typeof (T).Name;
            var prefab = _prefabs[name];
            return prefab.CreateEntity(db, new PrefabArgs(x,y));
        }

        /// <summary>
        /// Finds the prefab with the speficied name and 
        /// executes the prefab in the speficied database.
        /// </summary>
        /// <param name="db">The database to create the entity in.</param>
        /// <param name="name">The name of the prefab.</param>
        /// <returns>The created entity</returns>
        public int Create(NgxDatabase db, string name)
        {
            var prefab = _prefabs[name];
            return prefab.CreateEntity(db, null);
        }

        /// <summary>
        /// Finds the prefab with the speficied name and
        /// executes the prefab in the speficied database.
        /// </summary>
        /// <param name="db">The database to create the entity in.</param>
        /// <param name="name">The name of the prefab.</param>
        /// <param name="args">The prefab arguments.</param>
        /// <returns>The created entity</returns>
        public int Create(NgxDatabase db, string name, PrefabArgs args)
        {
            IPrefab prefab;
            if(_prefabs.TryGetValue(name, out prefab))
            {
                try
                {
                    return prefab.CreateEntity(db, args);
                }
                catch (Exception ex)
                {
                    Logger.Log("Could not create prefab '{0}' -> {1}", name, ex.Message);
                    return -1;
                }
            }

            Logger.Log("Unknown prefab type '{0}'", name);
            return -1;
        }
    }
}
