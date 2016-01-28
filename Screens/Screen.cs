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

        public Screen(ScreenManager manager)
        {
            this.manager = manager;
        }

        public virtual void LoadContent(ContentManager content)
        {
            this.loadComplete = true;
            this.content = new ContentManager(content.ServiceProvider, content.RootDirectory);
        }

        public virtual void UnloadContent()
        {
            content.Unload();
        }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);

        public bool LoadComplete
        {
            get { return loadComplete; }
        }
    }
}
