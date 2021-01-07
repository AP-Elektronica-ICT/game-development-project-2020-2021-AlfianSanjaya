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
        public bool GameOver { get; set; } = false;
        public bool Victory { get; set; } = false;

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
            foreach (var block in Level.Blocks)
            {
                if (block is CollisionTile)
                    Hero.CheckCollision(block.CollisionRectangle, Level.LevelWidth);
                else if (block is Flag)
                {
                    if (Hero.CollisionRectangle.Intersects(block.CollisionRectangle))
                    {
                        Victory = true;
                        Hero.SetToSpawn();
                    }
                }
                Global.Camera.Update(Hero.Position, Level.LevelWidth, Level.LevelHeight);
            }

            // Check if hero is dead
            Hero.CheckOutOfBounds(Level.LevelHeight);
            if (Hero.IsDead)
            {
                GameOver = true;
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
