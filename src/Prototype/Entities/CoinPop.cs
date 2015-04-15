using System;
using NgxLib;
using Prototype.Components;

namespace Prototype.Entities
{
    public class CoinPop : IPrefab
    {
        public const string Name = "CoinPop";

        public const string Tileset = "content/tilesets/smb3-entity.xml";
        public const int Animation = 10;

        public int CreateEntity(NgxDatabase db, PrefabArgs args)
        {
            return Create(db, args);
        }

        public static int Create(NgxDatabase db, PrefabArgs args)
        {
            var id = db.CreateEntity();

            var meta = db.New<MetaData>(id);
            meta.Prefab = Name;

            var spatial = db.New<Spatial>(id);
            spatial.X = args.X;
            spatial.Y = args.Y;

            var animator = db.New<Animator>(id);
            animator.Animation = Animation;

            var sprite = db.New<Sprite>(id);
            sprite.TilesetName = Tileset;

            return id;
        }
    }
}
