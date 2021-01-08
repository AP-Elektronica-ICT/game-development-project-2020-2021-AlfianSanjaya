using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using QuestMania.Commands;
using QuestMania.FileSystem;
using QuestMania.LevelDesign;
using QuestMania.LevelDesign.BuildingBlocks;
using System;
using System.Collections.Generic;

namespace QuestMania.LevelDesign
{
    public class World
    {
        public bool GameOver { get; set; } = false;
        public bool Victory { get; set; } = false;

        public Hero Hero { get; set; }

        public Level Level { get; set; }

        public World(Hero player, Level newLevel)
        {
            Hero = player;
            Level = newLevel;   
        }

        public void LoadContent()
        {
            Hero.LoadContent();
            Level.LoadWorld(FileManager.GetPath(@$"Content\Levels\map-{ Level.ID }.json"),
                            Global.Content.Load<Texture2D>($"Background/map-background-1"));
        }

        public void Update(GameTime gameTime)
        {
            Global.Camera.Update(Hero.Position, Level.LevelWidth, Level.LevelHeight);

            if (Hero.IsDead == false)
            {
                // Check for collision
                foreach (var block in Level.Blocks)
                {
                    if (block is CollisionTile)
                        Hero.CheckCollision(block.CollisionRectangle, Level.LevelWidth);
                    if (block is Flag)
                    {
                        if (Hero.CollisionRectangle.Intersects(block.CollisionRectangle))
                        {
                            Victory = true;
                            Hero.SetToSpawn();
                        }
                    }
                    if (block is Spike)
                    {
                        if (Hero.CollisionRectangle.Intersects(block.CollisionRectangle))
                        {
                            GameOver = true;
                            Hero.IsDead = true;
                            Hero.SetToSpawn();
                        }
                    }
                }
            }

            // Check if hero is dead
            Hero.CheckOutOfBounds(Level.LevelHeight);
            if (Hero.IsDead)
            {
                GameOver = true;
                Hero.IsDead = false;
                Hero.SetToSpawn();
            }

            Hero.Update(gameTime);
        }

        public void Draw()
        {
            Level.DrawWorld();

            Hero.Draw();
        } 
    }
}
