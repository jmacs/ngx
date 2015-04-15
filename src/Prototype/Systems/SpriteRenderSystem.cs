using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NgxLib;
using Prototype.Components;

namespace Prototype.Systems
{
    public class SpriteRenderSystem : NgxRenderSystem
    {
        protected SpriteLayer SpriteLayer { get; set; }

        protected Index<Sprite> Sprites { get; set; }

        public override void Initialize()
        {
            Sprites = new Index<Sprite>();

            if(RenderLayer.Index == 0)
            {
                SpriteLayer = SpriteLayer.Back;
            }

            else if(RenderLayer.Index == 1)
            {
                SpriteLayer = SpriteLayer.Front;
            }
        }

        public void SwapSpriteLayer(int entity)
        {
            var component = Database.Component<Sprite>(entity);

            if (component.Layer == SpriteLayer)
            {
                Sprites.Put(component.Entity, component);
            }
            else
            {
                Sprites.Take(component.Entity);                
            }
        }

        public void AddSprite(int entity)
        {
            var component = Database.Component<Sprite>(entity);

            if (component.Layer != SpriteLayer) return;
            
            Sprites.Put(component.Entity, component);

            if (component.TextureCache != null) return;

            var ts = Context.Tilesets.Get(component.TilesetName);
            component.TextureCache = ts.Texture;
            if (component.TileID > 0)
            {
                component.Texels = ts[component.TileID].Texels;
            }
        }

        public void RemoveSprite(int entity)
        {
            Sprites.Take(entity);
        }        

        public override void Draw(SpriteBatch batch)
        {
            foreach (var item in Sprites)
            {
                var sprite = item.Value;
                if (sprite.TextureCache == null) continue;

                batch.Draw(
                    sprite.TextureCache,
                    sprite.Position,
                    sprite.Texels,
                    sprite.Color * sprite.Alpha,
                    sprite.Rotation,
                    new Vector2(0, sprite.Texels.Height),
                    sprite.Scale,
                    sprite.Effects,
                    sprite.Depth);

                if (sprite.Layer != SpriteLayer)
                {

                    Ngx.Messenger.Send(Msg.Swap_Sprite_Layer, entity: item.Key);

                }
            }
        }
    }
}