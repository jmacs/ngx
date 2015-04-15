using System;
using NgxLib;
using Prototype.Components;

namespace Prototype.Entities
{
    public class WorldMario : IPrefab
    {
        public const float MaxWalkSpeed = 5.0f;
        public const float Speed = 0.1f;

        public const string Name = "WorldMario";

        public const string Tileset = "content/tilesets/smb3-entity.xml";

        public const int NormalAnimation = 1150;
        public const int SuperAnimation = 1150;
        public const int RacoonAnimation = 1150;

        public const string MovementStatesType = "Prototype.Modules.MarioMovement";

        public const int HitboxWidth = 16;
        public const int HitboxHeight = 16;

        public int CreateEntity(NgxDatabase db, PrefabArgs args)
        {
            return Create(db, args);
        }

        public static int Create(NgxDatabase db, PrefabArgs args)
        {
            var id = db.CreateEntity();

            var meta = db.New<MetaData>(id);
            meta.Prefab = Name;

            db.New<Player>(id);

            db.New<WorldNavigator>(id);

            db.New<Controller>(id);

            var anim = db.New<Animator>(id);
            anim.Animation = NormalAnimation;

            var position = db.New<Spatial>(id);
            position.X = args.X;
            position.Y = args.Y;

            var body = db.New<RigidBody>(id);
            body.Prefab = Name;
            body.Hitbox = new NgxRectangle(0, 0, HitboxWidth, HitboxHeight);

            var sprite = db.New<Sprite>(id);
            sprite.TilesetName = Tileset;

            return id;
        }
    }
}
