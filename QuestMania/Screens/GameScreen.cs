using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using QuestMania.Input;
using QuestMania.LevelDesign;
using QuestMania.UI;
using QuestMania.States;
using System.Collections.Generic;

namespace QuestMania.Screens
{
    public class GameScreen : Screen
    {
        private World currentWorld;

        private World[] worlds =
        {
            new World(Factory.CreateHero(1, 6), new Level(1)),
            new World(Factory.CreateHero(0, 1), new Level(2))
        };

        public override State State => State.Game;

        public override void Update(GameTime gameTime)
        {
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
                }
            }

            // Check if current world has to end
            if (currentWorld.GameOver)
            {
                // Switch to end screen
                Platformer.ScreenManager.SwitchToNextScreen(State.GameOver);
                currentWorld.GameOver = false;
            }
            else if (currentWorld.Victory)
            {
                Platformer.ScreenManager.SwitchToNextScreen(State.Victory);
                currentWorld.Victory = false;
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
            }
        }


    }
}
