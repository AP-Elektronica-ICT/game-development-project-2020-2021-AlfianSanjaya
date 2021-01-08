using Microsoft.Xna.Framework;
using QuestMania.States;
using QuestMania.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuestMania.Screens
{
    class GameOverScreen : Screen
    {
        private Button restartButton, menuButton;
        private Label titleLabel;

        public override State State => State.GameOver;

        public GameOverScreen(Button restart, Button menu, Label title)
        {
            restartButton = restart;
            menuButton = menu;
            titleLabel = title;

            components.Add(titleLabel);
            components.Add(restartButton);
            components.Add(menuButton);

            restartButton.Click += RestartButton_Click;
            menuButton.Click += MenuButton_Click;
        }

        public override void LoadContent()
        {
            base.LoadContent();

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

            restartButton.SetPosition(centerX, centerY);
            menuButton.SetPosition(centerX, centerY + (buttonTexture.Height * 3 / 2));
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
            Global.SpriteBatch.Begin();

            foreach (var component in components)
            {
                component.Draw();
            }

            Global.SpriteBatch.End();
        }

        private void RestartButton_Click(object sender, EventArgs e)
        {
            Platformer.ScreenManager.SwitchToNextScreen(State.Game);
        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            Platformer.ScreenManager.SwitchToNextScreen(State.Menu);
        }
    }
}
