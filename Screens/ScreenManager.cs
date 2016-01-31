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

        public ScreenManager(ContentManager content, ViewManager viewManager)
        {
            this.screens = new Stack<Screen>();
            this.content = content;
            this.viewManager = viewManager;

            Initialize();
        }

        private void Initialize()
        {
            // TODO: change to menu -> game
            PushScreen(new MenuScreen(this));
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

        public void UpdateView(int width, int height)
        {
            viewManager.Update(width, height);
            CurrentScreen.UpdateCameraWH(width, height);
        }

        #region properties

        public ViewManager ViewManager
        {
            get { return viewManager; }
        }

        private Screen CurrentScreen
        {
            get { return screens.Peek(); }
        }

        public bool HasScreen
        {
            get { return screens.Count > 0; }
        }
        #endregion
    }
}
