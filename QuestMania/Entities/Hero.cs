using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using QuestMania.Animation2D;
using QuestMania.Collisions;
using QuestMania.Commands;
using QuestMania.Input;
using QuestMania.States;
using System.Collections.Generic;
using System.Diagnostics;

namespace QuestMania
{
    public class Hero
    {
        #region Fields
        private float gravity = 20.0f;

        #region Animation
        private AnimationPlayer animationPlayer = new AnimationPlayer();
        private Animation currentAnimation;
        private List<Animation> animations = new List<Animation>();
        #endregion

        #region Character States
        private EntityState currentState;
        #endregion

        #region Input and Commands
        private IInputReader inputReader;

        private IGameCommand moveCommand, jumpCommand;
        #endregion

        public Rectangle CollisionRectangle;
        #endregion

        #region Properties

        #region Vectors
        public Vector2 Position;
        public Vector2 Velocity;
        public Vector2 Orientation;
        public Vector2 SpawnPosition;
        #endregion
        public float MaxJumpingHeight { get; } = 10f;
        public float JumpForce { get; } = -10f;
        public bool HasJumped { get; set; } = true;
        public bool IsDead { get; set; } = false;
        #endregion

        public Hero(IInputReader newInputReader, Vector2 spawnPosition, List<IGameCommand> commands) 
        {
            LoadCommands(commands);

            inputReader = newInputReader;
            Orientation = new Vector2(1, 0);
            Position = spawnPosition;
            SpawnPosition = spawnPosition;
            Velocity = new Vector2(0, 0);

            int yOffset = 0;
            CollisionRectangle = new Rectangle((int)Position.X, (int)Position.Y, Animation.FrameWidth, Animation.FrameHeight - yOffset);
        }

        public void SetToSpawn()
        {
            Position = SpawnPosition;
        }

        public void LoadContent()
        {
            LoadAnimations();
        }

        /// <summary>
        /// Load the spritesheet animations for this character
        /// </summary>
        public void LoadAnimations()
        {
            foreach (Animation heroAnimation in Global.Hero.Animations)
            {
                animations.Add(heroAnimation);
            }
        }

        /// <summary>
        /// Load the commands that are going to be used
        /// </summary>
        public void LoadCommands(List<IGameCommand> commands)
        {
            foreach (IGameCommand command in commands)
            {
                command.SetContext(this);
                if (command is MoveCommand)
                    moveCommand = command;
                if (command is JumpCommand)
                    jumpCommand = command;
            }
        }

        #region Update
        public void Update(GameTime gameTime)
        {
            Position += Velocity;

            // Execute any actions if there is any input from user
            Execute();

            // Update hitbox position
            CollisionRectangle.X = (int)Position.X;
            CollisionRectangle.Y = (int)Position.Y;

            // Apply gravity
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Velocity.Y += gravity * dt;
            if ((int)Velocity.Y > 0)
                SetState(EntityState.Fall);
            if (HasJumped == true)
            {
                if ((int)Velocity.Y < 0)
                    SetState(EntityState.Jump);
                else
                    SetState(EntityState.Fall);
            }

            Logger();

            // Load current animation based on the current state
            LoadCurrentAnimation();

            // Looping of the current animation
            animationPlayer.UpdateAnimation(gameTime);
        }
        #endregion Update

        public void CheckOutOfBounds(int mapHeight)
        {
            if (Position.Y > mapHeight)
            {
                IsDead = true;
            }
        }

        public void CheckCollision(Rectangle obstacle, int mapWidth)
        {
            if (CollisionRectangle.IsTouchingTopOf(obstacle))
            {
                Debug.WriteLine("HIT BOTTOM");
                Position.Y = obstacle.Y - CollisionRectangle.Height;
                Velocity.Y = 0f;
                HasJumped = false;                
            }
            else if (CollisionRectangle.IsTouchingBottomOf(obstacle))
            {
                Debug.WriteLine("HIT TOP");
                Velocity.Y = 1f;
            }

            if (CollisionRectangle.IsTouchingLeftOf(obstacle))
            {
                Debug.WriteLine("Collision from RIGHT -->");
                Position.X = obstacle.X - CollisionRectangle.Width - 1;
                
            }
            else if (CollisionRectangle.IsTouchingRightOf(obstacle))
            {
                Debug.WriteLine("Collision from LEFT <--");
                Position.X = obstacle.X + obstacle.Width + 1;
            }

            // Screen left - right boundaries
            if (Position.X < 0)
                Position.X = 0;
            if (Position.X > mapWidth - CollisionRectangle.Width)
                Position.X = mapWidth - CollisionRectangle.Width;
        }

        /// <summary>
        /// Load animation based on current state of the Hero
        /// </summary>
        public void LoadCurrentAnimation()
        {
            foreach (Animation heroAnimation in animations)
            {
                if (currentState == heroAnimation.State)
                    currentAnimation = heroAnimation;
            }
            animationPlayer.LoadCurrentAnimation(currentAnimation);
        }

        /// <summary>
        /// Set the current state of character to a new state
        /// </summary>
        /// <param name="newState">The new state to go to</param>
        public void SetState(EntityState newState)
        {
            currentState = newState;
        }

        public void Execute()
        {
            if (inputReader.ReadMoveInput())
            {
                if (inputReader.ReadMoveLeftInput() && inputReader.ReadMoveRightInput() == false)
                    Orientation.X = -1;
                if (inputReader.ReadMoveRightInput() && inputReader.ReadMoveLeftInput() == false)
                    Orientation.X = 1;
                moveCommand.Execute();
                SetState(EntityState.Run);
            }
            if (inputReader.ReadJumpInput() && HasJumped == false)
            {
                jumpCommand.Execute();
                SetState(EntityState.Jump);
            }
            if (inputReader.ReadMoveInput() == false && inputReader.ReadJumpInput() == false)
            {
                SetState(EntityState.Idle);
                Velocity.X = 0;
            }
        }

        /// <summary>
        /// For debugging 
        /// </summary>
        private void Logger()
        {
            Debug.WriteLine("Current state: " + currentState);
            //if ((int)Position.X % 64 == 0 && (int)Position.Y % 64 == 0)
            //{
            //    Debug.WriteLine($"Position { (int)Position.X },{ (int)Position.Y }");
            //}
            //Debug.WriteLine($"Position { (int)Position.X / 64},{ (int)Position.Y / 64}");

            Debug.WriteLine(Velocity);
            //Debug.WriteLine(Position);
        }

        #region Draw
        public void Draw()
        {            
            if (animationPlayer.CurrentAnimation != null)
            {
                SpriteEffects flip = SpriteEffects.None;
                if (Orientation.X == -1)
                {
                    flip = SpriteEffects.FlipHorizontally;
                }

                // Draw character animation
                animationPlayer.PlayAnimation(Position, flip);
            }

        }
        #endregion
    }
}
