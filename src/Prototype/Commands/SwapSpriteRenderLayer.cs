using NgxLib;
using Prototype.Systems;

namespace Prototype.Commands
{
    public class SwapSpriteRenderLayer : INgxCommand
    {
        public long GetMessageKey()
        {
            return Msg.Swap_Sprite_Layer;
        }

        public void Execute(NgxContext context, NgxMessage args)
        {
            var front = context.Engine.Layers[1].Get<SpriteRenderSystem>();
            var back = context.Engine.Layers[0].Get<SpriteRenderSystem>();
            front.SwapSpriteLayer(args.Entity1);
            back.SwapSpriteLayer(args.Entity1);
        }
    }
}
