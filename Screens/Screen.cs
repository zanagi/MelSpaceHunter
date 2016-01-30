using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace MelSpaceHunter.Screens
{
    abstract class Screen
    {
        protected ScreenManager manager;
        protected ContentManager content;
        protected bool loadComplete;
        protected Camera2D camera;

        public Screen(ScreenManager manager)
        {
            this.manager = manager;
            this.loadComplete = false;
            this.camera = new Camera2D(manager.ViewManager.Width, manager.ViewManager.Height);
        }

        public virtual void LoadContent(ContentManager contentRef)
        {
            this.loadComplete = true;
            this.content = new ContentManager(contentRef.ServiceProvider, contentRef.RootDirectory);
        }

        public virtual void UnloadContent()
        {
            content.Unload();
        }

        public void UpdateCameraWH(int width, int height)
        {
            if (width <= 0 || height <= 0)
                throw new Exception("Invalid parameters");

            camera.Width = width;
            camera.Height = height;
        }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);

        public bool LoadComplete
        {
            get { return loadComplete; }
        }
    }
}
