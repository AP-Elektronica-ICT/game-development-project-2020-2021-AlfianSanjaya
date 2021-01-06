using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuestMania.Collisions
{
    public static class CollisionManager
    {
        public static bool IsTouchingTop(this Rectangle r1, Rectangle obstacle)
        {
            return (r1.Bottom >= (obstacle.Top - 1) &&
                    r1.Bottom <= obstacle.Top + (obstacle.Height / 2) &&
                    r1.Right >= obstacle.Left + (obstacle.Width / 5) &&
                    r1.Left <= obstacle.Right - (obstacle.Width / 5));
        }

        public static bool IsTouchingBottom(this Rectangle r1, Rectangle obstacle)
        {
            return (r1.Top <= obstacle.Bottom + (obstacle.Height / 5) &&
                    r1.Top >= obstacle.Bottom - 1 &&
                    r1.Right >= obstacle.Left + (obstacle.Width / 5) &&
                    r1.Left <= obstacle.Right - (obstacle.Width / 5));
        }

        public static bool IsTouchingLeft(this Rectangle r1, Rectangle obstacle)
        {
            return r1.Right <= obstacle.Right &&
                r1.Right >= obstacle.Left - 5 &&
                r1.Top <= obstacle.Bottom - (obstacle.Width / 4) &&
                r1.Bottom >= obstacle.Top + (obstacle.Width / 4);
        }

        public static bool IsTouchingRight(this Rectangle r1, Rectangle obstacle)
        {
            return r1.Left >= obstacle.Left &&
                r1.Left <= obstacle.Right + 5 &&
                r1.Top <= obstacle.Bottom - (obstacle.Width / 4) &&
                r1.Bottom >= obstacle.Top + (obstacle.Width / 4);
        }
    }
}
