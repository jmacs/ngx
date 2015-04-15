using NgxLib;
using Prototype.Components;

namespace Prototype.Entities
{
    public class StagePortal : IPrefab
    {
        public const string Name = "StagePortal";

        public int CreateEntity(NgxDatabase db, PrefabArgs args)
        {
            return Create(db, args);
        }

        public static int Create(NgxDatabase db, PrefabArgs args)
        {
            var ent = db.CreateEntity();

            var metadata = db.New<MetaData>(ent);
            metadata.Prefab = Name;

            var position = db.New<Spatial>(ent);
            position.X = args.X;
            position.Y = args.Y;

            var decorator = db.New<Decorator>(ent);
            decorator.DecoratorType = Component.StagePortal;

            var portal = db.New<Components.Portal>(ent);
            portal.X = args.X;
            portal.Y = args.Y;
            portal.MID = args.GetInt("MID");

            var locale = db.New<MapLocale>(ent);
            locale.MID = args.MID;

            return ent;
        }
    }
}
