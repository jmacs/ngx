using System.Collections.Generic;
using NgxLib;
using NgxLib.Collisions;
using Prototype.Components;

namespace Prototype.Systems
{
    public class EntityCollisionSystem : TableSystem<RigidBody>
    {
        protected HashSet<long> CollidingPairs = new HashSet<long>();

        public override void Initialize()
        {
            EntityCollisions.Initialize();
        }

        protected override void Update(RigidBody body)
        {
            if (body.NoClip) return;

            // TODO: use spatial partitioning (grid)
            var enumerator = Table.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var other = enumerator.Current.Value;

                if (!other.Enabled) continue;
                if (other.Entity == body.Entity) continue;

                BroadPhase(body, other);
            }
            enumerator.Dispose();
        }


        protected void BroadPhase(RigidBody self, RigidBody other)
        {
            self.EntitySensory = Side.None;

            // convert two int values into one long value
            // this will act as a key for this entity pair
            var pair = ((long)self.Entity << 32) + other.Entity;

            var rectA = self.Hitbox;
            var rectB = other.Hitbox;

            if (!rectA.Intersects(ref rectB))
            {
                // TODO: fire a leave collision event
                if(CollidingPairs.Contains(pair))
                {
                    CollidingPairs.Remove(pair);
                }
                return;
            }

            // we only fire collision events on the 
            // first collision (once for each entity)
            if (CollidingPairs.Contains(pair)) return;

            CollidingPairs.Add(pair);

            // Now we do the more detailed collision detection
            NarrowPhase(self, other, pair);            
        }

        protected void NarrowPhase(RigidBody self, RigidBody other, long pair)
        {
            // TODO: clean this method up

            var rectA = self.Hitbox;
            var rectB = other.Hitbox;

            var doNarrowPhase = true;// HACK: only do narrow phase once per pair

            // the mask represents all components attached to this entity
            var left = Database.GetMask(self.Entity);
            var right = Database.GetMask(other.Entity);
            var collision = Collision.None;

            // the collision pairs contain all components that can collide
            for (int i = 0; i < CollisionMap.CollisionPairs.Count; i++)
            {
                // check if the entity have components that can collide
                var p = CollisionMap.CollisionPairs[i];
                if (left.Contains(p.Left) && right.Contains(p.Right))
                {
                    // if we are here that means 
                    // the entities have collidable components

                    if (doNarrowPhase) // HACK: only do narrow phase once per pair
                    {
                        // narrow phase collision
                        self.EntitySensory |= GetHotSpot(ref rectA, ref rectB);
                        var depth = rectA.GetIntersectionDepth(ref rectB);
                        doNarrowPhase = false;
                        collision = new Collision(depth);
                    }

                    // fire the collision message                    
                    var msg = Ngx.Messenger.Create(p.Topic);
                    msg.Entity1 = self.Entity;
                    msg.Entity2 = other.Entity;
                    msg.Collision = collision;
                    Ngx.Messenger.Send(msg);
                }
            }
        }

        // tells us where on the entity is being hit
        // TODO: this should be an array on rigid body
        protected Side GetHotSpot(ref NgxRectangle a, ref NgxRectangle b)
        {
            var hotspot = Side.None;

            if (b.Contains(a.LeftCenter))
            {
                hotspot |= Side.LeftCenter;
            }

            if (b.Contains(a.RightCenter))
            {
                hotspot |= Side.RightCenter;
            }

            if (b.Contains(a.BottomCenter))
            {
                hotspot |= Side.BottomCenter;
            }

            if (b.Contains(a.BottomLeft))
            {
                hotspot |= Side.BottomLeft;
            }

            if (b.Contains(a.BottomRight))
            {
                hotspot |= Side.BottomRight;
            }

            if (b.Contains(a.TopCenter))
            {
                hotspot |= Side.TopCenter;
            }

            if (b.Contains(a.TopLeft))
            {
                hotspot |= Side.TopLeft;
            }

            if (b.Contains(a.TopRight))
            {
                hotspot |= Side.TopRight;
            }

            return hotspot;
        }
    }
}