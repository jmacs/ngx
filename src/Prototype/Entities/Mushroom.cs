using NgxLib;
using Prototype.Components;

namespace Prototype.Entities
{
    public class Mushroom : IPrefab
    {
        public const string Name = "Mushroom";

        public const string Tileset = "content/tilesets/smb3-entity.xml";
        public const int TileID = 105;

        public const string BehaviorModule = "Prototype.Behaviors.MobBehavior";

        public const int HitboxWidth = 16;
        public const int HitboxHeight = 16;

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

            var sprite = db.New<Sprite>(ent);
            sprite.TilesetName = Tileset;
            sprite.TileID = TileID;

            var body = db.New<RigidBody>(ent);
            body.Prefab = Name;
            body.Hitbox = new NgxRectangle(0, 0, HitboxWidth, HitboxHeight);

            var pickup = db.New<Pickup>(ent);
            pickup.ItemID = Item.Mushroom;

            var locale = db.New<MapLocale>(ent);
            locale.MID = args.MID;

            return ent;
        }
    }
}
