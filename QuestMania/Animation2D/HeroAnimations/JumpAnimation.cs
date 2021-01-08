using Microsoft.Xna.Framework.Graphics;
using QuestMania.States;

namespace QuestMania.Animation2D.HeroAnimations
{
    public class JumpAnimation : Animation
    {
        public override EntityState State { get; protected set; }
        public JumpAnimation(Texture2D texture, int numberOfFrames, int animationSpeed) : base(texture, numberOfFrames, animationSpeed)
        {
            State = EntityState.Jump;
        }
    }
}
