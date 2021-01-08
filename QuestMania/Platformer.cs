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
using QuestMania.UI;
using System.Collections.Generic;

namespace QuestMania
{
    public class Platformer : Game
    {
        public static ScreenManager ScreenManager;
        private List<Screen> screens;

        public Platformer()
        {
            Global.Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            InitializeSettings();
            InitializeGlobals();
            InitializeHeroGlobals();

            screens = new List<Screen>() 
            { 
                new MenuScreen(this, new Button("Play"), new Button("Quit"), new Label("Quest Mania")),
                new SelectLevelScreen(new Button("Level 1"), new Button("Level 2"), new Button("Back"), new Label("Select a level")),
                new GameScreen(new World[]
                {
                    new World(Factory.CreateHero(1, 6), new Level(1)),
                    new World(Factory.CreateHero(0, 1), new Level(2))
                }),
                new GameOverScreen(new Button("Restart level"), new Button("Back to menu"), new Label("Game over!")),
                new VictoryScreen(new Button("Restart level"), new Button("Back to menu"), new Label("Huzza! Victory!"))
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
            Global.Graphics.PreferredBackBufferWidth = Global.ScreenWidth;
            Global.Graphics.PreferredBackBufferHeight = Global.ScreenHeight;
            Global.Graphics.ApplyChanges();
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
            foreach (Screen screen in screens)
            {
                screen.LoadContent();
            }
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
