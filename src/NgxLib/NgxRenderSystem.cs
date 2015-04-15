using Microsoft.Xna.Framework.Graphics;

namespace NgxLib
{
    public abstract class NgxRenderSystem : NgxGameSystem
    {
        protected NgxRenderLayer RenderLayer { get; private set; }

        public virtual void Draw(SpriteBatch batch)
        {
        }

        public void BindLayer(NgxRenderLayer renderLayer)
        {
            RenderLayer = renderLayer;
        }
    }
}