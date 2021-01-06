using System;
using System.Collections.Generic;
using System.Text;

namespace QuestMania.Commands
{
    public interface IGameCommand
    {
        void SetContext(Hero hero);
        void Execute();
    }
}
