using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace QuestMania.LevelDesign.BuildingBlocks
{
    public class Block
    {
        protected string name;
        protected int width;
        protected int height;

        public Texture2D texture { get; set; }
        public Vector2 Position;
        public Rectangle CollisionRectangle;

        public Block(Rectangle rectangle, string tileName)
        {
            CollisionRectangle = rectangle;
            name = tileName;

            Position.X = CollisionRectangle.X;
            Position.Y = CollisionRectangle.Y;
            width = CollisionRectangle.Width;
            height = CollisionRectangle.Height;
        }

        public void LoadContent()
        {
            texture = Global.Content.Load<Texture2D>($"Blocks/{ name }");
        }

        public void Draw()
        {
            Global.SpriteBatch.Draw(texture, CollisionRectangle, Color.White);
        }
    }
}
