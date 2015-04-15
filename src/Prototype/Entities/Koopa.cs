using System;
using NgxLib;
using Prototype.Components;

namespace Prototype.Entities
{
    public class Koopa : IPrefab
    {
        public const string Name = "Koopa";

        public const float MobilityMaxWalkSpeed = 0.5f;
        public const float MobilityWalkSpeed = 4.0f;

        public const string Tileset = "content/tilesets/smb3-entity.xml";
        
        public const int WalkAnimation = 1100;
        public const int IdleAnimation = 1110;
        public const int TorpedoAnimation = 1130;
        public const int ShellAnimation = 1120;

        public const string BehaviorModule = "Prototype.Behaviors.MobBehavior";

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

            var position = db.New<Spatial>(id);
            position.X = args.X;
            position.Y = args.Y;

            var body = db.New<RigidBody>(id);
            body.Prefab = Name;
            body.Hitbox = new NgxRectangle(0, 0, HitboxWidth, HitboxHeight);            

            db.New<Controller>(id);

            db.New<Animator>(id);

            var shell = db.New<Shell>(id);
            shell.TorpedoAnimation = TorpedoAnimation;
            shell.ShellAnimation = ShellAnimation;

            var inter = db.New<Interactor>(id);
            inter.Enabled = false;
            inter.Carryable = true;
            inter.Kickable = true;
            
            var sprite = db.New<Sprite>(id);
            sprite.TilesetName = Tileset;
           
            var brain = db.New<Brain>(id);
            brain.BehaviorModule = BehaviorModule;

            var mobility = db.New<Mobility>(id);
            mobility.WalkSpeed = MobilityWalkSpeed;
            mobility.MaxWalkSpeed = MobilityMaxWalkSpeed;
            mobility.WalkAnimation = WalkAnimation;
            mobility.IdleAnimation = IdleAnimation;

            var locale = db.New<MapLocale>(id);
            locale.MID = args.MID;

            return id;
        }
    }
}
