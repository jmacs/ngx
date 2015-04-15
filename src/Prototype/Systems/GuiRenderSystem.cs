using Microsoft.Xna.Framework.Graphics;
using NgxLib;

namespace Prototype.Systems
{
    public class GuiRenderSystem : NgxRenderSystem
    {
        public override void Initialize()
        {
            Context.Controls.Initialize(Context);
        }

        public override void Update()
        {
            Context.Controls.Update();
        }

        public override void Draw(SpriteBatch batch)
        {            
            Context.Controls.Render(batch);
        }
    }
}
