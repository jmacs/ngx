using NgxLib;
using Prototype.Components;

namespace Prototype.Systems
{
    public class SpatialSystem : TableSystem<Spatial>
    {
        protected NgxTable<Sprite> SpriteTable { get; set; }
        protected NgxTable<RigidBody> RigidBodyTable { get; set; }

        public override void Initialize()
        {
            SpriteTable = Database.Table<Sprite>();
            RigidBodyTable = Database.Table<RigidBody>();
        }


        // todo: redesign this so other entities can observe position
        protected override void Update(Spatial spatial)
        {
            if (!spatial.IsDirty) return;

            var sprite = SpriteTable[spatial.Entity];
            if (sprite != null)
            {
                sprite.PositionChanged(spatial.Position);
            }

            var body = RigidBodyTable[spatial.Entity];
            if (body != null)
            {
                body.PositionChanged(spatial.Position);
            }

            spatial.IsDirty = false;
        }

    }
}
