using System;
using Microsoft.Xna.Framework;
using NgxLib;
using Prototype.Components;

namespace Prototype.Entities
{
    public class WarpPipe : IPrefab
    {
        public const string Name = "WarpPipe";

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
            decorator.DecoratorType = Component.WarpPipeConnection;
            decorator.AdjacentX = 1;

            var pipe = db.New<WarpPipeConnection>(ent);
            pipe.Position = new Vector2(args.X, args.Y);
            pipe.PipeID = args.UID;
            pipe.Connection = args.GetInt("Connection");
            pipe.CanEnter = args.GetBool("Access");
            pipe.MID = args.GetInt("Map");

            var locale = db.New<MapLocale>(ent);
            locale.MID = args.MID;


            return ent;
        }
    }
}
