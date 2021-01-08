using Microsoft.Xna.Framework;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using QuestMania.FileSystem;
using QuestMania.LevelDesign.BuildingBlocks;

namespace QuestMania.LevelDesign
{
    public class Level
    {
        public List<Block> Blocks { get; } = new List<Block>();

        private Texture2D backgroundTexture;
        public int LevelWidth;
        public int LevelHeight;

        public int ID { get; set; }

        public char[,] TileArray { get; set; }

        public Level(int id)
        {
            ID = id;
        }

        public void LoadWorld(string path, Texture2D newBackgroundTexture)
        {
            backgroundTexture = newBackgroundTexture;
            TileArray = FileManager.LoadLevelData(path);
            LevelHeight = TileArray.GetLength(0) * 64;
            LevelWidth = TileArray.GetLength(1) * 64;          
            CreateWorld();

            foreach (var block in Blocks)
            {
                block.LoadContent();
            }
        }


        private void CreateWorld()
        {
            for (int x = 0; x < TileArray.GetLength(1); x++)
            {
                for (int y = 0; y < TileArray.GetLength(0); y++)
                {
                    char blockType = TileArray[y, x];
                    
                    if (blockType != '.')
                    {
                        Block block = null;
                        int unitX = x * Global.World.TileWidth;
                        int unitY = y * Global.World.TileHeight;
                        switch (blockType)
                        {
                            case 'G':
                                block = new CollisionTile(new Rectangle(unitX, unitY, Global.World.TileWidth, Global.World.TileHeight), "DefaultTile");
                                break;
                            case 'D':
                                block = new CollisionTile(new Rectangle(unitX, unitY, Global.World.TileWidth, Global.World.TileHeight), "Dirt2");
                                break;
                            case 'S':
                                block = new Spike(new Rectangle(unitX, unitY, Global.World.TileWidth, Global.World.TileHeight / 2), "Spike");
                                break;
                            case 'F':
                                block = new Flag(new Rectangle(unitX, unitY, Global.World.TileWidth, Global.World.TileHeight), "Flag");
                                break;
                            default:
                                break;
                        }
                        Blocks.Add(block);
                    }
                }
            }
        }

        public void DrawWorld()
        {
            Global.SpriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, LevelWidth, LevelHeight), Color.White);
            foreach (var tile in Blocks)
            {
                tile.Draw();
            }
        }
    }
}
