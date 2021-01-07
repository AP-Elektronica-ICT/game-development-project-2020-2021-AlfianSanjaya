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
        private Animation currentAnimation, idleAnimation, runAnimation, jump;
        #endregion

        #region Character States
        private EntityState currentState;
        #endregion

        #region Input and Commands
        private IInputReader inputReader;

        private IGameCommand moveCommand, jumpCommand;
        #endregion

        private Rectangle collisionRectangle;
        int offset = 0;
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
        #endregion

        public Hero(IInputReader newInputReader, Vector2 spawnPosition) 
        { 
            inputReader = newInputReader;
            Orientation = new Vector2(1, 0);
            Position = spawnPosition;
            SpawnPosition = spawnPosition;
            Velocity = new Vector2(0, 0);

            int yOffset = 0;
            collisionRectangle = new Rectangle((int)Position.X, (int)Position.Y, Animation.FrameWidth, Animation.FrameHeight - yOffset);
        }

        public void SetToSpawn()
        {
            Position = SpawnPosition;
        }

        public void LoadContent()
        {
            LoadAnimations();
            LoadCommands();
        }

        /// <summary>
        /// Load the spritesheet animations for this character
        /// </summary>
        public void LoadAnimations()
        {
            foreach (KeyValuePair<string, Animation> entry in Global.Hero.Animations)
            {
                switch (entry.Key)
                {
                    case "idle":
                        idleAnimation = entry.Value;
                        break;
                    case "run":
                        runAnimation = entry.Value;
                        break;
                    case "jump":
                        jump = entry.Value;
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Load the commands that are going to be used
        /// </summary>
        public void LoadCommands()
        {
            moveCommand = new MoveCommand();
            jumpCommand = new JumpCommand();

            moveCommand.SetContext(this);
            jumpCommand.SetContext(this);
        }

        #region Update
        public void Update(GameTime gameTime)
        {
            Position += Velocity;

            // Execute any actions if there is any input from user
            Execute();

            // Update hitbox position
            collisionRectangle.X = (int)Position.X;
            collisionRectangle.Y = (int)Position.Y;

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

        public void CheckCollision(Rectangle obstacle, int mapWidth)
        {
            if (collisionRectangle.IsTouchingTopOf(obstacle))
            {
                Debug.WriteLine("HIT BOTTOM");
                Position.Y = obstacle.Y - collisionRectangle.Height;
                Velocity.Y = 0f;
                HasJumped = false;                
            }
            else if (collisionRectangle.IsTouchingBottomOf(obstacle))
            {
                Debug.WriteLine("HIT TOP");
                Velocity.Y = 1f;
            }

            if (collisionRectangle.IsTouchingLeftOf(obstacle))
            {
                Debug.WriteLine("Collision from RIGHT -->");
                Position.X = obstacle.X - collisionRectangle.Width - 1;
                
            }
            else if (collisionRectangle.IsTouchingRightOf(obstacle))
            {
                Debug.WriteLine("Collision from LEFT <--");
                Position.X = obstacle.X + obstacle.Width + 1;
            }

            // Screen left - right boundaries
            if (Position.X < 0)
                Position.X = 0;
            if (Position.X > mapWidth - collisionRectangle.Width)
                Position.X = mapWidth - collisionRectangle.Width;
        }

        /// <summary>
        /// Load animation based on current state of the Hero
        /// </summary>
        public void LoadCurrentAnimation()
        {
            switch (currentState)
            {
                case EntityState.Idle:
                    currentAnimation = idleAnimation;
                    break;
                case EntityState.Run:
                    currentAnimation = runAnimation;
                    break;
                case EntityState.Jump:
                    currentAnimation = jump;
                    break;
                case EntityState.Fall:
                    currentAnimation = jump;
                    break;
                default:
                    break;
            }
            animationPlayer.LoadAnimation(currentAnimation);
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
            Debug.WriteLine(Position);
        }

        #region Draw
        public void Draw()
        {
            //Load current animation based on the current state
            //LoadCurrentAnimation();
            
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
