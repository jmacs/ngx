using System;
using NgxLib;
using Prototype.Components;

namespace Prototype.Entities
{
    public class CoinBlock : IPrefab
    {
        public const string Name = "CoinBlock";

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
            decorator.DecoratorType = Component.CoinBag;

            var coinPurse = db.New<CoinBag>(ent);
            coinPurse.NumberOfCoins = args.GetInt("Coins");

            var locale = db.New<MapLocale>(ent);
            locale.MID = args.MID;

            return ent;
        }
    }
}
