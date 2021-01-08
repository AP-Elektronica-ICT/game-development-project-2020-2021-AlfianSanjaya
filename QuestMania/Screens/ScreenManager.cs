using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using QuestMania.States;

namespace QuestMania.Screens
{
    public class ScreenManager
    {
        private List<Screen> screens;
        private Screen activeScreen;

        public ScreenManager(List<Screen> newScreens)
        {
            screens = newScreens;
        }

        public void SwitchToNextScreen(State nextState, int levelID = -1)
        {
            foreach (var screen in screens)
            {
                if (screen.State == nextState)
                {
                    activeScreen = screen;
                    if (activeScreen is GameScreen && levelID != -1)
                        activeScreen.SelectLevel = levelID;
                }
            }
            Debug.WriteLine("Switch screen to " + activeScreen.State);
        }

        public void ClearScreen()
        {
            Global.Graphics.GraphicsDevice.Clear(Color.BurlyWood);
        }

        public void LoadContent()
        {
            activeScreen.LoadContent();
        }

        public void Update(GameTime gameTime)
        {
            activeScreen.Update(gameTime);
        }

        public void Draw()
        {
            activeScreen.Draw();
        }
    }
}
