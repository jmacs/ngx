
namespace NgxLib
{
    /// <summary>
    /// A factory to help with the creation (or prefabrication) of new entities.
    /// </summary>
    public interface IPrefab
    {
        /// <summary>
        /// Creates an entity in the database.
        /// </summary>
        /// <param name="db">The database.</param>
        /// <param name="args">The prefab arguments.</param>
        /// <returns>The entity</returns>
        int CreateEntity(NgxDatabase db, PrefabArgs args);
    }
}