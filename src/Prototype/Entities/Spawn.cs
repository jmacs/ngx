using NgxLib;
using Prototype.Components;

namespace Prototype.Entities
{
    public class Spawn : IPrefab
    {
        public const string Name = "Spawn";

        public int CreateEntity(NgxDatabase db, PrefabArgs args)
        {
            return Create(db, args);
        }

        public static int Create(NgxDatabase db, PrefabArgs args)
        {
            var ent = db.CreateEntity();

            var meta = db.New<MetaData>(ent);
            meta.Prefab = Name;

            var spatial = db.New<Spatial>(ent);
            spatial.X = args.X;
            spatial.Y = args.Y;

            var spawn = db.New<SpawnPoint>(ent);
            spawn.X = args.X;
            spawn.Y = args.Y;
            spawn.SpawnID = args.GetInt("SpawnID");
            spawn.UID = args.UID;

            var locale = db.New<MapLocale>(ent);
            locale.MID = args.MID;

            return ent;
        }
    }
}
