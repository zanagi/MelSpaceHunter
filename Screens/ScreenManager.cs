using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MelSpaceHunter.Screens
{
    class ScreenManager
    {
        // Screens
        private Stack<Screen> screens;

        // Other
        private ContentManager content;
        private ViewManager viewManager;

        public ScreenManager(ContentManager content)
        {
            this.screens = new Stack<Screen>();
            this.content = content;
        }

        private void Initialize()
        {

        }

        public void LoadContent()
        {

        }

        public void PushScreen(Screen screen)
        {
            screens.Push(screen);

            if (!CurrentScreen.LoadComplete)
                CurrentScreen.LoadContent(content);
        }

        public void PopScreen()
        {
            if (HasScreen)
            {
                Screen removedScreen = screens.Pop();
                removedScreen.UnloadContent();
            }
        }

        public void Update(GameTime gameTime)
        {
            if (HasScreen)
                CurrentScreen.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (HasScreen)
                CurrentScreen.Draw(spriteBatch);
        }

        private Screen CurrentScreen
        {
            get { return screens.Peek(); }
        }

        private bool HasScreen
        {
            get { return screens.Count > 0; }
        }
    }
}
