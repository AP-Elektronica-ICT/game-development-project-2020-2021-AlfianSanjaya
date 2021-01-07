using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using QuestMania.Input;
using QuestMania.LevelDesign;
using QuestMania.UI;
using System.Collections.Generic;

namespace QuestMania.States
{
    public class GameScreen : Screen
    {
        private Button backButton;
        private World currentWorld;

        private World[] worlds =
        {
            new World(Factory.CreateHero(2, 5), new Level(1)),
            new World(Factory.CreateHero(0, 1), new Level(2))
        };

        public GameScreen() : base()
        {
            State = State.Game;
            backButton = new Button("Back", new Vector2(20, 10), buttonTexture, buttonFont);
            components.Add(backButton);

            backButton.Click += BackButton_Click;
        }

        private void BackButton_Click(object sender, System.EventArgs e)
        {
            Platformer.ScreenManager.SwitchToNextScreen(State.Select);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in components)
            {
                component.Update();
            }

            if (currentWorld == null)
            {
                if (SelectLevel != -1)
                {
                    currentWorld = worlds[SelectLevel - 1];
                }
            }
            else // User select a different level
            {
                if (SelectLevel != currentWorld.Level.ID)
                {
                    currentWorld = worlds[SelectLevel - 1];
                    currentWorld.Hero.SetToSpawn();
                }
            }

            // Check if current world has to end
            if (currentWorld.EndWorld)
            {
                // Switch to end screen
                Platformer.ScreenManager.SwitchToNextScreen(State.Menu);
                currentWorld.EndWorld = false;
            }

            currentWorld.Update(gameTime);
        }

        public override void Draw()
        {
            if (currentWorld != null)
            {
                Platformer.graphics.GraphicsDevice.Clear(Color.BurlyWood);

                Global.SpriteBatch.Begin(transformMatrix: Global.Camera.Transform);

                currentWorld.Draw();

                Global.SpriteBatch.End();

                Global.SpriteBatch.Begin();

                foreach (var component in components)
                {
                    component.Draw();
                }

                Global.SpriteBatch.End();
            }

        }


    }
}
