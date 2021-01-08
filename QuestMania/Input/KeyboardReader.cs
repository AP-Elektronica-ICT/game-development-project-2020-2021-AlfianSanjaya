using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using QuestMania.Commands;

namespace QuestMania.Input
{
    /// <summary>
    /// The invoker class
    /// </summary>
    public class KeyboardReader : IInputReader
    { 
        public bool ReadMoveInput()
        {
            return ReadMoveLeftInput() || ReadMoveRightInput();
        }

        public bool ReadMoveLeftInput()
        {
            return Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.A);
        }

        public bool ReadMoveRightInput()
        {
            return Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D);
        }

        public bool ReadAttackInput()
        {
            return Keyboard.GetState().IsKeyDown(Keys.Space);
        }

        public bool ReadJumpInput()
        {
            return Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.W);
        }
    }
}
