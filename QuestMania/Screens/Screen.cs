using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using QuestMania.UI;
using System;
using System.Collections.Generic;

using System.Text;

namespace QuestMania.States
{
    public abstract class Screen
    {
        protected Texture2D buttonTexture;
        protected SpriteFont buttonFont, titleFont;

        protected List<Component> components;

        public int SelectLevel { get; set; }

        public State State { get; set; }

        public Screen()
        {
            components = new List<Component>();
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
