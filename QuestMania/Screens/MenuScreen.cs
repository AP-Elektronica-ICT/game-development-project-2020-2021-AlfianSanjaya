using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using QuestMania.UI;
using QuestMania.States;
using System;
using System.Collections.Generic;

namespace QuestMania.Screens
{
    public class MenuScreen : Screen
    {
        private Button playButton, quitButton;
        private Label titleLabel;

        private Texture2D backgroundTexture;
        private Game game;

        public override State State => State.Menu;

        public MenuScreen(Game newGame, Button newPlayButton, Button newQuitButton, Label newTitleLabel)
        {
            game = newGame;
            playButton = newPlayButton;
            quitButton = newQuitButton;
            titleLabel = newTitleLabel;

            components.Add(playButton);
            components.Add(quitButton);
            components.Add(titleLabel);

            playButton.Click += PlayButton_Click;
            quitButton.Click += QuitButton_Click;
        }

        public override void LoadContent()
        {            
            base.LoadContent();
            backgroundTexture = Global.Content.Load<Texture2D>("Background/background-1");

            foreach (Component component in components)
            {
                if (component is Button)
                    component.LoadContent(buttonTexture, buttonFont);
                if (component is Label)
                    component.LoadContent(titleFont);
            }

            PlaceUI();
        }

        public void PlaceUI()
        {
            int centerX = Global.ScreenWidth / 2 - buttonTexture.Width / 2;
            int centerY = Global.ScreenHeight / 2 - buttonTexture.Height / 2;

            playButton.SetPosition(centerX, centerY);
            quitButton.SetPosition(centerX, centerY + (buttonTexture.Height * 3 / 2));
            titleLabel.SetPosition((int)((Global.ScreenWidth / 2) - (titleFont.MeasureString(titleLabel.Text).X / 2)), 150);
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
            Platformer.ScreenManager.ClearScreen();
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
