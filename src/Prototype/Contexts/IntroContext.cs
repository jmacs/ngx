using NgxLib;

namespace Prototype.Contexts
{
    public class IntroContext : NgxContext
    {
        protected override void Load()
        {

            Tilesets.Load("content/tilesets/smb3-entity.xml");
            Tilesets.Load("content/tilesets/smb3-world.xml");
            
        }
    }
}
