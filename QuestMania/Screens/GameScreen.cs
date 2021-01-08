using Microsoft.Xna.Framework;
using QuestMania.LevelDesign;
using QuestMania.States;

namespace QuestMania.Screens
{
    public class GameScreen : Screen
    {
        public override State State => State.Game;
        private World currentWorld;
        private World[] worlds;

        public GameScreen(World[] newWorlds)
        {
            worlds = newWorlds;
        }

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
