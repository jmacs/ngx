using NgxLib;
using NgxLib.Collisions;
using NgxLib.Maps;
using Prototype.Components;
using Prototype.Processes;

namespace Prototype.Systems
{
    public class MapCollisionSystem : TableSystem<RigidBody>
    {
        protected NgxTable<Spatial> SpatialTable { get; set; }

        public override void Initialize()
        {
            SpatialTable = Database.Table<Spatial>();
        }

        protected override void Update(RigidBody body)
        {
            if (body.NoClip) return;

            body.WallSensory = Side.None;    

            var map = Context.MapManager.Map;
            var hitbox = body.Hitbox;          
            var touchingFloor = false;

            if (hitbox.Top > map.Area.Bottom)
            {
                OutOfBoundsBottom(body, map);
                return;
            }

            if (hitbox.Top < 0)
            {
                OutOfBoundsTop(body, map);
            }

            if (hitbox.Left < 0)
            {
                OutOfBoundsLeft(body, map);
            }

            if (hitbox.Right > map.Area.Width)
            {
                OutOfBoundsRight(body, map);
            }


            // Is standing on a tile?
            var bc = map.PositionToCell(hitbox.BottomCenter);
            if (bc.Tile.IsSolid)
            {
                touchingFloor = true;
                HandleCollision(body, bc, Side.BottomCenter);
            }
            else
            {
                // is right foot touching?
                var br = map.PositionToCell(hitbox.BottomRight);
                if (br.Tile.IsSolid)
                {
                    touchingFloor = true;
                    HandleCollision(body, br, Side.BottomRight);
                }
                else
                {
                    // is left foot touching?
                    var bl = map.PositionToCell(hitbox.BottomLeft);
                    if (bl.Tile.IsSolid)
                    {
                        touchingFloor = true;
                        HandleCollision(body, bl, Side.BottomLeft);
                    }
                }
            }

            body.IsGrounded = touchingFloor;

            // check top only if not already touching the floor
            if (!touchingFloor)
            {
                // Is head touching a tile?
                var tc = map.PositionToCell(hitbox.TopCenter);
                if (tc.Tile.IsSolid)
                {
                    HandleCollision(body, tc, Side.TopCenter);
                }
                else
                {
                    // is right face touching?
                    var tr = map.PositionToCell(hitbox.TopRight);
                    if (tr.Tile.IsSolid)
                    {
                        HandleCollision(body, tr, Side.TopRight);
                    }
                    else
                    {
                        // is left face touching?
                        var tl = map.PositionToCell(hitbox.TopLeft);
                        if (tl.Tile.IsSolid)
                        {
                            HandleCollision(body, tl, Side.TopLeft);
                        }
                    }
                }
            }

            // check the sides for collisions when moving in that direction
            if (body.IsMovingRight)
            {
                // is right side touching?
                var rc = map.PositionToCell(hitbox.RightCenter);
                if (rc.Tile.IsSolid)
                {
                    HandleCollision(body, rc, Side.RightCenter);
                }
            }
            else if (body.IsMovingLeft)
            {
                // is left side touching?
                var lc = map.PositionToCell(hitbox.LeftCenter);
                if (lc.Tile.IsSolid)
                {
                    HandleCollision(body, lc, Side.LeftCenter);
                }
            }
        }

        protected void HandleCollision(RigidBody body, Cell cell, Side side)
        {
            body.WallSensory |= side;

            var pos = SpatialTable[body.Entity];
            var depth = body.GetIntersectionDepth(cell.Area);

            var collision = new Collision(depth);

            if (collision.Top || collision.Bottom)
            {
                pos.Y += depth.Y;
                
                body.Velocity.Y = 0;

                if (collision.Bottom)
                {
                    body.Acceleration.Y = 0;
                    DispatchBlockBehavior(body, cell, collision);
                }
            }

            if (collision.Left || collision.Right)
            {
                pos.X += depth.X;
                body.Velocity.X = 0;
            }

            if (cell.DecoratorType == Component.WarpPipeConnection)
            {
                var ctrl = Database.Component<Controller>(body.Entity);
                if (ctrl != null && ctrl.Is(Ctrl.Down))
                {
                    var proc = new EnterWarpPipe(body.Entity, cell.Decorator);
                    Context.ProcessManager.Start(proc);
                }
            }
        }

        private void OutOfBoundsLeft(RigidBody body, Map map)
        {
            body.WallSensory |= Side.LeftCenter;
            var pos = SpatialTable[body.Entity];
            pos.X = map.Area.Left;
            body.Velocity.X = 0;
        }

        private void OutOfBoundsRight(RigidBody body, Map map)
        {
            body.WallSensory |= Side.RightCenter;
            var pos = SpatialTable[body.Entity];
            pos.X = map.Area.Right - body.Width;
            body.Velocity.X = 0;
        }

        private void OutOfBoundsTop(RigidBody body, Map map)
        {
            body.WallSensory |= Side.TopCenter;
            var pos = SpatialTable[body.Entity];
            pos.Y = map.Area.Top + body.Height;
            body.Velocity.Y = 0;
        }

        private void OutOfBoundsBottom(RigidBody body, Map map)
        {
            var pos = SpatialTable[body.Entity];
            pos.Y = body.Height;
            pos.X = body.Width;
            body.Velocity.Y = 0;
            body.Velocity.X = 0;
        }

        // TODO: redesign this
        private void DispatchBlockBehavior(RigidBody body, Cell cell, Collision collision)
        {
            if (cell.DecoratorType == Component.PickUp)
            {
                var proc = new PopPower(cell);
                Context.ProcessManager.Start(proc);
                return;
            }
            
            if (cell.DecoratorType == Component.CoinBag)
            {
                var proc = new PopCoin(cell);
                Context.ProcessManager.Start(proc);
                return;
            }

            if (cell.Tile.Property == TileProperty.BrickBlock)
            {
                if (collision.Bottom && Database.Contains<SuperPower>(body.Entity))
                {
                    var proc = new BreakBrick(cell);
                    Context.ProcessManager.Start(proc);
                }
                else
                {
                    var proc = new PopBrick(cell);
                    Context.ProcessManager.Start(proc);    
                }
                
                return;
            }

            if (collision.Top)
            {
                //Context.Sounds.Play("smb3_bump");
            }
        }
    }
}
