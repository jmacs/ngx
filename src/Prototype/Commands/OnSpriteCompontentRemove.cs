using NgxLib;
using Prototype.Components;
using Prototype.Systems;

namespace Prototype.Commands
{
    public class OnSpriteCompontentRemove : INgxCommand
    {
        public long GetMessageKey()
        {
            return Ngx.Components.Get<Sprite>().RemoveMessageKey;
        }

        public void Execute(NgxContext context, NgxMessage args)
        {
            var system = context.Engine.Layers[0].Get<SpriteRenderSystem>();
            system.RemoveSprite(args.Entity1);
        }
    }
}
