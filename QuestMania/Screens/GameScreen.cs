using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using QuestMania.LevelDesign;

namespace QuestMania.States
{
    public class GameScreen : Screen
    {
        private Hero hero;
        public override State State => State.Game;

        public GameScreen() : base()
        {
            hero = Factory.CreateHero();
            
            Global.World.LevelOne = new Level();

            Global.World.LevelOne.LoadWorld(FileManager.GetPath(@"Content\Levels\map-1.json"),
                                            Global.Content.Load<Texture2D>("Background/background-test"));

            LoadContent();
        }

        public void LoadContent()
        {
            hero.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            hero.Update(gameTime);

            // Check for collision
            foreach (var tile in Global.World.LevelOne.Tiles)
            {
                hero.CheckCollision(tile.CollisionRectangle, Global.World.LevelOne.LevelWidth);
                Global.Camera.Update(hero.Position, Global.World.LevelOne.LevelWidth, Global.World.LevelOne.LevelHeight);
            }

        }

        public override void Draw()
        {
            Platformer.graphics.GraphicsDevice.Clear(Color.BurlyWood);

            Global.SpriteBatch.Begin(transformMatrix: Global.Camera.Transform);

            Global.World.LevelOne.DrawWorld();
            
            hero.Draw();

            Global.SpriteBatch.End();
        }


    }
}
