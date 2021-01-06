using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using QuestMania.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuestMania.States
{
    class SelectLevelScreen : Screen
    {
        private Button levelOneButton;
        public override State State => State.Select;

        public SelectLevelScreen() : base()
        {
            levelOneButton = new Button("Level 1", new Vector2(1280 / 4, 720 / 4), buttonTexture, buttonFont);

            levelOneButton.Click += LevelOneButton_Click;
        }
        public override void Update(GameTime gameTime)
        {
            levelOneButton.Update();
        }

        public override void Draw()
        {
            Platformer.graphics.GraphicsDevice.Clear(Color.BurlyWood);
            Global.SpriteBatch.Begin();
            levelOneButton.Draw();
            Global.SpriteBatch.End();
        }

        private void LevelOneButton_Click(object sender, EventArgs e)
        {
            Platformer.ScreenManager.SwitchToNextScreen(State.Game);
        }


    }
}
