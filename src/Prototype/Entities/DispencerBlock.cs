using NgxLib;
using Prototype.Components;

namespace Prototype.Entities
{
    public class DispencerBlock : IPrefab
    {
        public const string Name = "DispencerBlock";

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

            var decorator = db.New<Decorator>(ent);
            decorator.DecoratorType = Component.PickUp;

            var dispencer = db.New<Dispencer>(ent);
            dispencer.ItemID = args.GetInt("ItemID");

            var locale = db.New<MapLocale>(ent);
            locale.MID = args.MID;

            return ent;
        }
    }
}
