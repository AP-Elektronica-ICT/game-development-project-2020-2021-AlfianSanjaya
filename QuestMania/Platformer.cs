using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using QuestMania.Animation2D;
using QuestMania.Commands;
using QuestMania.LevelDesign;
using QuestMania.Screens;
using QuestMania.States;
using System;
using System.Collections.Generic;
using System.IO;

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
                new GameScreen(),
                new EndScreen()
            }; 

            ScreenManager = new ScreenManager(screens);
            ScreenManager.SwitchToNextScreen(State.Select);

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

        private void InitializeHeroGlobals()
        {
            Global.Hero.Animations.Add("idle", new Animation(Global.Content.Load<Texture2D>("Player/idle-sheet"), 4, 4));
            Global.Hero.Animations.Add("run", new Animation(Global.Content.Load<Texture2D>("Player/run-sheet"), 6, 8));
            Global.Hero.Animations.Add("jump", new Animation(Global.Content.Load<Texture2D>("Player/jump"), 1, 1));

            //string heroToLoad = "Dummy4";
            //Globals.Hero.Animations.Add("idle", new Animation(Globals.Content.Load<Texture2D>(heroToLoad), 1, 1));
            //Globals.Hero.Animations.Add("run", new Animation(Globals.Content.Load<Texture2D>(heroToLoad), 1, 1));
            //Globals.Hero.Animations.Add("jump", new Animation(Globals.Content.Load<Texture2D>(heroToLoad), 1, 1));

            List<IGameCommand> commands = new List<IGameCommand>()
            {
                new MoveCommand(),
                new JumpCommand()
            };

            Global.Hero.GameCommands = commands;
        }

        private void InitializeGlobals()
        {
            Global.Content = Content;
            Global.SpriteBatch = new SpriteBatch(GraphicsDevice);
            Global.Camera = new Camera(GraphicsDevice.Viewport);
        }



        protected override void LoadContent()
        {
            //Hero.GetInstance.LoadContent();
            //Global.World.LevelOne.LoadWorld(FileManager.GetPath(@"Content\Levels\map-2.json"),
            //                                Content.Load<Texture2D>("Background/background-test"));
            
            //Enemy.GetInstance.LoadCurrentAnimation();
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
