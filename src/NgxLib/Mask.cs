using System;

namespace NgxLib
{
    /// <summary>
    /// A bit mask that supports up to 1024 unique values (0 to 1023)
    /// </summary>
    public struct Mask : IEquatable<Mask>
    {
        public const int MaxValue = 1023;
        public const int Size = 32;

        public static readonly Mask Null = new Mask();

        private int x1;
        private int x2;

        /// <summary>
        /// Initializes a new instance of the <see cref="Mask"/> struct.
        /// <see cref="Mask"/> supports up to 1024 unique values. Value must be
        /// in the range of 0 to 1023.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">value;Must be positive number between 0 and 1023</exception>
        public Mask(int value)
        {
            if (value < 0 || value > MaxValue)
                throw new ArgumentOutOfRangeException("value", "Must be positive number between 0 and 1023");

            var a = value%Size;
            if (a > 0) x1 = (1 << a);
            else x1 = 0;

            var b = value/Size;
            if (b > 0) x2 = (1 << b);
            else x2 = 0;
        }

        /// <summary>
        /// Add the specified mask into this mask.
        /// </summary>
        /// <param name="other">The mask to add.</param>
        /// <returns>The result</returns>        
        public Mask Add(Mask other)
        {
            Mask mask;
            Add(ref this, ref other, out mask);
            return mask;
        }

        /// <summary>
        /// Remove the specified mask from this mask.
        /// </summary>
        /// <param name="other">The mask to remove</param>
        /// <returns>The result</returns>
        public Mask Remove(Mask other)
        {
            Mask mask;
            Remove(ref this, ref other, out mask);
            return mask;
        }

        /// <summary>
        /// Determines whether this mask contains the specified mask.
        /// </summary>
        /// <param name="other">The mask to check.</param>
        /// <returns>
        ///   <c>true</c> if this mask contains the specified mask; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(Mask other)
        {
            return Contains(ref this, ref other);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{{{0},{1}}}", x1, x2);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            var hash = 23;
            hash = hash * 31 + x1;
            hash = hash * 31 + x2;
            return hash;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj is Mask)
            {
                return Equals((Mask)obj);
            }
            return false;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public bool Equals(Mask other)
        {
            return Equals(ref this, ref other);
        }

        /// <summary>
        /// Add the specified mask (b) into mask (a)
        /// </summary>
        /// <param name="a">The mask to add to</param>
        /// <param name="b">The mask to add</param>
        /// <param name="result">The add result</param>
        public static void Add(ref Mask a, ref Mask b, out Mask result)
        {
            result.x1 = a.x1 | b.x1;
            result.x2 = a.x2 | b.x2;
        }

        /// <summary>
        /// Removes the specified mask (b) from mask (a)
        /// </summary>
        /// <param name="a">The mask to remove from.</param>
        /// <param name="b">The mask to remove.</param>
        /// <param name="result">The remove result.</param>
        public static void Remove(ref Mask a, ref Mask b, out Mask result)
        {
            result.x1 = a.x1;
            result.x2 = a.x2;

            result.x1 &= ~b.x1;
            result.x2 &= ~b.x2;
        }

        /// <summary>
        /// Determines whether the first mask contains the second mask.
        /// </summary>
        /// <param name="a">The mask to check</param>
        /// <param name="b">The mask to check if in first mask </param>
        /// <returns>
        ///   <c>true</c> if the first mask contains the specified mask; otherwise, <c>false</c>.
        /// </returns>
        public static bool Contains(ref Mask a, ref Mask b)
        {
            return (a.x1 & b.x1) == b.x1 && (a.x2 & b.x2) == b.x2;
        }

        /// <summary>
        /// Checks the equality of the two specified masks
        /// </summary>
        /// <param name="a">The mask</param>
        /// <param name="b">The mask</param>
        /// <returns>True if both masks are equal, otherwise false</returns>
        public static bool Equals(ref Mask a, ref Mask b)
        {
            return a.x1 == b.x1 && a.x2 == b.x2;
        }

        public static bool operator ==(Mask a, Mask b)
        {
            return Equals(ref a, ref b);
        }

        public static bool operator !=(Mask a, Mask b)
        {
            return !Equals(ref a, ref b);
        }

        public static Mask operator +(Mask a, Mask b)
        {
            Mask result;
            Add(ref a, ref b, out result);
            return result;
        }

        public static Mask operator -(Mask a, Mask b)
        {
            Mask result;
            Remove(ref a, ref b, out result);
            return result;
        }
    }
}
