using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuestMania.UI
{
    public class Label : Component
    { 
        private string text;
        private SpriteFont font;

        public Label(string labelText, Vector2 labelPosition, SpriteFont labelFont)
        {
            text = labelText;
            Position = labelPosition;
            font = labelFont;
        }
        public override void Update()
        {
            
        }

        public override void Draw()
        {
            Global.SpriteBatch.DrawString(font, text, Position, Color.Black);
        }


    }
}
