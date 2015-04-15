using System;
using Microsoft.Xna.Framework;

namespace NgxLib
{
    /// <summary>
    /// Defines a rectangle with floating point values.
    /// </summary>
    public struct NgxRectangle : IEquatable<NgxRectangle>
    {
        public static readonly NgxRectangle Empty = new NgxRectangle(); 

        public const int HotspotCount = 8;
        
        public float X;
        public float Y;
        public float Width;
        public float Height;        

        public Vector2 Center
        {
            get
            {
                return new Vector2 (X + (Width * 0.5f), Y + (Height * 0.5f));
            }
        }

        public Vector2 Location
        {
            get
            {
                return new Vector2 (X, Y);
            }
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return ((((Width == 0) && (Height == 0)) && (X == 0)) && (Y == 0));
            }
        }

        public float Left
        {
            get { return X; }
        }

        public float Right
        {
            get { return (X + Width); }
        }

        public float Top
        {
            get { return Y; }
        }

        public float Bottom
        {
            get { return (Y + Height); }
        }

        public NgxRectangle(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }


        public void Intersects(ref NgxRectangle value, out bool result)
        {
            result = (((value.X < (X + Width)) && (X < (value.X + value.Width))) && (value.Y < (Y + Height))) && (Y < (value.Y + value.Height));
        }

        public bool Contains(Vector2 value)
        {
            return ((((X <= value.X) && (value.X < (X + Width))) && (Y <= value.Y)) && (value.Y < (Y + Height)));
        }

        public bool Contains(NgxRectangle value)
        {
            return ((((X <= value.X) && ((value.X + value.Width) <= (X + Width))) && (Y <= value.Y)) && ((value.Y + value.Height) <= (Y + Height)));
        }

        public void Contains(ref NgxRectangle value, out bool result)
        {
            result = (((X <= value.X) && ((value.X + value.Width) <= (X + Width))) && (Y <= value.Y)) && ((value.Y + value.Height) <= (Y + Height));
        }

        public bool Contains(float x, float y)
        {
            return ((((X <= x) && (x < (X + Width))) && (Y <= y)) && (y < (Y + Height)));
        }

        public void Contains(ref Vector2 value, out bool result)
        {
            result = (((X <= value.X) && (value.X < (X + Width))) && (Y <= value.Y)) && (value.Y < (Y + Height));
        }

        public static bool operator ==(NgxRectangle a, NgxRectangle b)
        {
            return ((a.X == b.X) && (a.Y == b.Y) && (a.Width == b.Width) && (a.Height == b.Height));
        }

        public static bool operator !=(NgxRectangle a, NgxRectangle b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Moves Rect for both Vector2 values.
        /// </summary>
        /// <param name="offset">
        /// A <see cref="Vector2 "/>
        /// </param>
        public void Offset(Vector2 offset)
        {
            X += offset.X;
            Y += offset.Y;
        }

        /// <summary>
        /// Moves Rect for both values.
        /// </summary>
        /// <param name="offsetX">
        /// A <see cref="System.Int32"/>
        /// </param>
        /// <param name="offsetY">
        /// A <see cref="System.Int32"/>
        /// </param>
        public void Offset(float offsetX, float offsetY)
        {
            X += offsetX;
            Y += offsetY;
        }

        /// <summary>
        /// Grows the Rect. Down right Vector2 is in the same position.
        /// </summary>
        /// <param name="horizontalValue">
        /// A <see cref="System.Int32"/>
        /// </param>
        /// <param name="verticalValue">
        /// A <see cref="System.Int32"/>
        /// </param>
        public void Inflate(float horizontalValue, float verticalValue)
        {
            X -= horizontalValue;
            Y -= verticalValue;
            Width += horizontalValue * 2;
            Height += verticalValue * 2;
        }

        public bool Intersects(NgxRectangle rect)
        {
            return Intersects(ref rect);
        }

        /// <summary>
        /// It checks if two Rect intersects.
        /// </summary>
        /// <param name="rect">
        /// A <see cref="bool"/>
        /// </param>
        /// <returns>
        /// A <see cref="System"/>
        /// </returns>
        public bool Intersects(ref NgxRectangle rect)
        {
            if (X <= rect.X)
            {
                if ((X + Width) > rect.X)
                {
                    if (Y < rect.Y)
                    {
                        if ((Y + Height) > rect.Y)
                            return true;
                    }
                    else
                    {
                        if ((rect.Y + rect.Height) > Y)
                            return true;
                    }
                }
            }
            else
            {
                if ((rect.X + rect.Width) > X)
                {
                    if (Y < rect.Y)
                    {
                        if ((Y + Height) > rect.Y)
                            return true;
                    }
                    else
                    {
                        if ((rect.Y + rect.Height) > Y)
                            return true;
                    }
                }
            }
            return false;
        }


        public bool Equals(NgxRectangle other)
        {
            return this == other;
        }

        /// <summary>
        /// Returns true if recangles are same
        /// </summary>
        /// <param name="obj">
        /// A <see cref="System.Object"/>
        /// </param>
        /// <returns>
        /// A <see cref="System.Boolean"/>
        /// </returns>
        public override bool Equals(object obj)
        {
            return (obj is NgxRectangle) ? this == ((NgxRectangle)obj) : false;
        }

        public override string ToString()
        {
            return string.Format("{{X:{0} Y:{1} Width:{2} Height:{3}}}", X, Y, Width, Height);
        }

        public override int GetHashCode()
        {
            return ToRectangle().GetHashCode();
        }

        public Rectangle ToRectangle()
        {
            return new Rectangle((int)X, (int)Y, (int)Width, (int)Height);
        }

        public Vector2 GetIntersectionDepth(NgxRectangle rectangle)
        {
            return NgxRectangle.GetIntersectionDepth(ref this, ref rectangle);
        }

        public Vector2 GetIntersectionDepth(ref NgxRectangle rectangle)
        {
            return NgxRectangle.GetIntersectionDepth(ref this, ref rectangle);
        }

        /// <summary>
        /// Calculates the signed depth of intersection between two rectangles.
        /// </summary>
        /// <returns>
        /// The amount of overlap between two intersecting rectangles. These
        /// depth values can be negative depending on which wides the rectangles
        /// intersect. This allows callers to determine the correct direction
        /// to push objects in order to resolve collisions.
        /// If the rectangles are not intersecting, Vector2.Zero is returned.
        /// </returns>
        public static Vector2 GetIntersectionDepth(ref NgxRectangle rectA, ref NgxRectangle rectB)
        {
            // Calculate half sizes.
            float halfWidthA = rectA.Width * 0.5f;
            float halfHeightA = rectA.Height * 0.5f;
            float halfWidthB = rectB.Width * 0.5f;
            float halfHeightB = rectB.Height * 0.5f;

            // Calculate centers.
            var centerA = new Vector2(rectA.Left + halfWidthA, rectA.Top + halfHeightA);
            var centerB = new Vector2(rectB.Left + halfWidthB, rectB.Top + halfHeightB);

            // Calculate current and minimum-non-intersecting distances between centers.
            float distanceX = centerA.X - centerB.X;
            float distanceY = centerA.Y - centerB.Y;
            float minDistanceX = halfWidthA + halfWidthB;
            float minDistanceY = halfHeightA + halfHeightB;

            // If we are not intersecting at all, return (0, 0).
            if (Math.Abs(distanceX) >= minDistanceX || Math.Abs(distanceY) >= minDistanceY)
                return Vector2.Zero;

            // Calculate and return intersection depths.
            float depthX = distanceX > 0 ? minDistanceX - distanceX : -minDistanceX - distanceX;
            float depthY = distanceY > 0 ? minDistanceY - distanceY : -minDistanceY - distanceY;
            return new Vector2(depthX, depthY);
        }

        public Vector2 TopCenter
        {
            get { return new Vector2(X + (Width * 0.5f), Top); }
        }
          
        public Vector2 BottomCenter
        {
            get { return new Vector2(X + (Width * 0.5f), Bottom); }
        }

        public Vector2 BottomRight
        {
            get { return new Vector2(Right, Bottom); }
        }

        public Vector2 BottomLeft
        {
            get { return new Vector2(Left, Bottom); }
        }

        public Vector2 TopLeft
        {
            get { return new Vector2(Left, Top); }
        }

        public Vector2 TopRight
        {
            get { return new Vector2(Right, Top); }
        }

        public Vector2 RightCenter
        {
            get { return new Vector2(Right, Y + (Height * 0.5f)); }
        }

        public Vector2 LeftCenter
        {
            get { return new Vector2(Left, Y + (Height * 0.5f)); }
        }       
    }
}