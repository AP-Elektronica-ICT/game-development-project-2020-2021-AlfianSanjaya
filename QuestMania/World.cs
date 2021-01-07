using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using QuestMania.LevelDesign;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuestMania
{
    public class World
    {
        public bool EndWorld { get; set; } = false;

        public Hero Hero { get; set; }

        public Level Level { get; set; }

        //private Camera camera;

        public World(Hero player, Level newLevel)
        {
            Hero = player;
            Hero.LoadContent();

            Level = newLevel;
            Level.LoadWorld(FileManager.GetPath(@$"Content\Levels\map-{ Level.ID }.json"),
                            Global.Content.Load<Texture2D>("Background/background-test"));
        }

        public void Update(GameTime gameTime)
        {
            Hero.Update(gameTime);

            // Check for collision
            foreach (var tile in Level.Blocks)
            {
                if (tile is CollisionTile)
                    Hero.CheckCollision(tile.CollisionRectangle, Level.LevelWidth);
                Global.Camera.Update(Hero.Position, Level.LevelWidth, Level.LevelHeight);
            }

            // Check if hero is dead
            Hero.CheckOutOfBounds(Level.LevelHeight);
            if (Hero.IsDead)
            {
                EndWorld = true;
                Hero.IsDead = false;
                Hero.SetToSpawn();
            }
            
        }

        public void Draw()
        {
            Level.DrawWorld();

            Hero.Draw();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
