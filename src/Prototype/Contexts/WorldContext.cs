using NgxLib;
using Prototype.Systems;

namespace Prototype.Contexts
{
    public class WorldContext : NgxContext
    {
        protected override void Load()
        {
            Tilesets.Load("content/tilesets/smb3-entity.xml");
            Tilesets.Load("content/tilesets/smb3-world.xml");

            // Simulation Systems
            Engine.Systems.Add(new WorldRailSystem());
            Engine.Systems.Add(new CameraSystem());
            Engine.Systems.Add(new PlayerInputSystem());
            Engine.Systems.Add(new AnimationSystem());
            Engine.Systems.Add(new EntityCollisionSystem());
            Engine.Systems.Add(new SpatialSystem());

            // Render Systems

            var world0 = new WorldRenderLayer();
            world0.Systems.Add(new MapRenderSystem());
            Engine.Layers.Add(world0);

            var world1 = new WorldRenderLayer();
            world1.Systems.Add(new SpriteRenderSystem());
            Engine.Layers.Add(world1);

            var screen0 = new ScreenRenderLayer();
            screen0.Systems.Add(new DebugRenderSystem());
            screen0.Systems.Add(new HudRenderSystem());
            screen0.Systems.Add(new GuiRenderSystem());
            Engine.Layers.Add(screen0);

        }

        protected override void PostLoad()
        {
            Camera.Scale = 3;
        }
    }
}
