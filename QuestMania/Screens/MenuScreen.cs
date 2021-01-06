using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using QuestMania.UI;
using System;
using System.Collections.Generic;

namespace QuestMania.States
{
    public class MenuScreen : Screen
    {
        private Button playButton, quitButton;
        private Texture2D backgroundTexture;
        private Game game;
        private List<Component> components;

        public override State State => State.Menu;

        public MenuScreen(Game newGame) : base()
        {
            game = newGame;
            

            backgroundTexture = Global.Content.Load<Texture2D>("Background/background-1");

            int centerX = Global.ScreenWidth / 2 - buttonTexture.Width / 2;
            int centerY = Global.ScreenHeight / 2 - buttonTexture.Height / 2;
            playButton = new Button("Play",
                                    new Vector2(centerX, centerY),
                                    buttonTexture,
                                    buttonFont);
            quitButton = new Button("Quit", new Vector2(centerX, centerY + (buttonTexture.Height * 3 / 2)), buttonTexture, buttonFont);

            components = new List<Component>()
            {
               playButton,
               quitButton
            };


            playButton.Click += PlayButton_Click;
            quitButton.Click += QuitButton_Click;
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in components)
            {
                component.Update();
            }
        }

        public override void Draw()
        {
            Global.SpriteBatch.Begin();

            Global.SpriteBatch.Draw(
                backgroundTexture,
                Vector2.Zero,
                null,
                Color.White,
                0.0f,
                Vector2.Zero,
                1.0f,
                SpriteEffects.None,
                0.0f
            );

            var text = "PLATFORMER";
            var x = (Global.ScreenWidth / 2) - (titleFont.MeasureString(text).X / 2);
            var y = 150;

            Global.SpriteBatch.DrawString(titleFont, text, new Vector2(x, y), Color.Black);

            foreach (var component in components)
            {
                component.Draw();
            }
            Global.SpriteBatch.End();
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            Platformer.ScreenManager.SwitchToNextScreen(State.Select);
        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            game.Exit();
        }
    }
}
