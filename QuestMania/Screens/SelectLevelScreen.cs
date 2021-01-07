﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using QuestMania.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuestMania.States
{
    class SelectLevelScreen : Screen
    {
        private Button levelOneButton, levelTwoButton, backButton;
        private Label titleLabel;

        public override State State => State.Select;

        public SelectLevelScreen() : base()
        {
            int centerX = Global.ScreenWidth / 2 - buttonTexture.Width / 2;
            int centerY = Global.ScreenHeight / 2 - buttonTexture.Height / 2;
            levelOneButton = new Button("Level 1", new Vector2(centerX, centerY), buttonTexture, buttonFont);
            levelTwoButton = new Button("Level 2", new Vector2(centerX, centerY + (buttonTexture.Height * 3 / 2)), buttonTexture, buttonFont);
            backButton = new Button("Back", new Vector2(20, 10), buttonTexture, buttonFont);

            string title = "Select a level";
            titleLabel = new Label(title, new Vector2((Global.ScreenWidth / 2) - (titleFont.MeasureString(title).X / 2), 150), titleFont);

            components.Add(levelOneButton);
            components.Add(levelTwoButton);
            components.Add(backButton);
            components.Add(titleLabel);


            levelOneButton.Click += LevelOneButton_Click;
            levelTwoButton.Click += LevelTwoButton_Click;
            backButton.Click += BackButton_Click;
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
