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

        public byte[,] TileArray { get; set; }

        public Level(int id)
        {
            ID = id;
        }

        public void LoadWorld(string path, Texture2D newBackgroundTexture)
        {
            TileArray = FileManager.LoadLevelData(path);

            LevelHeight = TileArray.GetLength(0) * 64;
            LevelWidth = TileArray.GetLength(1) * 64;
            backgroundTexture = newBackgroundTexture;          
            CreateWorld();
        }


        private void CreateWorld()
        {
            for (int x = 0; x < TileArray.GetLength(1); x++)
            {
                for (int y = 0; y < TileArray.GetLength(0); y++)
                {
                    int number = TileArray[y, x];

                    Block block = null;
                    if (number > 0)
                    {
                        switch (number)
                        {
                            case 1:                           
                                block = new CollisionTile(new Rectangle(x * Global.World.TileWidth, y * Global.World.TileHeight, Global.World.TileWidth, Global.World.TileHeight), "DefaultTile");
                                break;
                            case 2:
                                block = new CollisionTile(new Rectangle(x * Global.World.TileWidth, y * Global.World.TileHeight, Global.World.TileWidth, Global.World.TileHeight), "Dirt2");
                                break;
                            case 3:
                                block = new Spike(new Rectangle(x * Global.World.TileWidth, y * Global.World.TileHeight, Global.World.TileWidth, Global.World.TileHeight / 2), "Spike");
                                break;
                            case 4:
                                block = new Flag(new Rectangle(x * Global.World.TileWidth, y * Global.World.TileHeight, Global.World.TileWidth, Global.World.TileHeight), "Flag");
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
