using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using QuestMania.Animation2D;
using QuestMania.Commands;
using QuestMania.Input;
using QuestMania.Interfaces;
using QuestMania.States;
using System.Diagnostics;

namespace QuestMania.Entities
{
    public class Enemy : IGameObject
    {
        // Singleton
        private static Enemy instance;
        public static Enemy GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Enemy();
                }
                return instance;
            }
        }
        private AnimationPlayer animationPlayer = new AnimationPlayer();
        protected Animation idleAnimation;

        public Vector2 Position { get; set; }
        public Vector2 Direction { get; set; }
        public Vector2 Velocity { get; set; }

        public Enemy()
        {
            idleAnimation = Global.Enemy.IdleAnimation;
            Position = new Vector2(500, 500);
        }

        public void LoadCurrentAnimation()
        {
            animationPlayer.LoadAnimation(idleAnimation);
        }

        public void Update(GameTime gameTime)
        {
            animationPlayer.UpdateAnimation(gameTime);
        }

        public void Draw()
        {
            animationPlayer.PlayAnimation(Position);
        }
    }
}
