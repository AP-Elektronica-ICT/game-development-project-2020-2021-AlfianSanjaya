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
        private Label titleLabel;

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

            string title = "Quest Mania";
            titleLabel = new Label(title, new Vector2((Global.ScreenWidth / 2) - (titleFont.MeasureString(title).X / 2), 150), titleFont);


            components = new List<Component>()
            {
               playButton,
               quitButton,
               titleLabel
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
