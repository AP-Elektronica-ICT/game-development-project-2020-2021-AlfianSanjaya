using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using QuestMania.LevelDesign;
using QuestMania.UI;
using System.Collections.Generic;

namespace QuestMania.States
{
    public class GameScreen : Screen
    {
        private Button backButton;
        //private Hero hero;
        private World world;

        private List<World> worlds;

        public override State State => State.Game;

        public GameScreen() : base()
        {
            worlds = new List<World>();
            //hero = Factory.CreateHero();

            //Global.World.LevelOne = new Level();

            //Global.World.LevelOne.LoadWorld(FileManager.GetPath(@"Content\Levels\map-1.json"),
            //                                Global.Content.Load<Texture2D>("Background/background-test"));

            //hero.LoadContent();

            //world = new World(LevelID);
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

            world.Update(gameTime);
        }

        public override void Draw()
        {
            if (world != null && world.LevelID != LevelID)
            {
                world = null;
                world = new World(LevelID);
            }

            if (world == null)
            {
                world = new World(LevelID);
            }

            Platformer.graphics.GraphicsDevice.Clear(Color.BurlyWood);

            Global.SpriteBatch.Begin(transformMatrix: Global.Camera.Transform);

            world.Draw();

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
