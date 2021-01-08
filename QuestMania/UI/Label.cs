using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuestMania.UI
{
    public class Label : Component
    { 
        public string Text { get; private set; }
        public SpriteFont Font { get; private set; }

        public Label(string labelText)
        {
            Text = labelText;
        }

        public void SetPosition(int x, int y)
        {
            Position.X = x;
            Position.Y = y;
        }

        public override void LoadContent(SpriteFont font)
        {
            Font = font;
        }

        public override void Draw()
        {
            Global.SpriteBatch.DrawString(Font, Text, Position, Color.Black);
        }
    }
}
