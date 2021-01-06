using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace QuestMania.States
{
    public class ScreenManager
    {
        private List<Screen> screens;
        private Screen activeScreen;

        public ScreenManager(List<Screen> newScreens)
        {
            screens = newScreens;
        }

        public void SwitchToNextScreen(State nextState)
        {
            foreach (var screen in screens)
            {
                if (screen.State == nextState)
                {
                    activeScreen = screen;
                }
            }
            Debug.WriteLine("Switch screen to " + activeScreen.State);
        }

        public void Update(GameTime gameTime)
        {
            activeScreen?.Update(gameTime);
        }

        public void Draw()
        {
            activeScreen?.Draw();
        }
    }
}
