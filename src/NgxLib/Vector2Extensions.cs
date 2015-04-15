using Microsoft.Xna.Framework;

namespace NgxLib
{
    /// <summary>
    /// XNA Vector2 extensions
    /// </summary>
    public static class Vector2Extensions
    {
        /// <summary>
        /// Helper function for moving a vector from one position to another over time.
        /// </summary>
        /// <param name="position">The current position.</param>
        /// <param name="destination">The destination position.</param>
        /// <param name="speed">The speed at which the move will happen.</param>
        /// <returns>A difference vector of the two positions or the destination vector if the position reached it's destination</returns>
        public static Vector2 Move(this Vector2 position, Vector2 destination, float speed)
        {
            var dist = destination - position;

            if (dist.LengthSquared() < speed)
            {
                return destination;
            }

            dist.Normalize();

            return position + dist * speed;
        }
    }
}