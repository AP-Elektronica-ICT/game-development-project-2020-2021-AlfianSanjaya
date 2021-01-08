using Microsoft.Xna.Framework;

namespace QuestMania.LevelDesign.BuildingBlocks
{
    public class Spike : Block
    {
        public Spike(Rectangle rectangle, string tileName) : base(rectangle, tileName)
        {
            CollisionRectangle.Y += Global.World.TileHeight / 2;
        }
    }
}
