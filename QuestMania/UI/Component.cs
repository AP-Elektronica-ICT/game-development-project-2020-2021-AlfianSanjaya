using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuestMania.UI
{
    public abstract class Component
    {
        protected Vector2 Position;
        public abstract void Update();
        public abstract void Draw();
    }
}
