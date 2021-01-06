using System;
using System.Collections.Generic;
using System.Text;

namespace QuestMania.Commands
{
    class MoveCommand : IGameCommand
    {
        private Hero character;
        public void Execute()
        {
            character.Velocity.X = 5 * character.Orientation.X;
        }

        public void SetContext(Hero hero)
        {
            character = hero;
        }
    }
}
