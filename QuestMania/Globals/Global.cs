using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using QuestMania.Animation2D;
using QuestMania.Commands;
using QuestMania.LevelDesign;
using System.Collections.Generic;

namespace QuestMania
{
    public static class Global
    {
        public static ContentManager Content;
        public static SpriteBatch SpriteBatch;
        public static Camera Camera;

        public static int ScreenWidth = 1280;
        public static int ScreenHeight = 720;

        public static class World
        {
            public static Level LevelOne;
            public static int TileWidth = 64;
            public static int TileHeight = 64;
        } 

        public static class Hero
        {
            public static Dictionary<string, Animation> Animations = new Dictionary<string, Animation>();
            public static List<IGameCommand> GameCommands;
        }

        public static class Enemy
        {
            public static Animation IdleAnimation;
        }
    }
}
