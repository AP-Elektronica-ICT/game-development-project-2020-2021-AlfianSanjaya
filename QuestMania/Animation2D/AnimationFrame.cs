using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuestMania.Animation2D
{
    public class AnimationFrame
    {
        public Rectangle SourceRectangle { get; private set; }
        public AnimationFrame(Rectangle frame)
        {
            SourceRectangle = frame;
        }
    }
}
