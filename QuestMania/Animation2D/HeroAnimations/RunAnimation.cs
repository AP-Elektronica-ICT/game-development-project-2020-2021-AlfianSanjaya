using Microsoft.Xna.Framework.Graphics;
using QuestMania.States;

namespace QuestMania.Animation2D.HeroAnimations
{
    public class RunAnimation : Animation
    {
        public override EntityState State { get; protected set; }
        public RunAnimation(Texture2D texture, int numberOfFrames, int animationSpeed) : base(texture, numberOfFrames, animationSpeed)
        {
            State = EntityState.Run;
        }
    }
}
