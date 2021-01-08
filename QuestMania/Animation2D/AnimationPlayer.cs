using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuestMania.Animation2D
{
    /// <summary>
    /// This class helps you with playing an animation
    /// </summary>
    public class AnimationPlayer
    {
        public Animation CurrentAnimation { get; private set; }

        /// <summary>
        /// Load the current animation.
        /// </summary>
        /// <param name="animationToLoad">The animation you want to load</param>
        public void LoadCurrentAnimation(Animation animationToLoad)
        {
            CurrentAnimation = animationToLoad;
        }

        /// <summary>
        /// Update the animation
        /// </summary>
        /// <param name="gameTime"></param>
        public void UpdateAnimation(GameTime gameTime)
        {
            CurrentAnimation.Update(gameTime);
        }

        /// <summary>
        /// Play the animation
        /// </summary>
        /// <param name="position">The position where you want to draw it</param>
        /// <param name="spriteEffects">For flipping</param>
        public void PlayAnimation(Vector2 position, SpriteEffects spriteEffects = SpriteEffects.None)
        {
            CurrentAnimation.Draw(position, spriteEffects);
        }
    }
}
