using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace QuestMania.LevelDesign
{
    public class Block
    {
        protected string name;
        protected int width;
        protected int height;

        public Texture2D texture { get; set; }
        public Vector2 Position;
        public Rectangle CollisionRectangle { get; protected set; }

        public Block(Rectangle rectangle, string tileName)
        {
            CollisionRectangle = rectangle;
            name = tileName;

            Position.X = CollisionRectangle.X;
            Position.Y = CollisionRectangle.Y;
            width = CollisionRectangle.Width;
            height = CollisionRectangle.Height;

            LoadTile();
        }

        public void LoadTile()
        {
            texture = Global.Content.Load<Texture2D>($"Components/{ name }");
        }

        public void Draw()
        {
            Global.SpriteBatch.Draw(texture, CollisionRectangle, Color.White);
        }
    }

    public class CollisionTile : Block
    {
        public CollisionTile(Rectangle rectangle, string tileName) : base(rectangle, tileName)
        { 
           
        }
    }

    public class Flag : Block
    {
        public Flag(Rectangle rectangle, string tileName) : base(rectangle, tileName)
        {
        }
    }
}
