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

        public Screen(ScreenManager manager)
        {
            this.manager = manager;
        }

        public abstract void LoadContent(ContentManager content);

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);

        protected bool RunTests()
        {
            Console.WriteLine("Abstract class Screen called.");
            return true;
        }
    }
}
