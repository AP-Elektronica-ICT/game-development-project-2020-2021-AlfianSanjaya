using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuestMania.States
{
    public abstract class Screen
    {
        protected Texture2D buttonTexture;
        protected SpriteFont buttonFont;
        protected SpriteFont titleFont;

        public abstract State State { get;}

        public Screen()
        {
            LoadContent();
        }

        public void LoadContent() 
        {
            buttonTexture = Global.Content.Load<Texture2D>("GUI/button");
            buttonFont = Global.Content.Load<SpriteFont>("Fonts/DefaultFont");
            titleFont = Global.Content.Load<SpriteFont>("Fonts/TitleFont");
        }
        public abstract void Update(GameTime gameTime);
        public abstract void Draw();
    }
}
