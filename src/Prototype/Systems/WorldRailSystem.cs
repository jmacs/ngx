using NgxLib;
using NgxLib.Maps;
using Prototype.Components;
using Prototype.Processes;

namespace Prototype.Systems
{
    public class WorldRailSystem : TableSystem<WorldNavigator>
    {
        // Number of tiles to look for a rail stop from current position
        private const int LookDistance = 9;
        // Pixels from the bottom of the character
        private const int PixelHeight = 8;

        protected NgxTable<Spatial> SpatialTable { get; set; }
        protected NgxTable<RigidBody> RigidBodyTable { get; set; }
        protected NgxTable<Controller> ControllerTable { get; set; }

        public override void Initialize()
        {
            SpatialTable = Database.Table<Spatial>();
            RigidBodyTable = Database.Table<RigidBody>();
            ControllerTable = Database.Table<Controller>();
        }

        protected override void Update(WorldNavigator nav)
        {
            var position = SpatialTable[nav.Entity];

            if (nav.IsNew)
            {
                nav.IsAtDestination = true;
                nav.Position = position.Position;
                nav.IsNew = false;
            }

            if (nav.Position == nav.Destination)
            {
                nav.IsAtDestination = true;
            }

            if (nav.IsAtDestination)
            {
                CheckEnterStage(nav);

                if (DetermineDestination(nav))
                {
                    Ngx.Messenger.Send(Msg.Play_Sound, sound: Snd.MapTravel);

                }
                return;
            }

            position.Position = nav.Position.Move(nav.Destination, nav.MoveSpeed);
            nav.Position = position.Position;
        }

        private void CheckEnterStage(WorldNavigator nav)
        {
            var controller = ControllerTable[nav.Entity];

            var map = Context.MapManager.Map;
            var cell = map.PositionToCell(nav.Position.X + PixelHeight, nav.Position.Y - PixelHeight);

            if (cell.DecoratorType == Component.StagePortal)
            {
                if (controller.Is(Ctrl.Jump))
                {
                    if(flag) return;
                    var portal = Database.Component<Portal>(cell.Decorator);
                    var proc = new EnterStage(portal.MID);
                    Context.ProcessManager.Start(proc);
                    flag = true;

                }
            }
        }

        // TODO: have some sort of cool down for buttons??
        protected bool flag;

        private bool DetermineDestination(WorldNavigator nav)
        {
            var controller = ControllerTable[nav.Entity];

            var map = Context.MapManager.Map;
            var source = map.PositionToCell(nav.Position.X, nav.Position.Y - PixelHeight);

            
            for (var i = 1; i < LookDistance; i++)
            {
                Cell cell = null;

                if (controller.Is(Ctrl.Right))
                {
                    cell = map.GetCell(source.X + i, source.Y);
                }
                else if(controller.Is(Ctrl.Left))
                {
                    cell = map.GetCell(source.X - i, source.Y);
                }
                else if (controller.Is(Ctrl.Down))
                {
                    cell = map.GetCell(source.X, source.Y + i);
                }
                else if (controller.Is(Ctrl.Up))
                {
                    cell = map.GetCell(source.X, source.Y - i);
                }

                if (cell == null) return false;

                if (cell.Tile.Property == TileProperty.Rail)
                {
                    continue;
                }

                if (cell.Tile.Property == TileProperty.RailStop)
                {
                    nav.Destination = cell.Area.BottomLeft;
                    nav.IsAtDestination = false;
                    return true;
                }

                return false;
            }

            return false;
        }
    }
}
