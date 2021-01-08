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
        public virtual void LoadContent(Texture2D texture, SpriteFont font) { }
        public virtual void LoadContent(SpriteFont font) { }
        public virtual void Update() { }
        public abstract void Draw();
    }
}
