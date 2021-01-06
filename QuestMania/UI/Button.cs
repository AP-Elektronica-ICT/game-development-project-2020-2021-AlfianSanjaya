using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuestMania.UI
{
    public class Button : Component
    {
        #region Fields
        private MouseState currentMouse;
        private SpriteFont font;
        private bool isHovering;
        private MouseState previousMouse;
        public Texture2D Texture;
        #endregion

        #region Properties
        public event EventHandler Click;
        public bool Clicked { get; private set; }
        public Color TextColor { get; set; }
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
            }
        }

        public string Text { get; set; }
        #endregion

        #region Methods
        public Button(string text, Vector2 newPosition, Texture2D texture, SpriteFont newFont)
        {
            Text = text;
            Position = newPosition;
            Texture = texture;
            font = newFont;
            TextColor = Color.Black;
        }

        public override void Update()
        {
            previousMouse = currentMouse;
            currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(currentMouse.X, currentMouse.Y, 1, 1);

            isHovering = false;

            if (mouseRectangle.Intersects(Rectangle))
            {
                isHovering = true;

                if (currentMouse.LeftButton == ButtonState.Released && previousMouse.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }

        public override void Draw()
        {
            var color = Color.White;

            if (isHovering)
            {
                color = Color.Gray;
            }

            Global.SpriteBatch.Draw(Texture, Rectangle, color);

            if (!string.IsNullOrEmpty(Text))
            {
                var x = (Rectangle.X + (Rectangle.Width / 2)) - (font.MeasureString(Text).X / 2);
                var y = (Rectangle.Y + (Rectangle.Height / 2)) - (font.MeasureString(Text).Y / 2);

                Global.SpriteBatch.DrawString(font, Text, new Vector2(x, y), TextColor);
            }

        }
        #endregion
    }
}
