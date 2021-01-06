using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuestMania.Animation2D
{
    // Class that handles a single animation
    public class Animation
    {
        public Texture2D SpriteSheetTexture { get; private set; }

        // List of key frames
        private List<AnimationFrame> frames;

        public static int FrameWidth = 65;
        public static int FrameHeight = 90;

        // Current frame that shows
        public AnimationFrame CurrentFrame { get; set; }

        private int frameCounter;
        private double frameMovement = 0;
        private int animationSpeed = 1;

        public Animation(Texture2D texture, int numberOfFrames, int animationSpeed)
        {
            this.animationSpeed = animationSpeed;
            frames = new List<AnimationFrame>();
            SpriteSheetTexture = texture;

            // Auto add key frames 
            int xOffset = 0;
            for (int i = 0; i < numberOfFrames; i++)
            {
                AddFrame(new AnimationFrame(new Rectangle(45 + xOffset, 18, FrameWidth, FrameHeight)));
                xOffset += (SpriteSheetTexture.Width / numberOfFrames);
            }
        }

        private void AddFrame(AnimationFrame frame)
        {
            frames.Add(frame);
            CurrentFrame = frames[0];
        }

        public void Update(GameTime gameTime)
        {
            CurrentFrame = frames[frameCounter];

            // Frame Independent Movement
            frameMovement = frameMovement + (CurrentFrame.SourceRectangle.Width * gameTime.ElapsedGameTime.TotalSeconds);
            if (frameMovement >= CurrentFrame.SourceRectangle.Width / animationSpeed)
            {
                frameCounter++;
                frameMovement = 0;
            }

            if (frameCounter >= frames.Count)
            {
                frameCounter = 0;
            }
        }

        // Draw the animation
        public void Draw(Vector2 position, SpriteEffects spriteEffects)
        {
            Global.SpriteBatch.Draw(
                SpriteSheetTexture,
                position,
                CurrentFrame.SourceRectangle,
                Color.White,
                0.0f,
                Vector2.Zero,
                1.0f,
                spriteEffects,
                0.0f
            );
        }
    }
}
