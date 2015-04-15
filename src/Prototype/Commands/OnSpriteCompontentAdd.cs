using NgxLib;
using Prototype.Components;
using Prototype.Systems;

namespace Prototype.Commands
{
    public class OnSpriteCompontentAdd : INgxCommand
    {
        public long GetMessageKey()
        {
            return Ngx.Components.Get<Sprite>().AddMessageKey;
        }

        public void Execute(NgxContext context, NgxMessage args)
        {
            var system = context.Engine.Layers[1].Get<SpriteRenderSystem>();
            system.AddSprite(args.Entity1);
        }
    }
}
