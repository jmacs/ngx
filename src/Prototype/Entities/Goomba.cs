using System;
using NgxLib;
using Prototype.Components;

namespace Prototype.Entities
{
    public class Goomba : IPrefab
    {
        public const string Name = "Goomba";

        public const float MobilityMaxWalkSpeed = 0.5f;
        public const float MobilityWalkSpeed = 4.0f;

        public const string Tileset = "content/tilesets/smb3-entity.xml";
        public const int WalkAnimation = 1000;
        public const int IdleAnimation = 1010;
        public const int StompAnimation = 1020;

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

            var position = db.New<Spatial>(ent);
            position.X = args.X;
            position.Y = args.Y;

            var body = db.New<RigidBody>(ent);
            body.Prefab = Name;
            body.Hitbox = new NgxRectangle(0, 0, HitboxWidth, HitboxHeight);

            var stompable = db.New<Stompable>(ent);
            stompable.StompAnimation = StompAnimation;

            db.New<Controller>(ent);

            db.New<Animator>(ent);

            var sprite = db.New<Sprite>(ent);
            sprite.TilesetName = Tileset;

            var brain = db.New<Brain>(ent);
            brain.BehaviorModule = BehaviorModule;

            var mobility = db.New<Mobility>(ent);
            mobility.WalkSpeed = MobilityWalkSpeed;
            mobility.MaxWalkSpeed = MobilityMaxWalkSpeed;
            mobility.WalkAnimation = WalkAnimation;
            mobility.IdleAnimation = IdleAnimation;

            var locale = db.New<MapLocale>(ent);
            locale.MID = args.MID;

            return ent;
        }
    }
}
