using System.Reflection;
using NgxLib;
using NgxLib.Behaviors;
using Prototype.Components;

namespace Prototype.Systems
{
    public class BehaviorSystem : TableSystem<Brain>
    {
        protected ModuleCollection<BehaviorModule> Modules { get; set; }
        protected NgxTable<Controller> ControllerTable { get; set; }

        public override void Initialize()
        {
            Modules = new ModuleCollection<BehaviorModule>(Context);
            Modules.Register(Assembly.GetExecutingAssembly());
            ControllerTable = Database.Table<Controller>();           
        }

        public override void Destroy()
        {
            Modules.Dispose();
        }

        protected override void Update(Brain brain)
        {
            ControllerTable[brain.Entity].Reset();

            BehaviorModule module;
            if (Modules.TryGet(brain.BehaviorModule, out module))
            {
                module.Update(brain.Entity);
            }
            else
            {
                Logger.Log("Cannot find behavior module '{0}'", brain.BehaviorModule);                
            }
        }
    }
}
