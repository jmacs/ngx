using System;
using Microsoft.Xna.Framework;

namespace NgxLib
{
    /// <summary>
    /// Represents the current game time
    /// </summary>
    public static class Time
    {
        public static float RealTime { get; private set; }
        public static float Delta { get; private set; }
        public static TimeSpan TotalGameTime { get; private set; }
        public static bool IsSlow { get; private set; }
        public static float TimeScale { get; set; }

        public static int FrameRate { get { return frameRate; } }

        static int frameRate = 0;
        static int frameCounter = 0;
        static TimeSpan elapsedTime = TimeSpan.Zero;

        static Time()
        {
            TimeScale = 1.0f;
        }

        public static void Update(GameTime t, bool countFrame)
        {
            RealTime = (float) t.ElapsedGameTime.TotalSeconds;
            Delta = (float)t.ElapsedGameTime.TotalSeconds * TimeScale;
            TotalGameTime = t.TotalGameTime;
            IsSlow = t.IsRunningSlowly;

            if (countFrame)
            {
                frameCounter++;
            }
            else
            {
                elapsedTime += t.ElapsedGameTime;

                if (elapsedTime > TimeSpan.FromSeconds(1))
                {
                    elapsedTime -= TimeSpan.FromSeconds(1);
                    frameRate = frameCounter;
                    frameCounter = 0;
                }
            }
            

        }
    }
}