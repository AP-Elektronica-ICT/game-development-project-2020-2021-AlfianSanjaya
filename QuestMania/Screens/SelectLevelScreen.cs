using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using QuestMania.UI;
using System;
using QuestMania.States;

namespace QuestMania.Screens
{
    class SelectLevelScreen : Screen
    {
        private Button levelOneButton, levelTwoButton, backButton;
        private Label titleLabel;

        private Texture2D backgroundTexture;

        public override State State => State.Select;

        public SelectLevelScreen(Button level1, Button level2, Button back, Label title)
        {
            levelOneButton = level1;
            levelTwoButton = level2;
            backButton = back;
            titleLabel = title;

            components.Add(levelOneButton);
            components.Add(levelTwoButton);
            components.Add(backButton);
            components.Add(titleLabel);


            levelOneButton.Click += LevelOneButton_Click;
            levelTwoButton.Click += LevelTwoButton_Click;
            backButton.Click += BackButton_Click;
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

            levelOneButton.SetPosition(centerX, centerY);
            levelTwoButton.SetPosition(centerX, centerY + (buttonTexture.Height * 3 / 2));
            backButton.SetPosition(20, 10);
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
            Platformer.graphics.GraphicsDevice.Clear(Color.BurlyWood);
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

        private void LevelOneButton_Click(object sender, EventArgs e)
        {
            Platformer.ScreenManager.SwitchToNextScreen(State.Game, 1);
        }

        private void LevelTwoButton_Click(object sender, EventArgs e)
        {
            Platformer.ScreenManager.SwitchToNextScreen(State.Game, 2);
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            Platformer.ScreenManager.SwitchToNextScreen(State.Menu);
        }
    }
}
