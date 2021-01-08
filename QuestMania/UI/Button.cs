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
        
        private bool isHovering;
        private MouseState previousMouse;
        
        #endregion

        #region Properties
        public SpriteFont Font { get; private set; }
        public Texture2D Texture { get; private set; }

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
        public Button(string text)
        {
            Text = text;
            TextColor = Color.Black;
        }

        public void SetPosition(int x, int y)
        {
            Position.X = x;
            Position.Y = y;
        }

        public override void LoadContent(Texture2D texture, SpriteFont font)
        {
            Texture = texture;
            Font = font;
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
                var x = (Rectangle.X + (Rectangle.Width / 2)) - (Font.MeasureString(Text).X / 2);
                var y = (Rectangle.Y + (Rectangle.Height / 2)) - (Font.MeasureString(Text).Y / 2);

                Global.SpriteBatch.DrawString(Font, Text, new Vector2(x, y), TextColor);
            }

        }
        #endregion
    }
}
