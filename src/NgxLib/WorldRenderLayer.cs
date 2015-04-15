using Microsoft.Xna.Framework.Graphics;

namespace NgxLib
{
    public class WorldRenderLayer : NgxRenderLayer
    {
        public override void Begin(SpriteBatch batch)
        {
            batch.Begin(
                SpriteSortMode.Immediate,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                DepthStencilState.Default,
                RasterizerState.CullCounterClockwise,
                null,
                Context.Camera.Transform);
        }
    }
}