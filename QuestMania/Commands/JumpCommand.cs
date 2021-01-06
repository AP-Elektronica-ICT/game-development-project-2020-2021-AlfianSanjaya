using System;
using System.Collections.Generic;
using System.Text;

namespace QuestMania.Commands
{
    class JumpCommand : IGameCommand
    {
        private Hero character;
        public void Execute()
        {
            character.Position.Y -= character.MaxJumpingHeight;
            character.Velocity.Y = character.JumpForce;
            character.HasJumped = true;
        }

        public void SetContext(Hero hero)
        {
            character = hero;
        }
    }
}
