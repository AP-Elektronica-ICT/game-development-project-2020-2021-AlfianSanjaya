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

        public override void LoadContent()
        {
            base.LoadContent();
            string title = "Game Over!";
            titleLabel = new Label(title, new Vector2((Global.ScreenWidth / 2) - (titleFont.MeasureString(title).X / 2), 150), titleFont);

            int centerX = Global.ScreenWidth / 2 - buttonTexture.Width / 2;
            int centerY = Global.ScreenHeight / 2 - buttonTexture.Height / 2;
            restartButton = new Button("Restart level",
                                    new Vector2(centerX, centerY),
                                    buttonTexture,
                                    buttonFont);
            menuButton = new Button("Back to menu", new Vector2(centerX, centerY + (buttonTexture.Height * 3 / 2)), buttonTexture, buttonFont);

            components.Add(titleLabel);
            components.Add(restartButton);
            components.Add(menuButton);

            restartButton.Click += RestartButton_Click;
            menuButton.Click += MenuButton_Click;
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
