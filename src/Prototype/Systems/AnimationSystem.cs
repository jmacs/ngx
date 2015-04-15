using NgxLib;
using Prototype.Components;

namespace Prototype.Systems
{
    public class AnimationSystem : TableSystem<Animator>
    {
        protected NgxTable<Sprite> SpriteTable { get; private set; }

        public override void Initialize()
        {
            SpriteTable = Database.Table<Sprite>();
        }

        protected override void Update(Animator animator)
        {
            if(animator.Changed)
            {
                UpdateSprite(animator);
            }

            if(animator.NextFrame())
            {
                UpdateSprite(animator);
            }
        }

        private void UpdateSprite(Animator animator)
        {
            var sprite = SpriteTable[animator.Entity];
            var tileset = Context.Tilesets[sprite.TilesetName];
            var animation = tileset.GetAnimation(animator.Animation);

            if (animation == null) return;
            
            var frame = animation.Frames[animator.FrameIndex];
            animator.FrameCount = animation.Frames.Count;
            animator.FrameTime = frame.FrameTime;
            animator.Texels = tileset[frame.TileId].Texels;
            sprite.Texels = animator.Texels;
        }
    }
}