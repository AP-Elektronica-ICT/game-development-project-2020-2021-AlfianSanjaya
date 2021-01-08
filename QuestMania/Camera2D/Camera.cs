using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuestMania.Camera2D
{
    public class Camera
    {
        private Matrix transform;
        public Matrix Transform
        {
            get
            {
                return transform;
            }
        }

        private Vector2 centre;
        private Viewport viewport;

        public Camera(Viewport newViewPort)
        {
            viewport = newViewPort;
        }

        public void Update(Vector2 position, int mapWidth, int mapHeight)
        {
            centre.X = position.X;
            centre.Y = position.Y;

            int halfScreenWidth = viewport.Width / 2;
            int halfScreenHeight = viewport.Height / 2;

            // X map boundaries
            if (position.X < halfScreenWidth)
                centre.X = halfScreenWidth;
            else if (position.X > mapWidth - halfScreenWidth)
                centre.X = mapWidth - halfScreenWidth;
            // Y map boundaries
            if (position.Y < halfScreenHeight)
                centre.Y = halfScreenHeight;
            else if (position.Y > mapHeight - halfScreenHeight)
                centre.Y = mapHeight - halfScreenHeight;

            transform = Matrix.CreateTranslation(new Vector3(-centre.X + halfScreenWidth,
                                                             -centre.Y + halfScreenHeight, 0));
        }
    }
}