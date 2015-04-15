using System.Collections.Generic;

namespace NgxLib
{
    /// <summary>
    /// A collection of prefab arguments
    /// </summary>
    public class PrefabArgs : Dictionary<string, string>
    {
        /// <summary>
        /// The entity X coordinate
        /// </summary>
        public readonly int X;

        /// <summary>
        /// The entity Y coordinate
        /// </summary>
        public readonly int Y;

        /// <summary>
        /// The entity UID (Unique ID); 
        /// used for identifing unique objects in the world.
        /// </summary>
        public readonly int UID;

        /// <summary>
        /// The entity MID (Map ID);
        /// used for identifing the map that the entity belongs to.
        /// </summary>
        public readonly int MID;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrefabArgs"/> class.
        /// </summary>
        public PrefabArgs()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PrefabArgs"/> class.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public PrefabArgs(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PrefabArgs"/> class.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="uid">The unique ID.</param>
        /// <param name="mid">The map ID.</param>
        /// <param name="values">The prefab arguments.</param>
        public PrefabArgs(int x, int y, int uid, int mid, IDictionary<string, string> values) : base(values)
        {
            X = x;
            Y = y;
            UID = uid;
            MID = mid;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PrefabArgs"/> class.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="uid">The unique ID.</param>
        /// <param name="mid">The map ID.</param>
        public PrefabArgs(int x, int y, int uid, int mid)
        {
            X = x;
            Y = y;
            UID = uid;
            MID = mid;
        }

        /// <summary>
        /// Gets a argument value as a string.
        /// </summary>
        /// <param name="key">The argument key.</param>
        /// <returns>The argument value</returns>
        public string GetString(string key)
        {
            string result;
            TryGetValue(key, out result);
            return result;
        }

        /// <summary>
        /// Gets a argument value as a float.
        /// </summary>
        /// <param name="key">The argument key.</param>
        /// <returns>The argument value</returns>
        public float GetFloat(string key)
        {
            string result;
            TryGetValue(key, out result);
            float value;
            float.TryParse(result, out value);
            return value;
        }

        /// <summary>
        /// Gets a argument value as an integer.
        /// </summary>
        /// <param name="key">The argument key.</param>
        /// <returns>The argument value</returns>
        public int GetInt(string key)
        {
            string result;
            TryGetValue(key, out result);
            int value;
            int.TryParse(result, out value);
            return value;
        }

        /// <summary>
        /// Gets a argument value as a boolean.
        /// </summary>
        /// <param name="key">The argument key.</param>
        /// <returns>The argument value</returns>
        public bool GetBool(string key)
        {
            string result;
            TryGetValue(key, out result);
            bool value;
            bool.TryParse(result, out value);
            return value;
        }     
    }
}