using Microsoft.Xna.Framework;
using QuestMania.Commands;

namespace QuestMania.Input
{
    public interface IInputReader
    {
        // Read input from the user
        public bool ReadMoveInput();
        public bool ReadMoveLeftInput();
        public bool ReadMoveRightInput();
        public bool ReadAttackInput();
        public bool ReadJumpInput();
    }
}
