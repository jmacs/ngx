using System;
using Microsoft.Xna.Framework;
using NgxLib;
using Prototype.Components;

namespace Prototype.Systems
{
    public class PhysicsSystem : TableSystem<RigidBody>
    {
        private const float MinSpeedX = 0.1f;

        public float Gravity { get; set; }
        public float Friction { get; set; }

        protected NgxTable<Spatial> SpatialTable { get; set; }

        public override void Initialize()
        {
            Gravity = 5.0f;
            Friction = 1.90f;
            SpatialTable = Database.Table<Spatial>();
        }

        protected override void Update(RigidBody body)
        {
            // ====================================
            // Horizontal  Movement
            // ====================================

            // Acceleration
            var vx = body.Velocity.X + body.Acceleration.X * Time.Delta;

            // Friction
            if (vx < 0) vx = vx + Friction * Time.Delta;
            else if (vx > 0) vx = vx - Friction * Time.Delta;

            // TODO - apply entity personal friction

            // Min Hortizontal Speed 
            if (body.Acceleration.X == 0 && Math.Abs(vx) < MinSpeedX) vx = 0;

            // Max Hortizontal Speed
            if (Math.Abs(vx) > body.MaxSpeedX)
            {
                vx = (vx > 0 ? body.MaxSpeedX : -body.MaxSpeedX);
            }

            
            // ====================================
            // Vertical Movement
            // ====================================

            // Acceleration
            var vy = body.Velocity.Y + body.Acceleration.Y * Time.Delta;

            if (!body.IsGrounded)
            {
                // Gravity
                vy += Gravity * Time.Delta;

                // Super gravity, makes for a faster fall speed 
                if (vy > 0.1)
                {
                    vy += Gravity * Time.Delta;
                }
            }
          
            // TODO - apply entity personal gravity

            // TODO - cap vy at 16 pixels so not to move through tiles

            // ====================================
            // Update Position
            // ====================================

            var spatial = SpatialTable[body.Entity];
            spatial.X = spatial.X + vx;
            spatial.Y = spatial.Y + vy;

            body.Velocity = new Vector2(vx, vy);
        }
    }
}
