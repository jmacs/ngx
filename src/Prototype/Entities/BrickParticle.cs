using NgxLib;
using Prototype.Components;

namespace Prototype.Entities
{
    public class BrickParticle
    {
        public const string Name = "BrickParticle";
        public const string Tileset = "content/tilesets/smb3-entity.xml";
        public const int TileID = 2;

        public static Sprite Create(NgxDatabase db, float x, float y)
        {
            var ent = db.CreateEntity();

            var sprite = db.New<Sprite>(ent);
            sprite.TilesetName = Tileset;
            sprite.TileID = TileID;
            sprite.Position.X = x;
            sprite.Position.Y = y;

            return sprite;
        }
    }
}
