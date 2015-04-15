using System;
using Microsoft.Xna.Framework;
using NgxLib;
using Prototype.Components;

namespace Prototype.Entities
{
    public class Camera : IPrefab
    {
        public const string Name = "Camera";

        public const string Tileset = "content/tilesets/smb3-entity.xml";
        public const int TileId = 1;

        public int CreateEntity(NgxDatabase db, PrefabArgs args)
        {
            return Create(db, args);
        }

        public static int Create(NgxDatabase db, PrefabArgs args)
        {
            var id = db.CreateEntity();

            var metaData = db.New<MetaData>(id);
            metaData.Prefab = Name;

            db.New<Spatial>(id);

            var sprite = db.New<Sprite>(id);
            sprite.TilesetName = Tileset;
            sprite.TileID = 1;

            return id;
        }

        public static int New(NgxDatabase database)
        {
            return Ngx.Prefabs.Create(database, Name, null);
        }
    }
}
