using Microsoft.Xna.Framework;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace QuestMania.LevelDesign
{
    public class Level
    {
        public List<Tile> Tiles { get; } = new List<Tile>();

        private Texture2D backgroundTexture;
        public int LevelWidth;
        public int LevelHeight;

        public byte[,] TileArray { get; set; }

        public void LoadWorld(string path, Texture2D newBackgroundTexture)
        {
            TileArray = FileManager.LoadLevelData(path);

            LevelHeight = TileArray.GetLength(0) * 64;
            LevelWidth = TileArray.GetLength(1) * 64;
            backgroundTexture = newBackgroundTexture;
            Debug.WriteLine($"Height: {LevelHeight} Column: {LevelWidth} ");

            CreateWorld();
        }


        private void CreateWorld()
        {
            for (int x = 0; x < TileArray.GetLength(1); x++)
            {
                for (int y = 0; y < TileArray.GetLength(0); y++)
                {
                    int number = TileArray[y, x];
                    string tileToLoad = "";
                    if (number > 0)
                    {
                        switch (number)
                        {
                            case 1:
                                tileToLoad = "DefaultTile";
                                break;
                            case 2:
                                tileToLoad = "Dirt2";
                                break;
                        }
                        Tiles.Add(new CollisionTile(new Rectangle(x * Global.World.TileWidth, y * Global.World.TileHeight, Global.World.TileWidth, Global.World.TileHeight), tileToLoad));
                    }
                }
            }
        }

        public void DrawWorld()
        {
            Global.SpriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, Global.World.LevelOne.LevelWidth, Global.World.LevelOne.LevelHeight), Color.White);
            foreach (var tile in Tiles)
            {
                tile.Draw();
            }
        }
    }
}
