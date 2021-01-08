using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuestMania.Interfaces
{
    interface IGameObject
    {
        // Vectors
        public Vector2 Position { get; set; }
        public Vector2 Direction { get; set; }
        public Vector2 Velocity { get; set; }
        void Update(GameTime gameTime);
        void Draw();
    }
}
