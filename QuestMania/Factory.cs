using QuestMania.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuestMania
{
    public static class Factory
    {
        //public static CreateTileInstance(int tileWidth, int tileHeight, string tileToLoad)
        //{
        //    return new CollisionTile(new Rectangle(x * 64, y * 64, 64, 64), tileToLoad);
        //}

        public static Hero CreateHero()
        {
            return new Hero(new KeyboardReader());
        }
    }
}
