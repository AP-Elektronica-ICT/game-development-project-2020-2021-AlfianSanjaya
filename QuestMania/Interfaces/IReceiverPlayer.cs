using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuestMania.Interfaces
{
    public interface IReceiverPlayer
    {
        public void Move();
        public void Attack();

        public void Jump(GameTime gameTime);
    }
}
