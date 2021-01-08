using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using QuestMania.Animation2D;
using QuestMania.Animation2D.HeroAnimations;
using QuestMania.Camera2D;
using QuestMania.Commands;
using QuestMania.LevelDesign;
using QuestMania.Screens;
using QuestMania.States;
using System.Collections.Generic;

namespace QuestMania
{
    public class Platformer : Game
    {
        public static GraphicsDeviceManager graphics;

        public static ScreenManager ScreenManager;
        
        public Platformer()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            InitializeSettings();
            InitializeGlobals();
            InitializeHeroGlobals();

            List<Screen> screens = new List<Screen>() 
            { 
                new MenuScreen(this), 
                new SelectLevelScreen(),
                new GameScreen(new World[]
                {
                    new World(Factory.CreateHero(1, 6), new Level(1), new List<IGameCommand>() { new MoveCommand(), new JumpCommand() }),
                    new World(Factory.CreateHero(0, 1), new Level(2), new List<IGameCommand>() { new MoveCommand(), new JumpCommand() })
                }),
                new GameOverScreen(),
                new VictoryScreen()
            }; 

            ScreenManager = new ScreenManager(screens);
            ScreenManager.SwitchToNextScreen(State.Menu);

            base.Initialize();
        }

        private void InitializeSettings()
        {
            Window.AllowUserResizing = false;

            Window.Title = "Quest Mania";

            // Custom screen resolution
            graphics.PreferredBackBufferWidth = Global.ScreenWidth;
            graphics.PreferredBackBufferHeight = Global.ScreenHeight;
            graphics.ApplyChanges();
        }

        private void InitializeGlobals()
        {
            Global.Content = Content;
            Global.SpriteBatch = new SpriteBatch(GraphicsDevice);
            Global.Camera = new Camera(GraphicsDevice.Viewport);
        }

        private void InitializeHeroGlobals()
        {           
            Global.Hero.Animations = new List<Animation>()
            {
                new IdleAnimation(Global.Content.Load<Texture2D>("Player/idle-sheet"), 4, 4),
                new RunAnimation(Global.Content.Load<Texture2D>("Player/run-sheet"), 6, 8),
                new JumpAnimation(Global.Content.Load<Texture2D>("Player/jump-sheet"), 1, 1),
                new FallAnimation(Global.Content.Load<Texture2D>("Player/fall-sheet"), 1, 1)
            };
        }

        protected override void LoadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (IsActive)
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();

                ScreenManager.Update(gameTime);
                base.Update(gameTime);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            ScreenManager.Draw();

            base.Draw(gameTime);
        }
    }
}
