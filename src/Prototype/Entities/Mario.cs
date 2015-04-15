using System;
using NgxLib;
using Prototype.Components;

namespace Prototype.Entities
{
    public class Mario : IPrefab
    {
        public const string Name = "Mario";

        public const float JumpImpulse = 25.0f;
        public const float JumpResponseTime = 0.3f;
        public const float JumpAirDrag = 3.5f;
        public const float JumpDrift = 20.0f;

        public const float MobilityMaxWalkSpeed = 1.4f;
        public const float MobilityWalkSpeed = 3.5f;

        public const string Tileset = "content/tilesets/smb3-entity.xml";
        
        public const int NormalIdleAnimation = 2000;
        public const int NormalWalkAnimation = 2010;
        public const int NormalJumpAnimation = 2020;
        public const int NormalDuckAnimation = 2000;
        public const int NormalHeadshotAnimation = 2000;

        public const int SuperIdleAnimation = 2100;
        public const int SuperWalkAnimation = 2110;
        public const int SuperJumpAnimation = 2120;
        public const int SuperDuckAnimation = 2130;
        public const int SuperHeadshotAnimation = 2140;

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

            db.New<Controller>(ent);

            db.New<Animator>(ent);

            db.New<Player>(ent);

            db.New<NullPower>(ent);
            //db.New<SuperPower>(ent);

            var sprite = db.New<Sprite>(ent);
            sprite.TilesetName = Tileset;

            var mobility = db.New<Mobility>(ent);
            mobility.WalkSpeed = MobilityWalkSpeed;
            mobility.MaxWalkSpeed = MobilityMaxWalkSpeed;

            var boots = db.New<JumpBoots>(ent);
            boots.AirDrag = JumpAirDrag;
            boots.JumpDrift = JumpDrift;
            boots.JumpImpulse = JumpImpulse;
            boots.JumpResponseTime = JumpResponseTime;

            return ent;
        }
    }
}
