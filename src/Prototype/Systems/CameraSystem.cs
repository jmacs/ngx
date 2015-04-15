using Microsoft.Xna.Framework;
using NgxLib;
using NgxLib.Cameras;
using Prototype.Components;
using Prototype.Entities;

namespace Prototype.Systems
{
    // TODO: this is a buggy fucking mess, refactor this class
    public class CameraSystem : NgxGameSystem
    {
        protected NgxTable<RigidBody> RigidBodyTable { get; set; }
        protected NgxTable<Player> PlayerTable { get; set; }
        protected NgxTable<Spatial> SpatialTable { get; set; }

        private Camera2D _camera;
        private int _cman;
        private NgxRectangle _deadzone = new NgxRectangle(0,0,32,16);

        public override void Initialize()
        {
            RigidBodyTable = Database.Table<RigidBody>();
            PlayerTable = Database.Table<Player>();
            SpatialTable = Database.Table<Spatial>();

            _camera = Context.Camera;
            _camera.Initialize();
            _cman = Camera.New(Database);
        }

        public override void Update()
        {
            _camera.Update();

            var cpos = SpatialTable[_cman];
            var epos = SpatialTable[_camera.Follow];

            if (epos == null)
            {
                var player = PlayerTable.First();
                if (player == null) return;
                Context.Camera.Follow = player.Entity;
                return;
            }

            Vector2 dest = Vector2.Zero;
        

            var map = Context.MapManager.Map.Area;

            if (map.Width < _camera.Viewport.Width && map.Height < _camera.Viewport.Height)
            {
                cpos.X = map.Width * 0.5f;
                cpos.Y = map.Height * 0.5f;
                _camera.SetPosition(cpos.X, cpos.Y);
                return;
            }

            var speedX = 0.05f;

            _camera.SetPosition(cpos.X, cpos.Y);
            
            var distanceToLeft = epos.X - map.Left;
            var distanceToRight = map.Right - epos.X;

            if (distanceToLeft < 320)
            {
                // Left Lock
                dest.X = 320;
            }
            else if (distanceToRight < 320)
            {
                // Right Lock
                dest.X = map.Right - 320;
            }
            else
            {
                // Entity Lock

                if (epos.X > _deadzone.Right)
                {
                    dest.X = epos.X + 64;
                    _deadzone.X = epos.X - _deadzone.Width;
                }
                else if (epos.X < _deadzone.Left)
                {
                    dest.X = epos.X - 64;
                    _deadzone.X = epos.X;
                }
            }

            if (dest.X != 0)
            {
                cpos.X += (dest.X - cpos.X) * speedX;
            }
            
            cpos.Y = 100;
        }
    }
}
