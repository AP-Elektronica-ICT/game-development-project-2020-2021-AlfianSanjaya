using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using QuestMania.LevelDesign;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuestMania
{
    public class World : IDisposable
    {
        public Hero hero;

        //public int LevelID { get; set; }
        public Level level;

        //private Camera camera;

        public World(Hero player, Level newLevel)
        {
            hero = player;
            hero.LoadContent();
            // Create level based on ID
            //LevelID = id;
            level = newLevel;
            level.LoadWorld(FileManager.GetPath(@$"Content\Levels\map-{ level.ID }.json"),
                            Global.Content.Load<Texture2D>("Background/background-test"));
        }

        public void Update(GameTime gameTime)
        {
            hero.Update(gameTime);

            // Check for collision
            foreach (var tile in level.Tiles)
            {
                hero.CheckCollision(tile.CollisionRectangle, level.LevelWidth);
                Global.Camera.Update(hero.Position, level.LevelWidth, level.LevelHeight);
            }
        }

        public void Draw()
        {
            level.DrawWorld();

            hero.Draw();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
