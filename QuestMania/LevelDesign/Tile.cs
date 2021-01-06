using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace QuestMania.LevelDesign
{
    public class Tile
    {
        protected string name;
        protected int width;
        protected int height;

        public Texture2D texture { get; set; }
        public Vector2 Position;
        public Rectangle CollisionRectangle { get; protected set; }

        public Tile(Rectangle rectangle)
        {
            CollisionRectangle = rectangle;
        }

        public void Draw()
        {
            Global.SpriteBatch.Draw(texture, CollisionRectangle, Color.White);
        }
    }

    public class CollisionTile : Tile
    {
        public CollisionTile(Rectangle rectangle, string tileName) : base(rectangle)
        {            
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
    }
}
