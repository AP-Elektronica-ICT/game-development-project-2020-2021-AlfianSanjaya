using Microsoft.Xna.Framework.Graphics;
using QuestMania.States;

namespace QuestMania.Animation2D.HeroAnimations
{
    public class FallAnimation : Animation
    {
        public override EntityState State { get; protected set; }
        public FallAnimation(Texture2D texture, int numberOfFrames, int animationSpeed) : base(texture, numberOfFrames, animationSpeed)
        {
            State = EntityState.Fall;
        }
    }
}
